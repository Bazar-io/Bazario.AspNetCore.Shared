using System.Reflection;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Base
{
    /// <summary>
    /// Base class for tests that require multiple assemblies to be set.
    /// </summary>
    public class TestsBaseWithAssemblies
    {
        private IEnumerable<Assembly> _testAssemblies;

        /// <summary>
        /// Getter for test assemblies.
        /// </summary>
        protected IEnumerable<Assembly> TestAssemblies =>
            _testAssemblies ?? throw new InvalidOperationException("Test assemblies aren't initialized.");

        /// <summary>
        /// Sets the assemblies for tests. Should be called inside derived class constructor.
        /// </summary>
        /// <param name="testAssemblies">Assemblies which contain the types to be tested.</param>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="testAssemblies"/> is already set.</exception>
        /// <exception cref="ArgumentException">Thrown if no assemblies are provided in <paramref name="testAssemblies"/>.</exception>
        protected void SetTestAssemblies(params Assembly[] testAssemblies)
        {
            if (_testAssemblies is not null)
            {
                throw new InvalidOperationException("Test assemblies are already set.");
            }

            if (testAssemblies.Length == 0)
            {
                throw new ArgumentException("At least one assembly must be provided.", nameof(testAssemblies));
            }

            _testAssemblies = testAssemblies;
        }
    }
}
