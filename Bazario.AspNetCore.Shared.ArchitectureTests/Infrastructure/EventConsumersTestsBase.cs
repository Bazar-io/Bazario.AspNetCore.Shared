using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Infrastructure
{
    /// <summary>
    /// Base class for event consumers tests.
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssembly.SetTestAssembly(Assembly)" /> method should be called in derived class constructor to set the tested assembly.
    /// </remarks>
    public class EventConsumersTestsBase : TestsBaseWithAssembly
    {
        [Fact]
        public void EventConsumers_Should_BeSealed()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IMessageConsumer<>))
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void EventConsumers_Should_BeNotPublic()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IMessageConsumer<>))
                .Should()
                .NotBePublic()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void EventConsumers_Should_HaveNameEndingWithEventConsumer()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IMessageConsumer<>))
                .Should()
                .HaveNameEndingWith("EventConsumer")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void EventConsumers_Should_Not_HavePublicFieldsAndProperties()
        {
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IMessageConsumer<>))
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
        public void EventConsumers_Should_Not_HaveStaticMembers()
        {
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IMessageConsumer<>))
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
