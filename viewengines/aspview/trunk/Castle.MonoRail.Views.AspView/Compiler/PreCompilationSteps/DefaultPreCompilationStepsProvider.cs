#region license
// Copyright 2006-2007 Ken Egozi http://www.kenegozi.com/
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
#endregion

using System.Collections.Generic;

namespace Castle.MonoRail.Views.AspView.Compiler.PreCompilationSteps
{
	public class DefaultPreCompilationStepsProvider : IPreCompilationStepsProvider
	{
		public ICollection<IPreCompilationStep> GetSteps()
		{
			return new IPreCompilationStep[] {
				new DetermineBaseClassStep(),
				new ImportStatementsStep(),
				new ProcessPropertiesStep(),
				new SubViewTagsStep(),
				new ViewFilterTagsStep(),
				new ViewComponentTagsStep(),
				new MarkupTransformationsStep(),
				new ProcessViewBodyStep(),
				new RenderConcreteClassStep()
			};
		}
	}
}
