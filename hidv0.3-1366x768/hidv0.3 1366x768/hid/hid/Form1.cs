using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UsbLibrary;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace hid
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);

        [DllImport("user32.dll")]

        private static extern int ShowWindow(int hwnd, int command);


        private const int SW_HIDE = 0;

        private const int SW_SHOW = 1;


        int zaman = 2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Pto_USB.ProductId = 0x0005;     
            this.Pto_USB.VendorId = 0x1234;
            this.Pto_USB.CheckDevicePresent();
        }
        private void usb_OnDeviceArrived(object sender, EventArgs e)
        {
            zaman = 2;
            this.Visible = true;
            label1.Visible = false;
            button1.Visible = true;
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);
        }

        private void usb_OnDeviceRemoved(object sender, EventArgs e)
        {
            
                zaman = 2;
                this.Visible = false;
                label1.Visible = false;
                button1.Visible = true;
                int hwnd = FindWindow("Shell_TrayWnd", "");
                ShowWindow(hwnd, SW_SHOW);

            }

        private void usb_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {

        }

        private void usb_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
           
        }

        private void usb_OnDataSend(object sender, EventArgs e)
        {
           
        }
        private void usb_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
           
        }
        protected override void WndProc(ref Message m)
        {
            Pto_USB.ParseMessages(ref m);
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            
            foreach (DriveInfo disk in DriveInfo.GetDrives()) // sürücü bilgilerini al
            {


                if (disk.DriveType == DriveType.Removable) // Eğer sürücü takılırsa string tipinde ki str değişkenine sürücü harfini gönder,şu işlemi yap
                {
                    textBox1.Clear();
                    string str = disk.Name.ToString();
                    textBox1.Text = str;
                    button1.Visible = false;
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // önce listboxu temizle

            DirectoryInfo di = new DirectoryInfo(textBox1.Text + "");  //kontrol edilecek disk etiketini belirle
            FileInfo[] files = di.GetFiles("*.lnk"); // Belirtilen disk etiketinde .lnk uzantısı ara
            foreach (FileInfo fi in files)
            {
                listBox1.Items.Add(fi.Name); // .lnk uzantılarını listboxa item olarak atama
            }
            int x = listBox1.Items.Count; // listbox üzerinde ki itemleri int tipinde ki x değişkenine ata

            if (x > 0) // eğer x 0'dan büyükse şu işlemi yap 
            {
                label1.Visible = true;
                button2.Visible = false;
            }
            else // değilse
            {
                int hwnd = FindWindow("Shell_TrayWnd", "");
                ShowWindow(hwnd, SW_SHOW);
                this.Visible = false;
                label1.Visible = false;
                button2.Visible = false;
                button1.Visible = true;

                }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zaman--;
            if (zaman == 0)
            {
                timer1.Enabled = false;
                button2.Visible = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
                System.Diagnostics.Process.Start("shutdown", "-f -s -t 0");
            e.Handled = true;
            if (e.Control)
                System.Diagnostics.Process.Start("shutdown", "-f -s -t 0");
            e.Handled = true;
            if (e.KeyCode == Keys.LWin)
                System.Diagnostics.Process.Start("Shutdown", " -f -s -t 0");
            if (e.KeyCode == Keys.RWin)
                System.Diagnostics.Process.Start("Shutdown", " -f -s -t 0");
            e.Handled = true;
            if (e.KeyCode == Keys.Escape)
                System.Diagnostics.Process.Start("Shutdown", "-f -s -t 0");
            e.Handled = true;
        }
        
    }
    
}

