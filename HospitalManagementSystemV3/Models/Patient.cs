using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.Models
{
    internal class Patient
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public required string Email  { get; set; }
        public required string Phone {  get; set; }
        public required string Address { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
