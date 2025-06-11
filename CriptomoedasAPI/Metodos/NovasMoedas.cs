using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CriptomoedasAPI.GetSet;
using static System.Net.WebRequestMethods;

namespace CriptomoedasAPI.Metodos
{
    public class NovasMoedas
    {
        public static async Task<List<NovasMoedasGetSet>> MoedasRecemLancadas()
        {
            string api = "https://api.coingecko.com/api/v3/coins/list";
            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; CriptomoedasApp/1.0)");
            return await cliente.GetFromJsonAsync<List<NovasMoedasGetSet>>(api) ?? new List<NovasMoedasGetSet>();
        }
    }
}
