using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.App.Print
{
    public class PrintText
    {
        public virtual void PrintHeader(string title)
        {
            int width = 40;

            Console.Write("┌");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┐");

            Console.Write("│");
            string header = "DOTNET Hospital Management System";
            int padding = (width - 2 - header.Length) / 2;
            Console.Write(new string(' ', padding));
            Console.Write(header);
            Console.Write(new string(' ', width - 2 - padding - header.Length));
            Console.WriteLine("│");

            Console.Write("│");
            Console.Write(new string('-', width - 2));
            Console.WriteLine("│");

            Console.Write("│");
            padding = (width - 2 - title.Length) / 2;
            Console.Write(new string(' ', padding));
            Console.Write(title);
            Console.Write(new string(' ', width - 2 - padding - title.Length));
            Console.WriteLine("│");

            Console.Write("│");
            Console.Write(new string(' ', width - 2));
            Console.WriteLine("│");

            Console.Write("└");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┘");
        }

    }
}
