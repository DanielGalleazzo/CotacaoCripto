using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
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
        Console.WriteLine("Diferença da máxima e baixa ( 24 horas ): " + (Moeda.High_24h - Moeda.Low_24h));
        Console.WriteLine("Volume total: " + Moeda.Total_volume);
        Console.WriteLine("Você deseja receber essas informações por e-mail ? ");

        string resposta = Console.ReadLine();
        if (resposta == "sim")
        {
           
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("dagalleazzo@gmail.com");
                Console.WriteLine("Insira o email do destinatário: ");
                string destinatario = Console.ReadLine();
                var to = new EmailAddress(destinatario);
                var subject = "Informações da moeda" + Moeda.Name;
                var plainTextContent = "Utilizando a API do SendGrid";
                var htmlContent = "Nome da moeda: " + Moeda.Name + "Preço atual: " + Moeda.Current_price + "Pico nas últimas 24 horas:" + Moeda.High_24h + "Baixa das últimas 24 horas: " + Moeda.Low_24h;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Status Code: {response.StatusCode}");

        }
    }
