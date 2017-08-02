using System;
using LocalApi;

namespace Manualfac.LocalApiIntegration
{
    public class ManualfacDependencyResolver : IDependencyResolver
    {
        #region Please implement the following class
        readonly Container roopScope;

        /*
         * We should create a manualfac dependency resolver so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */

        public ManualfacDependencyResolver(Container rootScope)
        {
            this.roopScope = rootScope;
        }

        public void Dispose()
        {
            roopScope.Dispose();
        }

        public object GetService(Type type)
        {
            return roopScope.Resolve(type);
        }

        public IDependencyScope BeginScope()
        {
            return new ManualfacDependencyScope(roopScope.BeginLifetimeScope());
        }

        #endregion
    }
}