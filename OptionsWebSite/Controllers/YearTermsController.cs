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
    public class YearTermsController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        // GET: YearTerms
        public ActionResult Index()
        {
            return View(db.YearTerms.ToList());
        }

        // GET: YearTerms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }

            var e = db.YearTerms.Find(id);
       
            ViewBag.q = "";

            if (e.Term == 10) ViewBag.q += "Winter";
            else if (e.Term == 20) ViewBag.q += "Spring/Summer";
            else if (e.Term == 30) ViewBag.q += "Fall";
            else ViewBag.q += ":(";
            
            return View(yearTerm);
        }

        // GET: YearTerms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YearTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YearTermId,Year,Term,IsDefault")] YearTerm yearTerm)
        {
            if (ModelState.IsValid)
            {
                db.YearTerms.Add(yearTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yearTerm);
        }

        // GET: YearTerms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }

            IList<YearTerm> yearTerms = db.YearTerms.ToList();
            var name = new Dictionary<int, string>();

            name[10] = "Winter";
            name[20] = "Spring/Summer";
            name[30] = "Fall";
            IEnumerable<SelectListItem> selectList = from year in yearTerms
                                                     select new SelectListItem
                                                     {
                                                         Text = name[year.Term],
                                                         Value = year.Term.ToString()
                                                     };
            ViewBag.Term = selectList;
            return View(yearTerm);
        }

        // POST: YearTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YearTermId,Year,Term,IsDefault")] YearTerm yearTerm)
        {
            if (yearTerm.Term != 10 && yearTerm.Term != 20 && yearTerm.Term != 30)
            {
                ViewBag.Error = "Invalid value for term";
                return View(yearTerm);
            }
            var q = from y in db.YearTerms
                        where y.IsDefault == true
                        select y;
                var s = q.ToArray();
                if (yearTerm.IsDefault == false && s.Length <= 1 && yearTerm.YearTermId == s[0].YearTermId)
                {
                    ModelState.AddModelError("IsDefault", "There is no current default");
                        
                     }
                if (yearTerm.IsDefault)
                {
                
                    foreach (var year in db.YearTerms.Where(a=>a.IsDefault))
                    {
                        if (year.YearTermId != yearTerm.YearTermId)
                            year.IsDefault = false;
                    }
                }

            if (ModelState.IsValid)
                {
                
                    YearTerm yt = db.YearTerms.Find(yearTerm.YearTermId);
                    yt.Year = yearTerm.Year;
                    yt.Term = yearTerm.Term;
                    yt.IsDefault = yearTerm.IsDefault;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(yearTerm);
            }

        // GET: YearTerms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YearTerm yearTerm = db.YearTerms.Find(id);
            if (yearTerm == null)
            {
                return HttpNotFound();
            }

            var e = db.YearTerms.Find(id);

            ViewBag.q = "";

            if (e.Term == 10) ViewBag.q += "Winter";
            else if (e.Term == 20) ViewBag.q += "Spring/Summer";
            else if (e.Term == 30) ViewBag.q += "Fall";
            else ViewBag.q += ":(";

            return View(yearTerm);
        }

        // POST: YearTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var choices = db.Choices.Where(a => a.YearTermId == id).Select(a => a.ChoiceId);
            YearTerm yearTerm = db.YearTerms.Find(id);

            if ( choices.Count() != 0 )
            {
                ViewBag.error = "A choice with this year already exists";
            }
            else
            {
                db.YearTerms.Remove(yearTerm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(yearTerm);

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
