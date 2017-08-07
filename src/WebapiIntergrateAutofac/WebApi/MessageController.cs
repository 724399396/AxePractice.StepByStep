using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi
{
    public class MessageController : ApiController
    {
        private readonly MessageSaver messageSaver;

        public MessageController(MessageSaver messageSaver)
        {
            this.messageSaver = messageSaver;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {message = messageSaver.Hello(id)});
        }
    }

    public class MessageSaver
    {
        public string Hello(int id)
        {
            return $"Hello from {id}";
        }
    }
}