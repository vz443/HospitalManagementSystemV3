using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemTest
{
    public class TestAppDbContext : AppDbContext
    {
        public override DbSet<Doctor> Doctors { get; set; }
        public override DbSet<Patient> Patients { get; set; }
        public override DbSet<Appointment> Appointments { get; set; }
        public override DbSet<Admin> Admins { get; set; }
    }
}
