using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using CriptomoedasAPI;

TimeZoneInfo fusoLocal = TimeZoneInfo.Local;
DateTime horaLocal = DateTime.Now;

Console.WriteLine("Qual informação você deseja executar ?");
Console.WriteLine("1: Ver o preço de uma moeda");
Console.WriteLine("2: Comparar duas moedas");
Console.WriteLine("3: Ver as dez moedas mais valiosas do momento");
Console.WriteLine("------------------");
Console.WriteLine("Atenção: todas as informações estão em USD");
int respostaPrimeiraPergunta = int.Parse(Console.ReadLine());

switch (respostaPrimeiraPergunta)
{
    case 1:
        Console.WriteLine("Digite o nome da moeda você deseja");
        string moedaNome = Console.ReadLine();
        var moeda = await BuscarMoedas.BuscarMoedasAsync(moedaNome, null);
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
        break;

        case 2:
        Console.WriteLine("Digite o nome da primeira moeda");
        string primeiraMoeda = Console.ReadLine().ToLower().Trim();
        Console.WriteLine("Digite o nome da segunda moeda");
        string segundaMoeda = Console.ReadLine().ToLower().Trim();
        var moedaComparacao = await BuscarMoedas.BuscarMoedasAsync(primeiraMoeda,segundaMoeda);
        var moeda1 = moedaComparacao[0];
        var moeda2 = moedaComparacao[1];
          if (moeda1 == null || moeda2 == null)
        {
            Console.WriteLine("Alguma das duas moedas estão faltando");
        }
 
        else {
            Console.WriteLine("Comparação entre:" + moeda1.Name + " e " + moeda2.Name);
            Console.WriteLine("Preço atual: " + moeda1.Current_price + " e " + +moeda2.Current_price + " diferença de " +
            (moeda1.Current_price - moeda2.Current_price));
            Console.WriteLine("Alta: " + moeda1.High_24h + " e " + moeda2.High_24h);
            Console.WriteLine("Baixa: " + moeda1.Low_24h + " e " + moeda2.Low_24h);
            Console.WriteLine("Volume total: " + moeda1.Total_volume + " e " + moeda2.Total_volume);

            Console.WriteLine("Voc~e deseja exportar essas informações para o e-mail ?");
            string respostaComparativo = Console.ReadLine();
            if (respostaComparativo == "sim")
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("dagalleazzo@gmail.com");
                Console.WriteLine("Insira o email do destinatário: ");
                string destinatario = Console.ReadLine();
                var to = new EmailAddress(destinatario);
                var subject = "Informações das moedas";
                var plainTextContent = "Utilizando a API do SendGrid";
                var htmlContent = " Nome da primeira moeda: " + moeda1.Name + "<br> Nome da segunda moeda: " + moeda2.Name
                    + "<br> Preços atuais: " + moeda1.Current_price + " || " + moeda2.Current_price +
                    "<br> Diferença de valor das duas moedas: " + (moeda1.Current_price - moeda2.Current_price);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine("E-mail enviado com sucesso ! Verifique o SPAN");

            }
            }
        break;

    case 3:
        Console.WriteLine("Aqui está uma lista das dez moedas mais valiosas");
        var MoedA = await MoedasMaisValiosas.MoedasMaisValiosasAsync();
        foreach(var MOEDA in MoedA)
        {
            Console.WriteLine("Nome da moeda: " + MOEDA.Name);
            Console.WriteLine("Preço atual: " + MOEDA.Current_price);
            /*
            Console.WriteLine("Você deseja receber essas informações pelo e-mail ?");
            string respostaTop10 = Console.ReadLine();
            if (respostaTop10 == "sim")
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("dagalleazzo@gmail.com");
                Console.WriteLine("Insira o email do destinatário: "); // pensar na logica de como melhorar isso daqui
                string destinatario = Console.ReadLine();
                var to = new EmailAddress(destinatario);
                var subject = "Informações das moedas";
                var plainTextContent = "Utilizando a API do SendGrid";
                var htmlContent = "Nome da moeda: " + MOEDA.Name +
                    "<br> Valor: " + MOEDA.Current_price;
            }
            */

        }

        break;
}






