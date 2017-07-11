using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LocalApi
{
    class DefaultHttpControllerTypeResolver : IHttpControllerTypeResolver
    {
        #region Please modify the following code to pass the test

        /*
         * This class is used to get all types that is non-abstract public controller
         * from given assemblies.
         */
        
        public ICollection<Type> GetControllerTypes(IEnumerable<Assembly> assemblies)
        {
            return
                assemblies.SelectMany(assembly => assembly.GetTypes()
                        .Where(type => type.IsPublic && !type.IsAbstract &&
                                       type.IsSubclassOf(typeof(HttpController))))
                    .ToList();
        }

        #endregion
    }
}