using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class LessonList
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Назва")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Name { get; set; }

        [Display(Name="Номер теми")]
        public int ThemeNumber { get; set; }

        [Display(Name = "Номер заняття")]
        public int LessonNumber { get; set; }

        [Display(Name="Кількість годин")]
        public int Hours { get; set; } 

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] 
        [DataType(DataType.Date)]
        [Display(Name = "Дата(гггг-мм-дд)")]
        public DateTime LessonDate { get; set; }

        public int LessonTypeId { get; set; }
       
        [Display(Name="Тип заняття")]
        public LessonType LessonType { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Модуль?")]
        public bool Required { get; set; }

        [Display(Name = "Обов'язкове?")]
        public bool RequiredField { get; set; }

        [Display(Name = "Відображати підсумок модуля?")]
        public bool isVisible { get; set; }

        //[Display(Name = "Модуль1")]
       //public bool isModule { get; set; }


        public int? DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public ICollection<Kadet> Kadets { get; set; }
        public ICollection<Eval> Evals { get; set; }
        
    }
}