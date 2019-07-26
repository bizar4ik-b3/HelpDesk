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
    public class KadetController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();

        //
        // GET: /Kadet/

        public ActionResult Index()
        {
            var kadets = db.Kadets.Include(k => k.Group).Include(k => k.Rank);
            List<Rank> ranks = db.Ranks.ToList();
            //Добавляем в список возможность выбора всех
            ViewBag.Ranks = new SelectList(ranks, "Id", "Name");

            List<NadpsuOrgState> groups = db.NadpsuOrgStates.ToList();
            //Добавляем в список возможность выбора всех
            ViewBag.Groups = new SelectList(groups, "Id", "GroupNumber");

            return View(kadets.ToList());
        }

        [HttpPost]
        public ActionResult Index(int Group)
        {
            IEnumerable<Kadet> AllKadets = null;
            AllKadets = from kadet in db.Kadets.Include(k=>k.Group)
                        where kadet.GroupId == Group
                        select kadet;

            var kadets = db.Kadets.Include(k => k.Group).Include(k => k.Rank);
            List<Rank> ranks = db.Ranks.ToList();
            //Добавляем в список возможность выбора всех
            ViewBag.Ranks = new SelectList(ranks, "Id", "Name");

            List<NadpsuOrgState> groups = db.NadpsuOrgStates.ToList();
            //Добавляем в список возможность выбора всех
            ViewBag.Groups = new SelectList(groups, "Id", "GroupNumber");

            return View(AllKadets.ToList());
        }

        //
        // GET: /Kadet/Details/5

        public ActionResult Details(int id = 0)
        {
            Kadet kadet = db.Kadets.Find(id);
            if (kadet == null)
            {
                return HttpNotFound();
            }
            return View(kadet);
        }

        //
        // GET: /Kadet/Create

        public ActionResult Create()
        {
            ViewBag.Groups = new SelectList(db.NadpsuOrgStates, "Id", "GroupNumber");

            SelectList ranks = new SelectList(db.Ranks, "Id", "Name");
            ViewBag.Ranks = ranks;
            return View();
        }

        //
        // POST: /Kadet/Create

        [HttpPost]
        public ActionResult Create(Kadet kadet)
        {
            if (ModelState.IsValid)
            {
                db.Kadets.Add(kadet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.NadpsuOrgStates, "Id", "GroupNumber", kadet.GroupId);

            SelectList ranks = new SelectList(db.Ranks, "Id", "Name");
            ViewBag.Ranks = ranks;

            return View(kadet);
        }

        //
        // GET: /Kadet/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Kadet kadet = db.Kadets.Find(id);
            if (kadet == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.NadpsuOrgStates, "Id", "GroupNumber", kadet.GroupId);

            SelectList ranks = new SelectList(db.Ranks, "Id", "Name");
            ViewBag.Ranks = ranks;
            return View(kadet);
        }

        //
        // POST: /Kadet/Edit/5

        [HttpPost]
        public ActionResult Edit(Kadet kadet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kadet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.NadpsuOrgStates, "Id", "DepartmentName", kadet.GroupId);

            SelectList ranks = new SelectList(db.Ranks, "Id", "Name");
            ViewBag.Ranks = ranks;
            return View(kadet);
        }

        //
        // GET: /Kadet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Kadet kadet = db.Kadets.Find(id);
            if (kadet == null)
            {
                return HttpNotFound();
            }
            return View(kadet);
        }

        //
        // POST: /Kadet/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Kadet kadet = db.Kadets.Find(id);
            db.Kadets.Remove(kadet);
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