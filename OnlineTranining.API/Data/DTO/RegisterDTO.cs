using Microsoft.AspNetCore.Identity;
using OnlineTranining.API.Data.DataAccess;

namespace OnlineTranining.API.Data.DTO
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        public string RoleId { get; set; }
        public IdentityRole RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
