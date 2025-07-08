using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests
{
    /// <summary>
    /// Base class for tests that check exception naming conventions.
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssemblies.SetTestAssemblies(Assembly[])" /> method should be called in derived class constructor to set the tested assemblies.
    /// </remarks>
    public class ExceptionsTestsBase : TestsBaseWithAssemblies
    {
        [Fact]
        public void Exceptions_ShouldHaveProperNaming()
        {
            var result = Types.InAssemblies(TestAssemblies)
                .That()
                .Inherit(typeof(Exception))
                .Should()
                .HaveNameEndingWith("Exception")
                .GetResult();

            if (!result.IsSuccessful)
            {
                var invalidTypes = result.FailingTypes
                    .Select(t => t.FullName)
                    .ToList();

                invalidTypes.Should().BeEmpty(
                    "The following exception types should end with 'Exception': {0}",
                    string.Join(", ", invalidTypes));
            }
        }
    }
}
