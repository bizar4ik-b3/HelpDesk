using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class Module
    {
        public int Id { get; set; }
        [Display(Name="Оцінка за модуль")] 
        public decimal ModuleValue { get; set; }

        [Display(Name = "0.8")] 
        public decimal Value0_8 { get; set; }

        [Display(Name = "0.2")]
        public decimal Value0_2 { get; set; }

        [Display(Name = "Середнє")]
        public decimal AvGValue { get; set; }

        [Display(Name = "Занальний")]
        public decimal TotalValue { get; set; }

        [Display(Name = "Модуль")]
        public string ModuleString { get; set; }

        public int KadetId { get; set; }
        public int JournalId { get; set; }

        public virtual Kadet Kadets { get; set; }
        public virtual Journal Journals { get; set; }

        public decimal Avg(int value)
        {

            return 0;
        }
    }

}