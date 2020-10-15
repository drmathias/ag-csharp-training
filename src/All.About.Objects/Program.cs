using System;
using System.Linq;

namespace All.About.Objects
{
    class Program
    {
        static void Main()
        {
            HttpExample();
            QueryingExample();
        }

        static void HttpExample()
        {
            var rates = new ExchangeRates().Latest();
            Console.WriteLine(rates.Rates.EUR);
        }

        static void QueryingExample()
        {
            var latestPosts = new TagUri[]
            {
                new TagUri("appreciate.co.uk", new DateTime(2020, 10, 01), "the-secret-recipe-to-customer-retention"),
                new TagUri("appreciate.co.uk", new DateTime(2020, 08, 21), "could-screwing-up-be-the-best-thing-you-do-for-customer-loyalty"),
                new TagUri("appreciate.co.uk", new DateTime(2020, 07, 29), "the-crucial-trick-to-motivating-and-engaging-your-remote-teams")
            };

            var result = latestPosts.Where(tag => tag.Date > new DateTime(2020, 09, 01));
            Console.WriteLine(string.Join('\n', result));
        }
    }
}
