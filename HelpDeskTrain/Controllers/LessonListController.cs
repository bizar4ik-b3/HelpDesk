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
    public class LessonListController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();

        //
        // GET: /LessonList/

        public ActionResult Index()
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            var lessons = db.LessonLists.Where(l => l.UserId == user.Id).OrderByDescending(l => l.ThemeNumber).ThenBy(l => l.LessonNumber);

            if (Session["disciplineId"] != null)
            {
                try
                {
                    int id = (int)Session["disciplineId"];
                    lessons = db.LessonLists.Where(l => l.DisciplineId == id).Where(l => l.UserId == user.Id).OrderBy(l => l.ThemeNumber).ThenBy(l => l.LessonNumber);
                }
                catch (Exception e)
                {
                   
                }
            }

            List<LessonType> types = db.LessonTypes.ToList();
            ViewBag.LessonTypes = new SelectList(types, "Id", "Name");

            List<Discipline> disc = db.Disciplines.ToList();
            ViewBag.Disciplines = new SelectList(disc, "Id", "Name");

            return View(lessons.ToList());
        }


        [HttpPost]
        public ActionResult Index(int Discipline)
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            IEnumerable<LessonList> Alllessons = null;
            Alllessons = from lesson in db.LessonLists.Include(k => k.Discipline)
                        where lesson.DisciplineId == Discipline
                        where lesson.UserId == user.Id
                        select lesson;

            List<LessonType> types = db.LessonTypes.ToList();
            ViewBag.LessonTypes = new SelectList(types, "Id", "Name");

            List<Discipline> disc = db.Disciplines.ToList();
            ViewBag.Disciplines = new SelectList(disc, "Id", "Name");

            return View(Alllessons.OrderBy(l => l.ThemeNumber).ThenBy(l => l.LessonNumber).ToList());
        }

        //
        // GET: /LessonList/Details/5

        public ActionResult Details(int id = 0)
        {
            LessonList lessonlist = db.LessonLists.Find(id);
            if (lessonlist == null)
            {
                return HttpNotFound();
            }
            return View(lessonlist);
        }


        //
        // GET: /LessonList/Create

        public ActionResult Create()
        {
            ViewBag.LessonTypes = new SelectList(db.LessonTypes, "Id", "Name");
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            return View();
        }

        //
        // POST: /LessonList/Create

        [HttpPost]
        public ActionResult Create(LessonList lessonlist)
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                //request.UserId = user.Id;
                lessonlist.UserId = user.Id;
                db.LessonLists.Add(lessonlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lessonlist);
        }

        //
        // GET: /LessonList/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LessonList lessonlist = db.LessonLists.Find(id);
            if (lessonlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            ViewBag.LessonTypes = new SelectList(db.LessonTypes, "Id", "Name");
            return View(lessonlist);
        }

        //
        // POST: /LessonList/Edit/5

        [HttpPost]
        public ActionResult Edit(LessonList lessonlist)
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (ModelState.IsValid)
            {
                lessonlist.UserId = user.Id;
                db.Entry(lessonlist).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            ViewBag.LessonTypes = new SelectList(db.LessonTypes, "Id", "Name");
            return View(lessonlist);
        }

        //
        // GET: /LessonList/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LessonList lessonlist = db.LessonLists.Find(id);
            if (lessonlist == null)
            {
                return HttpNotFound();
            }
            return View(lessonlist);
        }

        //
        // POST: /LessonList/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonList lessonlist = db.LessonLists.Find(id);
            db.LessonLists.Remove(lessonlist);
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