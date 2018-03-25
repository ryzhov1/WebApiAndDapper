using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int? ContragentId { get; set; }
        public Contragent Contragent { get; set; }
        public int? BankId { get; set; }
        public Bank Bank { get; set; }
        public Account()
        {
            Bank = new Bank();
            Contragent = new Contragent();
        }
    }
}
