using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Enums
{
    //wtf? 
    public enum Names
    {
        [PgName("Vasyl"), Display(Name = "Vasyl")]
        Vasyl,
        [PgName("Petro"), Display(Name = "Petro")]
        Petro,
        [PgName("Volodymyr"), Display(Name = "Volodymyr")]
        Volodymyr,
        [PgName("Igor"), Display(Name = "Igor")]
        Igor
    }
}
