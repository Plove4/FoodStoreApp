using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodStoreApp.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Materials { get; set; }
    }
}
