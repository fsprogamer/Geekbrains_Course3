using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePasswordDLL;
namespace WpfTestMailSender
{
    public static class VariablesClass
    {
        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }
        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            { "ya.superproger@yandex.ru",CodePasswordDLL.CodePassword.getPassword("superproger") },
            { "wpf.geekbrains",CodePassword.getPassword("geekbrains") }
        };

        public static Dictionary<string, int> SmtpServers
        {
            get { return dicSmtpServers; }
        }
        private static Dictionary<string, int> dicSmtpServers = new Dictionary<string, int>()
        {
            { "smtp.yandex.ru", 25 },
            { "smtp.google.com", 25 }
        };


    }

}
