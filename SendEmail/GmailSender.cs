using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using WebApiMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Bson;

using MongoDB_API.SendEmail;
using Microsoft.Extensions.Configuration;

namespace WebApiMongoDB.SendEmail
{
    public class GmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public GmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task NewMessageAsync(Persona per)

        {
            // class Config tendria q configurar valores 



            var mes = new MimeMessage();
            mes.From.Add(new MailboxAddress("Test Project", _config["Mymail"]));
            mes.To.Add(new MailboxAddress(per.Nombre, _config["Mymail"]));  // enviar a varios contactos 
            // mes.To.Add(new MailboxAddress(per.Nombre, "flazaro@mg-group.com.ar")); // tendria q empezar a cargar email de las personas en la DB
            mes.Subject = "Se a cargado un nuevo miebro a la coleccion ";
            mes.Body = new TextPart("plain")
            {
                Text = per.ToJson() // modificar forma en la que llegan los email con algun template

            };
            /// no se si esta bien llamarlo asi 

            try
            {
                using (var cl = new SmtpClient())
                {
                    await cl.ConnectAsync(_config["ConectEmail"], 587, false);
                    await cl.AuthenticateAsync(_config["Mymail"], _config["Mypass"]);// encriptar contraseña
                    await cl.SendAsync(mes);
                    await cl.DisconnectAsync(true);
                }

            }
            catch (System.Exception)
            {


            }


        }
    }
}
