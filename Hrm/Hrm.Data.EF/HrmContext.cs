using System.Data.Entity;
using Hrm.Data.EF.Mappings;
using Hrm.Data.EF.Models;
using Department = Hrm.Data.EF.Models.Department;

namespace Hrm.Data.EF
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0)]
    public class HrmContext : DbContext
    {
        public HrmContext()
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillCategory> SkillCategory { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new JobApplicationMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new JobSkillMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new SkillCategoryMap());
            modelBuilder.Configurations.Add(new SkillMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserSkillMap());
        }
    }
}
