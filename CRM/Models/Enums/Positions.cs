using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Enums
{
    public enum Positions
    {
        [PgName("Employee"), Display(Name = "Employee")]
        Employee,
        [PgName("HR Manager"), Display(Name = "HR Manager")]
        HrManager,
        [PgName("Project Manager"), Display(Name = "Project Manager")]
        ProjectManager,
        [PgName("Administrator"), Display(Name = "Administrator")]
        Administrator
    }
}
