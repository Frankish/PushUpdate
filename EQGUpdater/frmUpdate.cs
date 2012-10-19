using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PushUpdate
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e) // Cancel the operation
        {
            string msg = "Are you sure you wish to cancel the update?";
            string cap = "Cancel Update?";

            DialogResult result = MessageBox.Show(msg, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // Operation timeout (10 seconds)
        {
            //On tick close dialog with OK result
            this.DialogResult = DialogResult.OK;
        }
    }
}
