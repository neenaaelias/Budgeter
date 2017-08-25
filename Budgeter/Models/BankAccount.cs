using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class BankAccounts
    {
        public BankAccounts()
        {
            this.Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int HouseHoldId { get; set; }
        public string Name { get; set; }
        public double Balance { get; }

        public virtual HouseHold HouseHold { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}