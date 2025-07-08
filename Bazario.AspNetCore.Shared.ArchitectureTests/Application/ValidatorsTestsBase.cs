using Bazario.AspNetCore.Shared.ArchitectureTests.Base;
using FluentAssertions;
using FluentValidation;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Application
{
    /// <summary>
    /// Base class for validators tests.
    /// </summary>
    /// <remarks>
    /// <see cref="TestsBaseWithAssembly.SetTestAssembly(Assembly)" /> method should be called in derived class constructor to set the tested assembly.
    /// </remarks>
    public class ValidatorsTestsBase : TestsBaseWithAssembly
    {
        [Fact]
        public void Validators_Should_BeSealed()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .BeSealed()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Validators_Should_BeNotPublic()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .NotBePublic()
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Validators_Should_HaveNameEndingWithValidator()
        {
            // Arrange
            // Act
            var result = Types.InAssembly(TestAssembly)
                .That()
                .Inherit(typeof(AbstractValidator<>))
                .Should()
                .HaveNameEndingWith("Validator")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
