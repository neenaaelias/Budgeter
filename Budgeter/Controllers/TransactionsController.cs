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

namespace Budgeter.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        double tempAmount = 0;

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.Category).Include(t => t.Type);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create(int? id)
        {
            ViewBag.BankAccountId = id;
            //ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankAccountId,Payee,Description,Date,Balance,TypeId,CategoryId,EnteredById,IsDeleted")] Transaction transaction,int BankAccountId)
        {
            if (ModelState.IsValid)
            {
                transaction.BankAccountId = BankAccountId;
                var bankaccount = db.BankAccounts.Find(BankAccountId);
                if (bankaccount != null)
                {
                    transaction.BankAccount = bankaccount;
                    transaction.EnteredById = User.Identity.GetUserId();
                    if (transaction.TypeId == 1)
                    {
                        transaction.BankAccount.Balance += transaction.Balance;
                    }
                    else if (transaction.TypeId == 2)
                    {
                        transaction.BankAccount.Balance -= transaction.Balance;
                    }
                        db.Transactions.Add(transaction);
                        db.SaveChanges();
                        return RedirectToAction("Index","Home");
                    }

            }

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", transaction.TypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            transaction.BankAccount = db.BankAccounts.Find(transaction.BankAccountId);
            ViewBag.TempBalance = transaction.Balance;
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", transaction.TypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankAccountId,Payee,Description,Date,Balance,TypeId,CategoryId,EnteredById,IsDeleted")] Transaction transaction,double tempAmount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                transaction.BankAccount = db.BankAccounts.Find(transaction.BankAccountId);

                if (tempAmount != transaction.Balance)
                {
                    if (transaction.TypeId == 1)
                    {
                        transaction.BankAccount.Balance = transaction.BankAccount.Balance - tempAmount;//reverse the amount
                        transaction.BankAccount.Balance = transaction.BankAccount.Balance + transaction.Balance;//addd the edit
                    }
                    else//expense
                    {
                        if (transaction.BankAccount.Balance >= transaction.Balance)//make sure edit amount doesnt go over account balance
                        {
                            transaction.BankAccount.Balance = transaction.BankAccount.Balance + tempAmount;
                            transaction.BankAccount.Balance = transaction.BankAccount.Balance - transaction.Balance;

                        }
                        else//return user to same page if they go over balance
                        {
                            return RedirectToAction("Edit", "Transactions", new { id = transaction.Id });

                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Details","HouseHolds",new { id = transaction.BankAccount.HouseHoldId });
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name", transaction.BankAccountId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "Name", transaction.TypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
