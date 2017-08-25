﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public string Payee { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Amount { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public int EnteredById { get; set; }
        public bool IsDeleted {get;set;}

        public virtual BankAccounts BankAccount { get; set; }
        public virtual Category Category { get; set; }
        public virtual Budgeter.Models.Type Type { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }

    }
}