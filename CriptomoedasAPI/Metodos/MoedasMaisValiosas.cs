using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CriptomoedasAPI.GetSet;

namespace CriptomoedasAPI.Metodos
{
    public class MoedasMaisValiosas
    {
        public static async Task<List<Moedas>> MoedasMaisValiosasAsync()
        {
            string api = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1&sparkline=false";
            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; CriptomoedasApp/1.0)");
            return await cliente.GetFromJsonAsync<List<Moedas>>(api) ?? new List<Moedas>();

        }
    }
}
