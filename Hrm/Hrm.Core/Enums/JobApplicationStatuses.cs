using System;

namespace Hrm.Core.Enums
{
    [Flags]
    public enum JobApplicationStatuses
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        Approved = 3,
        Denied = 4
    }
}