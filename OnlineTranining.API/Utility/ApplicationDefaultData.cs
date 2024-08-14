using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineTranining.API.Data;
using OnlineTranining.API.Data.DataAccess;

namespace OnlineTranining.API.Utility
{
    public static class ApplicationDefaultData
    {
        public static async Task ApplicationDefaultIdentityRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(SD.Role_Admin.ToLower()))
                roleManager.CreateAsync(new IdentityRole(SD.Role_Admin.ToLower())).GetAwaiter().GetResult();

            if (!await roleManager.RoleExistsAsync(SD.Role_Trainer.ToLower()))
                roleManager.CreateAsync(new IdentityRole(SD.Role_Trainer.ToLower())).GetAwaiter().GetResult();

            if (!await roleManager.RoleExistsAsync(SD.Role_User.ToLower()))
                roleManager.CreateAsync(new IdentityRole(SD.Role_User.ToLower())).GetAwaiter().GetResult();

            await Task.CompletedTask;
        }

        public static async Task ApplicationDefaultAminAsync(UserManager<ApplicationUser> userManager,
            ApplicationDbContext appDbContext)
        {
            ApplicationUser appUser = new ApplicationUser()
            {
                UserName = "kltadmin",
                Email = "it-korat@kawasumi.co.th",
                CreateDate = DateTime.UtcNow.AddHours(7),
                Firstname = "Klt",
                LastName = "Admin"
            };

            string defaultPassword = "kltadminAdmsmdc!24";

            var userFromDb = await userManager.FindByNameAsync(appUser.UserName);
            IdentityResult result = null;


            if (userFromDb is null)
                result = userManager.CreateAsync(appUser, defaultPassword).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                appUser = await userManager.FindByNameAsync("kltadmin");

                var isInRole = await userManager.IsInRoleAsync(appUser, SD.Role_Admin.ToLower());

                if (!isInRole)
                    await userManager.AddToRoleAsync(appUser, SD.Role_Admin.ToLower());


                var isInDept = await appDbContext
                    .Departments
                    .AnyAsync(x => x.DepartmentId == appUser.DepartmentId &&
                    x.DepartmentName == "PC&IT");

                if (!isInDept)
                {
                    var itDept = await appDbContext
                        .Departments.FirstOrDefaultAsync(x => x.DepartmentName.Equals("PC&IT"));

                    appUser.DepartmentId = itDept.DepartmentId;
                    appDbContext.ApplicationUsers.Update(appUser);

                    await appDbContext.SaveChangesAsync();
                }

            }

            await Task.FromResult(appUser);
        }


        public static async Task DefaultDepartmentAsync(ApplicationDbContext appDbContext)
        {
            string[] departments = new string[]
            {
                "PC&IT", "FIN", "GA", "PUR", "PL&LG",
                "Assy3B", "WH", "QSRC"
            };

            var departmentList = await appDbContext.Departments.ToListAsync();

            HashSet<string> deptSet = new HashSet<string>(departmentList.Select(x => x.DepartmentName));

            foreach (var item in departments)
            {
                if (!deptSet.Contains(item.ToLower()))
                {

                    Department newDepartment = new Department();
                    newDepartment.DepartmentId = Guid.NewGuid().ToString().Substring(0, 9);
                    newDepartment.DepartmentName = item;

                    appDbContext.Departments.Add(newDepartment);
                }
            }

            await appDbContext.SaveChangesAsync();
        }


    }
}
