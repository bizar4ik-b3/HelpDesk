using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpDeskTrain.Models;

namespace HelpDeskTrain.Controllers
{
    [Authorize(Roles = "SuperAdmin, Администратор, Модератор, Исполнитель")]
    public class UserController : Controller
    {
        private HelpdeskContext db = new HelpdeskContext();

        // отображение списка пользователей
        [HttpGet]
        public ActionResult Index()
        {
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            var users = db.Users.Where(r => r.DepartmentId == user.DepartmentId).Include(u => u.Department).Include(u => u.Role).ToList();
            if (HttpContext.User.IsInRole("SuperAdmin"))
            {
                users = db.Users.Include(u => u.Department).Include(u => u.Role).ToList();
            }

            List<Department> departments = db.Departments.ToList();
            //Добавляем в список возможность выбора всех
            departments.Insert(0, new Department { Name = "Все", Id = 0 });
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            List<Role> roles = db.Roles.ToList();
            roles.Insert(0, new Role { Name = "Все", Id = 0 });

            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            /*if (!HttpContext.User.IsInRole("Администратор"))
            {
                users.ForEach(x => { x.Login = null; x.Password = null; });
            }*/
            return View(users);
        }

        // поиск пользователей по департамену и статусу
        [HttpPost]
        public ActionResult Index(int department, int role)
        {
            IEnumerable<User> allUsers = null;
            if (role == 0 && department == 0)
            {
                return RedirectToAction("Index");
            }
            if (role == 0 && department != 0)
            {
                allUsers = from user in db.Users.Include(u => u.Department).Include(u=>u.Role)
                           where user.DepartmentId==department
                           select user;
            }
            else if (role != 0 && department == 0)
            {
                allUsers = from user in db.Users.Include(u => u.Department).Include(u => u.Role)
                           where user.RoleId==role
                           select user;
            }
            else
            {
                allUsers = from user in db.Users.Include(u => u.Department).Include(u => u.Role)
                           where user.DepartmentId == department && user.RoleId == role
                           select user;
            }

            List<Department> departments = db.Departments.ToList();
            //Добавляем в список возможность выбора всех
            departments.Insert(0, new Department { Name = "Все", Id = 0 });
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            List<Role> roles = db.Roles.ToList();
            roles.Insert(0, new Role { Name = "Все", Id = 0 });
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(allUsers.ToList());
        }

        // При создании нового пользователя передаем в представление список отделов и ролей
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Администратор")]
        public ActionResult Create()
        {
            SelectList departments = new SelectList(db.Departments, "Id", "Name");
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            SelectList ranks = new SelectList(db.Ranks, "Id", "Name");
            ViewBag.Ranks = ranks;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Администратор")]
        public ActionResult Create(User user, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                user.Photo = fileName;
            }
            if (ModelState.IsValid)
            {
                
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //SelectList departments = new SelectList(db.Departments, "Id", "Name");
            //ViewBag.Departments = departments;
            //SelectList roles = new SelectList(db.Roles, "Id", "Name");
            // ViewBag.Roles = roles;


            SelectList departments = new SelectList(db.Departments, "Id", "Name");
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            SelectList ranks = new SelectList(db.Ranks, "Id", "Name");
            ViewBag.Ranks = ranks;


 
            return View(user);
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Администратор")]
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            SelectList departments = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(db.Roles, "Id", "Name", user.RoleId);
            ViewBag.Roles = roles;

            SelectList ranks = new SelectList(db.Ranks, "Id", "Name",user.RankId);
            ViewBag.Ranks = ranks;

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Администратор")]
        public ActionResult Edit(User user,HttpPostedFileBase upload)
        {
        
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    // сохраняем файл в папку Files в проекте
                    upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                    user.Photo = fileName;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else 
                {
  
                    db.Entry(user).State = EntityState.Modified;
                    db.Entry(user).Property(x => x.Photo).IsModified = false;
                    db.SaveChanges();
                }
                              
                
                
                return RedirectToAction("Index");
            }

            SelectList departments = new SelectList(db.Departments, "Id", "Name");
            ViewBag.Departments = departments;
            SelectList roles = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }
        [Authorize(Roles = "SuperAdmin, Администратор")]
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
