using System;
using System.Threading.Tasks;

namespace TokenTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Task task = new Task(Logon);
            task.Start();
            task.Wait();
            Console.ReadKey();
        }

        private static async void Logon()
        {
            var service = new LoginService();
            var _accessToken = await service.Login("test@test.com", "Helloworld1234%");
            Console.WriteLine("AccessToken: {0}", _accessToken);
            Get(_accessToken);
        }

        private static async void Get(string token)
        {
            var service = new GetService();
            var result = await service.Get("1", token);

            Console.WriteLine("api/Values/{0}  return:{1}","1", result);
        }
    }
}
