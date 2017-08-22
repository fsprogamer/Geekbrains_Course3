using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WpfTestMailSender
{

    public class Email
    {
        [Key]
        public int Id { get; set; }
        public string EmailAdress { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Mailing> Mailing { get; set; }

        public Email()
        {
            Mailing = new ObservableCollection<Mailing>();
        }

       
    }

}
