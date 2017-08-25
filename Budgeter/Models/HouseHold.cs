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
            this.Users = new HashSet<ApplicationUser>();
            this.Budgets = new HashSet<Budget>();
            this.BankAccounts = new HashSet<BankAccounts>();
      }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        // if we need multiple budgets on house hold

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<BankAccounts> BankAccounts { get; set; }

    }
}