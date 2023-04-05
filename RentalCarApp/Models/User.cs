using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCarApp.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<User>? Users { get; set; }

        public virtual ICollection<UserRole>? UserRoles { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }


        [NotMapped]
        public IEnumerable<SelectListItem>? AllRoles { get; set; }
    }
}
