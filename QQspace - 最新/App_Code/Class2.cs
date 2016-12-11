using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Library.DAL
{
    public class Send : System.Web.UI.Page
    {
        /// <summary> 
        /// 发送电子邮件 
        /// </summary> 
        /// <param name="MessageFrom">发件人邮箱地址 </param> 
        /// <param name="MessageTo">收件人邮箱地址 </param> 
        /// <param name="MessageSubject">邮件主题 </param> 
        /// <param name="MessageBody">邮件内容 </param> 
        /// <returns> </returns> 
       static public bool Sendemails(string MessageFrom, string MessageTo, string MessageSubject, string MessageBody)
        {
            MailMessage message = new MailMessage();
            MailAddress from = new MailAddress(MessageFrom);
            message.From = from;
            MailAddress messageto = new MailAddress(MessageTo);
            message.To.Add(messageto);              //收件人邮箱地址可以是多个以实现群发 
            message.Subject = MessageSubject;
            message.Body = MessageBody;
            message.IsBodyHtml = true;              //是否为html格式 
            message.Priority = MailPriority.High;   //发送邮件的优先等级
            //指定发送邮件的服务器地址或IP 
            //指定发送邮件端口
            SmtpClient sc = new SmtpClient("smtp.163.com", 25);
            sc.Credentials = new System.Net.NetworkCredential("17806282596@163.com", "itstudio1"); //指定登录服务器的用户名和密码  


            sc.Send(message);       //发送邮件                              
            return true;
        }
    }
}