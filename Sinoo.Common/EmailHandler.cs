using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Sinoo.Common
{
    /// <summary>
    /// SendMail邮箱
    /// </summary>
    public class EmailHandler
    {
         private string _mailform;//发送方邮箱地址
        /// <summary>
        /// 发送方邮箱地址
        /// </summary>
        public string MailForm { get { return _mailform; } set { _mailform = value; } }
        private string _host;//发送方地址
        public string Host
        {
            set { _host = value; }
            get { return _host; }
        }
        private string _smtpUserName;//发送方用户名
        public string SmtpUserName
        {
            set { _smtpUserName = value; }
            get { return _smtpUserName; }
        }
        private string _smtpPassword;//发送方密码
        public string SmtpPassword
        {
            set { _smtpPassword = value; }
            get { return _smtpPassword; }
        }
        private int _Port;
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        public EmailHandler()
        {
            //_mailform= Config.GetAppSettings("mailFrom");
            //_host = Config.GetAppSettings("host");
            //_smtpUserName = Config.GetAppSettings("mailUserName");
            //_smtpPassword = Config.GetAppSettings("mailPwd");
        }

        public bool Send(string MailTo, string Subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_mailform);
            if (mail == null) { return false; }
            mail.To.Add(new MailAddress(MailTo));
            mail.Subject = Subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.GetEncoding("GB2312");
            return Send(mail, Port);
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="myPort">端口号</param>
        /// <returns></returns>
        private bool Send(MailMessage mail,int myPort)
        {
            SmtpClient sc = new SmtpClient();
            sc.UseDefaultCredentials = true;
            sc.EnableSsl = false;
            sc.Host = _host;
            sc.Port = myPort;
            sc.Credentials = new System.Net.NetworkCredential(_smtpUserName, _smtpPassword);
            try
            {
                sc.Send(mail);
                return true;
            }
            catch{
                return false;
                }
        }
    }
}
