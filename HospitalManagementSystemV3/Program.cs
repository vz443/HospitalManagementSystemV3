using HospitalManagementSystemV3.App;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

var context = new AppDbContext();

Console.WriteLine("\x1b[37m         ______________");  
Console.WriteLine("\x1b[37m        |              |");
Console.WriteLine("\x1b[37m        |    \x1b[31mHOSPITAL\x1b[37m  |");  
Console.WriteLine("\x1b[37m        |     ____     |");
Console.WriteLine("\x1b[37m        |    |    |    |");
Console.WriteLine("\x1b[37m        |____|____|____|");
Console.WriteLine("\x1b[37m       /  _         _  \\");
Console.WriteLine("\x1b[37m      / _|_|_______|_|_ \\");
Console.WriteLine("\x1b[37m     / |________________| \\");
Console.WriteLine("\x1b[37m    |______________________|\x1b[0m");  
Console.WriteLine();

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
