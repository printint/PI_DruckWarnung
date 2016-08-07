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
        static bool checkIt = false;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            WarnFenster Warnung = new WarnFenster();
            Warnung.Show();
        }

       // bool checkIt = false;

        public static void DruckerKontrolle()
        {


            



            while (true)
            {
                //Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                //           new Action(() =>
                //           {
                //           }));

                                
                               string wmiQuery = "SELECT * FROM Win32_PrintJob";
                               ManagementObjectSearcher jobsSearcher = new ManagementObjectSearcher(wmiQuery);
                               ManagementObjectCollection jobCollection = jobsSearcher.Get();
                               foreach (ManagementObject mo in jobCollection)
                               {
                                   
                                   if ((Convert.ToUInt32(mo["TotalPages"]) > 0) && (GlobalVar.checkDruckActive == false))
                                   {

                                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                        new Action(() =>
                                        {
                                                 WarnFenster Warnung = new WarnFenster();
                                                 Warnung.Show();
                                        }));

                                        GlobalVar.checkDruckActive = true;
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
