using CRM.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class LeaveRequest : IValidatableObject
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required, Display(Name = "Absence Reason"), Column("AbsenceReason")]
        public AbsenceReason Reason { get; set; }
        [Required, Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; } = DateTime.UtcNow;
        [Required, Display(Name = "End Date")]
        public DateTime? EndDate { get; set; } = DateTime.UtcNow;
        [MaxLength(255)]
        public string? Comment { get; set; }
        [Required, Display(Name = "Status")]
        public RequestStatus? Status { get; set; } = RequestStatus.New;
        public int EmployeeId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        [Required, ForeignKey("EmployeeId"), Display(Name = "Employee")]
        public Employee? Employee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate >= EndDate)
            {
                yield return new ValidationResult("End date must be greater than the start date.");
            }
            else if (StartDate < DateTime.Now)
            {
                yield return new ValidationResult("Start day must be greater then today.");
            }
            else if ((EndDate.Value - StartDate.Value).TotalDays > Employee.Balance)
            {
                yield return new ValidationResult("There are not enough days on balance.");
            }
        }
    }
}
