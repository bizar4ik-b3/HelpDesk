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
    public class DisciplineController : Controller
    {
         private HelpdeskContext db = new HelpdeskContext();

        //
        // GET: /Discipline/

        public ActionResult Index()
        {
            var disciplines = db.Disciplines;
            return View(disciplines.ToList());
        }

        //
        // GET: /Discipline/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Discipline/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Discipline/Create

        [HttpPost]
        public ActionResult Create(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                db.Disciplines.Add(discipline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discipline);
        }

        //
        // GET: /Discipline/Edit/5

        [HttpPost]
        public ActionResult Edit(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discipline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discipline);
        }
        //
        // POST: /Discipline/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Discipline discipline = db.Disciplines.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }

            return View(discipline);
        }

        //
        // GET: /Discipline/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Discipline discipline = db.Disciplines.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }
            return View(discipline);
        }

        //
        // POST: /Discipline/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Discipline discipline = db.Disciplines.Find(id);
            db.Disciplines.Remove(discipline);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
