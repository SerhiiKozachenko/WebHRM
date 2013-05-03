namespace Hrm.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Salary = c.Int(),
                        Department_Id = c.Long(),
                        Project_Id = c.Long(),
                        RequiredSkillMatrix_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .ForeignKey("dbo.SkillMatrices", t => t.RequiredSkillMatrix_Id)
                .Index(t => t.Department_Id)
                .Index(t => t.Project_Id)
                .Index(t => t.RequiredSkillMatrix_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        LastName = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                        Profile_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .Index(t => t.Profile_Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Skype = c.String(),
                        DateOfBirth = c.DateTime(),
                        LastJobTitle = c.String(),
                        TotalWorkExperience = c.Int(),
                        ResumePath = c.String(),
                        SkillMatrix_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SkillMatrices", t => t.SkillMatrix_Id)
                .Index(t => t.SkillMatrix_Id);
            
            CreateTable(
                "dbo.SkillMatrices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageSkills_Id = c.Long(),
                        ManagementSkills_Id = c.Long(),
                        ProgrammingSkills_Id = c.Long(),
                        DesignSkills_Id = c.Long(),
                        QualityAssuranceSkills_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LanguageSkills", t => t.LanguageSkills_Id)
                .ForeignKey("dbo.ManagementSkills", t => t.ManagementSkills_Id)
                .ForeignKey("dbo.ProgrammingSkills", t => t.ProgrammingSkills_Id)
                .ForeignKey("dbo.DesignSkills", t => t.DesignSkills_Id)
                .ForeignKey("dbo.QualityAssuranceSkills", t => t.QualityAssuranceSkills_Id)
                .Index(t => t.LanguageSkills_Id)
                .Index(t => t.ManagementSkills_Id)
                .Index(t => t.ProgrammingSkills_Id)
                .Index(t => t.DesignSkills_Id)
                .Index(t => t.QualityAssuranceSkills_Id);
            
            CreateTable(
                "dbo.LanguageSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        English = c.Int(nullable: false),
                        French = c.Int(nullable: false),
                        German = c.Int(nullable: false),
                        Chinese = c.Int(nullable: false),
                        Other = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ManagementSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Negotiation = c.Int(nullable: false),
                        LeaderShip = c.Int(nullable: false),
                        TeamBuilding = c.Int(nullable: false),
                        ProjectManagement = c.Int(nullable: false),
                        TimeManagement = c.Int(nullable: false),
                        Presentation = c.Int(nullable: false),
                        RUP = c.Int(nullable: false),
                        Scrum = c.Int(nullable: false),
                        Agile = c.Int(nullable: false),
                        XP = c.Int(nullable: false),
                        Other = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProgrammingSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        WebDevelopment = c.Int(nullable: false),
                        DesktopDevelopment = c.Int(nullable: false),
                        DriverDevelopment = c.Int(nullable: false),
                        MobileDevelopment = c.Int(nullable: false),
                        TDD = c.Int(nullable: false),
                        Patterns = c.Int(nullable: false),
                        Refactoring = c.Int(nullable: false),
                        UnitTests = c.Int(nullable: false),
                        Java = c.Int(nullable: false),
                        Dotnet = c.Int(nullable: false),
                        C = c.Int(nullable: false),
                        PHP = c.Int(nullable: false),
                        Ruby = c.Int(nullable: false),
                        Python = c.Int(nullable: false),
                        Other = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DesignSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ThreeD = c.Int(nullable: false),
                        TwoD = c.Int(nullable: false),
                        Typography = c.Int(nullable: false),
                        WebDesign = c.Int(nullable: false),
                        Photography = c.Int(nullable: false),
                        Other = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QualityAssuranceSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IntegrationTesting = c.Int(nullable: false),
                        AutomationTesting = c.Int(nullable: false),
                        Documentation = c.Int(nullable: false),
                        Other = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FilingDate = c.DateTime(nullable: false),
                        DesiredSalary = c.Int(),
                        Status = c.Int(nullable: false),
                        Job_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.Job_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Job_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Estimate = c.Int(nullable: false),
                        User_Id = c.Long(),
                        Skill_Id = c.Long(),
                        SkillCategory_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Skills", t => t.Skill_Id)
                .ForeignKey("dbo.SkillCategories", t => t.SkillCategory_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Skill_Id)
                .Index(t => t.SkillCategory_Id);
            
            CreateTable(
                "dbo.SkillCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        SkillCategory_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SkillCategories", t => t.SkillCategory_Id)
                .Index(t => t.SkillCategory_Id);
            
            CreateTable(
                "dbo.JobSkills",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Estimate = c.Int(nullable: false),
                        Category_Id = c.Long(),
                        Skill_Id = c.Long(),
                        Job_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SkillCategories", t => t.Category_Id)
                .ForeignKey("dbo.Skills", t => t.Skill_Id)
                .ForeignKey("dbo.Jobs", t => t.Job_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Skill_Id)
                .Index(t => t.Job_Id);
            
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Project_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Project_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.UserDepartments",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        Department_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Department_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Department_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserDepartments", new[] { "Department_Id" });
            DropIndex("dbo.UserDepartments", new[] { "User_Id" });
            DropIndex("dbo.UserProjects", new[] { "Project_Id" });
            DropIndex("dbo.UserProjects", new[] { "User_Id" });
            DropIndex("dbo.JobSkills", new[] { "Job_Id" });
            DropIndex("dbo.JobSkills", new[] { "Skill_Id" });
            DropIndex("dbo.JobSkills", new[] { "Category_Id" });
            DropIndex("dbo.Skills", new[] { "SkillCategory_Id" });
            DropIndex("dbo.UserSkills", new[] { "SkillCategory_Id" });
            DropIndex("dbo.UserSkills", new[] { "Skill_Id" });
            DropIndex("dbo.UserSkills", new[] { "User_Id" });
            DropIndex("dbo.JobApplications", new[] { "User_Id" });
            DropIndex("dbo.JobApplications", new[] { "Job_Id" });
            DropIndex("dbo.SkillMatrices", new[] { "QualityAssuranceSkills_Id" });
            DropIndex("dbo.SkillMatrices", new[] { "DesignSkills_Id" });
            DropIndex("dbo.SkillMatrices", new[] { "ProgrammingSkills_Id" });
            DropIndex("dbo.SkillMatrices", new[] { "ManagementSkills_Id" });
            DropIndex("dbo.SkillMatrices", new[] { "LanguageSkills_Id" });
            DropIndex("dbo.Profiles", new[] { "SkillMatrix_Id" });
            DropIndex("dbo.Users", new[] { "Profile_Id" });
            DropIndex("dbo.Jobs", new[] { "RequiredSkillMatrix_Id" });
            DropIndex("dbo.Jobs", new[] { "Project_Id" });
            DropIndex("dbo.Jobs", new[] { "Department_Id" });
            DropForeignKey("dbo.UserDepartments", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.UserDepartments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.UserProjects", "User_Id", "dbo.Users");
            DropForeignKey("dbo.JobSkills", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.JobSkills", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.JobSkills", "Category_Id", "dbo.SkillCategories");
            DropForeignKey("dbo.Skills", "SkillCategory_Id", "dbo.SkillCategories");
            DropForeignKey("dbo.UserSkills", "SkillCategory_Id", "dbo.SkillCategories");
            DropForeignKey("dbo.UserSkills", "Skill_Id", "dbo.Skills");
            DropForeignKey("dbo.UserSkills", "User_Id", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "User_Id", "dbo.Users");
            DropForeignKey("dbo.JobApplications", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.SkillMatrices", "QualityAssuranceSkills_Id", "dbo.QualityAssuranceSkills");
            DropForeignKey("dbo.SkillMatrices", "DesignSkills_Id", "dbo.DesignSkills");
            DropForeignKey("dbo.SkillMatrices", "ProgrammingSkills_Id", "dbo.ProgrammingSkills");
            DropForeignKey("dbo.SkillMatrices", "ManagementSkills_Id", "dbo.ManagementSkills");
            DropForeignKey("dbo.SkillMatrices", "LanguageSkills_Id", "dbo.LanguageSkills");
            DropForeignKey("dbo.Profiles", "SkillMatrix_Id", "dbo.SkillMatrices");
            DropForeignKey("dbo.Users", "Profile_Id", "dbo.Profiles");
            DropForeignKey("dbo.Jobs", "RequiredSkillMatrix_Id", "dbo.SkillMatrices");
            DropForeignKey("dbo.Jobs", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Jobs", "Department_Id", "dbo.Departments");
            DropTable("dbo.UserDepartments");
            DropTable("dbo.UserProjects");
            DropTable("dbo.JobSkills");
            DropTable("dbo.Skills");
            DropTable("dbo.SkillCategories");
            DropTable("dbo.UserSkills");
            DropTable("dbo.JobApplications");
            DropTable("dbo.QualityAssuranceSkills");
            DropTable("dbo.DesignSkills");
            DropTable("dbo.ProgrammingSkills");
            DropTable("dbo.ManagementSkills");
            DropTable("dbo.LanguageSkills");
            DropTable("dbo.SkillMatrices");
            DropTable("dbo.Profiles");
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
            DropTable("dbo.Jobs");
            DropTable("dbo.Departments");
        }
    }
}
