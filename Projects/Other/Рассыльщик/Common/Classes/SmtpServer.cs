using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace WpfTestMailSender.Classes
{
   public class SmtpServer
    {
        [Key]
        public int Id { get; set; }
        public int Port { get; set; }
        public string Smtp { get; set; }

        public virtual ICollection<EmailSender> Senders { get; set; }

    }
}