using System.Net;
using System.Net.Http;
using Moq;
using WebApi;
using Xunit;

namespace Test
{
    public class ElpasedTimeLogFilterFact : ApiTestBase
    {
        readonly Mock<ILogger> logger;

        public ElpasedTimeLogFilterFact()
        {
            logger = new Mock<ILogger>();
            logger.Setup(log => log.Log(It.IsAny<string>()));
            MockDepedency(logger.Object);
        }

        [Fact]
        public async void should_log_elpased_time()
        {
            var response =
                await Client.SendAsync(new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/message/10"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            logger.Verify(logger => logger.Log(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void should_not_log_elpased_time()
        {
            var response =
                await Client.SendAsync(
                    new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/another-message"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            logger.Verify(logger => logger.Log(It.IsAny<string>()), Times.Once);
        }
    }
}