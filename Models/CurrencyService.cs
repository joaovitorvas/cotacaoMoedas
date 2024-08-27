using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CotacaoMoedas.Models;

namespace CotacaoMoedas.Services
{
    public class CurrencyService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        
        private static readonly string[] urls = 
        {
            "https://economia.awesomeapi.com.br/json/daily/USD-BRL",
            "https://economia.awesomeapi.com.br/json/daily/EUR-BRL",
            "https://economia.awesomeapi.com.br/json/daily/BTC-BRL",
            "https://economia.awesomeapi.com.br/json/daily/BRL-USD",
            "https://economia.awesomeapi.com.br/json/daily/BRL-EUR",
        };

        public static async Task<List<CurrencyResponse[]>> FetchCurrencyDataAsync()
        {
            var currencyData = new List<CurrencyResponse[]>();

            foreach (var url in urls)
            {
                try
                {
                    var response = await httpClient.GetStringAsync(url);
                    var data = JsonSerializer.Deserialize<CurrencyResponse[]>(response);
                    
                    if (data != null && data.Length > 0)
                    {
                        currencyData.Add(data);
                    }
                    else
                    {
                        System.Console.WriteLine($"Nenhum dado foi retornado para {url}.");
                    }
                }
                catch (HttpRequestException e)
                {
                    System.Console.WriteLine($"Erro na solicitação para {url}: {e.Message}");
                }
                catch (JsonException e)
                {
                    System.Console.WriteLine($"Erro na desserialização JSON para {url}: {e.Message}");
                }
            }

            return currencyData;
        }
    }
}