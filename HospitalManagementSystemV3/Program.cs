using HospitalManagementSystemV3.App;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System;

var context = new AppDbContext();

Login login = new Login(context);

while (true)
{
    if (login.IsLoggedIn)
    {
        if (login.LoggedInUser.GetType() == typeof(Doctor))
        {
            DoctorMenu doctorMenu = new DoctorMenu(context, login.LoggedInUser, login);
        }
        else if (login.LoggedInUser.GetType() == typeof(Patient))
        {
            PatientMenu patientMenu = new PatientMenu(context, login.LoggedInUser, login);
        }
        else if (login.LoggedInUser.GetType() == typeof(Admin))
        {
            AdminMenu adminMenu = new AdminMenu(context, login.LoggedInUser, login);
        }
    }
}
