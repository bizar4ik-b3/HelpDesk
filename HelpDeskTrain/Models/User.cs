using System;
using System.ComponentModel.DataAnnotations;
using HelpDeskTrain.Models;
namespace HelpDeskTrain.Models
{
    public class User
    {   
        // ID 
        public int Id { get; set; }
        // Фамилия Имя Отчество
        [Required]
        [Display(Name = "Імя, Прізвище, По-Батькові")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Name { get; set; }
        //Фио сокращенное
        [Display(Name = "П.І.Б. скорочено")]
        public string FioShort { get; set; }
        // Логин
        [Required]
        [Display(Name = "Логін")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Login { get; set; }
        // Пароль
        [Required]
        [Display(Name = "Пароль")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Password { get; set; }
        // Должность
        [Required]
        [Display(Name = "Посада")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Position { get; set; }
        // Отдел
        [Display(Name = "Відділ")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        // Статус
        [Required]
        [Display(Name = "Статус")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        //Идентификационный код
        [Display(Name = "Идентифікаційний код")]
        public string PersonCode { get; set; }
        //военное звание
        [Display(Name = "Військове звання")]
        public int RankId { get; set; }
        public Rank Rank { get; set; }
     
        //телефон
        [Display(Name = "Номер телефону")]
        public string Phone { get; set; }
        //Класность
        //Номер приказа на работу
        [Display(Name = "Номер приказу на работу")]
        public string OrderNumber { get; set; }
        //год начала службы

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] 
        [DataType(DataType.Date)]
        [Display(Name = "Дата початку служби")]
        public DateTime WorkStartAt { get; set; }
        //приказ назначения на должность
        [Display(Name= "Наказ призначення на посаду")]
        public string OrderPosition { get; set; }
        //приказ военное звание
        [Display(Name = "Наказ на військове звання")]
        public string OrderRank { get; set; }
        //Фото
        [Display(Name="Фото")]
        public string Photo { get; set; }
        //пмк
        [Display(Name ="ПМК")]
        public string PmkNumber { get; set; }

        [Display(Name = "Доступ до Інвентарного довідника")]
        public bool InvAccess { get; set; }

    }
}