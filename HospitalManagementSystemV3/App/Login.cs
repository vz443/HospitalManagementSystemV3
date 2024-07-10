using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Interface;
using HospitalManagementSystemV3.Models;

namespace HospitalManagementSystemV3.App
{
    class Login : PrintText, IMenuDisplay
    {
        public Login(AppDbContext context)
        {
            PrintHeader("Login");
        }

        public override void PrintHeader(string title)
        {
            base.PrintHeader(title);
        }

        public void PrintBody()
        {
            Console.WriteLine("ID: ");
            var username = Console.ReadLine();

            Console.WriteLine("Password: ");
            var password = Console.ReadLine();

            if (GetPassword != null)
            {
                if (username == GetPassword(username))
                {

                }
            }
        }

        public string GetPassword(string username)
        {
            return null;
        }
    }               
}
