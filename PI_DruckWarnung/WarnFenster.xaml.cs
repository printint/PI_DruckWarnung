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
            ReadDruckerInfo info = new ReadDruckerInfo();                        
            lblDrucker.Content = info.DruckerKontrolle();
        }
    }
}
