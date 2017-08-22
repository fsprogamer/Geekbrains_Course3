using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender.Classes
{
    public class EmailSender
    {
        [Key]
        public int Id { get; set; }
        public string EmailAdress { get; set; }
        public string Name { get; set; }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string password;

        public bool IsDefault { get; set; }
        private SmtpServer smtpServer;
        public SmtpServer SmtpServer
        {
            get { return smtpServer; }
            set { smtpServer = value; }
        }
    }
}
