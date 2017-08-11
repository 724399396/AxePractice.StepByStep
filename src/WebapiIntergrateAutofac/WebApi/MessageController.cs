using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi
{
    public class MessageController : ApiController
    {
        readonly MessageProducer _messageProducer;

        public MessageController(MessageProducer messageProducer)
        {
            this._messageProducer = messageProducer;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {message = _messageProducer.Hello(id)});
        }

        [HttpGet]
        public HttpResponseMessage GetWithoutLog()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}