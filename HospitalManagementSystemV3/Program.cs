using HospitalManagementSystemV3.App;
using HospitalManagementSystemV3.Database;

var context = new AppDbContext();

var doctors = context.Doctors.ToList();
foreach (var doctor in doctors)
{
    Console.WriteLine($"TEST TEST ID: {doctor.Id}, Name: {doctor.Name}, Specialty: {doctor.Password}");
}

Login login = new Login(context);
if (login.IsLoggedIn)
{
    
}
