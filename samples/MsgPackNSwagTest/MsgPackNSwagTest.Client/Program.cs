using System;
using System.Threading.Tasks;

namespace MsgPackNSwagTest.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press [ENTER] to start");
            Console.ReadKey();

            Console.WriteLine("\nMessagePack:");
            await MessagePack.MessagePackCalls.ExecuteAsync();

            Console.WriteLine("\nJson:");
            await Json.JsonCalls.ExecuteAsync();
        }
    }
}
