using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.CleanArchitecture
{
    /// <summary>
    /// Base class for testing Clean Architecture layers dependencies
    /// </summary>
    public class LayersTestsBase
    {
        private TestBaseAsseblies _testAssemblies;

        /// <summary>
        /// Getter for test assemblies
        /// </summary>
        protected TestBaseAsseblies TestAssemblies => 
            _testAssemblies ?? throw new InvalidOperationException("Test assemblies aren't initialized.");

        /// <summary>
        /// Sets test assemblies. Should be called inside derived class constructor.
        /// </summary>
        /// <param name="testAssemblies">Assemblies class of type <see cref="TestBaseAsseblies"/></param>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="testAssemblies"/> are already set.</exception>
        protected void SetTestAssemblies(
            TestBaseAsseblies testAssemblies)
        {
            if (_testAssemblies is not null)
            {
                throw new InvalidOperationException(
                    "Test assemblies are already set.");
            }

            _testAssemblies = testAssemblies;
        }

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnApplicationLayer()
        {
            // Arrange
            var domainAssembly = TestAssemblies.DomainAssembly;
            var applicationAssembly = TestAssemblies.ApplicationAssembly;

            // Act
            var result = Types.InAssembly(domainAssembly)
                .Should()
                .NotHaveDependencyOn(applicationAssembly.FullName)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnInfrastructureLayer()
        {
            // Arrange
            var domainAssembly = TestAssemblies.DomainAssembly;
            var infrastructureAssembly = TestAssemblies.InfrastructureAssembly;

            // Act
            var result = Types.InAssembly(domainAssembly)
                .Should()
                .NotHaveDependencyOn(infrastructureAssembly.FullName)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnPresentationLayer()
        {
            // Arrange
            var domainAssembly = TestAssemblies.DomainAssembly;
            var presentationAssembly = TestAssemblies.PresentationAssembly;

            // Act
            var result = Types.InAssembly(domainAssembly)
                .Should()
                .NotHaveDependencyOn(presentationAssembly.FullName)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyOnInfrastructureLayer()
        {
            // Arrange
            var applicationAssembly = TestAssemblies.ApplicationAssembly;
            var infrastructureAssembly = TestAssemblies.InfrastructureAssembly;

            // Act
            var result = Types.InAssembly(applicationAssembly)
                .Should()
                .NotHaveDependencyOn(infrastructureAssembly.FullName)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyOnPresentationLayer()
        {
            // Arrange
            var applicationAssembly = TestAssemblies.ApplicationAssembly;
            var presentationAssembly = TestAssemblies.PresentationAssembly;

            // Act
            var result = Types.InAssembly(applicationAssembly)
                .Should()
                .NotHaveDependencyOn(presentationAssembly.FullName)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
