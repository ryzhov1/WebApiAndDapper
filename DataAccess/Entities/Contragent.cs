using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Contragent
    {
        public int Id { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string Name { get; set; }
        //public string FullName { get; set; }
        public List<Account> Accounts { get; set; }

        public Contragent()
        {
            Accounts = new List<Account>();
            KPP = "0";
        }

        public Boolean Equals(Contragent contragent)
        {
            if (contragent == null)
                return false;

            return ((this.INN.Equals(contragent.INN)) &&
                    (this.KPP.Equals(contragent.KPP)) &&
                    (this.Name.Equals(contragent.Name)));
        }

    } 
}
