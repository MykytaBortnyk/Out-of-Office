using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Enums
{
    public enum ProjectType
    {
        [PgName("Saas"), Display(Name = "SaaS")]
        SaaS,
        [PgName("Fintech"), Display(Name = "Fintech")]
        Fintech,
        [PgName("Education"), Display(Name = "Education")]
        Education,
        [PgName("Gambling"), Display(Name = "Gambling")]
        Gambling,
        [PgName("Telecom"), Display(Name = "Telecom")]
        Telecom
    }
}
