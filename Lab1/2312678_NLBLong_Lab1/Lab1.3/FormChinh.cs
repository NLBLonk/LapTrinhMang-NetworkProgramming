using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab1._3
{
    public partial class FormChinh : Form
    {
        public FormChinh()
        {
            InitializeComponent();
        }

        private void btn_PhanGiai_Click(object sender, EventArgs e)
        {
            string domainName = txtTenMien.Text; // Get the domain name from the textbox

            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(domainName);

                rTxtBoxThongTin.Text = $"Phan giai ten mien: {domainName}\n"; // Display resolved domain name
                rTxtBoxThongTin.Text += $"Ten mien: {hostInfo.HostName}\n"; // Display host name
                rTxtBoxThongTin.Text += "Dia chi IP:\n";

                foreach (IPAddress ipaddr in hostInfo.AddressList)
                {
                    rTxtBoxThongTin.Text += $"  {ipaddr}\n"; // Display IP addresses with indentation
                }
            }
            catch (Exception ex)
            {
                rTxtBoxThongTin.Text = $"Khong phan giai duoc ten mien: '{domainName}'. ({ex.Message})";
            }
        }
    }
}
