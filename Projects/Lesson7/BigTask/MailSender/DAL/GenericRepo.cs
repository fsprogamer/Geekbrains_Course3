using System.Collections.Generic;
using System.Linq;
using System;
using Common.Model;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace MailSender.DAL
{     
    public abstract class GenericRepo<C,T> :  IGenericRepo<T> where T : class, IEntity, new()
                                                                           where C : DbContext
    {
        private C _entities;
        DbSet<T> dbSet;
        public C Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        static protected readonly object Locker = new object();

        public GenericRepo(C conn)
        {
            Context = conn;
            dbSet = Context.Set<T>();
        }
        //Get all items in the database method
        public IEnumerable<T> GetItems()
        {
            lock (Locker)
            {
                try
                {
                    return dbSet;// (from i in dbSet select i)/*.ToList()*/;
                }
                catch (Exception e)
                {                   
                    return null;
                }
            }
        }
        //Get specific item in the database method with Id
        public T GetItem(int id) 
        {
            lock (Locker)
            {
                try
                {
                    return dbSet.Find(id);
                }
                catch (Exception e)
                {    
                    return default(T);
                }
            }
        }
        public void InsertItem(T item) 
        {
            lock (Locker)
            {
                try
                {
                    dbSet.Add(item);
                }
                catch (Exception e)
                {                   
                }
            }
        }
        
        public void DeleteItem(int id) 
        {
            lock (Locker)
            {
                try
                {
                    T entityToDelete = dbSet.Find(id);
                    Delete(entityToDelete);
                }
                catch (Exception e)
                {                                        
                }
            }
        }

        public virtual void Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void UpdateItem(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;            
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
