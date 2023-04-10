using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer
{
    public class MSMQ
    {
        MessageQueue messageQueue = new MessageQueue();
        private string recieverEmailAddress;
        private string recieverName;

        public void SendMessage(string token,string emailId,string name)
        {
            recieverEmailAddress = emailId;
            recieverName = name;
            messageQueue.Path = @".\Private$\Token";
            try
            {
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_RecieveCompleted;
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void MessageQueue_RecieveCompleted(object sender,ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token=msg.Body.ToString();
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, //for gmail
                    EnableSsl = true,
                    Credentials = new NetworkCredential("anilbamne20@gmail.com", "uydwandvzjtyokei"),
                };
                mailMessage.From = new MailAddress("anilbamne20@gmail.com");
                mailMessage.To.Add(new MailAddress(recieverEmailAddress));
                string mailBody = $"<!DOCTYPE html>" +
                                $"<html>" +
                                $"<style>" +
                                $".blink" +
                                $"</style>" +
                                $"<body style=\"background-color:#WDBFF73;text-align:center;padding:5px;\">" +
                                $"<h1 style=\"color:#648D02;border-bottom:3px solid #84AF08;margin-top:5px\">Dear <b>{recieverName}</b></h1>\n" +
                                $"<h3 style=\"color:#8AB411;\"> For Resetting Password The Below Link Is Issued</h3>" +
                                $"<h3 style=\"color:#8AB411;\"> Click Below Link For Resetting Your Password</h3>" +
                                $"<a style=\"color:#00802b;text-decoration:none;font-size:20px;\" href='http://localhost:4200/resetPassword/{token}'>Click Here</a>\n" +
                                $"<h3 style=\"color:#8AB411;margin-bottom:5px;\"><blink>Token Will Be Valid For Next 1 Hour</blink></h3>" +
                                $"</body>" +
                                $"</html>";

                mailMessage.Body = mailBody;
                mailMessage.IsBodyHtml= true;
                mailMessage.Subject = "Fundoo Notes Password Reset Link";
                smtpClient.Send(mailMessage);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
