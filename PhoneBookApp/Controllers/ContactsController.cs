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
    public class ContactsController : Controller
    {
        
        private PhoneBookDBContext db;
        public ContactsController()
        {
            db = new PhoneBookDBContext();

        }

        public ContactsController(PhoneBookDBContext dbcontext)
        {
            db = dbcontext;
        }


        // GET: Contacts
        public ActionResult Index(string searchString)
        {

            var contacts = db.Contacts.Include(c => c.PhoneBook);

            if (!string.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).OrderBy(x=>x.Name);

            }
            return View(contacts.OrderBy(x=>x.Name).ToList());
        }

        //// GET: Contacts/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contact contact = db.Contacts.Find(id);
        //    if (contact == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contact);
        //}

        // GET: Contacts/Create
        public ActionResult Create()
        {            
            ViewBag.PhoneBookID = getPhoneBookOption();
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,PhoneNumber,PhoneBookID")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PhoneBookID = getPhoneBookOption();//new SelectList(db.PhoneBook, "ID", "Name", contact.PhoneBookID);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhoneBookID = getPhoneBookOption();
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,PhoneNumber,PhoneBookID")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PhoneBookID = getPhoneBookOption();
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private List<SelectListItem> getPhoneBookOption()
        {
            var phoneBookTip = new SelectListItem() { Text = "------Select PhoneBook-----", Value = null };
            var PhonebookSelectOptions = new SelectList(db.PhoneBook, "ID", "Name").ToList();
            PhonebookSelectOptions.Insert(0, phoneBookTip);
            return PhonebookSelectOptions.ToList();
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
