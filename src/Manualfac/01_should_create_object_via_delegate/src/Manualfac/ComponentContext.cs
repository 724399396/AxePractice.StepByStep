using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ComponentContext : IComponentContext
    {
        #region Please modify the following code to pass the test

        /*
         * A ComponentContext is used to resolve a component. Since the component
         * is created by the ContainerBuilder, it brings all the registration
         * information. 
         * 
         * You can add non-public member functions or member variables as you like.
         */
        private readonly Dictionary<Type, object> dict;

        public ComponentContext(Dictionary<Type, object> dict)
        {
            this.dict = dict;
        }


        public object ResolveComponent(Type type)
        {
            if (!dict.ContainsKey(type))
            {
                throw new DependencyResolutionException();
            }
            return ((Func<IComponentContext, object>) dict[type]).Invoke(null);
        }

        #endregion
    }
}