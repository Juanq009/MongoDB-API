using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using WebApiMongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Bson;

using MongoDB_API.SendEmail;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Authentication;
using System.Net.Sockets;

namespace WebApiMongoDB.SendEmail
{
    public class GmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public GmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task NewMessageAsync(string mensaje, Persona per)

        {
            // class Config tendria q configurar valores 
            var mes = new MimeMessage();
            mes.From.Add(new MailboxAddress("Test Project", _config["Mymail"]));
            mes.To.Add(new MailboxAddress(per.Nombre, _config["Mymail"]));  // enviar a varios contactos 
            // mes.To.Add(new MailboxAddress(per.Nombre, "flazaro@mg-group.com.ar")); // tendria q empezar a cargar email de las personas en la DB
            mes.Subject = mensaje;
            mes.Body = new TextPart("plain")
            {
                Text = per.ToJson()

            };




            using (var cl = new SmtpClient())
            {
                try
                { // default: 587
                    await cl.ConnectAsync(_config["ConectEmail"], 587, false);
                    //  no entra en catch devuelve error en postman y carga la collecion
                }
                catch (SocketException e)
                {
                    throw e;
                }
                catch (SmtpCommandException ex)
                {
                    Console.WriteLine("Error al intentar conectar: {0}", ex.Message);
                    Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);
                    return;
                }
                catch (SmtpProtocolException ex)
                {
                    Console.WriteLine("Error de protocolo al intentar conectarse: {0}", ex.Message);
                    return;
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("Error desconocido: {0}", e.Message);
                    throw;
                }
                if (cl.Capabilities.HasFlag(SmtpCapabilities.Authentication))
                {
                    try
                    {
                        await cl.AuthenticateAsync(_config["Mymail"], _config["Mypass"]);
                    }
                    catch (AuthenticationException)
                    {
                        Console.WriteLine("Usuario o contraseña invalido.");
                        return;
                    }
                    catch (SmtpCommandException ex)
                    {
                        Console.WriteLine("Error al intentar autenticar: {0}", ex.Message);
                        Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);
                        return;
                    }
                    catch (SmtpProtocolException ex)
                    {
                        Console.WriteLine("Error de protocolo al intentar autenticar: {0}", ex.Message);
                        return;
                    }
                }

                try
                {
                    await cl.SendAsync(mes);
                }
                catch (SmtpCommandException ex)
                {
                    Console.WriteLine("Error al enviar el mensaje: {0}", ex.Message);
                    Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);

                    switch (ex.ErrorCode)
                    {
                        case SmtpErrorCode.RecipientNotAccepted:
                            Console.WriteLine("\tRecipient not accepted: {0}", ex.Mailbox);
                            break;
                        case SmtpErrorCode.SenderNotAccepted:
                            Console.WriteLine("\tSender not accepted: {0}", ex.Mailbox);
                            break;
                        case SmtpErrorCode.MessageNotAccepted:
                            Console.WriteLine("\tMessage not accepted.");
                            break;
                    }
                }
                catch (SmtpProtocolException ex)
                {
                    Console.WriteLine("Error de protocolo al intentar autenticar: {0}", ex.Message);
                }

                await cl.DisconnectAsync(true);
            }




        }
    }
}
