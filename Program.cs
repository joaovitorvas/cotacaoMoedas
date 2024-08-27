using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CotacaoMoedas.Models;
using CotacaoMoedas.Services;

class Program
{
    private static async Task Main(string[] args)
    {
        var currencyData = await CurrencyService.FetchCurrencyDataAsync();

        await File.WriteAllTextAsync("cotacoes.json", JsonSerializer.Serialize(currencyData, new JsonSerializerOptions { WriteIndented = true }));

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Escolha a cotação para visualizar:");
            Console.WriteLine("");
            Console.WriteLine("1. Dólar (USD-BRL)");
            Console.WriteLine("2. Euro (EUR-BRL)");
            Console.WriteLine("3. Bitcoin (BTC-BRL)");
            Console.WriteLine("4. Dólar/Real (BRL-USD)");
            Console.WriteLine("5. Euro/Real (BRL-EUR)");
            Console.WriteLine("");
            Console.Write("Digite o número da opção desejada: ");
            var input = Console.ReadLine();

            int index = -1;
            switch (input)
            {
                case "1":
                    index = 0; // Dólar
                    break;
                case "2":
                    index = 1; // Euro
                    break;
                case "3":
                    index = 2; // Bitcoin
                    break;
                case "4":
                    index = 3; // Dolar/Real
                    break;
                case "5":
                    index = 4; // Euro/Real
                    break;
                default:
                    break;
            }

            if (index >= 0 && index < currencyData.Count)
            {
                var dataArray = currencyData[index];
                foreach (var data in dataArray)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"--- {data.Code} ---");
                    Console.WriteLine($"Nome: {data.Name}");
                    Console.WriteLine($"Valor mais alto registrado hoje: {FormatCurrency(data.High)}");
                    Console.WriteLine($"Valor mais baixo registrado hoje: {FormatCurrency(data.Low)}");
                    Console.WriteLine($"Valor atual de venda (Ask): {FormatCurrency(data.Ask)}");
                    Console.WriteLine($"Atualizado em: {data.CreateDate}");
                    Console.WriteLine();

                    Console.WriteLine("Aguardando 7 segundos antes de mostrar o menu novamente...");
                    await Task.Delay(7000);
                }
            }
            else
            {
                Console.WriteLine("Não há dados disponíveis para a opção selecionada.");
                Console.WriteLine();
                await Task.Delay(2000); 
            }

        }
    }

    // Formata valores monetários para duas casas decimais
    private static string FormatCurrency(string value)
    {
        if (decimal.TryParse(value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue))
        {
            return decimalValue.ToString("F2", CultureInfo.InvariantCulture);
        }
        return value;
    }
}
