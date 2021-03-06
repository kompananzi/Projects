#region License
//  Copyright 2004-2010 Castle Project - http:www.castleproject.org/
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http:www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// 
#endregion
namespace Castle.ActiveWriter.Rules
{
	using Microsoft.VisualStudio.Modeling;

	[RuleOn(typeof(ModelProperty), FireTime = TimeToFire.TopLevelCommit)]
    public sealed class ModelPropertyChangedRule : ChangeRule
    {
        public override void  ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            base.ElementPropertyChanged(e);
            
            ModelProperty property = e.ModelElement as ModelProperty;
            if (property != null)
            {
                Transaction transaction = property.Store.TransactionManager.CurrentTransaction.TopLevelTransaction;
                
                if (transaction != null && !transaction.IsSerializing)
                {
                    // TODO: Add nested class support
                    if (property.ModelClass != null)
                        property.ModelClass.Model.OnModelPropertyChanged(e);
                }
            }
        }
    }
}
