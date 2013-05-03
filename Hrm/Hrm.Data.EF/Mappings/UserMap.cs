using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class UserMap : BaseModelMap<User>
    {
        public UserMap()
        {
            this.ToTable("Users");

            this.Property(t => t.Login)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Password)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.Email)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.LastName)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.MiddleName)
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.IsConfirmed)
                .IsRequired();

            this.Property(t => t.Role)
                .IsRequired();

            this.Property(p => p.ProfileId).HasColumnName("Profile_id");

            this.HasMany(t => t.Departments)
                .WithMany(r => r.Users)
                .Map(v => v.ToTable("UsersInDepartments")
                    .MapLeftKey("User_id")
                    .MapRightKey("Department_id"));

            this.HasMany(t => t.Projects)
                .WithMany(r => r.Users)
                .Map(v => v.ToTable("UsersInProjects")
                    .MapLeftKey("User_id")
                    .MapRightKey("Project_id"));
        }
    }
}

