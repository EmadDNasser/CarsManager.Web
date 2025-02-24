using System.ComponentModel.DataAnnotations;

namespace CarsManager.Web.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
    }
}
