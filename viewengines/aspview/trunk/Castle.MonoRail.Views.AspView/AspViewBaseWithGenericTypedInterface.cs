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

namespace Castle.MonoRail.Views.AspView
{
    using System.IO;
    using Framework;
	using Components.DictionaryAdapter;

    public abstract class AspViewBase<IView> : AspViewBase
    {
		protected IView view;
        public AspViewBase(AspViewEngine viewEngine, TextWriter output, IRailsEngineContext context, Controller controller)
			:base(viewEngine, output, context, controller)
        {
			view = new DictionaryAdapterFactory().GetAdapter<IView>(Properties);
        }
	}
}