using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class NadpsuOrgState
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Номер групи")] 
        public int GroupNumber { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        [Display(Name="Назва факультету")]
        public string DepartmentName { get; set; }

        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        [Display(Name = "Коротка назва факультету")]
        public string ShortDepartmentName { get; set; }

        [Display(Name = "Кількість особового складу")]
        public int PersonCount { get; set; }

        [Required]
        [Display(Name = "Курс")]
        public int GroupCourse { get; set; }

        [Display(Name="Класс Сампо")]
        public int? SampoClass { get; set; }

        [Display(Name="Спеціальність")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Specialty { get; set; }
    }
}