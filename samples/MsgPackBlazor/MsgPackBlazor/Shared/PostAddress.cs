using MessagePack;

namespace MsgPackBlazor.Shared
{
    [MessagePackObject]
    public class PostAddress
    {
        [Key(0)]
        public string Street { get; set; }
        [Key(1)]
        public string Zip { get; set; }
        [Key(2)]
        public string City { get; set; }
        [Key(3)]
        public string CountryCode { get; set; }
    }
}
