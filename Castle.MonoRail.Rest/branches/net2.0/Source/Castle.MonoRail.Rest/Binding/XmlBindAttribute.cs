﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using Castle.MonoRail.Framework;
using System.Xml.Serialization;

namespace Castle.MonoRail.Rest.Binding
{
    public delegate TR Func<T0, TR>( T0 arg0 );

    public class XmlBindAttribute : Attribute, IParameterBinder
    {
        private readonly Dictionary<Type,Func<Stream,Object>> _factories;

        public XmlBindAttribute() {

            _factories = new Dictionary<Type, Func<Stream, object>>();

            _factories[ typeof( XmlReader ) ] =
                delegate( Stream inputStream ) { return XmlReader.Create( inputStream ); };
            _factories[ typeof( String ) ] = delegate( Stream inputStream )
                                                 {
                                                     return new StreamReader( inputStream ).ReadToEnd();
                                                 };
            _factories[typeof(XPathNavigator)] = delegate( Stream inputStream )
                                                     {
                                                         XPathDocument doc = new XPathDocument(inputStream);
                                                         return doc.CreateNavigator();
                                                     };
            _factories[ typeof( XDocument ) ] =
                delegate( Stream inputStream ) { return XDocument.Load( XmlReader.Create( inputStream ) ); }; 
        }

        public Object CreateValueFromInputStream(Type valueType, Stream inputStream)
        {
            if (_factories.ContainsKey(valueType))
            {
                return _factories[valueType](inputStream);
            }
            else
            {
                XmlSerializer serial = new XmlSerializer(valueType);
                return serial.Deserialize(inputStream);
            }
            
        }

        #region IParameterBinder Members

        public object Bind(SmartDispatcherController controller, System.Reflection.ParameterInfo parameterInfo)
        {
            Stream inputStream = controller.Context.UnderlyingContext.Request.InputStream;
            return CreateValueFromInputStream(parameterInfo.ParameterType, inputStream);
        }

        public int CalculateParamPoints(SmartDispatcherController controller, System.Reflection.ParameterInfo parameterInfo)
        {
            return 10; 
        }

        #endregion
    }
}
