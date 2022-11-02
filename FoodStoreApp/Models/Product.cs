using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodStoreApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; } 
        public string? Image { get; set; }
        [Display (Name ="Тип Категории")]
        public int Categoryid { get; set; }
        public int Managerid { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Category Category { get; set; }
    }
}
