using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class BankAccount
    {
        public BankAccount()
        {
            this.Transactions = new HashSet<Transaction>();
        }
        public int Id { get; set; }
        public int HouseHoldId { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString ="{0:C}", ApplyFormatInEditMode = false)]
        public double Balance { get; set; }

        public virtual HouseHold household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}