using System.ComponentModel.DataAnnotations;

namespace RentalCarApp.Models
{
    public class Make
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of the Make is required")]
        public string Name { get; set; }

        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
