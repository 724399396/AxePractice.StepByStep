using System.Web.Http;

namespace SimpleSolution.WebApp
{
    public class Bootstrapper
    {
        public static void Init(HttpConfiguration configuration)
        {
//             Note. Since response message generation is out of scope
//             of our test. So I have create an extension method called
//             Request.Text(HttpStatusCode, string) to help you generating
//             a textual response.
            configuration.Routes.MapHttpRoute("users-id-dependents",
                "users/{did}/dependents",
                new
                {
                    controller = "Users"
                });

            configuration.Routes.MapHttpRoute("users-dependents",
                "users/dependents",
                new
                {
                    controller = "Users",
                    Action = "Dependents"
                });

            configuration.Routes.MapHttpRoute("users-id",
                "users/{id}",
                new
                {
                    controller = "Users",
                },
                new
                {
                    id = @"\d+"
                });

            configuration.Routes.MapHttpRoute(
                "users", "users",
                new
                {
                    controller = "Users"
                });

           
          
        }
    }
}