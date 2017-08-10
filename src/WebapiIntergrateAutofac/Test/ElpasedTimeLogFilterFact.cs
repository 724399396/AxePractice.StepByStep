﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Moq;
using WebApi;
using Xunit;

namespace Test
{
    public class ElpasedTimeLogFilterFact : ApiTestBase
    {
        [Fact]
        public async void should_log_elpased_time()
        {
            var response =
                await client.SendAsync(new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/message/10"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            mock.Verify(logger => logger.Log(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void should_not_log_elpased_time()
        {
            var response =
                await client.SendAsync(
                    new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/another-message"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            mock.Verify(logger => logger.Log(It.IsAny<string>()), Times.Once);
        }
    }
}