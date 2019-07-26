using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;


namespace HelpDeskTrain.Models
{
    public class Gart
    {
        [Display(Name = "Оберіть базу для 'Гарт 3'")] 
        public string Base { get; set; }

        [Display(Name = "Запустити")]
        public bool Status { get; set; }

        [Display(Name = "Оповіщення")] 
        public bool Aso { get; set; }

        [Display(Name = "Телеграфіст")] 
        public bool Tele { get; set; }

        [Display(Name = "Донесення")] 
        public bool Gart2 { get; set; }

        [Display(Name = "Порушник")] 
        public bool Cordon { get; set; }

        [Display(Name = "Гарт 3")] 
        public bool Viddilps { get; set; }

        int timerValue = 60;
        [Display(Name = "Таймер оновлення(хв.)")]
        public int timer { 
            get{return timerValue;}
            set { timerValue = value; }
        }

        public List<Kabinet> Kabinets { get; set; }

        public List<string> Folders(string BaseDir)
        {
            DirectoryInfo folders = new DirectoryInfo(BaseDir);

            var folderlist=new List<string>();
            folderlist.Add("Оберіть базу");
            foreach (var item in folders.GetDirectories())
            {
               folderlist.Add(item.Name);

            }
            return folderlist;
        }

    }

    public class Kabinet
    {
        
        public string KabNUm { get; set; }
        public bool check { get; set; }
    }
}