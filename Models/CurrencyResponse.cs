// Arquivo: Models/CurrencyResponse.cs
namespace CotacaoMoedas.Models
{
    using System.Text.Json.Serialization;

    public class CurrencyResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("codein")]
        public string Codein { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("high")]
        public string High { get; set; }

        [JsonPropertyName("low")]
        public string Low { get; set; }

        [JsonPropertyName("varBid")]
        public string VarBid { get; set; }

        [JsonPropertyName("pctChange")]
        public string PctChange { get; set; }

        [JsonPropertyName("bid")]
        public string Bid { get; set; }

        [JsonPropertyName("ask")]
        public string Ask { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("create_date")]
        public string CreateDate { get; set; }
    }
}
