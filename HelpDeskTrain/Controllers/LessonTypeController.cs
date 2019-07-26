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
    public class LessonTypeController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();

        //
        // GET: /LessonType/

        public ActionResult Index()
        {
            return View(db.LessonTypes.ToList());
        }

        //
        // GET: /LessonType/Details/5

        public ActionResult Details(int id = 0)
        {
            LessonType lessontype = db.LessonTypes.Find(id);
            if (lessontype == null)
            {
                return HttpNotFound();
            }
            return View(lessontype);
        }

        //
        // GET: /LessonType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LessonType/Create

        [HttpPost]
        public ActionResult Create(LessonType lessontype)
        {
            if (ModelState.IsValid)
            {
                db.LessonTypes.Add(lessontype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lessontype);
        }

        //
        // GET: /LessonType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LessonType lessontype = db.LessonTypes.Find(id);
            if (lessontype == null)
            {
                return HttpNotFound();
            }
            return View(lessontype);
        }

        //
        // POST: /LessonType/Edit/5

        [HttpPost]
        public ActionResult Edit(LessonType lessontype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessontype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lessontype);
        }

        //
        // GET: /LessonType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LessonType lessontype = db.LessonTypes.Find(id);
            if (lessontype == null)
            {
                return HttpNotFound();
            }
            return View(lessontype);
        }

        //
        // POST: /LessonType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            LessonType lessontype = db.LessonTypes.Find(id);
            db.LessonTypes.Remove(lessontype);
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