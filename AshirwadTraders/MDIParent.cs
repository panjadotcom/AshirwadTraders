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

        private void CommonToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            Close_Opened_Forms();
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == accountToolStripMenuItem)
            {
                formViewer = new AccountsPage();
            }
            else if (menuItem == transactionToolStripMenuItem)
            {
                formViewer = new TransactionsPage();
            }
            else if (menuItem == ledgerToolStripMenuItem)
            {
                formViewer = new LedgerPage();
            }
            else if (menuItem == profitLossToolStripMenuItem)
            {
                formViewer = new ProfitLossPage("MATERIALS, PAYMENTS");
            }
            else if (menuItem == saleAllMaterialToolStripMenuItem)
            {
                formViewer = new ProfitLossPage("MATERIALS");
            }
            else if (menuItem == allPaymentToolStripMenuItem)
            {
                formViewer = new ProfitLossPage("PAYMENTS");
            }
            else
            {
                //MessageBox.Show("Error in Menu Items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formViewer.WindowState = FormWindowState.Maximized;
            formViewer.MdiParent = this;
            formViewer.ControlBox = false;
            formViewer.Show();
        }
    }
}
