using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CRM.Models.Enums
{
    public enum RequestStatus
    {
        [PgName("New"), Display(Name = "New")]
        New,
        [PgName("Approve"), Display(Name = "Approve")]
        Approve,
        [PgName("Reject"), Display(Name = "Reject")]
        Reject
    }
}
