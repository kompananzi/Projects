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

namespace Castle.MonoRail.Views.AspView.Tests.Compiler.PreCompilationSteps
{
	using AspView.Compiler;
	using AspView.Compiler.PreCompilationSteps;
	using NUnit.Framework;

	[TestFixture]
	public class ViewFilterTagsStepTestFixture
	{
		readonly IPreCompilationStep step = new ViewFilterTagsStep();

		SourceFile file;

		[SetUp]
		public void Setup()
		{
			file = new SourceFile();
		}

		[Test]
		public void Process_WhenThereAreNoViewFilterTags_DoesNothing()
		{
			string source = @"
dkllgk
fgkdlfk
dfg
fdslk";

			file.ViewSource = source;
			step.Process(file);

			Assert.AreEqual(source, file.ViewSource);
		}

		[Test]
		public void Process_WhenThereIsAViewFilterTag_Transforms()
		{
			string source = @"
dkllgk
<filter:Simple>some stuff</filter:Simple>
fgkdlfk
dfg
fdslk";
			string expected = @"
dkllgk
<% StartFiltering(""SimpleViewFilter""); %>some stuff<% EndFiltering(); %>
fgkdlfk
dfg
fdslk";

			file.ViewSource = source;
			step.Process(file);

			Assert.AreEqual(expected, file.ViewSource);
		}

		[Test]
		public void Process_WhenThereIsAnEarlyBoundViewFilterTag_Transforms()
		{
			string source = @"
dkllgk
<filter:LowerCase>some stuff</filter:LowerCase>
fgkdlfk
dfg
fdslk";
			string expected = @"
dkllgk
<% StartFiltering(new Castle.MonoRail.Views.AspView.ViewFilters.LowerCaseViewFilter()); %>some stuff<% EndFiltering(); %>
fgkdlfk
dfg
fdslk";

			file.ViewSource = source;
			step.Process(file);

			Assert.AreEqual(expected, file.ViewSource);
		}

		[Test]
		public void Process_WhenNested_Transforms()
		{
			string source = @"
dkllgk
<filter:LowerCase>
some stuff
<filter:Simple>
yoodle doodle
</filter:Simple>
blah blah
</filter:LowerCase>
fgkdlfk
dfg
fdslk";
			string expected = @"
dkllgk
<% StartFiltering(new Castle.MonoRail.Views.AspView.ViewFilters.LowerCaseViewFilter()); %>
some stuff
<% StartFiltering(""SimpleViewFilter""); %>
yoodle doodle
<% EndFiltering(); %>
blah blah
<% EndFiltering(); %>
fgkdlfk
dfg
fdslk";

			file.ViewSource = source;
			step.Process(file);

			Assert.AreEqual(expected, file.ViewSource);
		}
	}
}
