using HospitalManagementSystemV3.App;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

var context = new AppDbContext();
var doctors = context.Doctors.ToList();
foreach (var doctor in doctors)
{
    Console.WriteLine($"TEST TEST ID: {doctor.Id}, Name: {doctor.Name}, Specialty: {doctor.Password}");
}

Login login = new Login(context);

if (login.IsLoggedIn)
{
    if (login.LoggedInUser.GetType() == typeof(Doctor))
    {
        DoctorMenu doctor = new(context, login.LoggedInUser);
    }
}
