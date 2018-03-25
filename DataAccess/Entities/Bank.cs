using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bic { get; set; }
        public string CorrespondingAccount { get; set; }
        public string City { get; set; }

        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new List<Account>();
        }

        public Boolean Equals(Bank bank)
        {
            if (bank == null)
                return false;

            return ((this.Name.Equals(bank.Name)) &&
                    (this.Bic.Equals(bank.Bic)) &&
                    (this.CorrespondingAccount.Equals(bank.CorrespondingAccount)) &&
                    (this.City.Equals(bank.City)));
        }
    }
}
