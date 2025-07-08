using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;
using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Application
{
    /// <summary>
    /// Base class for testing domain event handlers architecture.
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssembly.SetTestAssembly(Assembly)" /> method should be called in derived class constructor to set the tested assembly.
    /// </remarks>
    public class DomainEventHandlersTestsBase : TestsBaseWithAssembly
    {
        [Fact]
        public void DomainEventHandlers_Should_BeSealed()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEventHandler<>))
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainEventHandlers_Should_BeNotPublic()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEventHandler<>))
                .Should()
                .NotBePublic()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainEventHandlers_Should_HaveNameEndingWithDomainEventHandler()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEventHandler<>))
                .Should()
                .HaveNameEndingWith("DomainEventHandler")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainEventHandlers_Should_Not_HavePublicFieldsAndProperties()
        {
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEventHandler<>))
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
        public void DomainEventHandlers_Should_Not_HaveStaticMembers()
        {
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEventHandler<>))
                .GetTypes();

            foreach (var type in types)
            {
                var staticFields = type.GetFields(
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

                staticFields.Should().BeEmpty();

                var staticProperties = type.GetProperties(
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

                staticProperties.Should().BeEmpty();
            }
        }
    }
}
