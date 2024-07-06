using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual ICollection<ApplicationRole> UserRoles { get; set; }

        [Required, ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } = new();
        public int EmployeeId { get; set; }
    }
}
