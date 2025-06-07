using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace CriptomoedasAPI
{
    public class Moedas
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("current_price")]
        public double Current_price { get; set; }

        [JsonPropertyName("high_24h")]
        public double High_24h { get; set; }

        [JsonPropertyName("low_24h")]
        public double Low_24h { get; set; }

        [JsonPropertyName("total_volume")]
        public double Total_volume { get; set; }

    }
  
}
