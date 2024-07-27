using CRM.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid> { }
    public class ApplicationUserRole : IdentityUserRole<Guid> { }
}
