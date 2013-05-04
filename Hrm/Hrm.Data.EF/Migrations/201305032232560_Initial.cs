namespace Hrm.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false),
                        Salary = c.Int(),
                        Department_id = c.Long(nullable: false),
                        Project_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_id, cascadeDelete: true)
                .Index(t => t.Department_id)
                .Index(t => t.Project_id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        ProjectFormalizeName_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectFormalizeNames", t => t.ProjectFormalizeName_id, cascadeDelete: true)
                .Index(t => t.ProjectFormalizeName_id);
            
            CreateTable(
                "dbo.ProjectFormalizeNames",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FormalizeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(nullable: false, maxLength: 50),
                        IsConfirmed = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                        Profile_id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_id)
                .Index(t => t.Profile_id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PhoneNumber = c.String(maxLength: 50),
                        Skype = c.String(maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        LastJobTitle = c.String(maxLength: 250),
                        TotalWorkExperience = c.Int(),
                        ResumePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Job_id = c.Long(nullable: false),
                        User_id = c.Long(nullable: false),
                        FilingDate = c.DateTime(nullable: false),
                        DesiredSalary = c.Int(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.Job_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.Job_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.UsersSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        User_id = c.Long(nullable: false),
                        SkillCategory_id = c.Long(nullable: false),
                        Skill_id = c.Long(nullable: false),
                        Estimate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.SkillCategories", t => t.SkillCategory_id, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skill_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.SkillCategory_id)
                .Index(t => t.Skill_id);
            
            CreateTable(
                "dbo.SkillCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(maxLength: 250),
                        SkillCategory_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SkillCategories", t => t.SkillCategory_id, cascadeDelete: true)
                .Index(t => t.SkillCategory_id);
            
            CreateTable(
                "dbo.JobsSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Job_id = c.Long(nullable: false),
                        SkillCategory_id = c.Long(nullable: false),
                        Skill_id = c.Long(nullable: false),
                        Estimate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.Job_id, cascadeDelete: true)
                .ForeignKey("dbo.SkillCategories", t => t.SkillCategory_id, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skill_id, cascadeDelete: true)
                .Index(t => t.Job_id)
                .Index(t => t.SkillCategory_id)
                .Index(t => t.Skill_id);
            
            CreateTable(
                "dbo.UsersInProjects",
                c => new
                    {
                        User_id = c.Long(nullable: false),
                        Project_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Project_id })
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Project_id);
            
            CreateTable(
                "dbo.UsersInDepartments",
                c => new
                    {
                        User_id = c.Long(nullable: false),
                        Department_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Department_id })
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Department_id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UsersInDepartments", new[] { "Department_id" });
            DropIndex("dbo.UsersInDepartments", new[] { "User_id" });
            DropIndex("dbo.UsersInProjects", new[] { "Project_id" });
            DropIndex("dbo.UsersInProjects", new[] { "User_id" });
            DropIndex("dbo.JobsSkills", new[] { "Skill_id" });
            DropIndex("dbo.JobsSkills", new[] { "SkillCategory_id" });
            DropIndex("dbo.JobsSkills", new[] { "Job_id" });
            DropIndex("dbo.Skills", new[] { "SkillCategory_id" });
            DropIndex("dbo.UsersSkills", new[] { "Skill_id" });
            DropIndex("dbo.UsersSkills", new[] { "SkillCategory_id" });
            DropIndex("dbo.UsersSkills", new[] { "User_id" });
            DropIndex("dbo.JobApplications", new[] { "User_id" });
            DropIndex("dbo.JobApplications", new[] { "Job_id" });
            DropIndex("dbo.Users", new[] { "Profile_id" });
            DropIndex("dbo.Projects", new[] { "ProjectFormalizeName_id" });
            DropIndex("dbo.Jobs", new[] { "Project_id" });
            DropIndex("dbo.Jobs", new[] { "Department_id" });
            DropForeignKey("dbo.UsersInDepartments", "Department_id", "dbo.Departments");
            DropForeignKey("dbo.UsersInDepartments", "User_id", "dbo.Users");
            DropForeignKey("dbo.UsersInProjects", "Project_id", "dbo.Projects");
            DropForeignKey("dbo.UsersInProjects", "User_id", "dbo.Users");
            DropForeignKey("dbo.JobsSkills", "Skill_id", "dbo.Skills");
            DropForeignKey("dbo.JobsSkills", "SkillCategory_id", "dbo.SkillCategories");
            DropForeignKey("dbo.JobsSkills", "Job_id", "dbo.Jobs");
            DropForeignKey("dbo.Skills", "SkillCategory_id", "dbo.SkillCategories");
            DropForeignKey("dbo.UsersSkills", "Skill_id", "dbo.Skills");
            DropForeignKey("dbo.UsersSkills", "SkillCategory_id", "dbo.SkillCategories");
            DropForeignKey("dbo.UsersSkills", "User_id", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "User_id", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "Job_id", "dbo.Jobs");
            DropForeignKey("dbo.Users", "Profile_id", "dbo.Profiles");
            DropForeignKey("dbo.Projects", "ProjectFormalizeName_id", "dbo.ProjectFormalizeNames");
            DropForeignKey("dbo.Jobs", "Project_id", "dbo.Projects");
            DropForeignKey("dbo.Jobs", "Department_id", "dbo.Departments");
            DropTable("dbo.UsersInDepartments");
            DropTable("dbo.UsersInProjects");
            DropTable("dbo.JobsSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillCategories");
            DropTable("dbo.UsersSkills");
            DropTable("dbo.JobApplications");
            DropTable("dbo.Profiles");
            DropTable("dbo.Users");
            DropTable("dbo.ProjectFormalizeNames");
            DropTable("dbo.Projects");
            DropTable("dbo.Jobs");
            DropTable("dbo.Departments");
        }
    }
}
