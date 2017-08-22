using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WpfTestMailSender.Classes
{

    public class Mailing
    {
        public Mailing()
        {
            Recipients = new ObservableCollection<Email>();
        }

        [Key]
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public virtual ICollection<Email> Recipients { get; set; }
        public string Text { get; set; }

        public Nullable<bool> IsSent { get; set; }

    }
}
