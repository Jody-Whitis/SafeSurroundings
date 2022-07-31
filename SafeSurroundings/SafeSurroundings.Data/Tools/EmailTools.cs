using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
 namespace SafeSurroundings.Data.Tools
{
    public class EmailTools
    {
        public static Boolean SendEmail(string emailSubject, List<string> senderList)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage mailMessage = new MailMessage();           
            Boolean isSent = false;

            try
            {
                string emailTemplate = File.ReadAllText(@"EmailTemplates\EmailTemplate.html").ToString();
                smtp.UseDefaultCredentials = Properties.Settings.Default.emailDefaultCreditials;
                smtp.Credentials = new System.Net.NetworkCredential(Properties.Settings.Default.emailCreditials,Properties.Settings.Default.passEmailCreditials);
                smtp.Port = Properties.Settings.Default.emailPort;
                smtp.EnableSsl = true;
                smtp.Host = Properties.Settings.Default.emailServer;

                mailMessage.Sender = new MailAddress(Properties.Settings.Default.senderEmail);
                mailMessage.From = new MailAddress("safeSurroundings@safe.com");
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = emailSubject;

                foreach(string sender in senderList)
                {
                    mailMessage.To.Add(sender);
                }

                mailMessage.Body = emailTemplate;
                
                smtp.Send(mailMessage);
                isSent = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                isSent = false;
            }
            return isSent;
        }
    }
}
