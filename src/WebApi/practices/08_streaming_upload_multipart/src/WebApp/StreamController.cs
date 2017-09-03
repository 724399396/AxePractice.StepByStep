using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebApp
{
    public class StreamController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> CreateMultipart()
        {
            #region Please implement the method

            /*
             * Please implement the method to retrive all the files data.
             */

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/uploaded");
            
            var provider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(provider);

            var result = provider.FileData.Select(multipartFileData =>
            {
                var fileName = multipartFileData.Headers.ContentDisposition.FileName;
                var content = File.ReadAllText(multipartFileData.LocalFileName);
                return $"{fileName}:{content}";
            });

            return Request.CreateResponse(HttpStatusCode.OK, result);

            #endregion
        }
    }
}