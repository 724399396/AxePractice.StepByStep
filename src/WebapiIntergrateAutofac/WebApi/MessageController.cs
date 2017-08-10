using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi
{
    [ElpasedTimeLogFilter]
    public class MessageController : ApiController
    {
        private readonly MessageProducer messageProducer;

        public MessageController(MessageProducer messageProducer)
        {
            this.messageProducer = messageProducer;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {message = messageProducer.Hello(id)});
        }

        [HttpGet]
        public HttpResponseMessage GetWithoutLog()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}