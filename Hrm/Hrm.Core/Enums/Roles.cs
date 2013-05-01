using System;

namespace Hrm.Core.Enums
{
    [Flags]
    public enum Roles
    {
        User = 1,
        Admin = 2,
        Manager = 4
    }
}