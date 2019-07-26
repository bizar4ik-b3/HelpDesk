using System.ComponentModel.DataAnnotations;
namespace HelpDeskTrain.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Назва категорії проблеми")]
        [MaxLength(50, ErrorMessage = "Перевищена довжина запису")]
        public string Name { get; set; }
    }
}