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
    public class NadpsuOrgStateController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();

        //
        // GET: /NadpsuOrgState/

        public ActionResult Index()
        {
            return View(db.NadpsuOrgStates.ToList());
        }

        //
        // GET: /NadpsuOrgState/Details/5

        public ActionResult Details(int id = 0)
        {
            NadpsuOrgState nadpsuorgstate = db.NadpsuOrgStates.Find(id);
            if (nadpsuorgstate == null)
            {
                return HttpNotFound();
            }
            return View(nadpsuorgstate);
        }

        //
        // GET: /NadpsuOrgState/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NadpsuOrgState/Create

        [HttpPost]
        public ActionResult Create(NadpsuOrgState nadpsuorgstate)
        {
            if (ModelState.IsValid)
            {
                db.NadpsuOrgStates.Add(nadpsuorgstate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nadpsuorgstate);
        }

        //
        // GET: /NadpsuOrgState/Edit/5

        public ActionResult Edit(int id = 0)
        {
            NadpsuOrgState nadpsuorgstate = db.NadpsuOrgStates.Find(id);
            if (nadpsuorgstate == null)
            {
                return HttpNotFound();
            }
            return View(nadpsuorgstate);
        }

        //
        // POST: /NadpsuOrgState/Edit/5

        [HttpPost]
        public ActionResult Edit(NadpsuOrgState nadpsuorgstate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nadpsuorgstate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nadpsuorgstate);
        }

        //
        // GET: /NadpsuOrgState/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NadpsuOrgState nadpsuorgstate = db.NadpsuOrgStates.Find(id);
            if (nadpsuorgstate == null)
            {
                return HttpNotFound();
            }
            return View(nadpsuorgstate);
        }

        //
        // POST: /NadpsuOrgState/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            NadpsuOrgState nadpsuorgstate = db.NadpsuOrgStates.Find(id);
            db.NadpsuOrgStates.Remove(nadpsuorgstate);
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