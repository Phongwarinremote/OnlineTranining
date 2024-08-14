using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTraining.DataAccess.Models
{
    public class Department
    {
        [Key]
        public string DepartmentId { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        public ICollection<ApplicationUser> ApplicationUser { get; }

        public Department()
        {
                ApplicationUser = new List<ApplicationUser>();
        }
    }
}
