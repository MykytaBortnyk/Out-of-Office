using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Enums
{
    public enum AbsenceReason
    {
        [PgName("Vacation"), Display(Name = "Vacation")]
        Vacation,
        [PgName("Health Issue"), Display(Name = "Health Issue")]
        HealthIssue,
        [PgName("Family Emergancy"), Display(Name = "Family Emergancy")]
        FamilyEmergancy
    }
}
