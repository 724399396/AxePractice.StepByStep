using System;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace LocalApi
{
    static class ControllerActionInvoker
    {
        public static HttpResponseMessage InvokeAction(ActionDescriptor actionDescriptor)
        {
            var controller = actionDescriptor.Controller;
            var actionName = actionDescriptor.ActionName;
            var action = controller.GetType().GetMethod(actionName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (action == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            try
            {
                return (HttpResponseMessage) action.Invoke(controller, new object[] { });
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
