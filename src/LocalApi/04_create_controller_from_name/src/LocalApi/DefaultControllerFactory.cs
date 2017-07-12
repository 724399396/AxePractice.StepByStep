using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace LocalApi
{
    class DefaultControllerFactory : IControllerFactory
    {
        public HttpController CreateController(
            string controllerName,
            ICollection<Type> controllerTypes,
            IDependencyResolver resolver)
        {
            #region Please modify the following code to pass the test.

            /*
             * The controller factory will create controller by its name. It will search
             * form the controllerTypes collection to get the correct controller type,
             * then create instance from resolver.
             */
            var matchType = controllerTypes.Where(t => t.Name.Equals(controllerName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!matchType.Any())
            {
                return null;
            }
            if (matchType.Count() > 1)
            {
                throw new ArgumentException();
            }
            return (HttpController) resolver.GetService(matchType.First());

            #endregion
        }
    }
}