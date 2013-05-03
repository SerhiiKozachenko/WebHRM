using System;

namespace Hrm.Data.EF.Models.Enums
{
    [Flags]
    public enum Roles
    {
        User = 1,
        Admin = 2,
        Manager = 4
    }
}