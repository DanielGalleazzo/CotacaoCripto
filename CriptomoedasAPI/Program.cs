using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using CriptomoedasAPI;

TimeZoneInfo fusoLocal = TimeZoneInfo.Local;
DateTime horaLocal = DateTime.Now;              // Extrai a sua localização atual
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
    DateTime suaLocalizacao = TimeZoneInfo.ConvertTimeFromUtc(Moeda.Last_update, fusoLocal);  // Converte a São Fransisco para a sua localização atual
    Console.WriteLine("Horário da informação (São Paulo): " + suaLocalizacao);
    Console.WriteLine("Horário da informação ( São Francisco) " + Moeda.Last_update);
    Console.WriteLine("Você deseja receber essas informações por e-mail ? ");
    string resposta = Console.ReadLine();
    if (resposta.ToLower() == "sim" || resposta.ToLower() == "s")
    {
        try
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("dagalleazzo@gmail.com");
            Console.WriteLine("Insira o email do destinatário: ");
            string destinatario = Console.ReadLine();
            var to = new EmailAddress(destinatario);
            var subject = "Informações da moeda" + Moeda.Name;
            var plainTextContent = "Utilizando a API do SendGrid";
            Console.WriteLine("Você quer receber o link da imagem da moeda escolhida ?");
            string respostaImagem = Console.ReadLine();

            if (respostaImagem.ToLower() == "sim" || respostaImagem.ToLower() == "s")
            {
                var htmlContent = " Nome da moeda: " + Moeda.Name + "<br> Preço atual: " + Moeda.Current_price +
                "<br> Pico nas últimas 24 horas:" + Moeda.High_24h + "<br> Baixa das últimas 24 horas: " + Moeda.Low_24h +
                 "<br> Informações extraidas às: " + suaLocalizacao + "<br> Link da imagem: " + Moeda.image;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine("E-mail enviado com sucesso ! Verifique o SPAN");
            }
            else
            {
                var htmlContent = " Nome da moeda: " + Moeda.Name + "<br> Preço atual: " + Moeda.Current_price +
               "<br> Pico nas últimas 24 horas:" + Moeda.High_24h + "<br> Baixa das últimas 24 horas: " + Moeda.Low_24h +
                "<br> Informações extraidas às: " + suaLocalizacao;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine("E-mail enviado com sucesso ! Verifique o SPAN");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Mensagem do erro:" + ex.Message);
        }
    }
    else
    {
        Console.WriteLine("Obrigado por usar o programa !");
    }
}
