using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid UserRoleId { get; set; }
        [Required, ForeignKey(nameof(UserRoleId))]
        public virtual ApplicationRole UserRole { get; set; }

        [Required, ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; } = new();
        public int EmployeeId { get; set; }
    }
}
