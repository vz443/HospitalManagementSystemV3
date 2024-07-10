using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.Models
{
    internal class Appointment
    {
        [Key]
        public int Id { get; set; }

        public required Doctor Doctor { get; set; }
        public required Patient Patient { get; set; }
        public required string Description { get; set; }

    }
}
