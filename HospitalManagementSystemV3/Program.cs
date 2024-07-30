using HospitalManagementSystemV3.App;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

var context = new AppDbContext();

Admin admins = new  Admin{ Id = Guid.NewGuid(), Name = "Dr. A", Email = "a@example.com", Phone = "123456", Address = "Address A", Username = "admin", Password = "admin" };
context.Admins.Add(admins);
context.SaveChanges();
Login login = new Login(context);



if (login.IsLoggedIn)
{
    if (login.LoggedInUser.GetType() == typeof(Doctor))
    {
        DoctorMenu doctor = new(context, login.LoggedInUser);
    }
    else if (login.LoggedInUser.GetType() == typeof(Patient))
    {
        PatientMenu patient = new(context, login.LoggedInUser);
    }
    else if (login.LoggedInUser.GetType() == typeof(Admin))
    {
        AdminMenu admin = new(context, login.LoggedInUser);
    }
}
