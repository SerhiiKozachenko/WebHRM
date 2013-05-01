using Hrm.Core.Entities.Skills.Base;

namespace Hrm.Core.Entities.Skills
{
    public class ManagementSkill : BaseSkill
    {
        public virtual int Negotiation { get; set; }

        public virtual int LeaderShip { get; set; }

        public virtual int TeamBuilding { get; set; }

        public virtual int ProjectManagement { get; set; }

        public virtual int TimeManagement { get; set; }

        public virtual int Presentation { get; set; }

        public virtual int RUP { get; set; }

        public virtual int Scrum { get; set; }

        public virtual int Agile { get; set; }

        public virtual int XP { get; set; }
    }
}