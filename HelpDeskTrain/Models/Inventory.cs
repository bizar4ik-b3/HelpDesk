using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Найменування")]
        [MaxLength(100, ErrorMessage = "Перевищена довжина запису")]
        public string Name { get; set; }
       
        [Display(Name = "Заводський №")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Characteristic { get; set; }
      
        [Display(Name = "Призначення")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Appointment { get; set; }// Назначение

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата випуску")]
        public DateTime DateRelease { get; set; }

        [Required]
        [Display(Name = "Інвентарний номер")]
        [MaxLength(100, ErrorMessage = "Перевищена довжина запису")]
        public string InvNumber { get; set; }
        
        [Display(Name = "Ціна")]
        [MaxLength(10, ErrorMessage = "Перевищена довжина запису")]
        public string Price { get; set; }
       
        [Display(Name = "Кількість")]
        public int Count { get; set; }
        
        [Display(Name = "Знаходження")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Comment { get; set; }
       
        [Display(Name = "Одиниця виміру")]
        public string Units  { get; set; }// Еденица измерения

        [Display(Name = "Видано")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Published { get; set; }// Видано

        [Display(Name = "Перевірено")]
        public bool Checked { get; set; }// Проверено
       
    }
}