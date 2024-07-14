using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.Interface
{
    public interface IMenu
    {
        public void PrintHeader(string title);

        public void DisplayMainMenu();
    }
}
