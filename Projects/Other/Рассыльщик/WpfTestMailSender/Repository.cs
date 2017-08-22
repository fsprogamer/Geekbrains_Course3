using OfficeOpenXml;
using OfficeOpenXml.Style;
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

        public SchedulerClass sc = new SchedulerClass();

        public SqlLocalDbContext Context;

        public ICollectionView MailingsPlanned;
        public ICollectionView MailingsDone;


        public Repository(bool GenerateTestData)
        {
            Context = new SqlLocalDbContext();
            Context.Mailings.Load();
            Context.SendersEmails.Load();
            Context.SmtpServers.Load();
            Context.Emails.Load();
            if (GenerateTestData)
            {
                GenerateTest();
            }
            DevideMailings();

            bool late = Context.Mailings.Any(m => m.IsSent == false && m.DateAndTime < DateTime.Now);
            if (late)
            {
                MessageBox.Show("Обнаружены рассылки, которые должны были быть отправлены ранее, не не были отправлены. Вы можете отправить их вручную на панели статистики");
            }
        }

        public void DevideMailings()
        {
            MailingsPlanned = new CollectionViewSource { Source = Context.Mailings.Local }.View;
            MailingsPlanned.Filter = new Predicate<object>(x => ((Mailing)x).DateAndTime >= DateTime.Now);
            MailingsDone = new CollectionViewSource { Source = Context.Mailings.Local }.View;
            MailingsDone.Filter = new Predicate<object>(x => ((Mailing)x).DateAndTime < DateTime.Now);
        }

        public void AddMail(Email em)
        {
            Context.Emails.Add(em);
            SaveChanges();
        }


        public void DeleteMail(Email mail)
        {
            Email email = Context.Emails.Where(b => b.Id == mail.Id).First();
            Context.Emails.Remove(email);
            SaveChanges();
        }

        public void AddSmtp(SmtpServer smtp)
        {
            Context.SmtpServers.Add(smtp);
            SaveChanges();
        }
        public void DeleteSmtp(SmtpServer Smtp)
        {
            SmtpServer smtp = Context.SmtpServers.Where(b => b.Id == Smtp.Id).First();
            Context.SmtpServers.Remove(smtp);
            SaveChanges();
        }

        public void AddSender(EmailSender sender)
        {
            Context.SendersEmails.Add(sender);
            SaveChanges();
        }
        public void DeleteSender(EmailSender sender)
        {
            EmailSender Sender = Context.SendersEmails.Where(b => b.Id == sender.Id).First();
            Context.SendersEmails.Remove(Sender);
            SaveChanges();
        }

        public void AddMailing(Mailing m)
        {
            Context.Mailings.Add(m);
            SaveChanges();
        }
        public void DeleteMailing(Mailing mail)
        {
            Mailing mailing = Context.Mailings.Where(b => b.Id == mail.Id).First();
            Context.Mailings.Remove(mailing);
            Context.SaveChanges();
        }

        public void DeleteRecipient(Mailing m, Email em)
        {
            Context.Mailings.Where(x => x.Id == m.Id).First().Recipients.Remove(em);
            Context.Emails.First(x => x.Id == em.Id).Mailing.Remove(m);
            Context.SaveChanges();
        }


        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public bool IfEmailIsNew(Email em)
        {

            if (Context.Emails.Any(b => b.EmailAdress == em.EmailAdress))
            {
                MessageBox.Show("Данный емэйл уже находится в базе");
                return false;
            }
            return true;
        }


        public EmailSender DefaultSender()
        {
            return Context.SendersEmails.First(x => x.IsDefault == true);
        }


        public void GenerateTest()
        {
            //Создаем smtp
            Context.SmtpServers.RemoveRange(Context.SmtpServers);
            Context.SmtpServers.Add(new SmtpServer() { Port = 25, Smtp = "smtp.yandex.ru" });
            Context.SmtpServers.Add(new SmtpServer() { Port = 25, Smtp = "smtp.google.com" });
            Context.SaveChanges();

            //Создаем отправителей
            Context.SendersEmails.RemoveRange(Context.SendersEmails);
            Context.SendersEmails.Add(new EmailSender() { Name = "Виталий", EmailAdress = "ya.superproger@yandex.ru", Password = CodePasswordDLL.CodePassword.getPassword("superproger"), IsDefault = true, SmtpServer = Context.SmtpServers.First() });
            Context.SendersEmails.Add(new EmailSender() { Name = "Артем", EmailAdress = "wpf.geekbrains@yandex.ru", Password = CodePasswordDLL.CodePassword.getPassword("geekbrains"), IsDefault = false, SmtpServer = Context.SmtpServers.First() });
            Context.SaveChanges();

            //Создаем Emails получателей
            Context.Emails.RemoveRange(Context.Emails);
            var c = GenerateTestMail(10);
            foreach (Email m in c)
            {
                Context.Emails.Add(m);
            }
            Context.Emails.Add(new Email() { Name = "Виталий", EmailAdress = "ya.superproger@yandex.ru" });
            Context.SaveChanges();

            //Создаем рассылки
            Context.Mailings.RemoveRange(Context.Mailings);

            var n = GenerateTestMailings();
            foreach (Mailing m in n)
            {
                Context.Mailings.Add(m);
            }
            Context.SaveChanges();
            MessageBox.Show("Создан тестовый набор данных. Чтобы изменения сохранялись при перезапуске программы при создании репзитория укажите в конструкторе false");

            MailingsPlanned = new CollectionViewSource { Source = Context.Mailings.Local }.View;
            MailingsPlanned.Filter = new Predicate<object>(x => ((Mailing)x).DateAndTime > DateTime.Now);
            MailingsDone = new CollectionViewSource { Source = Context.Mailings.Local }.View;
            MailingsDone.Filter = new Predicate<object>(x => ((Mailing)x).DateAndTime <= DateTime.Now);
        }

        public List<Mailing> GenerateTestMailings()
        {
            List<Mailing> list = new List<Mailing>();
            Random r = new Random();
            int f = 0;
            for (DateTime d = DateTime.Now.AddDays(-7); d < DateTime.Now.AddDays(7); d = d.AddDays(1))
            {
                f++;
                List<Email> listmail = new List<Email>();

                for (int i = 0; i < r.Next(3, 10); i++)
                {
                    string adress = "testEmail" + (i + 1) + "@gmail.com";
                    listmail.Add(Context.Emails.Where(b => b.EmailAdress == adress).First());
                }
                listmail.Add(Context.Emails.Where(b => b.EmailAdress == "ya.superproger@yandex.ru").First());
                bool sent = false;
                if (f < 7) sent = true;
                list.Add(new Mailing() { DateAndTime = d, Text = "Добрый день! Это тестовая рассылка от " + d.ToLongDateString() + ". С уважением, Ваш бот", Recipients = new ObservableCollection<Email>(listmail.ToList()), IsSent = sent });
            }
            return list.ToList();
        }
        public List<Email> GenerateTestMail(int numberOfMails)
        {
            List<Email> list = new List<Email>();
            for (int i = 0; i < numberOfMails; i++)
            {
                list.Add(new Email() { Name = "Получатель номер " + (i + 1), EmailAdress = "testEmail" + (i + 1) + "@gmail.com" });
            }
            return list;
        }

        public void GenerateExcelForEmails()
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {

                //Создаем лист   
                ExcelWorksheet Worksheet = excelPackage.Workbook.Worksheets.Add("Trakcs");
                //Загружаем БД на лист, начиная с ячейки А2     
                Worksheet.Cells["A2"].LoadFromCollection(Context.Emails.Local);
                Worksheet.Cells["A1"].Value = "Id";
                Worksheet.Cells["B1"].Value = "Adress";
                Worksheet.Cells["C1"].Value = "Name";
                Worksheet.Cells.AutoFitColumns();
                //Изменяем стиль всего диапозона ячеек (первый ряд)   
                using (ExcelRange range = Worksheet.Cells["A1:XFD1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.Fill.PatternType = ExcelFillStyle.LightGray;
                }
                excelPackage.SaveAs(new System.IO.FileInfo("test.xlsx"));

            }

        }
    }
}
