using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using WebApiMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace WebApiMongoDB.SendEmail
{
    public class Contact
    {

        public string MyEmail { get; set; }
        public string Host { get; set; }
        public int Puerto { get; set; }
        public string MyPass { get; set; }

        public Contact()
        {
        }

        public async Task NewMessageAsync(Persona per)

        {
            // class Config tendria q configurar valores 



            var mes = new MimeMessage();
            mes.From.Add(new MailboxAddress("Test Project", "juanq009.jq@gmail.com"));
            mes.To.Add(new MailboxAddress(per.Nombre, "juanq009.jq@gmail.com"));  // enviar a varios contactos 
            mes.To.Add(new MailboxAddress(per.Nombre, "flazaro@mg-group.com.ar")); // tendria q empezar a cargar email de las personas en la DB
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
                    await cl.ConnectAsync("smtp.gmail.com", 587, false);
                    await cl.AuthenticateAsync("juanq009.jq@gmail.com", "1982gonzo");// encriptar contraseña
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
