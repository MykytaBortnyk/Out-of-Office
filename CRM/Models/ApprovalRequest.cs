using CRM.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Models
{
    public class ApprovalRequest
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required, Display(Name = "Status")]
        public RequestStatus? Status { get; set; }
        [Display(Name = "Comment")]
        public string? Comment { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        [Required, ForeignKey("LeaveRequestId"), Display(Name = "Leave Request")]
        public LeaveRequest? LeaveRequest { get; set; }
        public int ApproverId { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        [Required, ForeignKey("ApproverId"), Display(Name = "Approver")]
        public Employee? Approver { get; set; }
    }
}
