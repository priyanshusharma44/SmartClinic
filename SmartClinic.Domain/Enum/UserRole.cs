using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Domain.Enum
{
    public enum UserRole
    {
        Admin,    // System Administrator
        Doctor,   // Medical Professional
        Patient   // Registered Patient
    }
}
