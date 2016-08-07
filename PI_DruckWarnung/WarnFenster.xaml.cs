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
using System.Windows.Shapes;
using System.Management;

namespace PI_DruckWarnung
{
    /// <summary>
    /// Interaktionslogik für WarnFenster.xaml
    /// </summary>
    public partial class WarnFenster : Window
    {
        public WarnFenster()
        {
            InitializeComponent();
            //ReadDruckerInfo info = new ReadDruckerInfo();                        
            //lblDrucker.Content = info.DruckerKontrolle();
            GetPrinterJobs();
          


    }
        private void GetPrinterJobs()
        {
            UInt32 totalPages = 0;
            string wmiQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher jobsSearcher = new ManagementObjectSearcher(wmiQuery);
            ManagementObjectCollection jobCollection = jobsSearcher.Get();
           

            foreach (ManagementObject mo in jobCollection)
            {
                if (Convert.ToUInt32(mo["TotalPages"]) > 0)
                {
                    //PrintJob printJob = new PrintJob();
                    //printJob.Caption = (string)mo["Caption"];
                    //printJob.DataType = (string)mo["DataType"];
                    //printJob.Description = (string)mo["Description"];
                    //printJob.Document = (string)mo["Document"];
                    //printJob.DriverName = (string)mo["DriverName"];
                    //printJob.ElapsedTime = (string)mo["ElapsedTime"];
                    //printJob.HostPrintQueue = (string)mo["HostPrintQueue"];
                    //printJob.InstallDate = (string)mo["InstallDate"];
                    //printJob.JobId = Convert.ToUInt32(mo["JobId"]);
                    //printJob.JobStatus = (string)mo["JobStatus"];
                    //printJob.Name = (string)mo["Name"];
                    //printJob.Notify = (string)mo["Notify"];
                    //printJob.Owner = (string)mo["Owner"];
                    //printJob.PagesPrinted = Convert.ToUInt32(mo["PagesPrinted"]);
                    //printJob.Parameters = (string)mo["Parameters"];
                    //printJob.PrintProcessor = (string)mo["PrintProcessor"];
                    //printJob.Priority = Convert.ToUInt32(mo["Priority"]);
                    //printJob.Size = Convert.ToUInt32(mo["Size"]);
                    //printJob.StartTime = (string)mo["StartTime"];
                    //printJob.Status = (string)mo["Status"];
                    //printJob.StatusMask = Convert.ToUInt32(mo["StatusMask"]);
                    //printJob.TimeSubmitted = (string)mo["TimeSubmitted"];
                    //printJob.TotalPages = Convert.ToUInt32(mo["TotalPages"]);
                    //printJob.UntilTime = (string)mo["UntilTime"];

                    lblDrucker.Content = (string)mo["Name"];
                    totalPages += (uint)(mo["TotalPages"]);
                }
            }

            lblFarbmodus.Content = totalPages;

        }

        private void btnAuftragBestaetigt_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.checkDruckActive = false;
            this.Close();
        }

        private void btn_AuftragAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            GlobalVar.checkDruckActive = false;
            this.Close();
        }
    }
}
