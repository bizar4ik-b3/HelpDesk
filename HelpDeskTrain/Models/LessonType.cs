using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class LessonType
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Назва")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Name { get; set; }

        [Display(Name="Коротка назва")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string ShortName { get; set; }

         [Display(Name = "Коментар")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Comment { get; set; }

         [Display(Name = "Модуль?")]
         public bool isModule { get; set; }

         public ICollection<LessonList> LessonLists { get; set; }
    }
}