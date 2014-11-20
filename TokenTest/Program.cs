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
            Post(token);
        }

        private static async void Post(string token)
        {
            Person person = new Person(){FirstName ="FirstName", LastName = "LastName"};
            var service = new PostService();
            var result = await service.Post(person, token) as Person;

            Console.WriteLine("Post api/Values  return:FirstName: {1} LastName: {2}", "1", result.FirstName, result.LastName);
        }

    }

    public class Person: IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
