using HospitalManagementSystemV3.Interface;
using HospitalManagementSystemV3.Models;
using System.ComponentModel.DataAnnotations;

internal class Doctor : IUser
{
    [Key]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}