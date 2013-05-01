using Hrm.Core.Entities.Skills.Base;

namespace Hrm.Core.Entities.Skills
{
    public class ProgrammingSkill : BaseSkill
    {
        public virtual int WebDevelopment { get; set; }

        public virtual int DesktopDevelopment { get; set; }

        public virtual int DriverDevelopment { get; set; }

        public virtual int MobileDevelopment { get; set; }

        public virtual int TDD { get; set; }

        public virtual int Patterns { get; set; }

        public virtual int Refactoring { get; set; }

        public virtual int UnitTests { get; set; }

        public virtual int Java { get; set; }

        public virtual int Dotnet { get; set; }

        public virtual int C { get; set; }

        public virtual int PHP { get; set; }

        public virtual int Ruby { get; set; }

        public virtual int Python { get; set; }
    }
}