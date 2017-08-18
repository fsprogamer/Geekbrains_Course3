using System.Collections.Generic;

namespace MailSender
{
        public static class VariablesClass
        {
            public static Dictionary<string, string> Senders
            {
                get { return dicSenders; }
            }
            private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
            {
                { "progamer@yandex.ru", PasswordClass.getPassword("kbqbo") },
                { "fsprogamer@gmail.com", PasswordClass.getPassword("{3t1l2m6") }
            };

            public static Dictionary<string, int> SmtpServers
            {
                get { return dicSmtpServers; }
            }
            private static Dictionary<string, int> dicSmtpServers = new Dictionary<string, int>()
            {
                { "smtp.yandex.ru", 25 },
                { "smtp.gmail.com", 587 }
            };
    }
}
