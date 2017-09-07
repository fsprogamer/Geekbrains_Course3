using CommonLibrary.Model;
using System.Data.Linq;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext db = new DataContext(Properties.Settings.Default.MailsAndSendersConnectionString);

            Table<Email> emails = db.GetTable<Email>();
        }
    }  
}
