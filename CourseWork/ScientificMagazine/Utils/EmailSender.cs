using System.Net.Mail;
using System.Net;

namespace ScientificMagazine.Utils
{
    public static class EmailSender
    {
        private static string From = "tillrol7@gmail.com";
        private static int port = 587;
        private static string Password = "rqgytjciabfwvaoq";
        public static void Send(string to, string text, List<string> attachments = null, string subject="Публикация в журнал")
        {

            MailMessage newMail = new MailMessage();
            // use the Gmail SMTP Host
            SmtpClient client = new SmtpClient("smtp.gmail.com");

            // Follow the RFS 5321 Email Standard
            newMail.From = new MailAddress(From, "no-reply");

            newMail.To.Add(to);// declare the email subject
            if(attachments != null) foreach(var attachment in attachments)
            {
                newMail.Attachments.Add(new Attachment(attachment));
            }
            newMail.Subject = subject; 
            newMail.Body = text;

            // enable SSL for encryption across channels
            client.EnableSsl = true;
            // Port 587 for SSL communication
            client.Port = port;
            // Provide authentication information with Gmail SMTP server to authenticate your sender account
            client.Credentials = new System.Net.NetworkCredential(From, Password);

            client.Send(newMail); // Send the constructed mail
           
        }
    }
}



