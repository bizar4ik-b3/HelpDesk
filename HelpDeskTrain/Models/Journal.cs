using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class Journal
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GroupId { get; set; }

        [Display(Name = "Оберіть групу")]
        public NadpsuOrgState Group { get; set; }

        public int? DisciplineId { get; set; }
        [Display(Name = "Оберіть дисципліну")]
        public Discipline Discipline { get; set; }

        [Required]
        [Display(Name="Назва журналу")]
        [MaxLength(50,ErrorMessage="Перевищена довжина запису")]
        public string Name { get; set; }

        public ICollection<Module> Modules { get; set; }
    }
}