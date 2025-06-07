using CriptomoedasAPI;

Console.WriteLine("Insira o nome da moeda ( letra minúscula) ");
string moedaNome = Console.ReadLine();

var moeda = await BuscarMoedas.BuscarMoedasAsync(moedaNome);

foreach (var Moeda in moeda)
{
    Console.WriteLine("Nome da moeda: " + Moeda.Name);
    Console.WriteLine("Preço atual: " + Moeda.Current_price);
}