using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFTest
{
    public partial class Form1 : Form
    {
        string otp;
        int status, error;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (otp.Equals(otPassword.Text))
            {
                MessageBox.Show("Successful MFA!");
            } else
            {
                MessageBox.Show("Failed to enter proper one time password");
            }
            otp = String.Empty;
            status = 0;
            error = 0;
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            txtUsername.Text = "";
            txtPassword.Text = "";
            otPassword.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "test" || txtPassword.Text != "test")
            {
                MessageBox.Show("Intial user credentials invalid");
                return;
            }

            PfAuthParams pfAuthParams = new PfAuthParams();

            pfAuthParams.Username = txtUsername.Text;
            pfAuthParams.Phone = "8622664878";
            pfAuthParams.Mode = pf_auth.MODE_SMS_ONE_WAY_OTP;

            pfAuthParams.CertFilePath = System.IO.Directory.GetCurrentDirectory() + "\\cert_key.p12";

            if (pf_auth.pf_authenticate(pfAuthParams, out otp, out status, out error))
            {
                MessageBox.Show("User must enter " + otp + " to continue!");
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
            } else
            {
                MessageBox.Show("Status: " + status + " Error: " + error);
                otp = String.Empty;
                status = 0;
                error = 0;
            }
        }
    }
}
