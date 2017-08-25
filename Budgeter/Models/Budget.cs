﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Budget
    {
        public Budget()
        {
            this.Items = new HashSet<Item>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int HouseHoldId { get; set; }
        public double Amount { get; set; }


        public virtual HouseHold Household { get; set; }
        public virtual ICollection <Item> Items{get ;set;}
    }
}