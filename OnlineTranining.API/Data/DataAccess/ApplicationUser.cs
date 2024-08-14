using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineTranining.API.Data.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "This field requied.")]
        [StringLength(6,ErrorMessage = "The code must be at least {1} - {0}", MinimumLength = 1)]
        public string Code { get; set; }

        [Required(ErrorMessage = "This field requied.")]
        [StringLength(50, ErrorMessage = "The firstname must be at least {1} - {0}", MinimumLength = 3 )]
        public string Firstname { get; set; }
        public string LastName { get; set;  }
        
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastLogin { get; set; }
 

        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Result> Results { get; } 

        public ApplicationUser()
        {
            Results = new List<Result>();
        }
    }
}
