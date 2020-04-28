using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PW_DropBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateThreadsDetailsList();
        }

        private void UpdateThreadsDetailsList()
        {
            this.lvwThreadsDetails.Clear();

            foreach(var thread in Server.ThreadStates)
            {
                this.lvwThreadsDetails.Items.Add( new ListViewItem(thread.ToString()));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateThreadsDetailsList();
        }
    }
}
