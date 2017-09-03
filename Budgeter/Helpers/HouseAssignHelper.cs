using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budgeter.Models.Helpers
{
    public class HouseAssignHelper
    {
        private ApplicationDbContext db;

        public HouseAssignHelper(ApplicationDbContext context)
        {

            this.db = context;
        }

        public bool IsHouseOnUser(int HouseId, string userId)
        {
            var house = db.HouseHolds.Find(HouseId);
            var userCheck = house.Users.Any(p => p.Id == userId);
            return (userCheck);
        }

        public ICollection<ApplicationUser> ListAssignedUsers(int houseId)
        {
            HouseHold household = db.HouseHolds.Find(houseId);
            var userList = household.Users.ToList();
            return (userList);
        }

        public bool AddHouseToUser(int houseId, string userId)
        {
            HouseHold household= db.HouseHolds.Find(houseId);
            ApplicationUser user = db.Users.Find(userId);

            household.Users.Add(user);

            try
            {
                var userAdded = db.SaveChanges();

                if (userAdded != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }


        }

        public bool RemoveHouseFromUser(int houseId, string userId)
        {
            HouseHold house= db.HouseHolds.Find(houseId);
            ApplicationUser user = db.Users.Find(userId);

            var result = house.Users.Remove(user);

            try
            {
                var userRemoved = db.SaveChanges();

                if (userRemoved != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }


        }


    }
}
