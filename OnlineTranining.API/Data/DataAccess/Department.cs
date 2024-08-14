using System.ComponentModel.DataAnnotations;

namespace OnlineTranining.API.Data.DataAccess
{
    public class Department
    {
        [Key]
        public string DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter name of department.")]
        [StringLength(50, ErrorMessage = "The department name must be at lesat {1} - {0} Characters", MinimumLength = 3)]
        public string DepartmentName { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUser { get; }

        public Department()
        {
                ApplicationUser = new List<ApplicationUser>();
        }
    }
}
