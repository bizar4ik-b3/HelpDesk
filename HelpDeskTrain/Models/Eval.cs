using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class Eval
    {
        private int value;
        private int [] size;

        public int Id { get; set; }

        public int KadetId { get; set; }
        public int LessonListId { get; set; }
        public int JournalId { get; set; }

        public virtual Kadet Kadets { get; set; }
        public virtual LessonList LessonLists { get; set; }
        //public virtual Journal Journals { get; set; }

        [Display(Name = "Введіть оцінку")]
        public string Value
        {
            get;
            set;
        }


    }
}