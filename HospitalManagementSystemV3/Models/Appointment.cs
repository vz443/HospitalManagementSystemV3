using HospitalManagementSystemV3.Models;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    [Key]
    public int Id { get; set; }
    public required Guid DoctorId { get; set; }
    public required Guid PatientId { get; set; }
    public required string Description { get; set; }
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}