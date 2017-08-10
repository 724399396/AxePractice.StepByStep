using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi;
using Xunit;

namespace Test
{
    public class MessageControllerFact
    {
        [Fact]
        public async void should_return_special_message()
        {
            var configuration = new HttpConfiguration();
            BootStrapper.Init(configuration);

            using (var server = new HttpServer(configuration))
            using (var client = new HttpClient(server))
            {
                var response = await client.SendAsync(new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/message/10"));
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();

                Assert.Equal("Hello from 10", JsonConvert.DeserializeAnonymousType(content, new { message = default(string)}).message);
            }
        }
    }
}
