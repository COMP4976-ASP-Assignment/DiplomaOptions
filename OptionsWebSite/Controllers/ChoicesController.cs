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
            return View(choice);
        }

        // GET: Choices/Create
        public ActionResult Create()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            var sId = db.Choices.ToList(); //get all items in choices tabke
            ViewBag.StudentId = user.UserName;
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title");

            IList<YearTerm> yearTerms = db.YearTerms.ToList();
            var name = new Dictionary<int, string>();

            name[10] = "Winter";
            name[20] = "Spring/Summer";
            name[30] = "Fall";

            var term = from query in yearTerms
                       where query.IsDefault.Equals(true)
                       select query;

            
            //IEnumerable<SelectListItem> selectList = from year in yearTerms
            //                                         select new SelectListItem
            //                                         {
            //                                             Text = name[year.Term],
            //                                             Value = year.YearTermId.ToString()
            //                                         };
            ViewBag.YearTermIdValue = term.ToArray().ElementAt(0).YearTermId;
            ViewBag.YearTermDisplay = term.ToArray().ElementAt(0).Year + " " + name[term.ToArray().ElementAt(0).Term];


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
            var sId = db.Choices.ToList();
            foreach (var id in sId)
            {
                if (id.StudentId == choice.StudentId)
                {
                    ViewBag.Error = "Already Choosen options";
                    ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
                    ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
                    ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
                    ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
                    ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);

                    return View(choice);
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
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "YearTermId", choice.YearTermId);
      
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
