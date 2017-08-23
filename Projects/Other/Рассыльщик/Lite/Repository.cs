
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using WpfTestMailSender.Classes;

namespace WpfTestMailSender
{

    public class Repository
    {

       

        public SqlLocalDbContext Context;

        //public ICollectionView MailingsPlanned;
        //public ICollectionView MailingsDone;


        public Repository(bool GenerateTestData)
        {
            Context = new SqlLocalDbContext();
            //Context.Mailings.Load();
            //Context.SendersEmails.Load();
            //Context.SmtpServers.Load();
            Context.Emails.Load();
            //if (GenerateTestData)
            //{
               
            //}
            

            //bool late = Context.Mailings.Any(m => m.IsSent == false && m.DateAndTime < DateTime.Now);
            //if (late)
            //{
            //    MessageBox.Show("Обнаружены рассылки, которые должны были быть отправлены ранее, не не были отправлены. Вы можете отправить их вручную на панели статистики");
            //}
        }

    }
}
