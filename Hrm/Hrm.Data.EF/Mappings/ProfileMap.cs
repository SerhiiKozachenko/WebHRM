using Hrm.Data.EF.Models;

namespace Hrm.Data.EF.Mappings
{
    public class ProfileMap : BaseModelMap<Profile>
    {
        public ProfileMap()
        {
            this.ToTable("Profiles");

            this.Property(t => t.PhoneNumber)
                .HasMaxLength(50);

            this.Property(t => t.Skype)
                .HasMaxLength(50);

            this.Property(t => t.DateOfBirth)
                .IsOptional();

            this.Property(t => t.LastJobTitle)
                .HasMaxLength(250);

            this.Property(t => t.TotalWorkExperience)
                .IsOptional();

            this.Property(t => t.ResumePath)
                .HasMaxLength(int.MaxValue);
        }
    }
}

