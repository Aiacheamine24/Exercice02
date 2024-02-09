using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Helpers;
using Exercice02.Models;

namespace Exercice02.utils
{
    public class Mail
    {
        // Définir les valeurs directement dans la classe
        private const string Sender = "studybridge.ca@gmail.com";
        private const string Password = "uvjc luht kttm qqzl";
        // Méthode pour envoyer un email
        public static ErrorManager SendMail(Email email)
        {
            try
            {
                // Créer un client SMTP
                SmtpClient smtpclient = new SmtpClient("smtp.gmail.com", 587);
                smtpclient.EnableSsl = true;
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(Sender, Password);
                // Créer un message email
                MailMessage mailMessage = new System.Net.Mail.MailMessage(Sender, email.To, email.Subject, email.Message);
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                smtpclient.Send(mailMessage);
                // Retourner vrai si l'email est envoyé
                return new ErrorManager(true, "Email sent successfully", null);
            }
            catch (Exception ex)
            {
                return new ErrorManager(false, ex.Message, null);
            }

        }
    }
}
