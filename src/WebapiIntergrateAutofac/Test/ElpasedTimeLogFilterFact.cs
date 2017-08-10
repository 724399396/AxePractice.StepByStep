using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi;
using Xunit;

namespace Test
{
    public class ElpasedTimeLogFilterFact
    {
        [Fact]
        public async void should_log_elpased_time()
        {
            var configuration = new HttpConfiguration();
            BootStrapper.Init(configuration);

            using (var server = new HttpServer(configuration))
            using (var client = new HttpClient(server))
            {
                var response = await client.SendAsync(new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/message/10"));
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                Assert.True(Logger.Logs.Any(x => x.Contains("Get action used")));
            }
        }

        [Fact]
        public async void should_not_log_elpased_time()
        {
            var configuration = new HttpConfiguration();
            BootStrapper.Init(configuration);

            using (var server = new HttpServer(configuration))
            using (var client = new HttpClient(server))
            {
                var response = await client.SendAsync(new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/another-message"));
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                Assert.False(Logger.Logs.Any(x => x.Contains("GetWithoutLog action used")));
            }
        }
    }
}