using System.Reflection;

namespace Bazario.AspNetCore.Shared.ArchitectureTests.CleanArchitecture
{
    /// <summary>
    /// Class for configuring assemblies for testing. 
    /// Extendable for adding new assemblies to define new tests.
    /// </summary>
    public class TestBaseAsseblies
    {
        public Assembly DomainAssembly { get; private set; }

        public Assembly ApplicationAssembly { get; private set; }

        public Assembly InfrastructureAssembly { get; private set; }

        public Assembly PresentationAssembly { get; private set; }

        public TestBaseAsseblies(
            Assembly domainAssembly,
            Assembly applicationAssembly,
            Assembly infrastructureAssembly,
            Assembly presentationAssembly)
        {
            DomainAssembly = domainAssembly;
            ApplicationAssembly = applicationAssembly;
            InfrastructureAssembly = infrastructureAssembly;
            PresentationAssembly = presentationAssembly;
        } 
    }
}
