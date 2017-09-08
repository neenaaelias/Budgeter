using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budgeter.Models;
using Microsoft.AspNet.Identity;
using Budgeter.Models.Helpers;

namespace Budgeter.Controllers
{
    public class HouseHoldsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HouseHolds
        public ActionResult Index()
        {
            return View(db.HouseHolds.ToList());
        }

        // GET: HouseHolds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // GET: HouseHolds/Create
        public ActionResult Create(int? id)
        {
            var household = new HouseHold();

            string userId = User.Identity.GetUserId();
            household.CreatorId = userId;

            ViewBag.Id = id;
            ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: HouseHolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsDeleted,CreatorId")] HouseHold household)
        {
            if (ModelState.IsValid)
            {
                household.CreatorId = User.Identity.GetUserId();
                db.HouseHolds.Add(household);
                // -------Added to work on budgets
                Budget budget = new Budget
                {
                    HouseHoldId = household.Id,
                    Household = db.HouseHolds.Find(household.Id),
                    Name = household.Name + "Budget",
                    Amount = 0

                };
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            //------- Added to work on budgets
            // ViewBag.CreatorId = new SelectList(db.Users, "Id", "FirstName", household.CreatorId);
            return View(household);
        }
        // GET: HouseHolds/Leave household/5
        public ActionResult leavehousehold(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        //POST: HouseHolds/Leave household/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult leavehousehold(int id)
        {
            HouseAssignHelper helper = new HouseAssignHelper(db);
            string userId = User.Identity.GetUserId();
            //helper.RemoveHouseFromUser(household.Id, user);

            helper.RemoveHouseFromUser(id, userId);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        // POST: HouseHolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseHold houseHold = db.HouseHolds.Find(id);
            db.HouseHolds.Remove(houseHold);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: HouseHolds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }

        // POST: HouseHolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsDeleted")] HouseHold houseHold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(houseHold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(houseHold);
        }

        // GET: HouseHolds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseHold houseHold = db.HouseHolds.Find(id);
            if (houseHold == null)
            {
                return HttpNotFound();
            }
            return View(houseHold);
        }
        //GET: Assign User to Houses
        //[Authorize(Roles = "Admin, HouseholdAdmin")]
        public ActionResult HouseAssign(int id)
        {
            var household = db.HouseHolds.Find(id);
            HouseAssignHelper helper = new HouseAssignHelper(db);
            var model = new HouseAssignUserViewModel();

            model.HouseHold = household;

            model.SelectedUsers = helper.ListAssignedUsers(id).Select(u => u.Id).ToArray();
            model.Users = new MultiSelectList(db.Users.Where(u => (u.FirstName != "N/A" && u.FirstName != "(Remove Assigned User)")).OrderBy(u => u.FirstName), "Id", "FirstName", model.SelectedUsers);


            return View(model);
        }

        //POST: Assign Users to House
        [HttpPost]
        //[Authorize(Roles = "HouseholdAdmin, Admin")]
        public ActionResult HouseAssign(HouseAssignUserViewModel model)
        {
            var household = db.HouseHolds.Find(model.HouseHold.Id);
            HouseAssignHelper helper = new HouseAssignHelper(db);

            foreach (var user in db.Users.Select(r => r.Id).ToList())
            {
                helper.RemoveHouseFromUser(household.Id, user);
            }
            if (model.SelectedUsers != null)
            {
                foreach (var user in model.SelectedUsers)
                {
                    helper.AddHouseToUser(household.Id, user);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        //// POST: HouseHolds/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirm(int id)
        //{
        //    HouseHold houseHold = db.HouseHolds.Find(id);
        //    db.HouseHolds.Remove(houseHold);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
