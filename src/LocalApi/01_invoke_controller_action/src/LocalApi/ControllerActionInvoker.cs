using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using static System.String;

namespace LocalApi
{
    static class ControllerActionInvoker
    {
        public static HttpResponseMessage InvokeAction(ActionDescriptor actionDescriptor)
        {
            var controller = actionDescriptor.Controller;
            var actionName = actionDescriptor.ActionName;
            Type type = controller.GetType();
            var instance = Activator.CreateInstance(type);
            var action = type.GetMethods().SingleOrDefault(m => String.Equals(m.Name, actionName, StringComparison.CurrentCultureIgnoreCase));

            if (instance == null || action == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            try
            {
                return (HttpResponseMessage) action.Invoke(instance, new object[] { });
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
