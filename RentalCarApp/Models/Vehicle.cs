using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentalCarApp.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of the vehicle is required")]
        [StringLength(100, ErrorMessage = "Name of the vehicle can not be longer then 100 characters")]
        [MinLength(5, ErrorMessage = "Name of the vehicle needs at least 5 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type of the vehicle is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Transmission of the vehicle is required")]
        public string Transmission { get; set; }

        [Required(ErrorMessage = "Number of passengers is required")]
        public int NumberPassengers { get; set; }

        [Required(ErrorMessage = "Number of bags is required")]
        public int NumberBags { get; set; }

        [Required(ErrorMessage = "Price per day is required")]
        public double PricePerDay { get; set; }

        [Url]
        public string Picture { get; set; }

        public float Rating { get; set; }

        public string? UserId { get; set; }

        public virtual User? User { get; set; }

        [Required(ErrorMessage = "Make is required")]
        public int? MakeId { get; set; }
        public virtual Make? Make { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Mk { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem>? Ratings { get; set; }


    }
}
