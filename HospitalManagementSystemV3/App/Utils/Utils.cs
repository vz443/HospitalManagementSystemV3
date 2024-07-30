using HospitalManagementSystemV3.App.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.App
{
    public class Utils
    {
        public static void CreateHeader(string name)
        {
            Console.Clear();
            PrintText.PrintHeader(name);
            Console.WriteLine();
        }
    }
}
