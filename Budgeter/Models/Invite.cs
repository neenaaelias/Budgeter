using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Budgeter.Models
{
    public class Invite
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int HouseHoldId { get; set; }
        public string Secret { get; set; }
    }
}