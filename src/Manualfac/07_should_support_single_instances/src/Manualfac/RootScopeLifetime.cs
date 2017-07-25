using System;

namespace Manualfac
{
    class RootScopeLifetime : IComponentLifetime
    {
        public ILifetimeScope FindLifetimeScope(ILifetimeScope mostNestedLifetimeScope)
        {
            #region Please implement this method

            /*
             * This class will always create and share instaces in root scope.
             */
            var parentScope = mostNestedLifetimeScope;
            while (parentScope.RootScope != null)
            {
                parentScope = parentScope.RootScope;
            }

            return parentScope;

            #endregion
        }
    }
}