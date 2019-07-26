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
    public class EvalController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();

        //
        // GET: /Eval/

        public ActionResult Index()
        {
            //return View(db.Evals.ToList());
            return HttpNotFound();
        }

        //
        // GET: /Eval/Details/5

        public ActionResult Details(int id = 0)
        {
            
           // var eval = db.Evals.Where(s => s.JournalId == id).GroupBy(s => s.KadetId);
           // var ev = db.Evals.Where(s => s.JournalId == id);
            Journal journal=db.Journals.Where(j=>j.Id==id).FirstOrDefault();
            if (journal != null)
            {

                Session["disciplineId"] = journal.DisciplineId;
                var group = db.NadpsuOrgStates.Where(s => s.Id == journal.GroupId).Select(s => s.GroupNumber);

                // SelectList kadets = new SelectList(db.Kadets, "Id", "Pib");
                ViewBag.Journal = db.NadpsuOrgStates.Where(s => s.Id == journal.GroupId).Select(s => s.GroupNumber).FirstOrDefault();
                // var eval = db.Evals.Where(s => s.JournalId == id).ToList();

                User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
                var module = db.LessonLists.Where(l => l.UserId == user.Id).Where(l => l.Required == true).Select(l => l.ThemeNumber).ToList();
                ViewBag.Module = module;
                ViewBag.JournalId = journal.Id;
                //Обновление записей в журнале
                var kadets = db.Kadets.Where(l => l.GroupId == journal.GroupId).ToList();

                var LesIdInJournal = db.Evals.GroupBy(l => l.LessonListId).ToList();//10 records(id=5)

                var Lessons = db.LessonLists.Include(s => s.LessonType).Where(l => l.DisciplineId == journal.DisciplineId).Where(l => l.UserId == user.Id).ToList();//15 trcords(id=10)objects


                foreach (LessonList less in Lessons)
                {
                    bool inJournal = false;
                    foreach (var lj in LesIdInJournal)
                    {
                        if (less.Id == lj.Key)
                        {
                            inJournal = true;
                            break;
                        }
                        else { inJournal = false; }
                    }

                    if (inJournal == false)
                    {
                        Eval e = new Eval();
                        foreach (Kadet k in kadets)
                        {
                            e.JournalId = journal.Id;
                            e.KadetId = k.Id;
                            e.LessonListId = less.Id;
                            db.Evals.Add(e);
                            db.SaveChanges();
                        }
                    }
                }
                //////////////////

                //var evals = db.Evals.Where(e => e.JournalId == id).ToList();
                //foreach (var ev in eval.Where(e=>e.JournalId==id)
                //{ 

                //}
                var eval = db.Evals.Where(s => s.JournalId == id).Include(s => s.Kadets).Include(s => s.LessonLists).OrderBy(s => s.KadetId).ToArray();
                if (eval == null)
                {
                    return HttpNotFound();
                }
                return View(eval);
            }
            else  return HttpNotFound();
            
        }

        [HttpPost]
        public ActionResult Details(bool allowEdit, bool showModule, int journalId)
        {
            ViewBag.JournalId = journalId;
            Session["moduleMath"] = showModule;
            Session["allowedEdit"] = allowEdit;
            this.Details(journalId);
            return View();
        }
        //
        // GET: /Eval/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Eval/Create

        [HttpPost]
        public ActionResult Create(Eval eval)
        {
            if (ModelState.IsValid)
            {
                db.Evals.Add(eval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eval);
        }

        //
        // GET: /Eval/Edit/5

        public ActionResult Edit(int id = 0,int j = 0)
        {

            var eval = db.Evals.Include(s=>s.LessonLists.LessonType).Where(x => x.KadetId == id).Where(x=>x.JournalId==j).OrderBy(x=>x.LessonLists.ThemeNumber).ThenBy(x=>x.LessonLists.LessonNumber).ToList();

            //Eval eval = db.Evals.Find(id);
            if (eval == null)
            {
                return HttpNotFound();
            }
            return View(eval);
        }

        //
        // POST: /Eval/Edit/5

        [HttpPost]
        public ActionResult Edit(List<Eval> eval)
        {
            int idj = 0;
            if (ModelState.IsValid)
            {
                foreach (Eval e in eval)
                {
                    var EvalRows = db.Evals.Where(a => a.Id.Equals(e.Id)).FirstOrDefault();
                    idj=EvalRows.JournalId;
                    if (EvalRows != null)
                    {
                        EvalRows.Value = e.Value;
                    }
                }
               // db.Entry(eval).State = EntityState.Modified;
                db.SaveChanges();
               // return RedirectToAction("Index");
                return RedirectToAction("Details", "Eval", new { id = idj });
            }
            return View(eval);
            //return RedirectToAction("Details", "Eval", new { a = 49 });
        }
        [HttpPost]
        public ActionResult Save(FormCollection eval)
        {
            var JouralId = eval.GetValue("journalId");
            int i = 0;
            if (ModelState.IsValid)
            {
                var EvalIdArray = eval.GetValues("item.Id");
                var EvalValueArray = eval.GetValues("item.Value");
               
                for (i = 0; i < EvalIdArray.Count(); i++)
                {
                    Eval ev = db.Evals.Find(Convert.ToInt32(EvalIdArray[i]));
                    ev.Value = EvalValueArray[i];
                    
                    db.Entry(ev).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Details", "Eval", new { id = JouralId.AttemptedValue });
            }
            return View(eval);
        }

        //
        // GET: /Eval/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Eval eval = db.Evals.Find(id);
            if (eval == null)
            {
                return HttpNotFound();
            }
            return View(eval);
        }

        //
        // POST: /Eval/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Eval eval = db.Evals.Find(id);
            db.Evals.Remove(eval);
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