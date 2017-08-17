using System.Collections.Generic;
using CodePasswordDLL;

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
            { "79257443993@yandex.ru", PasswordClass.getPassword("{3t1l2m6") },
            { "sok74@yandex.ru", PasswordClass.getPassword("{3t1l2m6") }
        };

        }

}
