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

namespace Castle.MonoRail.Views.AspView.Tests.ViewTests
{
	using Views;
	using Xunit;

	
	public class LayoutsTestFixture : AbstractViewTestFixture
	{
		[Fact]
		public void Render_WhenRendersSimpleStringAndNoLayout_Works()
		{
			InitializeView(typeof(SimpleView));
			SetLayout(typeof(SimplestLayout));
			view.Process();

			expected = @"Layout - before
Simple
Layout - after";

			AssertViewOutputEqualsToExpected();
		}

		[Fact]
		public void Render_WhenHasNestedLayouts_RenderLayoutsAndInCorrectOrder()
		{
			InitializeView(typeof(SimpleView));
			SetLayout(typeof(SimplestLayout));
			SetLayout(typeof(OuterLayout));
			view.Process();

			expected = @"Outer Layout - before
Layout - before
Simple
Layout - after
Outer Layout - after";

			AssertViewOutputEqualsToExpected();
		}

	}
}