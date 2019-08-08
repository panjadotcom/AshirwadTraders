using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AshirwadTraders
{
    public partial class MDIParent : Form
    {
        Form formViewer;
        public MDIParent()
        {
            InitializeComponent();
        }

        private void Close_Opened_Forms()
        {
            if (formViewer != null)
            {
                formViewer.Close();
            }
        }

        private void AccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close_Opened_Forms();
            formViewer = new AccountsPage();
            formViewer.WindowState = FormWindowState.Maximized;
            formViewer.MdiParent = this;
            formViewer.ControlBox = false;
            formViewer.Show();
        }

        private void TransactionToolStripMenuItem_clicked(object sender, EventArgs e)
        {
            Close_Opened_Forms();
            formViewer = new TransactionsPage();
            formViewer.WindowState = FormWindowState.Maximized;
            formViewer.MdiParent = this;
            formViewer.ControlBox = false;
            formViewer.Show();
        }
    }
}
