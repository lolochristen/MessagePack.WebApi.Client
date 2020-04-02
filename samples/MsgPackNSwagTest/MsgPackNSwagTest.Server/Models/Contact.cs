using MessagePack;
using System;

namespace MsgPackNSwagTest.Models
{
    [MessagePackObject]
    public class Contact
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string FirstName { get; set; }
        [Key(2)]
        public string LastName { get; set; }
        [IgnoreMember]
        public string Nothing { get; set; }
        [Key(3)]
        public string EmailAdress { get; set; }
        [Key(4)]
        public string PhoneNumber { get; set; }
        [Key(5)]
        public Genders Gender { get; set; }

        [Key(6)]
        public PostAddress StandardAddress { get; set; }


        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName} - {EmailAdress} - {PhoneNumber}";
        }
    }
}
