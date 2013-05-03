using System;

namespace Hrm.Data.EF.Models.Enums
{
    [Flags]
    public enum ProjectStatuses
    {
        Pending = 0,
        Started = 1,
        Finished = 2
    }
}