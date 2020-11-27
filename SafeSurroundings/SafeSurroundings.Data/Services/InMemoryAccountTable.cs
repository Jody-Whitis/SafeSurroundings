﻿using SafeSurroundings.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSurroundings.Data.Services
{
    public class InMemoryAccountTable :ILocalTables
    {
        List<Account> accounts;
        
        public InMemoryAccountTable()
        {
            accounts = new List<Account> { new Account { ID = 1, UserName = "jodywhitis0407@gmail.com", DisplayName="Jody", 
                Password = "test", IsActive = true, LastLogin = DateTime.Now, IsSubscribed = false,
                IsTwoFactor=false } };
        }

        public IEnumerable<Account> GetAll()
        {
            return accounts.OrderBy(a => a.ID);
        } 

        public Account GetLoginAccount(string user,string password)
        {
            return accounts.Where(a => a.UserName == user && a.Password == password).FirstOrDefault();
        }

        public Account SelectAccount(int id)
        {
            return accounts.Where(a => a.ID == id).FirstOrDefault();
        }
        
        public void AddAccount(Account newAccount)
        {
            newAccount.ID = accounts.Max(a => a.ID) + 1;
            accounts.Add(newAccount);
        }
        
        public void Update(int id)
        {
            
        }

        public void Delete(int id)
        {
            Account account = accounts.Where(a => a.ID == id).FirstOrDefault();
            accounts.Remove(account);
        }
    }
}
