using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Application
{
    /// <summary>
    /// Base class for testing command and query handlers architecture.
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssembly.SetTestAssembly(Assembly)" /> method should be called in derived class constructor to set the tested assembly.
    /// </remarks>
    public class CommandAndQueryHandlersTestsBase : TestsBaseWithAssembly
    {
        [Fact]
        public void CommandAndQueryHandlers_Should_BeSealed()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Or()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void CommandAndQueryHandlers_Should_BeNotPublic()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Or()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .NotBePublic()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void CommandHandlers_Should_HaveNameEndingWithCommandHandler()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Should()
                .HaveNameEndingWith("CommandHandler")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void QueryHandlers_Should_HaveNameEndingWithQueryHandler()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .HaveNameEndingWith("QueryHandler")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void CommandAndQueryHandlers_Should_Not_HavePublicFieldsAndProperties()
        {
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Or()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .GetTypes();

            foreach (var type in types)
            {
                var publicFields = type.GetFields(
                    BindingFlags.Public | BindingFlags.Instance);

                publicFields.Should().BeEmpty();

                var publicProperties = type.GetProperties();

                publicProperties.Should().BeEmpty();
            }
        }

        [Fact]
        public void CommandAndQueryHandlers_Should_Not_HaveStaticMembers()
        {
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Or()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .GetTypes();

            foreach (var type in types)
            {
                var staticFields = type.GetFields(
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

                staticFields.Should().BeEmpty();

                var staticProperties = type.GetProperties(
                    BindingFlags.NonPublic | BindingFlags.Public |BindingFlags.Static);

                staticProperties.Should().BeEmpty();
            }
        }
    }
}
