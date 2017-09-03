using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Budgeter.Models.Helpers
{
    public class HouseAssignUserViewModel
    {
        public HouseHold HouseHold { get; set; }
        public MultiSelectList Users { get; set; }
        public string[] SelectedUsers { get; set; }

    }
}