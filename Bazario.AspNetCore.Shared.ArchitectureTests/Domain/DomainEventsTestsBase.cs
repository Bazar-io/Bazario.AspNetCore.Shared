using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;
using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Domain
{
    /// <summary>
    /// Base class for testing domain events architecture
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssembly.SetTestAssembly(Assembly)" /> method should be called in derived class constructor to set the tested assembly.
    /// </remarks>
    public class DomainEventsTestsBase : TestsBaseWithAssembly
    {
        [Fact]
        public void DomainEvents_Should_BeSealed()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .And()
                .AreNotAbstract()
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainEvents_Should_BePublic()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .And()
                .AreNotAbstract()
                .Should()
                .BePublic()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainEvents_Should_HaveNameEndingWithEvent()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .And()
                .AreNotAbstract()
                .Should()
                .HaveNameEndingWith("DomainEvent")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void DomainEvents_Should_BeRecords()
        {
            // Arrange
            // Act
            var types = Types.InAssembly(TestAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .And()
                .AreNotAbstract()
                .GetTypes();

            // Assert
            foreach (var type in types)
            {
                type.GetMethods().Any(m => m.Name == "<Clone>$").Should().BeTrue();
            }
        }
    }
}
