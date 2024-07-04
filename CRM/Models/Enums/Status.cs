using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Enums
{
    public enum Status
    {
        [PgName("Active"), Display(Name = "Active")]
        Active,
        [PgName("Inactive"), Display(Name = "Inactive")]
        Inactive
    }
}
