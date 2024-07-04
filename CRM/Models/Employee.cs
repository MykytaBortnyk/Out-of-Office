using CRM.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class Employee
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required, Display(Name = "Full Name")]
        public Names FullName { get; set; }
        [Required, Display(Name = "Subdivision")]
        public Subdivision Subdivision { get; set; }
        [Required, Display(Name = "Position")]
        public Positions Position { get; set; }
        [Required, Display(Name = "Status")]
        public Status Status { get; set; }
        [Required, Display(Name = "Out-of-Office Balance")]
        public short Balance { get; set; }
        public int PartnerId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        [Required, ForeignKey("PartnerId"), Display(Name = "People Partner")]
        public Employee? Partner { get; set; }
        public override string ToString() => FullName.GetDisplayName();
    }
}
