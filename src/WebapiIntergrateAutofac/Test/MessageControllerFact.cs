﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtonsoft.Json;
using WebApi;
using Xunit;

namespace Test
{
    public class MessageControllerFact : ApiTestBase
    {
        [Fact]
        public async void should_return_special_message()
        {
            var response =
                await client.SendAsync(new HttpRequestMessage(new HttpMethod("GET"), "http://www.url.com/message/10"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal("Hello from 10",
                JsonConvert.DeserializeAnonymousType(content, new {message = default(string)}).message);
        }
    }
}
