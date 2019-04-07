using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using WebApiMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApiMongoDB.SendEmail
{
    public class Contact
    {
        public Contact()
        {
        }

        public async Task NewMessageAsync(Persona per)
        {
            var mes = new MimeMessage();
            mes.From.Add(new MailboxAddress("Test Project", "juanq009.jq@gmail.com"));
            mes.To.Add(new MailboxAddress(per.Nombre, "juanq009.jq@gmail.com"));
            mes.Subject = "Nuevo mienbro agregado a la collection";
            mes.Body = new TextPart("plain")
            {
                Text = " "
            };
            /// no se si esta bien llamarlo asi 
            using (var cl = new SmtpClient())
            {

                await cl.ConnectAsync("smtp.gmail.com", 587, false);
                await cl.AuthenticateAsync("juanq009.JQ@gmail.com", "1982gonzo");// encriptar contraseña
                await cl.SendAsync(mes);
                await cl.DisconnectAsync(true);
            }

        }
    }
}