using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class Kadet
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Поле 'П.І.Б' має бути заповнене")]
        [Display(Name="П.І.Б")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Pib { get; set; }

        public int GroupId { get; set; }
        [Display(Name = "Номер группи")]
        public NadpsuOrgState Group { get; set; }

        public int RankId { get; set; }
        [Display(Name = "Звання")]
        public Rank Rank { get; set; }

        [Display(Name="Номер телефону")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Telephone { get; set; }

        [Display(Name="Коментар")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Comment { get; set; }

        
        public ICollection<Eval> Evals { get; set; }
        public ICollection<LessonList> LessonLists { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}