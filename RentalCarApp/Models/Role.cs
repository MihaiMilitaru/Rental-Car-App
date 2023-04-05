using Microsoft.AspNetCore.Identity;

namespace RentalCarApp.Models
{
    public class Role : IdentityRole
    {
        public string? RoleName { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
