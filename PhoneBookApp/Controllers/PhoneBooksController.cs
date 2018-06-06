using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBookApp.Models;

namespace PhoneBookApp.Controllers
{
    public class PhoneBooksController : Controller
    {
        private PhoneBookDBContext db;
        public PhoneBooksController()
        {
            db = new PhoneBookDBContext();

        }

        public PhoneBooksController(PhoneBookDBContext dbcontext)
        {
            db = dbcontext;
        }

        // GET: PhoneBooks
        public ActionResult Index()
        {
            //var contacts = db.PhoneBook.Include(i => i.contacts.)
            return View(db.PhoneBook.ToList());
        }

        // GET: PhoneBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneBook phoneBook = db.PhoneBook.Find(id);
            
   

            if (phoneBook == null)
            {
                return HttpNotFound();
            }
            return View(phoneBook);
        }

        // GET: PhoneBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CreatedAt,ModifiedAt")] PhoneBook phoneBook)
        {
            //phoneBook.CreatedAt = DateTime.Now;
            //phoneBook.ModifiedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.PhoneBook.Add(phoneBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phoneBook);
        }

        // GET: PhoneBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneBook phoneBook = db.PhoneBook.Find(id);
            if (phoneBook == null)
            {
                return HttpNotFound();
            }
            return View(phoneBook);
        }

        // POST: PhoneBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CreatedAt,ModifiedAt")] PhoneBook phoneBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phoneBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phoneBook);
        }

        // GET: PhoneBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneBook phoneBook = db.PhoneBook.Find(id);
            if (phoneBook == null)
            {
                return HttpNotFound();
            }
            return View(phoneBook);
        }

        // POST: PhoneBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhoneBook phoneBook = db.PhoneBook.Find(id);
            db.PhoneBook.Remove(phoneBook);
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
