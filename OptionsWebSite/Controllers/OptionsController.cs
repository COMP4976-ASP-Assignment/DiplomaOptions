using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.Models;

namespace OptionsWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OptionsController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        // GET: Options
        public ActionResult Index()
        {
            return View(db.Options.ToList());
        }

        // GET: Options/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // GET: Options/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OptionId,Title,IsActive")] Option option)
        {
            if (ModelState.IsValid)
            {
                db.Options.Add(option);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(option);
        }

        // GET: Options/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OptionId,Title,IsActive")] Option option)
        {
            if (ModelState.IsValid)
            {
                db.Entry(option).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(option);
        }

        // GET: Options/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Option option = db.Options.Find(id);
           

            var choice1 = db.Choices.Where(a => a.FirstChoiceOptionId == id).Select(a => a.ChoiceId);
            var choice2 = db.Choices.Where(a => a.SecondChoiceOptionId == id).Select(a => a.ChoiceId);
            var choice3 = db.Choices.Where(a => a.ThirdChoiceOptionId == id).Select(a => a.ChoiceId);
            var choice4 = db.Choices.Where(a => a.FourthChoiceOptionId == id).Select(a => a.ChoiceId);


            if (choice1.Count() != 0)
            {
                ViewBag.error = "A choice uses this as its first option";
            }
            else if (choice2.Count() != 0)
            {
                ViewBag.error = "A choice uses this as its second option";
            }
            else if (choice3.Count() != 0)
            {
                ViewBag.error = "A choice uses this as its third option";
            }
            else if (choice4.Count() != 0)
            {
                ViewBag.error = "A choice uses this as its fourth option";
            }
            else
            {
                db.Options.Remove(option);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(option);
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
