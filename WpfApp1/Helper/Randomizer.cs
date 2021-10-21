using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using WpfApp1.Model;


namespace WpfApp1.Helper
{
    public static class Randomizer
    {
        private const string URL = "";

        static async Task<List<string>> getRandomData(string Type, Dictionary<string, string> parameters, int count = 100)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            client.DefaultRequestHeaders.Add("X-Api-Key", "52fd186b6aa1471c8ddc05a0e97d9718");
            //client.Timeout = new TimeSpan(100);
            //client.BaseAddress = new Uri(URL);

            var additionalUrl = string.Format("https://randommer.io/api/{0}?{1}", Type,
                string.Join("&",
                    parameters.Select(
                        kvp =>
                            $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}")));
            
            var res =  await client.GetAsync(additionalUrl);

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadAsAsync<List<string>>();
                
            }
            else return null;
        }

        public static async Task<ICollection<BankClient>> GetData(ICollection<BankClient>? col, int quantity)
        {

            if (col is null) col = new List<BankClient>();
                var names = await getRandomData("Name", new Dictionary<string, string>
                {
                    {"nameType", "firstname"},
                    {"quantity", quantity.ToString()}
                });

                // ReSharper disable once PossibleNullReferenceException
                var secondNames = await getRandomData("Name", new Dictionary<string, string>
                {
                    {"nameType", "firstname"},
                    {"quantity", quantity.ToString()}
                });

                var surNames = await getRandomData("Name", new Dictionary<string, string>
                {
                    {"nameType", "surname"},
                    {"quantity", quantity.ToString()}
                });

                // ReSharper disable once PossibleNullReferenceException
                var phones = await getRandomData("Phone/Generate", new Dictionary<string, string>
                {
                    {"CountryCode", "RU"},
                    {"quantity", quantity.ToString()}
                });

                col.Clear();

                var rnd = new Random();

                for (int i = 0; i < quantity; i++)
                {
                    col.Add(new BankClient(rnd.Next(1000, 9999).ToString(), rnd.Next(10000000, 99999999).ToString(),
                        phones[i], names[i], secondNames[i], surNames[i]));
                }

                return col;
           

        }
    }

   
}