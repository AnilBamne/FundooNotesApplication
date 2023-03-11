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
                                $"</style>";

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
