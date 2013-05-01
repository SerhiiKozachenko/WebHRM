using System;

namespace Hrm.Core.Enums
{
    [Flags]
    public enum ProjectStatuses
    {
        Pending = 0,
        Started = 1,
        Finished = 2
    }
}