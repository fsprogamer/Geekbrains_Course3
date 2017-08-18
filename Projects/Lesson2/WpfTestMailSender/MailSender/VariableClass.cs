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
                { "sok74@yandex.ru", PasswordClass.getPassword("{3t1l2m6") }
            };
        }
}
