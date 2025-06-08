using CriptomoedasAPI;

Console.WriteLine("Insira o nome da moeda");
string moedaNome = Console.ReadLine();
Console.WriteLine("Todas as informações estão em USD");

var moeda = await BuscarMoedas.BuscarMoedasAsync(moedaNome);

foreach (var Moeda in moeda)
{
    Console.WriteLine("Nome da moeda: " + Moeda.Name);
    Console.WriteLine("Preço atual: " + Moeda.Current_price);
    Console.WriteLine("Pico nas últimas 24 horas: " + Moeda.High_24h);
    Console.WriteLine("Baixa das ultimás 24 horas: " + Moeda.Low_24h);
    Console.WriteLine("Diferença da máxima e baixa ( 24 horas ): " + ( Moeda.High_24h - Moeda.Low_24h));
    Console.WriteLine("Volume total: " + Moeda.Total_volume);
}