using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cw1
{



    class Program
    {
        static async Task Main(string[] args)
        {
            Uri uriResult;
            string website;
            var httpClient = new HttpClient();

            Console.WriteLine("Podaj stronę: ");
            website = Console.ReadLine();

            if (website == null)
            {
                throw new System.ArgumentNullException();
            }


            bool result = Uri.TryCreate(website, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result)
            {
                throw new System.ArgumentException();
            }

            try
            {
                var response = await httpClient.GetAsync(website);


                if (response.IsSuccessStatusCode)
                {
                    var html = await response.Content.ReadAsStringAsync();
                    var regex = new Regex("[a-z0-9]+@[a-z.]+");

                    MatchCollection matches = regex.Matches(html);
                    int howMuchEmails = matches.Count;
                    if (howMuchEmails > 0)
                    {
                        foreach (var i in matches)
                        {
                            Console.WriteLine(i);
                        }
                    }
                    else { Console.WriteLine("Nie znaleziono adresów email"); }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Bład wczytywania strony");
            }

            httpClient.Dispose();
            Console.WriteLine("------------");
            Console.WriteLine("Socket closed by Dispose");



        }


    }
}
