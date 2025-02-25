using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Domain.Enums
{
    public enum UserRole
    {
        Admin,
        Doctor,
        Patient,
        LabTechnician
    }
}