using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Printing;
using System.Threading;
using System.Windows.Threading;

namespace PI_DruckWarnung
{




    class ReadDruckerInfo
    {

        public string PrinterName { get; set; }


        public string DruckerKontrolle()
        {


            var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
            foreach (var printer in printerQuery.Get())
            {
                var name = printer.GetPropertyValue("Name");
                string DruckerName = name.ToString();
                var status = printer.GetPropertyValue("Status");
                var isDefault = printer.GetPropertyValue("Default");
                string DefaultDrucker = isDefault.ToString();
                var isNetworkPrinter = printer.GetPropertyValue("Network");



                if (DefaultDrucker == "True")
                {
                    this.PrinterName = DruckerName;


                }
            }
            return PrinterName;


        }
    }
}