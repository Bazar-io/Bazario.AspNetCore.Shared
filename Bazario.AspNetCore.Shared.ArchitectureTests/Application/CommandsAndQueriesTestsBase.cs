using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Application
{
    /// <summary>
    /// Base class for testing commands and queries architecture.
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssembly.SetTestAssembly(Assembly)" /> method should be called in derived class constructor to set the tested assembly.
    /// </remarks>
    public class CommandsAndQueriesTestsBase : TestsBaseWithAssembly
    {
        [Fact]
        public void CommandsAndQueries_Should_BeSealed()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .Or()
                .ImplementInterface(typeof(IQuery<>))
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void CommandsAndQueries_Should_BePublic()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .Or()
                .ImplementInterface(typeof(IQuery<>))
                .Should()
                .BePublic()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Commands_Should_HaveNameEndingWithCommand()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .Should()
                .HaveNameEndingWith("Command")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Queries_Should_HaveNameEndingWithQuery()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IQuery<>))
                .Should()
                .HaveNameEndingWith("Query")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void CommandsAndQueries_Should_BeRecords()
        {
            // Arrange
            // Act
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .Or()
                .ImplementInterface(typeof(IQuery<>))
                .GetTypes();

            // Assert
            foreach (var type in types)
            {
                type.GetMethods().Any(m => m.Name == "<Clone>$").Should().BeTrue();
            }
        }
    }
}
