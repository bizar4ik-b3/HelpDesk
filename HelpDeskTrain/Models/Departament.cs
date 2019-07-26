using System.ComponentModel.DataAnnotations;
namespace HelpDeskTrain.Models
{
    // модель отделы
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Назва Кафедри/Відділу")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Name { get; set; }
    }

    // Модель Активы
    public class Activ
    {
        public int Id { get; set; }
        // номер кабинета
        [Required]
        [Display(Name = "Номер кабінету")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string CabNumber { get; set; }

        // Внешний ключ
        // ID Отдела - обычное свойство
        [Required]
        [Display(Name = "Відділ")]
        public int? DepartmentId { get; set; }
        // Отдел - Навигационное свойство
        public Department Department { get; set; }
    }
}