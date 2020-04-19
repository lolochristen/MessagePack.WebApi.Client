using System;
using System.Threading.Tasks;
using Xunit;

namespace MessagePack.WebApi.Client.Tests
{
    public class MessagePackContentTests
    {
        [Fact]
        public async Task MessagePackContent_StreamSerialized()
        {
            var o = new TestObject() {Id = 1, Text = "..."};
            var content = new MessagePackContent(o);
            var stream = await content.ReadAsStreamAsync();
        
            Assert.NotNull(stream);
            var o2 = MessagePackSerializer.Deserialize<TestObject>(stream);
            Assert.Equal(o.Id, o2.Id);
        }

        [Fact]
        public async Task MessagePackContentTyped_StreamSerialized()
        {
            var o = new TestObject() { Id = 1, Text = "..." };
            var content = new MessagePackContent<TestObject>(o);
            var stream = await content.ReadAsStreamAsync();

            Assert.NotNull(stream);
            var o2 = MessagePackSerializer.Deserialize<TestObject>(stream);
            Assert.Equal(o.Id, o2.Id);
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
