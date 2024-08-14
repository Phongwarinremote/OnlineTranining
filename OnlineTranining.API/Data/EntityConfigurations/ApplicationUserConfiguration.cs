using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineTranining.API.Data.DataAccess;

namespace OnlineTranining.API.Data.EntityConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(x => x.Department)
                .WithMany(x => x.ApplicationUser)
                .HasForeignKey(x => x.DepartmentId);



        }
    }
}
