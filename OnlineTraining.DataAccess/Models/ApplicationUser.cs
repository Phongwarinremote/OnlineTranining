using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTraining.DataAccess.Models
{
    public class ApplicationUser : IdentityUser
    {


        public string DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}
