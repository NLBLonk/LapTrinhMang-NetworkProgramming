using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
    public class List
    {
        private static List instance;
        private List<Account> listAccountUser;
        public List<Account> ListAccountUser { get => listAccountUser; set => listAccountUser = value; }

        public static List Instance
        {
            get
            {
                if (instance == null)
                    instance = new List();
                return instance;
            }
            set => instance = value;
        }

        public List()
        {
            listAccountUser = new List<Account>();
            listAccountUser.Add(new Account("Long", "123"));
            listAccountUser.Add(new Account("Quy", "123"));
            listAccountUser.Add(new Account("Duck", "123"));
            listAccountUser.Add(new Account("An", "123"));
        }
    }
}
