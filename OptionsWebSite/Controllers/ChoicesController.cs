using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DiplomaDataModel;
using System.Collections;

namespace OptionsWebSite.Controllers
{
    public class ChoicesController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        [Authorize(Roles = "Admin")]
        // GET: Choices
        public ActionResult Index()
        {
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerm);
            return View(choices.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }

            var a = db.Options.Find(choice.FirstChoiceOptionId);
            var b = db.Options.Find(choice.SecondChoiceOptionId);
            var c = db.Options.Find(choice.ThirdChoiceOptionId);
            var d = db.Options.Find(choice.FourthChoiceOptionId);
            var e = db.YearTerms.Find(choice.YearTermId);

            ViewBag.f = a.Title;
            ViewBag.s = b.Title;
            ViewBag.t = c.Title;
            ViewBag.z = d.Title;
            ViewBag.q = e.Year + " ";

            if (e.Term == 10) ViewBag.q += "Winter";
            else if (e.Term == 20) ViewBag.q += "Spring/Summer";
            else if (e.Term == 30) ViewBag.q += "Fall";
            else ViewBag.q += ":(";

            return View(choice);
        }

        // GET: Choices/Create
        public ActionResult Create()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var sId = db.Choices.ToList(); //get all items in choices tabke
            ViewBag.StudentId = user.UserName;
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(a => a.IsActive), "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(a => a.IsActive), "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(a => a.IsActive), "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(a => a.IsActive), "OptionId", "Title");

            IList<YearTerm> yearTerms = db.YearTerms.ToList();
            var name = new Dictionary<int, string>();

            name[10] = "Winter";
            name[20] = "Spring/Summer";
            name[30] = "Fall";

            var term = from query in yearTerms
                       where query.IsDefault.Equals(true)
                       select query;

            if (term != null)
            {
                ViewBag.YearTermIdValue = term.ToArray().ElementAt(0).YearTermId;
                if (term.ToArray().ElementAt(0).Term == 10 || term.ToArray().ElementAt(0).Term == 20 || term.ToArray().ElementAt(0).Term == 30)
                {
                    ViewBag.YearTermDisplay = term.ToArray().ElementAt(0).Year + " " + name[term.ToArray().ElementAt(0).Term];
                }
                else ViewBag.YearTermDisplay = term.ToArray().ElementAt(0).Year + " New";
            }
            else
            {
                ViewBag.YearTermIdValue = 0;
                ViewBag.YearTermDisplay = "N/A";
            }
            

            foreach (var id in sId)
            {
                if (id.StudentId == user.UserName)
                {
                    ViewBag.Error = "Already Choosen options";
                    return View();
                }
            }

                  
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,StudentId,YearTermId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId")] Choice choice)
        {
            int[] userChoices = {(int) choice.FirstChoiceOptionId, (int) choice.SecondChoiceOptionId, (int)choice.ThirdChoiceOptionId, (int)choice.FourthChoiceOptionId };
            IList<YearTerm> yearTerms = db.YearTerms.ToList();
            var term = from query in yearTerms
                       where query.IsDefault.Equals(true)
                       select query;
            var name = new Dictionary<int, string>();

            name[10] = "Winter";
            name[20] = "Spring/Summer";
            name[30] = "Fall";
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


            //if (userChoices.Distinct().Count() != userChoices.Count())
            //{
            //    ViewBag.YearTermIdValue = term.ToArray().ElementAt(0).YearTermId;
            //    ViewBag.YearTermDisplay = term.ToArray().ElementAt(0).Year + " " + name[term.ToArray().ElementAt(0).Term];
            //    ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            //    ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            //    ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            //    ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            //    ViewBag.ErrorChoices = "Choices cannot be the same";
            //    ViewBag.StudentId = user.UserName;


            //    return View(choice);
            //}
               
            var sId = db.Choices.ToList();
            foreach (var id in sId)
            {
                if (id.StudentId == choice.StudentId)
                {
                    ViewBag.Error = "Already Choosen options";
                    return View();
                }
            }

            if (ModelState.IsValid)
            {
                db.Choices.Add(choice);
                db.SaveChanges();
                return RedirectToAction("../Home/Index");
            }

            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermIdValue = term.ToArray().ElementAt(0).YearTermId;
            ViewBag.YearTermDisplay = term.ToArray().ElementAt(0).Year + " " + name[term.ToArray().ElementAt(0).Term];
            ViewBag.StudentId = user.UserName;


            return View(choice);
        }

        [Authorize(Roles = "Admin")]
        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }

            IList<YearTerm> yearTerms = db.YearTerms.ToList();
            var term = from query in yearTerms
                       where query.IsDefault.Equals(true)
                       select query;
            var name = new Dictionary<int, string>();
            name[10] = "Winter";
            name[20] = "Spring/Summer";
            name[30] = "Fall";

            ViewBag.YearTermIdValue = term.ToArray().ElementAt(0).YearTermId;
            ViewBag.YearTermDisplay = term.ToArray().ElementAt(0).Year + " " + name[term.ToArray().ElementAt(0).Term];
            ViewBag.StudentId = db.Choices.Find(id).StudentId;
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        [Authorize(Roles = "Admin")]
        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceId,StudentId,YearTermId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            IList<YearTerm> yearTerms = db.YearTerms.ToList();
            var term = from query in yearTerms
                       where query.IsDefault.Equals(true)
                       select query;
            var name = new Dictionary<int, string>();
            name[10] = "Winter";
            name[20] = "Spring/Summer";
            name[30] = "Fall";

            ViewBag.YearTermIdValue = term.ToArray().ElementAt(0).YearTermId;
            ViewBag.YearTermDisplay = term.ToArray().ElementAt(0).Year + " " + name[term.ToArray().ElementAt(0).Term];
            ViewBag.StudentId = choice.StudentId;
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
            return View(choice);
        }

        [Authorize(Roles = "Admin")]
        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        [Authorize(Roles = "Admin")]
        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
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
