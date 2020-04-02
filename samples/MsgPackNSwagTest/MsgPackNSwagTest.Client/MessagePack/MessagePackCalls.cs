using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MsgPackNSwagTest.Client.MessagePack
{
    public class MessagePackCalls
    {
        public static async Task ExecuteAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestVersion = new Version(2, 0); // boost a bit with http2

            var service = new ContactsService(client);

            var contact = await service.GetContactAsync(4123);
            Console.WriteLine($"{contact.EmailAdress}");

            var response = await service.DownloadAsync("234");

            await service.UpdateContactAsync(contact);

            var start = DateTime.Now;
            var tasks = new List<Task>();

            for (int i = 0; i < 500; i++)
            {
                var task = Task.Run(async () =>
                {
                    Console.Write(">");
                    var list = await service.GetAllAsync();
                    Console.Write("<");
                });
                tasks.Add(task);
            };

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"{DateTime.Now - start}");
        }
    }
}
