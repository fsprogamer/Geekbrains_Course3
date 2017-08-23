using System.Collections.Generic;
using Common.Model;
using System.Collections.ObjectModel;

namespace MailSender.DAL
{ 
    public interface IGenericRepo<T> where T : class, IEntity, new()
    {        
        //Get all items in the database method
        IEnumerable<T> GetItems();        
        //ObservableCollection<T> GetItems();
        //Get specific item in the database method with Id
        T GetItem(int id);
        void InsertItem(T item);
        void DeleteItem(int id);
        void UpdateItem(T item);
        void Save();
    }
}
