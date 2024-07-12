using CRM.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required, Display(Name = "Full Name")]
        public Names FullName { get; set; } = Names.Vasyl;
        [Required, Display(Name = "Subdivision")]
        public Subdivision Subdivision { get; set; } = Subdivision.It;
        [Required, Display(Name = "Position")]
        public Positions Position { get; set; } = Positions.Employee;
        [Required, Display(Name = "Status")]
        public Status Status { get; set; } = Status.Inactive;
        [Required, Display(Name = "Out-of-Office Balance")]
        public short Balance { get; set; } = 28;
        public int? PartnerId { get; set; }
        public int? ProjectId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        [Required, ForeignKey("PartnerId"), Display(Name = "People Partner")]
        public Employee? Partner { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        [ForeignKey("ProjectId")]
        public Project? Project { get; set; } = default!;
        public override string ToString() => FullName.GetDisplayName();
    }
}
