using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    // Модель Заявка
    public class Request
    {
        // ID 
        public int Id { get; set; }
        // Наименование заявки
        [Required(ErrorMessage="Поле 'Назва заявки' має бути заповнене")]
        [Display(Name = "Назва заявки")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Name { get; set; }
        // Описание
        [Required(ErrorMessage = "Поле 'Описание' має бути заповнене'")]
        [Display(Name = "Опис")]
        [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Description { get; set; }
        // Комментарий к заявке
        [Display(Name = "Коментар")]
       [MaxLength(200, ErrorMessage = "Перевищена довжина запису")]
        public string Comment { get; set; }
        // Статус заявки
        [Display(Name = "Статус")]
        public int Status { get; set; }
        // Приоритет заявки
        [Display(Name = "Пріоритет")]
        public int Priority { get; set; }
        // Номер кабинета
        [Display(Name = "Кабінет")]
        public int? ActivId { get; set; }
        public Activ Activ { get; set; }
        // Файл ошибки
        [Display(Name = "Файл с помилкою")]
        public string File { get; set; }

        // Внешний ключ Категория
        [Display(Name = "Категорія")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        // Внешний ключ
        // ID Пользователя - обычное свойство
        public int? UserId { get; set; }
        // Отдел пользователя - Навигационное свойство
        public User User { get; set; }

        // Внешний ключ
        // ID Пользователя - обычное свойство
        public int? ExecutorId { get; set; }
        // Отдел пользователя - Навигационное свойство
        public User Executor { get; set; }

        // Внешний ключ
        // ID жизненного цикла заявки - обычное свойство
        public int LifecycleId { get; set; }
        // Ссылка на жизненный цикл заявки - Навигационное свойство
        public Lifecycle Lifecycle { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата готовності")]
        public DateTime WorkDoneAt { get; set; }
    }

    // Модель жизенного цикла заявки
    public class Lifecycle
    {
        // ID 
        public int Id { get; set; }
        // Дата открытия
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Opened { get; set; }
        // Дата распределения
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Distributed { get; set; }
        // Дата обработки
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Proccesing { get; set; }
        // Дата проверки
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Checking { get; set; }
        // Дата закрытия
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Closed { get; set; }
    }

    // Перечисление для статуса заявки
    public enum RequestStatus
    {
        Open = 1,
        Distributed = 2,
        Proccesing = 3,
        Checking = 4,
        Closed = 5
    }
    // Перечисление для приоритета заявки
    public enum RequestPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
}