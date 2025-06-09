using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


namespace CriptomoedasAPI
{
    public class BuscarMoedas
    {
        public static async Task<List<Moedas>> BuscarMoedasAsync (string nomeMoeda)
        {
            var Link = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids={Uri.EscapeDataString(nomeMoeda)}";
            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; CriptomoedasApp/1.0)");
            return await cliente.GetFromJsonAsync<List<Moedas>>(Link) ?? new List<Moedas>();
        }
    }
}
