using System.ComponentModel.DataAnnotations;

namespace HelpDeskTrain.Models
{
    public class LogViewModel
    {
        [Required]
        [Display(Name = "Логін")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Запамятати?")]
        public bool RememberMe { get; set; }
    }
}