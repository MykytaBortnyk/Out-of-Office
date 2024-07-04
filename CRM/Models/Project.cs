using CRM.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class Project
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required, Display(Name = "Project Type")]
        public ProjectType Type { get; set; }
        [Required, Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Comment")]
        public string? Comment { get; set; }
        [Required, Display(Name = "Status")]
        public Status Status { get; set; }
        public int ManagerId { get; set; }
        /// <summary>
        /// Navigation Property
        /// </summary>
        [Required, ForeignKey("ManagerId"), Display(Name = "Project manager")]
        public Employee? ProjectManager { get; set; }
    }
}
