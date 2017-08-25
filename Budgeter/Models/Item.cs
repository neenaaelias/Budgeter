﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int BudgetId { get; set; }
        public double Amount { get; set; }
        public bool IsDeleted { get; set; }


        public virtual Budget Budget  { get; set; }
        public virtual  Category Category{ get; set; }


    }
}