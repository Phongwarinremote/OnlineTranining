using Microsoft.OpenApi.Writers;
using System.ComponentModel.DataAnnotations;

namespace OnlineTranining.API.Data.DataAccess
{
    public class Result
    {
        [Key]
        public string ResultId { get; set; }
        public string Score { get; set; }
        public DateTime DateRecord { get; set; } = DateTime.UtcNow.AddHours(7);
        public DateTime TakeTime { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
