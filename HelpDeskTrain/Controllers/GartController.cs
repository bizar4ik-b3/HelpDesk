using HelpDeskTrain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskTrain.Controllers
{
    public class GartController : Controller
    {
        HelpdeskContext db = new HelpdeskContext();
        //
        // GET: /Gart/

        private string Base3Bases = @"C:\inetpub\wwwroot\helpdesk\Content\share\G3";
        private string ConfigPath = @"C:\inetpub\wwwroot\helpdesk\Content\share\";
        private string LogPath = @"C:\inetpub\wwwroot\helpdesk\Content\share\";
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Log(string MN)
        {
            if (MN != "clear")
            {
                StreamWriter file = new StreamWriter(LogPath + "Log.txt", append: true);
                file.WriteLine(DateTime.Now.ToString()+"=="+MN);
                file.Close();
            }
            else 
            {
                StreamWriter file = new StreamWriter(LogPath + "Log.txt");
                file.WriteLine("");
                file.Close();
            }
            return new EmptyResult();
        }


        [HttpGet]
        public ActionResult Index()
        {

          Gart gart = new Gart();
          List<Activ> clases = db.Activs.ToList();
          List<Kabinet> kabinetlist = new List<Kabinet>();
         
          foreach (var c in clases)
          {
               kabinetlist.Add(new Kabinet() 
               {
                   KabNUm=c.CabNumber,
                   check=false
               }
               );
            }
            gart.Kabinets = kabinetlist;
            //==============Get directories  Base Gart3===================


            ViewBag.Gart3Folders = gart.Folders(Base3Bases);

            //============================================================
            ViewBag.Kabinets = gart.Kabinets.ToList();
            return View(gart);
        }

        [HttpPost]
        public ActionResult Index(Gart gart)
        {

            //==========Config Generate======

            string path = "";
            foreach(var item in gart.Kabinets)
            {
                if(item.check==true)
                {
                    path = ConfigPath + item.KabNUm + @"\config.txt";
                    StreamWriter file = new StreamWriter(path);

                    //================Write Database Gart3 Name===========================
                    string s = Request.Form["Gart3Base"];
                    
                    if (s == "Оберіть базу")
                    {
                        file.WriteLine("0");
                    }
                    else file.WriteLine(s);
                    //Write Status od Update

                    file.WriteLine(gart.Status);
                    //Write Status od Update
                    file.WriteLine(gart.timer);
                    //==========ASO=============

                    if (gart.Aso == true)
                    {
                        file.WriteLine("ASO");
                    }
                   
                    //==========TELE=============
                    if (gart.Tele == true)
                    {
                        file.WriteLine("tele");
                    }
                    
                    //==========GART2=============
                    if (gart.Gart2 == true)
                    {
                        file.WriteLine("GART2");
                    }
                  
                    //==========CORDON=============
                    if (gart.Cordon == true)
                    {
                        file.WriteLine("cordon");
                    }
                   

                    //==========VIDDILPS=============
                    if (gart.Viddilps == true)
                    {
                        file.WriteLine("viddilps");
                    }
                    
                    file.Close();
                 }
            
            }
            //==========End Config Generate======
            List<Activ> clases = db.Activs.ToList();
            List<Kabinet> kabinetlist = new List<Kabinet>();

            foreach (var c in clases)
            {
                kabinetlist.Add(new Kabinet()
                {
                    KabNUm = c.CabNumber,
                    check = false
                }
                );
            }
            ViewBag.Gart3Folders = gart.Folders(Base3Bases);
            gart.Kabinets = kabinetlist;
            return View(gart);
        }

    }
}
