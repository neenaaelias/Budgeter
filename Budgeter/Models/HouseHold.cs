using Budgeter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class HouseHold
    {
        public HouseHold()
         {
            this.Budgets = new HashSet<Budget>();
            this.Users = new HashSet<ApplicationUser>();
            this.BankAccount = new HashSet<BankAccount>();
        }



        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        // if we need multiple budgets on house hold

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<BankAccount>BankAccount   { get; set; }


    }
}