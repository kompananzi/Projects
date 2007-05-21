// Copyright 2007 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading;
using MbUnit.Framework;
using WatiN.Core;

namespace Castle.FlexBridge.Tests.IntegrationTests
{
    /// <summary>
    /// Base integration test.
    /// All integration tests inherit from this class.
    /// </summary>
    [TestFixture(ApartmentState = ApartmentState.STA)]
    public abstract class BaseIntegrationTest : IFlashExternalInterface
    {
        private IE browser;

        /// <summary>
        /// Gets the WatiN Internet Explorer browser handle.
        /// </summary>
        public IE Browser
        {
            get { return browser; }
        }

        [SetUp]
        public virtual void SetUp()
        {
            browser = new IE(ConfigureWebResources.FlexHarnessUrl);
            browser.WaitForComplete();
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (browser != null)
            {
                browser.Dispose();
                browser = null;
            }
        }

        protected Element GetFlexHarnessElement()
        {
            return browser.Element(Find.ById("FlexHarness"));
        }

        public object InvokeMethod(string methodName, params object[] args)
        {
            object flashElement = GetFlexHarnessElement().HTMLElement;
            object result = flashElement.GetType().InvokeMember(methodName,
                BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                null, flashElement, args);

            return result;
        }
    }
}