using System;
using System.Linq;
using System.Net.Http;

namespace LocalApi.Routing
{
    public class HttpRoute
    {
        public HttpRoute(string controllerName, string actionName, HttpMethod methodConstraint) : 
            this(controllerName, actionName, methodConstraint, null)
        {
        }

        #region Please modifies the following code to pass the test

        /*
         * You can add non-public helper method for help, but you cannot change public
         * interfaces.
         */

        private void IdentifierCheck(string identifier)
        {
            if (identifier == null)
            {
                return;
            }
            var specialCharacter = @"/@.#";

            if (identifier.Length == 0 ||
                specialCharacter.Any(identifier.Contains) ||
                Char.IsDigit(identifier.First()))
            {
                throw new ArgumentException();
            }
        }

        public HttpRoute(string controllerName, string actionName, HttpMethod methodConstraint, string uriTemplate)
        {
            IdentifierCheck(controllerName);
            IdentifierCheck(actionName);

            ControllerName = controllerName ?? throw new ArgumentNullException(nameof(controllerName));
            ActionName = actionName ?? throw new ArgumentNullException(nameof(actionName));
            MethodConstraint = methodConstraint ?? throw new ArgumentNullException(nameof(methodConstraint));
            UriTemplate = uriTemplate;
        }

        #endregion

        public string ControllerName { get; }
        public string ActionName { get; }
        public HttpMethod MethodConstraint { get; }
        public string UriTemplate { get; }

        public bool IsMatch(Uri uri, HttpMethod method)
        {
            if (uri == null) { throw new ArgumentNullException(nameof(uri)); }
            if (method == null) { throw new ArgumentNullException(nameof(method)); }
            string path = uri.AbsolutePath.TrimStart('/');
            return path.Equals(UriTemplate, StringComparison.OrdinalIgnoreCase) &&
                   method == MethodConstraint;
        }
    }
}