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
        private void button_Click(object sender, RoutedEventArgs e)
        {
            WarnFenster Warnung = new WarnFenster();
            Warnung.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        public static void DruckerKontrolle()
        {
            string printerName = "";
            

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
                    printerName = DruckerName;
                   

                }
               

            }



            //MessageBox.Show(AlleDrucker);



            while (true)
            {

                foreach (var job in LocalPrintServer.GetDefaultPrintQueue().GetPrintJobInfoCollection())
                {


                    using (PrintServer ps = new PrintServer())
                    {
                        using (PrintQueue Warteschlange = new PrintQueue(ps, printerName,
                              PrintSystemDesiredAccess.AdministratePrinter))
                        {
                            Warteschlange.Pause();

                            Thread.Sleep(3000);

                            if (job.NumberOfPages != 0)
                            {

                                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() =>
                            {
                                WarnFenster Warnung = new WarnFenster();
                                Warnung.Show();


                            }));
                                
                            }



                        }
                    }
                }

            }
        }

        public MainWindow()
        {
            ReadDruckerInfo Name = new ReadDruckerInfo();
            InitializeComponent();

            Thread druckerKontrolle = new Thread(DruckerKontrolle);

            druckerKontrolle.Start();

            

        }


    }


    }
