using System.Reflection;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.Base
{
    /// <summary>
    /// Base class for tests that require an assembly to be set.
    /// </summary>
    public class TestsBaseWithAssembly
    {
        private Assembly _testAssembly;

        /// <summary>
        /// Getter for test assembly.
        /// </summary>
        protected Assembly TestAssembly =>
            _testAssembly ?? throw new InvalidOperationException("Test assembly isn't initialized.");

        /// <summary>
        /// Sets the assembly for tests. Should be called inside derived class constructor.
        /// </summary>
        /// <param name="testAssembly">Test assembly that contains the types to be tested.</param>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="testAssembly"/> is already set.</exception>
        protected void SetTestAssembly(Assembly testAssembly)
        {
            if (_testAssembly is not null)
            {
                throw new InvalidOperationException("Test assembly is already set.");
            }

            _testAssembly = testAssembly;
        }
    }
}
