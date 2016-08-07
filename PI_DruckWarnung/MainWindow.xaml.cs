using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Management;
using System.Printing;
using System.Threading;
using System.Windows.Threading;

namespace PI_DruckWarnung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
       

      

        public static void DruckerKontrolle()
        {


            



            while (true)
            {
                //Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                //           new Action(() =>
                //           {
                //           }));
                
                

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
                        GlobalVar.PrinterName = DruckerName;
                    }


                }


                               string wmiQuery = "SELECT * FROM Win32_PrintJob";
                               ManagementObjectSearcher jobsSearcher = new ManagementObjectSearcher(wmiQuery);
                               ManagementObjectCollection jobCollection = jobsSearcher.Get();
                               foreach (ManagementObject mo in jobCollection)
                               {
                                   
                                   if ((Convert.ToUInt32(mo["TotalPages"]) > 0) && (GlobalVar.CheckDruckActive == false))
                                   {


                                        foreach (var job in LocalPrintServer.GetDefaultPrintQueue().GetPrintJobInfoCollection())
                                        {


                                            using (PrintServer ps = new PrintServer())
                                            {
                                                using (PrintQueue Warteschlange = new PrintQueue(ps, GlobalVar.PrinterName, PrintSystemDesiredAccess.AdministratePrinter))
                                                {
                                                    Warteschlange.Pause();
                                                }
                                            }
                                        }

                                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                                        {
                                                 WarnFenster Warnung = new WarnFenster();
                                                 Warnung.Show();
                                        }));

                                        GlobalVar.CheckDruckActive = true;
                                   }
                               }
                               
                          
            }
        }




        public MainWindow()
        {
            
            InitializeComponent();

            Thread druckerKontrolle = new Thread(DruckerKontrolle);
            druckerKontrolle.Start();

            

        }


    }


    }
