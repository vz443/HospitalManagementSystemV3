using HospitalManagementSystemV3.Models;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    [Key]
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public required string Description { get; set; }
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}