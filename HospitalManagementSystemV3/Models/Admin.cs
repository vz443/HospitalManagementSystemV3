using HospitalManagementSystemV3.App.Interface;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemV3.Models
{
    public class Admin : IUser
    {
        [Key]
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
