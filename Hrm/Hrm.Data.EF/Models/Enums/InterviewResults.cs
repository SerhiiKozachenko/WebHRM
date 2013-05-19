using System;

namespace Hrm.Data.EF.Models.Enums
{
    [Flags]
    public enum InterviewResults
    {
        UnderConsideration = 0,
        Adopted = 1,
        Refused = 2,
        NotSuitable = 3,
        NotAccepted = 4
    }
}