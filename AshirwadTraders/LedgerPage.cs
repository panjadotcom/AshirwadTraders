using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AshirwadTraders
{
    public partial class LedgerPage : Form
    {
        static string mySqlConStr = Properties.Settings.Default.MySqlConStr;
        public LedgerPage()
        {
            InitializeComponent();
        }

        private void LedgerPage_Load(object sender, EventArgs e)
        {
            string queryString = "SELECT `acc_id` as `id`, `acc_number` as `number` FROM `account` ORDER BY `acc_id`";
            MySqlConnection mySqlConnection = new MySqlConnection(mySqlConStr);
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception erropen)
            {
                MessageBox.Show("connection cannot be opened because " + erropen.Message + "");
            }
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(queryString, mySqlConnection);
            DataSet dataSet = new DataSet();
            try
            {
                mySqlDataAdapter.Fill(dataSet, "ID_COMBO_BOX");
                if (dataSet != null)
                {
                    DataRow dataRow = dataSet.Tables["ID_COMBO_BOX"].NewRow();
                    dataRow["id"] = "0";
                    dataRow["number"] = "select";
                    dataSet.Tables["ID_COMBO_BOX"].Rows.InsertAt(dataRow, 0);
                    comboBoxAccountId.DataSource = dataSet.Tables["ID_COMBO_BOX"];
                    comboBoxAccountId.ValueMember = "id";
                    comboBoxAccountId.DisplayMember = "number";
                    comboBoxAccountId.SelectedIndex = 0;
                }
            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString + " \n cannot be executed because " + errquery.Message + "");
            }
            try
            {
                mySqlConnection.Close();
            }
            catch (Exception errclose)
            {
                MessageBox.Show("Eror in closing connection : " + errclose.Message + "");
            }
            dateTimePickerStartDate.MaxDate = DateTime.Today;
            dateTimePickerEndDate.MaxDate = DateTime.Today;
            checkBoxSelectAll.Checked = true;
        }

        private void ComboBoxAccountId_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewTransaction.DataSource = null;
            textBoxBF.Text = "";
            textBoxCF.Text = "";
            textBoxTotal.Text = "";
            if (comboBoxAccountId.SelectedIndex == 0)
            {
                return;
            }
            string queryString = "SELECT " +
                "`mtrl_date` as `Date`, `mtrl_item` as `Particulars`, `mtrl_rate` as `Rate`, `mtrl_unit` as `Unit`, `mtrl_qty` as `Qty`, `mtrl_extra` as `Extra`, `mtrl_total` as `Credited`, '' as `Debited` FROM `materials` WHERE `mtrl_acc_number` = '" + comboBoxAccountId.Text + "' ";
            if (!checkBoxSelectAll.Checked)
            {
                queryString += "AND `mtrl_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `mtrl_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            }
            queryString += "UNION SELECT " +
                "`pmt_date` as `Date`, 'DEPOSITS' as `Particulars`, '' as `Rate`, `pmt_mode` as `Unit`, '' as `Qty`, '' as `Extra`, '' as `Credited`, `pmt_amount` as `Debited` FROM `payments` WHERE `pmt_acc_number` = '" + comboBoxAccountId.Text + "' ";
            if (!checkBoxSelectAll.Checked)
            {
                queryString += "AND `pmt_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `pmt_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            }
            queryString += "ORDER BY `Date`";

            MySqlConnection mySqlConnection = new MySqlConnection(mySqlConStr);
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception erropen)
            {
                MessageBox.Show("connection cannot be opened because " + erropen.Message + "");
            }
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(queryString, mySqlConnection);
            DataSet dataSet = new DataSet();
            try
            {
                mySqlDataAdapter.Fill(dataSet, "ACC_PMT_MTRL");

            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString + " \n cannot be executed because " + errquery.Message + "");
            }
            try
            {
                dataGridViewTransaction.DataSource = dataSet.Tables["ACC_PMT_MTRL"];
                dataGridViewTransaction.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTransaction.Columns["Particulars"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewTransaction.Columns["Rate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTransaction.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewTransaction.Columns["Unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTransaction.Columns["Unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewTransaction.Columns["Extra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTransaction.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewTransaction.Columns["Qty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTransaction.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewTransaction.Columns["Credited"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTransaction.Columns["Credited"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewTransaction.Columns["Debited"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTransaction.Columns["Debited"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewTransaction.ClearSelection();
            }
            catch (Exception errdataset)
            {
                MessageBox.Show("Data cannot be load because " + errdataset.Message + "");
            }

            mySqlDataAdapter.Dispose();
            queryString = "SELECT " +
                "'1' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` = '" + comboBoxAccountId.Text + "' ";
            queryString += "AND `mtrl_date` < '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            queryString += "UNION SELECT " +
                "'2' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` = '" + comboBoxAccountId.Text + "' ";
            queryString += "AND `pmt_date` < '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            
            queryString += "UNION SELECT " +
                "'3' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` = '" + comboBoxAccountId.Text + "' ";
            queryString += "AND `mtrl_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `mtrl_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            queryString += "UNION SELECT " +
                "'4' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` = '" + comboBoxAccountId.Text + "' ";
            queryString += "AND `pmt_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `pmt_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";

            queryString += "UNION SELECT " +
                "'5' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` = '" + comboBoxAccountId.Text + "' ";
            queryString += "AND `mtrl_date` > '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            queryString += "UNION SELECT " +
                "'6' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` = '" + comboBoxAccountId.Text + "' ";
            queryString += "AND `pmt_date` >'" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";

            queryString += "UNION SELECT " +
                "'7' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` = '" + comboBoxAccountId.Text + "' ";
            queryString += "UNION SELECT " +
                "'8' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` = '" + comboBoxAccountId.Text + "' ";

            queryString += "ORDER BY `Index` ";

            mySqlDataAdapter = new MySqlDataAdapter(queryString, mySqlConnection);
            try
            {
                mySqlDataAdapter.Fill(dataSet, "ACC_LDGR");
            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString + " \n cannot be executed because " + errquery.Message + "");
            }

            try
            {
                //dataGridViewTransaction.DataSource = dataSet.Tables["ACC_LDGR"];
                double amountBF = 0.00;
                double amountCF = 0.00;
                double amountTotal = 0.00;
                if (!dataSet.Tables["ACC_LDGR"].Rows[0]["Amount"].ToString().Equals(""))
                {
                    amountBF = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[0]["Amount"]);
                }
                if (!dataSet.Tables["ACC_LDGR"].Rows[1]["Amount"].ToString().Equals(""))
                {
                    amountBF = amountBF - Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[1]["Amount"]);
                }
                if (!dataSet.Tables["ACC_LDGR"].Rows[4]["Amount"].ToString().Equals(""))
                {
                    amountCF = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[4]["Amount"]);
                }
                if (!dataSet.Tables["ACC_LDGR"].Rows[5]["Amount"].ToString().Equals(""))
                {
                    amountCF = amountCF - Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[5]["Amount"]);
                }
                if (!dataSet.Tables["ACC_LDGR"].Rows[6]["Amount"].ToString().Equals(""))
                {
                    amountTotal = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[6]["Amount"]);
                }
                if (!dataSet.Tables["ACC_LDGR"].Rows[7]["Amount"].ToString().Equals(""))
                {
                    amountTotal = amountTotal - Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[7]["Amount"]);
                }
                if (checkBoxSelectAll.Checked)
                {
                    textBoxBF.Text = "";
                    textBoxCF.Text = "";
                }
                else
                {
                    textBoxBF.Text = amountBF.ToString();
                    textBoxCF.Text = amountCF.ToString();
                }
                textBoxTotal.Text = amountTotal.ToString();
            }
            catch (Exception errdataset)
            {
                MessageBox.Show("Data cannot be load because " + errdataset.Message + "");
            }
            try
            {
                mySqlConnection.Close();
            }
            catch (Exception errclose)
            {
                MessageBox.Show("Eror in closing connection : " + errclose.Message + "");
            }
        }

        private void CheckBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSelectAll.Checked)
            {
                dateTimePickerEndDate.Enabled = false;
                dateTimePickerStartDate.Enabled = false;
                ComboBoxAccountId_SelectedIndexChanged(null, null);
            }
            else
            {
                dateTimePickerEndDate.Enabled = true;
                dateTimePickerStartDate.Enabled = true;
            }
        }

        private void StartEnd_DateChanged(object sender, EventArgs e)
        {
            ComboBoxAccountId_SelectedIndexChanged(null, null);
        }
    }
}
