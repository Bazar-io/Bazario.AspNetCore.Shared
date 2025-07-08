using Bazario.AspNetCore.Shared.Abstractions.Mappers;
using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Application
{
    /// <summary>
    /// Base class for testing mappers architecture.
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssembly.SetTestAssembly(Assembly)" /> method should be called in derived class constructor to set the tested assembly.
    /// </remarks>
    public class MappersTestsBase : TestsBaseWithAssembly
    {
        [Fact]
        public void Mappers_Should_BeNotBePublic()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(Mapper<,>))
                .Should()
                .NotBePublic()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Mappers_Should_BeSealed()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(Mapper<,>))
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Mappers_Should_HaveNameEndingWithMapper()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(Mapper<,>))
                .Should()
                .HaveNameEndingWith("Mapper")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Mappers_Should_Not_HavePublicFielsAndProperties()
        {
            // Arrange
            // Act
            var types = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(Mapper<,>))
                .GetTypes();

            // Assert
            foreach (var type in types)
            {
                var publicFields = type.GetFields(
                    BindingFlags.Public | BindingFlags.Instance);

                publicFields.Should().BeEmpty();

                var publicProperties = type.GetProperties(
                    BindingFlags.Public | BindingFlags.Instance);

                publicProperties.Should().BeEmpty();
            }
        }

        [Fact]
        public void Mappers_Should_Not_HaveStaticMembers()
        {
            // Arrange
            // Act
            var types = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(Mapper<,>))
                .GetTypes();

            // Assert
            foreach (var type in types)
            {
                var staticFields = type.GetFields(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                staticFields.Should().BeEmpty();

                var staticProperties = type.GetProperties(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                staticProperties.Should().BeEmpty();
            }
        }
    }
}
