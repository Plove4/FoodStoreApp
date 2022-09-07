using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodStoreApp.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DisplayName("Название")]
        public string? Title { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Рейтинг")]
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Range(1,int.MaxValue,ErrorMessage = "Значение должно быть больше нуля")]
        public string Rating { get; set; }
    }
}
