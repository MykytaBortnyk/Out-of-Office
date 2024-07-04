using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Enums
{
    public enum Subdivision
    {
        [PgName("ssm"), Display(Name = "SMM")]
        Smm,
        [PgName("it"), Display(Name = "IT")]
        It,
        [PgName("Support"), Display(Name = "Support")]
        Support,
        [PgName("Sales"), Display(Name = "Sales")]
        Sales,
        [PgName("Human Resources"), Display(Name = "Human Resources")]
        HumanResources
    }
}
