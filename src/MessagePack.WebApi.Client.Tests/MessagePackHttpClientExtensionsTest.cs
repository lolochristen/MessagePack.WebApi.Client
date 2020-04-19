using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessagePack.WebApi.Client;
using RichardSzalay.MockHttp;
using Xunit;

namespace MessagePack.WebApi.Client.Tests
{
    public class MessagePackHttpClientExtensionsTest
    {
        private HttpClient _client;
        private TestObject _testObject = new TestObject() { Id = 1235123, Text = "BLABLAöä![]" };
        private TestObject _receivedTestObject;

        public MessagePackHttpClientExtensionsTest()
        {
            var mockHttp = new MockHttpMessageHandler();

            // Setup a respond for the user api (including a wildcard in the URL)
            mockHttp.When(HttpMethod.Get, "http://localhost/api*")
                .Respond("application/x-msgpack", (message) =>
                {
                    var stream = new MemoryStream();
                    MessagePack.MessagePackSerializer.Serialize(stream, _testObject);
                    stream.Position = 0;
                    return stream;
                });

            mockHttp.When(HttpMethod.Post, "http://localhost/api*")
                .With(message => message.Content.Headers.ContentType.MediaType == "application/x-msgpack")
                .Respond((request) =>
                {
                    _receivedTestObject = request.Content.ReadAsMessagePackAsync<TestObject>().Result;
                    return new HttpResponseMessage(HttpStatusCode.OK);
                });

            mockHttp.When(HttpMethod.Put, "http://localhost/api*")
                .With(message => message.Content.Headers.ContentType.MediaType == "application/x-msgpack")
                .Respond((request) =>
                {
                    _receivedTestObject = request.Content.ReadAsMessagePackAsync<TestObject>().Result;
                    return new HttpResponseMessage(HttpStatusCode.OK);
                });

            mockHttp.When("http://localhost/long*")
                .Respond((request) =>
                {
                    System.Threading.Thread.Sleep(500);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                });

            // Inject the handler or client into your application code
            _client = mockHttp.ToHttpClient();
        }


        [Fact]
        public async Task GetFromMessagePackAsync_Successful()
        {
            var result = await _client.GetFromMessagePackAsync<TestObject>("http://localhost/api");
            Assert.NotNull(result);
            Assert.Equal(_testObject.Id, result.Id);
        }

        [Fact]
        public async Task GetFromMessagePackAsyncUri_Successful()
        {
            var result = await _client.GetFromMessagePackAsync<TestObject>(new Uri("http://localhost/api"));
            Assert.NotNull(result);
            Assert.Equal(_testObject.Id, result.Id);
        }

        [Fact]
        public async Task PostAsMessagePackAsync_Successful()
        {
            var o = new TestObject() { Id = 5, Text = "ABC" };
            var result = await _client.PostAsMessagePackAsync("http://localhost/api", o);
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(_receivedTestObject.Id, o.Id);
        }

        [Fact]
        public async Task PostAsMessagePackAsyncUri_Successful()
        {
            var o = new TestObject() { Id = 5, Text = "ABC" };
            var result = await _client.PostAsMessagePackAsync(new Uri("http://localhost/api"), o);
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(_receivedTestObject.Id, o.Id);
        }

        [Fact]
        public async Task PostAsMessagePackAsyncCancellation_Cancelled()
        {
            var s = new CancellationTokenSource(); 
            var o = new TestObject() { Id = 5, Text = "ABC" };
            var task = _client.PostAsMessagePackAsync(new Uri("http://localhost/long"), o, s.Token);
            s.Cancel();
            try
            {
                task.Wait();
            }
            catch (Exception)
            {

            }
            Assert.Equal(true, task.IsCanceled);
        }

        [MessagePackObject]
        public class TestObject
        {
            [Key(0)]
            public int Id { get; set; }

            [Key(1)]
            public string Text { get; set; }
        }
    }
}
