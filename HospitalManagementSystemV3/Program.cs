using HospitalManagementSystemV3.App;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

var context = new AppDbContext();

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
