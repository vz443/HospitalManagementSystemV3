using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.App.Print
{
    public class PrintText
    {
        public static void PrintHeader(string title)
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

        public static void PrintSingleDoctor(Doctor doctor)
        {
            const int nameWidth = 21;
            const int emailWidth = 23;
            const int phoneWidth = 19;
            const int addressWidth = 25;

            string FormatField(string value, int width)
            {
                if (value.Length <= width) return value.PadRight(width);
                return string.Join("\n", Enumerable.Range(0, (value.Length + width - 1) / width)
                    .Select(i => value.Substring(i * width, Math.Min(width, value.Length - i * width)).PadRight(width)));
            }

            Console.WriteLine("Name             | Email Address     | Phone         | Address              ");
            Console.WriteLine("-------------------------------------------------------------------------------");

            var nameLines = FormatField(doctor.Name, nameWidth).Split('\n');
            var emailLines = FormatField(doctor.Email, emailWidth).Split('\n');
            var phoneLines = FormatField(doctor.Phone, phoneWidth).Split('\n');
            var addressLines = FormatField(doctor.Address, addressWidth).Split('\n');

            var maxLines = new[] { nameLines.Length, emailLines.Length, phoneLines.Length, addressLines.Length }.Max();

            for (int i = 0; i < maxLines; i++)
            {
                var name = i < nameLines.Length ? nameLines[i] : new string(' ', nameWidth);
                var email = i < emailLines.Length ? emailLines[i] : new string(' ', emailWidth);
                var phone = i < phoneLines.Length ? phoneLines[i] : new string(' ', phoneWidth);
                var address = i < addressLines.Length ? addressLines[i] : new string(' ', addressWidth);

                Console.WriteLine($"{name} | {email} | {phone} | {address}");
            }
        }

        public static void PrintMultipleDoctors(ICollection<Doctor> doctors)
        {
            const int numberWidth = 5;
            const int nameWidth = 16;
            const int emailWidth = 18;
            const int phoneWidth = 14;
            const int addressWidth = 20;

            string FormatField(string value, int width)
            {
                if (value.Length <= width) return value.PadRight(width);
                return string.Join("\n", Enumerable.Range(0, (value.Length + width - 1) / width)
                    .Select(i => value.Substring(i * width, Math.Min(width, value.Length - i * width)).PadRight(width)));
            }

            Console.WriteLine("No.  | Name             | Email Address     | Phone         | Address");
            Console.WriteLine("-------------------------------------------------------------");

            int number = 1;
            foreach (var doctor in doctors)
            {
                var numberLines = number.ToString().PadRight(numberWidth).Split('\n');
                var nameLines = FormatField(doctor.Name, nameWidth).Split('\n');
                var emailLines = FormatField(doctor.Email, emailWidth).Split('\n');
                var phoneLines = FormatField(doctor.Phone, phoneWidth).Split('\n');
                var addressLines = FormatField(doctor.Address, addressWidth).Split('\n');

                var maxLines = new[] { numberLines.Length, nameLines.Length, emailLines.Length, phoneLines.Length, addressLines.Length }.Max();

                for (int i = 0; i < maxLines; i++)
                {
                    var numberText = i < numberLines.Length ? numberLines[i] : new string(' ', numberWidth);
                    var name = i < nameLines.Length ? nameLines[i] : new string(' ', nameWidth);
                    var email = i < emailLines.Length ? emailLines[i] : new string(' ', emailWidth);
                    var phone = i < phoneLines.Length ? phoneLines[i] : new string(' ', phoneWidth);
                    var address = i < addressLines.Length ? addressLines[i] : new string(' ', addressWidth);

                    Console.WriteLine($"{numberText} | {name} | {email} | {phone} | {address}");
                }

                Console.WriteLine("-------------------------------------------------------------");
                number++;
            }
        }


    }
}
