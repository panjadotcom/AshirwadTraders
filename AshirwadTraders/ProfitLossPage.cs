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
    public partial class ProfitLossPage : Form
    {
        static string mySqlConStr = Properties.Settings.Default.MySqlConStr;
        public ProfitLossPage()
        {
            InitializeComponent();
        }

        private void ProfitLossPage_Load(object sender, EventArgs e)
        {
            var options = new string[] { "SELECT", "MATERIALS", "PAYMENTS" };
            dateTimePickerStartDate.MaxDate = DateTime.Today;
            dateTimePickerEndDate.MaxDate = DateTime.Today;
            checkBoxSelectAll.Checked = true;
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("CMB_BOX_OPT");
            DataColumn dataColumn = new DataColumn("OPTION", typeof(string));
            dataTable.Columns.Add(dataColumn);
            for (int index = 0; index < options.Length; index++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["OPTION"] = options[index];
                dataTable.Rows.Add(dataRow);
            }
            dataSet.Tables.Add(dataTable);
            comboBoxAccountId.DataSource = dataSet.Tables["CMB_BOX_OPT"];
            comboBoxAccountId.ValueMember = "OPTION";
            comboBoxAccountId.DisplayMember = "OPTION";
            comboBoxAccountId.SelectedIndex = 0;
        }

        private void ComboBoxAccountId_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* function to halde combobox change event */
            dataGridViewTransaction.DataSource = null;
            textBoxExp.Text = "";
            textBoxRevenue.Text = "";
            textBoxTotal.Text = "";
            string queryString = "";
            if (comboBoxAccountId.SelectedIndex == 0)
            {
                return;
            }
            else if (comboBoxAccountId.SelectedIndex == 1)
            {
                queryString = "SELECT " +
                "`mtrl_date` as `Date`, 'Self' as `Account`, `mtrl_item` as `Particulars`, `mtrl_rate` as `Rate`, `mtrl_unit` as `Unit`, `mtrl_qty` as `Qty`, `mtrl_extra` as `Extra`, `mtrl_total` as `Purchased`, '' as `Sell` FROM `materials` WHERE `mtrl_acc_number` = (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
                if (!checkBoxSelectAll.Checked)
                {
                    queryString += "AND `mtrl_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `mtrl_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
                }
                queryString += "UNION SELECT " +
                    "`mtrl_date` as `Date`, `mtrl_acc_number` as `Account`, `mtrl_item` as `Particulars`, `mtrl_rate` as `Rate`, `mtrl_unit` as `Unit`, `mtrl_qty` as `Qty`, `mtrl_extra` as `Extra`, '' as `Purchased`, `mtrl_total` as `Sell` FROM `materials` WHERE `mtrl_acc_number` != (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
                if (!checkBoxSelectAll.Checked)
                {
                    queryString += "AND `mtrl_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `mtrl_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
                }
                queryString += "ORDER BY `Date` DESC";
            }
            else if (comboBoxAccountId.SelectedIndex == 2)
            {
                queryString = "SELECT " +
                    "`pmt_date` as `Date`, 'Self' as `Account`, 'Paid' as `Particulars`, `pmt_mode` as `Mode`, `pmt_info` as `Info`, '' as `Recieved`, `pmt_amount` as `Paid` FROM `payments` WHERE `pmt_acc_number` = (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
                if (!checkBoxSelectAll.Checked)
                {
                    queryString += "AND `pmt_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `pmt_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
                }
                queryString += "UNION SELECT " +
                    "`pmt_date` as `Date`, `pmt_acc_number` as `Account`, 'Recieved' as `Particulars`, `pmt_mode` as `Mode`, `pmt_info` as `Info`, `pmt_amount` as `Recieved`, '' as `Paid` FROM `payments` WHERE `pmt_acc_number` != (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
                if (!checkBoxSelectAll.Checked)
                {
                    queryString += "AND `pmt_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `pmt_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
                }
                queryString += "ORDER BY `Date` DESC";
            }
            else
            {
                return;
            }

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
                if (comboBoxAccountId.SelectedIndex == 1)
                {
                    dataGridViewTransaction.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Account"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Particulars"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewTransaction.Columns["Rate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Rate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Extra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Extra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Qty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Purchased"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Purchased"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Sell"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Sell"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (comboBoxAccountId.SelectedIndex == 2)
                {
                    //"`pmt_date` as `Date`, 'Self' as `Account`, 'Paid' as `Particulars`, `pmt_mode` as `Mode`, `pmt_info` as `Info`, '' as `Recieved`, `pmt_amount` as `Paid` FROM `payments` WHERE `pmt_acc_number` = (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
                    dataGridViewTransaction.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Account"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Particulars"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridViewTransaction.Columns["Mode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Mode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Info"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Info"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Recieved"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Recieved"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransaction.Columns["Paid"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTransaction.Columns["Paid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                dataGridViewTransaction.ClearSelection();
            }
            catch (Exception errdataset)
            {
                MessageBox.Show("Data cannot be load because " + errdataset.Message + "");
            }

            mySqlDataAdapter.Dispose();
            queryString = "SELECT " +
                "'1' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` = (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
            queryString += "AND `mtrl_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `mtrl_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            queryString += "UNION SELECT " +
                "'2' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` = (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
            queryString += "AND `pmt_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `pmt_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";

            queryString += "UNION SELECT " +
                "'3' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` != (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
            queryString += "AND `mtrl_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `mtrl_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            queryString += "UNION SELECT " +
                "'4' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` != (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
            queryString += "AND `pmt_date` >= '" + dateTimePickerStartDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' AND `pmt_date` <= '" + dateTimePickerEndDate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
            
            queryString += "UNION SELECT " +
                "'5' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` = (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
            queryString += "UNION SELECT " +
                "'6' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` = (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";

            queryString += "UNION SELECT " +
                "'7' as `Index`, SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` != (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";
            queryString += "UNION SELECT " +
                "'8' as `Index`, SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` != (SELECT `acc_number` FROM `account` WHERE `acc_id` = 1) ";

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
                double totalExp = 0.00;
                double totalRvnu = 0.00;
                double amountProfitLoss = 0.00;
                if (checkBoxSelectAll.Checked)
                {
                    if (comboBoxAccountId.SelectedIndex == 1)
                    {
                        if (!dataSet.Tables["ACC_LDGR"].Rows[4]["Amount"].ToString().Equals(""))
                            totalExp = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[4]["Amount"]);
                        if (!dataSet.Tables["ACC_LDGR"].Rows[6]["Amount"].ToString().Equals(""))
                            totalRvnu = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[6]["Amount"]);
                    }
                    else
                    {
                        if (!dataSet.Tables["ACC_LDGR"].Rows[7]["Amount"].ToString().Equals(""))
                            totalRvnu = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[7]["Amount"]);
                        if (!dataSet.Tables["ACC_LDGR"].Rows[5]["Amount"].ToString().Equals(""))
                            totalExp = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[5]["Amount"]);
                    }
                }
                else
                {
                    if (comboBoxAccountId.SelectedIndex == 1)
                    {
                        if (!dataSet.Tables["ACC_LDGR"].Rows[0]["Amount"].ToString().Equals(""))
                            totalExp = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[0]["Amount"]);
                        if (!dataSet.Tables["ACC_LDGR"].Rows[2]["Amount"].ToString().Equals(""))
                            totalRvnu = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[2]["Amount"]);
                    }
                    else
                    {
                        if (!dataSet.Tables["ACC_LDGR"].Rows[3]["Amount"].ToString().Equals(""))
                            totalRvnu = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[3]["Amount"]);
                        if (!dataSet.Tables["ACC_LDGR"].Rows[1]["Amount"].ToString().Equals(""))
                            totalExp = Convert.ToDouble(dataSet.Tables["ACC_LDGR"].Rows[1]["Amount"]);
                    }
                }
                amountProfitLoss = totalRvnu - totalExp;
                textBoxExp.Text = String.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-IN"), "{0:c2}", totalExp);
                textBoxRevenue.Text = String.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-IN"), "{0:c2}", totalRvnu);
                if (amountProfitLoss < 0)
                {
                    labelTotal.Text = "Loss";
                    amountProfitLoss = 0 - amountProfitLoss;
                }
                else
                {
                    labelTotal.Text = "Profit";
                }
                textBoxTotal.Text = String.Format(System.Globalization.CultureInfo.CreateSpecificCulture("en-IN"), "{0:c2}", amountProfitLoss);
            }
            catch (Exception errdataset)
            {
                MessageBox.Show("Price cannot be loaded : " + errdataset.Message + "");
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
