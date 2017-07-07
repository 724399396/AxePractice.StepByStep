using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SimpleSolution.WebApp.controller
{
    public class UserController : ApiController
    {
        public HttpResponseMessage GetById(int id)
        {
            return Request.Text(HttpStatusCode.OK, $"get user by id({id})");
        }
    }
}