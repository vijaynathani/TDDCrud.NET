using Data;
using Ninject;

namespace Domain
{
    public class Kernel
    {
        private static readonly IKernel Registry =
            new StandardKernel(
                new DataDependenciesModule(),
                new DomainDependenciesModule());

        public static IKernel GetRegistry()
        {
            return Registry;
        }
    }
}