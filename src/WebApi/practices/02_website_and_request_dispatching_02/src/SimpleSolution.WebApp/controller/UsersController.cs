using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleSolution.WebApp.controller
{
    public class UsersController : ApiController
    {
//        [HttpGet]
//        public HttpResponseMessage GetById(int id)
//        {
//            return Request.Text(HttpStatusCode.OK, $"user(id={id}) found");
//        }
//
        [HttpGet]
        public HttpResponseMessage GetByName(string name)
        {
            return Request.Text(HttpStatusCode.OK, $"user(name={name}) found");
        }
//
//        [HttpPut]
//        public HttpResponseMessage PutByIdAndName(int id, string name)
//        {
//            return Request.Text(HttpStatusCode.OK, $"user(id={id})'s name updated to {name}");
//        }
//
//        [HttpGet]
//        public HttpResponseMessage Dependents()
//        {
//            return Request.Text(HttpStatusCode.OK, "This will get all users's dependents");
//        }
//
//        [HttpGet]
//        public HttpResponseMessage IdDependents(int did)
//        {
//            return Request.Text(HttpStatusCode.OK, $"This will get user(id={did})'s dependents");
//        }
       
    }
}