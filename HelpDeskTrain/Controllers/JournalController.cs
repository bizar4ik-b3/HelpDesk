using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpDeskTrain.Models;

namespace HelpDeskTrain.Controllers
{
    [Authorize(Roles = "SuperAdmin, Администратор, Модератор, Исполнитель, Пользователь")]
    public class JournalController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();

        //
        // GET: /Journal/

        public ActionResult Index()
        {

            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            var journals = db.Journals.Include(j => j.Group).Where(s=>s.UserId==user.Id);

            List<Discipline> disc = db.Disciplines.ToList();
            ViewBag.Disciplines = new SelectList(disc, "Id", "Name");

            return View(journals.ToList());
        }

        //
        // GET: /Journal/Details/5

        public ActionResult Details(int id = 0)
        {
            //var les = db.Journals.Include(j => j.Evals).ToList();

            Journal journal = db.Journals.Find(id);

           // var evals = db.Evals.Where(e => e.JournalId == id).ToList();

            if (journal == null)
            {
                return HttpNotFound();
            }

            return View(journal);
        }

        //
        // GET: /Journal/Create

        public ActionResult Create()
        {
            ViewBag.Groups = new SelectList(db.NadpsuOrgStates, "Id", "GroupNumber");
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            return View();
        }

        //
        // POST: /Journal/Create

        [HttpPost]
        public ActionResult Create(Journal journal)
        {
            Eval e = new Eval();
           
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();

            var lessons = db.LessonLists.Where(l => l.DisciplineId == journal.DisciplineId).Where(l => l.UserId == user.Id).ToList();

            var kadets = db.Kadets.Where(l => l.GroupId == journal.GroupId).ToList();

            var journals= db.Journals.Where(l => l.UserId == user.Id).ToList();

            //var jkadets = db.Kadets.Where(l => l.GroupId == journal.GroupId).GroupBy(l => l.Id, (key, group) => group.FirstOrDefault());

            if (ModelState.IsValid)
            {
               // var evals = db.Evals.ToList();
                journal.UserId = user.Id;
                db.Journals.Add(journal);
                db.SaveChanges();

                foreach (LessonList l in lessons)
                {
                    foreach (Kadet k in kadets)
                    {
                        e.JournalId = journal.Id;
                        e.KadetId = k.Id;
                        e.LessonListId = l.Id;
                        db.Evals.Add(e);
                        db.SaveChanges();
                    }
                }
                ViewBag.GroupId = new SelectList(db.NadpsuOrgStates, "Id", "Group");
                ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.NadpsuOrgStates, "Id", "Group");
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            return View(journal);
        }

        //
        // GET: /Journal/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }

            ViewBag.Groups = new SelectList(db.NadpsuOrgStates, "Id", "GroupNumber");
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            return View(journal);
        }

        //
        // POST: /Journal/Edit/5

        [HttpPost]
        public ActionResult Edit(Journal journal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.NadpsuOrgStates, "Id", "DepartmentName", journal.GroupId);
            return View(journal);
        }

        //
        // GET: /Journal/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        //
        // POST: /Journal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var evals = db.Evals.Where(x => x.JournalId == id);

            foreach (var e in evals)
            {
                db.Evals.Remove(e);
            }
            Journal journal = db.Journals.Find(id);

            db.Journals.Remove(journal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}