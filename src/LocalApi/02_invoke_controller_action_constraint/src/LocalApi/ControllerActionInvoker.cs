using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using LocalApi.MethodAttributes;

namespace LocalApi
{
    static class ControllerActionInvoker
    {
        public static HttpResponseMessage InvokeAction(ActionDescriptor actionDescriptor)
        {
            MethodInfo method = GetAction(actionDescriptor);
            if (method == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            HttpResponseMessage errorResponse = ProcessConstraint(method, actionDescriptor.Method);
            if (errorResponse != null)
            {
                return errorResponse;
            }

            return Execute(actionDescriptor, method);
        }

        static HttpResponseMessage ProcessConstraint(MethodInfo method, HttpMethod methodConstraint)
        {
            var attributes = method.GetCustomAttributes();
            if (attributes.Any())
            {
                if (attributes.Cast<IMethodProvider>().All(mp => mp.Method.Method != methodConstraint.Method))
                {
                    return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                }
            }
            else
            {
                if (!method.Name.StartsWith(methodConstraint.Method))
                {
                    return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                }
            }
            return null;
        }

        static HttpResponseMessage Execute(ActionDescriptor actionDescriptor, MethodInfo method)
        {
            try
            {
                return (HttpResponseMessage) method.Invoke(actionDescriptor.Controller, null);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        static MethodInfo GetAction(ActionDescriptor actionDescriptor)
        {
            HttpController controller = actionDescriptor.Controller;
            string actionName = actionDescriptor.ActionName;

            Type controllerType = controller.GetType();
            const BindingFlags controllerActionBindingFlags =
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase;
            MethodInfo method = controllerType.GetMethod(actionName, controllerActionBindingFlags);
            return method;
        }
    }
}