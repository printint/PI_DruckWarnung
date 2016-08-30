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
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace WpfApplication1
{
    [StructLayout(LayoutKind.Sequential)]
    struct PRINTER_DEFAULTS
    {
        public IntPtr pDatatype;
        public IntPtr pDevMode;
        public int DesiredAccess;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct PRINTER_INFO_2
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pServerName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrinterName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pShareName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPortName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDriverName;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pComment;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pLocation;
        public IntPtr pDevMode;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pSepFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pPrintProcessor;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDatatype;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pParameters;
        public IntPtr pSecurityDescriptor;
        public uint Attributes;
        public uint Priority;
        public uint DefaultPriority;
        public uint StartTime;
        public uint UntilTime;
        public uint Status;
        public uint cJobs;
        public uint AveragePPM;
    }

    struct POINTL
    {
        public Int32 x;
        public Int32 y;
    }

    [Flags()]
    enum DM : int
    {
        Orientation = 0x1,
        PaperSize = 0x2,
        PaperLength = 0x4,
        PaperWidth = 0x8,
        Scale = 0x10,
        Position = 0x20,
        NUP = 0x40,
        DisplayOrientation = 0x80,
        Copies = 0x100,
        DefaultSource = 0x200,
        PrintQuality = 0x400,
        Color = 0x800,
        Duplex = 0x1000,
        YResolution = 0x2000,
        TTOption = 0x4000,
        Collate = 0x8000,
        FormName = 0x10000,
        LogPixels = 0x20000,
        BitsPerPixel = 0x40000,
        PelsWidth = 0x80000,
        PelsHeight = 0x100000,
        DisplayFlags = 0x200000,
        DisplayFrequency = 0x400000,
        ICMMethod = 0x800000,
        ICMIntent = 0x1000000,
        MediaType = 0x2000000,
        DitherType = 0x4000000,
        PanningWidth = 0x8000000,
        PanningHeight = 0x10000000,
        DisplayFixedOutput = 0x20000000
    }


    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    struct DEVMODE
    {
        public const int CCHDEVICENAME = 32;
        public const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        [System.Runtime.InteropServices.FieldOffset(0)]
        public string dmDeviceName;
        [System.Runtime.InteropServices.FieldOffset(32)]
        public Int16 dmSpecVersion;
        [System.Runtime.InteropServices.FieldOffset(34)]
        public Int16 dmDriverVersion;
        [System.Runtime.InteropServices.FieldOffset(36)]
        public Int16 dmSize;
        [System.Runtime.InteropServices.FieldOffset(38)]
        public Int16 dmDriverExtra;
        [System.Runtime.InteropServices.FieldOffset(40)]
        public DM dmFields;

        [System.Runtime.InteropServices.FieldOffset(44)]
        Int16 dmOrientation;
        [System.Runtime.InteropServices.FieldOffset(46)]
        public Int16 dmPaperSize;
        [System.Runtime.InteropServices.FieldOffset(48)]
        Int16 dmPaperLength;
        [System.Runtime.InteropServices.FieldOffset(50)]
        Int16 dmPaperWidth;
        [System.Runtime.InteropServices.FieldOffset(52)]
        Int16 dmScale;
        [System.Runtime.InteropServices.FieldOffset(54)]
        public Int16 dmCopies;
        [System.Runtime.InteropServices.FieldOffset(56)]
        Int16 dmDefaultSource;
        [System.Runtime.InteropServices.FieldOffset(58)]
        Int16 dmPrintQuality;

        [System.Runtime.InteropServices.FieldOffset(44)]
        public POINTL dmPosition;
        [System.Runtime.InteropServices.FieldOffset(52)]
        public Int32 dmDisplayOrientation;
        [System.Runtime.InteropServices.FieldOffset(56)]
        public Int32 dmDisplayFixedOutput;

        [System.Runtime.InteropServices.FieldOffset(60)]
        public short dmColor;
        [System.Runtime.InteropServices.FieldOffset(62)]
        public short dmDuplex;
        [System.Runtime.InteropServices.FieldOffset(64)]
        public short dmYResolution;
        [System.Runtime.InteropServices.FieldOffset(66)]
        public short dmTTOption;
        [System.Runtime.InteropServices.FieldOffset(68)]
        public short dmCollate;
        [System.Runtime.InteropServices.FieldOffset(72)]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        [System.Runtime.InteropServices.FieldOffset(102)]
        public Int16 dmLogPixels;
        [System.Runtime.InteropServices.FieldOffset(104)]
        public Int32 dmBitsPerPel;
        [System.Runtime.InteropServices.FieldOffset(108)]
        public Int32 dmPelsWidth;
        [System.Runtime.InteropServices.FieldOffset(112)]
        public Int32 dmPelsHeight;
        [System.Runtime.InteropServices.FieldOffset(116)]
        public Int32 dmDisplayFlags;
        [System.Runtime.InteropServices.FieldOffset(116)]
        public Int32 dmNup;
        [System.Runtime.InteropServices.FieldOffset(120)]
        public Int32 dmDisplayFrequency;
    }

    class PrinterSettings
    {
        private IntPtr gPrinter = new System.IntPtr();
        private PRINTER_DEFAULTS gPrinterValues = new PRINTER_DEFAULTS();
        private PRINTER_INFO_2 gPInfo = new PRINTER_INFO_2();
        private DEVMODE gDevMode;
        private IntPtr gPtrDevMode;
        private IntPtr gPtrPrinterInfo;
        private int gSizeOfDevMode = 0;
        private int gLastError;
        private int gNBytesNeeded;
        private long gNRet;
        private int gIntError;
        private int gNTemporary;
        private IntPtr gDevModeData;

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesA", SetLastError = true,
        ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter,
         [MarshalAs(UnmanagedType.LPStr)] string pDeviceName,
        IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetPrinter(IntPtr hPrinter, Int32 dwLevel, IntPtr pPrinter, Int32 dwBuf, out Int32 dwNeeded);

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA",
            SetLastError = true, CharSet = CharSet.Ansi,
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter,
            out IntPtr hPrinter, ref PRINTER_DEFAULTS pd);

        [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern bool SetPrinter(IntPtr hPrinter, int Level,
                                           IntPtr pPrinter, int Command);

        private const int DM_DUPLEX = 4096; //0x1000
        private const int DM_IN_BUFFER = 8;
        private const int DM_OUT_BUFFER = 2;
        private const int PRINTER_ACCESS_ADMINISTER = 4; //0x4
        private const int STANDARD_RIGHTS_REQUIRED = 983040; //0xF0000
        private const int PRINTER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE;
        private const int PRINTER_ACCESS_USE = 8; //0x8

        public bool ChangePrinterSetting(string iPrinterName)
        {
            gDevMode = this.GetPrinterSettings(iPrinterName);
            Marshal.StructureToPtr(gDevMode, gDevModeData, false);
            gPInfo.pDevMode = gDevModeData;
            gPInfo.pSecurityDescriptor = IntPtr.Zero;
            //bring up the printer preferences dialog
            DocumentProperties(IntPtr.Zero, gPrinter, iPrinterName, gDevModeData
                , gPInfo.pDevMode, DM_IN_BUFFER | DM_OUT_BUFFER | PRINTER_ACCESS_ADMINISTER);
            //update driver dependent part of the DEVMODE 
            Marshal.StructureToPtr(gPInfo, gPtrPrinterInfo, false);
            gLastError = Marshal.GetLastWin32Error();
            gNRet = Convert.ToInt16(SetPrinter(gPrinter, 2, gPtrPrinterInfo, 0));
            if (gNRet == 0)
            {
                //Unable to set extern printer settings.
                gLastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(gLastError);
            }
            if (gPrinter != IntPtr.Zero)
            {
                ClosePrinter(gPrinter);
            }
            return Convert.ToBoolean(gNRet);
        }

        public DEVMODE GetPrinterSettings(string PrinterName)
        {
            DEVMODE lDevMode;
            gPrinterValues.pDatatype = IntPtr.Zero;
            gPrinterValues.pDevMode = IntPtr.Zero;
            gPrinterValues.DesiredAccess = PRINTER_ALL_ACCESS;

            // HERE CRASHES
            gNRet = Convert.ToInt32(OpenPrinter(PrinterName, out gPrinter, ref gPrinterValues));

            if (gNRet == 0)
            {
                gLastError = Marshal.GetLastWin32Error();
                throw new Win32Exception(gLastError);
            }

            GetPrinter(gPrinter, 2, IntPtr.Zero, 0, out gNBytesNeeded);
            if (gNBytesNeeded <= 0)
                throw new System.Exception("Unable to allocate memory");
            else
            {
                // Allocate enough space for PRINTER_INFO_2... 
                gPtrPrinterInfo = Marshal.AllocCoTaskMem(gNBytesNeeded);
                gPtrPrinterInfo = Marshal.AllocHGlobal(gNBytesNeeded);
                //The second GetPrinter fills in all the current settings, so all you 
                //need to do is modify what youre interested in...
                gNRet = Convert.ToInt32(GetPrinter(gPrinter, 2,
                    gPtrPrinterInfo, gNBytesNeeded, out gNTemporary));
                if (gNRet == 0)
                {
                    gLastError = Marshal.GetLastWin32Error();
                    throw new Win32Exception(gLastError);
                }
                gPInfo = (PRINTER_INFO_2)Marshal.PtrToStructure(gPtrPrinterInfo, typeof(PRINTER_INFO_2));
                IntPtr lTempBuffer = new IntPtr();
                if (gPInfo.pDevMode == IntPtr.Zero)
                {
                    //if GetPrinter didnt fill in the DEVMODE, try to get it by calling
                    //DocumentProperties...
                    IntPtr ptrZero = IntPtr.Zero;
                    //get the size of the devmode struct
                    gSizeOfDevMode = DocumentProperties(IntPtr.Zero, gPrinter,
                                       PrinterName, ptrZero, ptrZero, 0);
                    gPtrDevMode = Marshal.AllocCoTaskMem(gSizeOfDevMode);
                    int i = DocumentProperties(IntPtr.Zero, gPrinter, PrinterName, gPtrDevMode,
                    ptrZero, DM_OUT_BUFFER);
                    if (i < 0 || gPtrDevMode != IntPtr.Zero)
                    {
                        //Cannot get the DEVMODE struct.
                        throw new System.Exception("Cannot get DEVMODE data");
                    }
                    gPInfo.pDevMode = gPtrDevMode;
                }
                gIntError = DocumentProperties(IntPtr.Zero, gPrinter,
                          PrinterName, IntPtr.Zero, lTempBuffer, 0);
                gDevModeData = Marshal.AllocHGlobal(gIntError);
                gIntError = DocumentProperties(IntPtr.Zero, gPrinter,
                         PrinterName, gDevModeData, lTempBuffer, 2);
                lDevMode = (DEVMODE)Marshal.PtrToStructure(gDevModeData, typeof(DEVMODE));
                if (gNRet == 0 || gPrinter == IntPtr.Zero)
                {
                    gLastError = Marshal.GetLastWin32Error();
                    throw new Win32Exception(gLastError);
                }
                return lDevMode;
                
            }
            
        }
    }            
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
        public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PrinterSettings drucker = new PrinterSettings();
            DEVMODE bub = new DEVMODE();
            
               bub = drucker.GetPrinterSettings("[M10] Aficio MPC 4501");
         
     


       
            foreach()


                        //bub.dmCopies = 4;

                        label.Content = bub.dmSpecVersion;



        }
    }
}
