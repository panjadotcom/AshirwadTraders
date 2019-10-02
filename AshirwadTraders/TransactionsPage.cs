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
    public partial class TransactionsPage : Form
    {
        static string mySqlConStr = Properties.Settings.Default.MySqlConStr;
        public TransactionsPage()
        {
            InitializeComponent();
        }

        private void TransactionsPage_Load(object sender, EventArgs e)
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
        }

        private void ComboBoxAccountId_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxAccInfo.Text = "";
            textBoxQty.Text = "0";
            textBoxRate.Text = "0";
            textBoxTotal.Text = "0";
            textBoxUnit.Text = "";
            textBoxExtra.Text = "";
            textBoxAmount.Text = "";
            textBoxPmtId.Text = "";
            comboBoxPmtMode.SelectedIndex = 0;
            dataGridViewMaterial.DataSource = null;
            dataGridViewPayment.DataSource = null;
            buttonDelete.Enabled = false;
            comboBoxItems.DataSource = null;
            buttonUpdate.Enabled = false;
            dateTimePickerTransaction.Value = DateTime.Now;
            if (comboBoxAccountId.SelectedIndex == 0)
            {
                return;
            }
            buttonDelete.Enabled = true;
            buttonUpdate.Enabled = true;
            string queryString = "SELECT * FROM `account` WHERE `acc_id` = " + comboBoxAccountId.SelectedValue.ToString();
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
                mySqlDataAdapter.Fill(dataSet, "ID_DATA");
                
            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString + " \n cannot be executed because " + errquery.Message + "");
            }
            try
            {
                if (dataSet.Tables["ID_DATA"].Rows.Count > 0)
                {
                    textBoxAccInfo.Text = "NAME = " + dataSet.Tables["ID_DATA"].Rows[0]["acc_name"].ToString();
                    textBoxAccInfo.Text += "\r\nPHONE = " + dataSet.Tables["ID_DATA"].Rows[0]["acc_phone"].ToString();
                    textBoxAccInfo.Text += "\r\nADDRESS = " + dataSet.Tables["ID_DATA"].Rows[0]["acc_address"].ToString();
                    textBoxAccInfo.Text += "\r\nINFO = " + dataSet.Tables["ID_DATA"].Rows[0]["acc_info"].ToString();
                }
            }
            catch (Exception errdataset)
            {
                MessageBox.Show("Data cannot be load because " + errdataset.Message + "");
            }
            mySqlDataAdapter.Dispose();
            queryString = "SELECT `mtrl_id` as `Id`, `mtrl_date` as `Date`, `mtrl_item` as `Item`, `mtrl_unit` as `Unit` , `mtrl_rate` as `Rate`, `mtrl_qty` as `Qty`, `mtrl_extra` as `Extra` , `mtrl_total` as `Amount`  FROM `materials` WHERE `mtrl_acc_number` = '" + comboBoxAccountId.Text + "' " +
                "union " +
                "SELECT '------', current_date(), 'Total', '', '', '', '', SUM(`mtrl_total`) as `Amount` FROM `materials` WHERE `mtrl_acc_number` = '" + comboBoxAccountId.Text + "' " +
                "ORDER BY `Date` ";
            mySqlDataAdapter = new MySqlDataAdapter(queryString, mySqlConnection);
            try
            {
                mySqlDataAdapter.Fill(dataSet, "ACC_MTRL");
                
            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString + " \n cannot be executed because " + errquery.Message + "");
            }

            try
            {
                dataGridViewMaterial.DataSource = dataSet.Tables["ACC_MTRL"];
                dataGridViewMaterial.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewMaterial.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewMaterial.Columns["Item"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewMaterial.Columns["Unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewMaterial.Columns["Rate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewMaterial.Columns["Qty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewMaterial.Columns["Extra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewMaterial.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewMaterial.ClearSelection();
            }
            catch (Exception errdataset)
            {
                MessageBox.Show("Data cannot be load because " + errdataset.Message + "");
            }
            mySqlDataAdapter.Dispose();
            queryString = "SELECT `pmt_id` as `Id`, `pmt_date` as `Date`, `pmt_info` as `Info`, `pmt_mode` as `Mode`, `pmt_amount` as `Amount` FROM `payments` WHERE `pmt_acc_number` = '" + comboBoxAccountId.Text + "' " +
                "union " +
                "SELECT '------', current_date(), 'Total', '', SUM(`pmt_amount`) as `Amount` FROM `payments` WHERE `pmt_acc_number` = '" + comboBoxAccountId.Text + "' " +
                "ORDER BY `Date`";
            mySqlDataAdapter = new MySqlDataAdapter(queryString, mySqlConnection);
            try
            {
                mySqlDataAdapter.Fill(dataSet, "ACC_PMT");

            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString + " \n cannot be executed because " + errquery.Message + "");
            }

            try
            {
                dataGridViewPayment.DataSource = dataSet.Tables["ACC_PMT"];
                dataGridViewPayment.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridViewPayment.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewPayment.Columns["Mode"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewPayment.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewPayment.Columns["Info"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewPayment.ClearSelection();
            }
            catch (Exception errdataset)
            {
                MessageBox.Show("Data cannot be load because " + errdataset.Message + "");
            }
            mySqlDataAdapter.Dispose();
            queryString = "SELECT DISTINCT `mtrl_item` as `Item` FROM `materials` ORDER BY `mtrl_item`";
            mySqlDataAdapter = new MySqlDataAdapter(queryString, mySqlConnection);
            try
            {
                mySqlDataAdapter.Fill(dataSet, "ITEM_COMBO_BOX");
            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString + " \n cannot be executed because " + errquery.Message + "");
            }
            try
            {
                if (dataSet.Tables["ITEM_COMBO_BOX"].Rows.Count > 0)
                {
                    DataRow dataRow = dataSet.Tables["ITEM_COMBO_BOX"].NewRow();
                    if (comboBoxAccountId.SelectedIndex == 1)
                    {
                        dataRow["Item"] = "";
                    }
                    else
                    {
                        dataRow["Item"] = "SELECT ITEM";
                    }
                    dataSet.Tables["ITEM_COMBO_BOX"].Rows.InsertAt(dataRow, 0);
                    comboBoxItems.DataSource = dataSet.Tables["ITEM_COMBO_BOX"];
                    comboBoxItems.ValueMember = "Item";
                    comboBoxItems.DisplayMember = "Item";
                    comboBoxItems.SelectedIndex = 0;
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
        }

        private void DataGridViewTransaction_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView.SelectedRows.Count == 0)
            {
                return;
            }
            try
            {
                if (dataGridView.SelectedRows[0].Cells["Id"].Value.ToString().Equals("------"))
                {
                    return;
                }
                dateTimePickerTransaction.Value = DateTime.Parse(dataGridView.SelectedRows[0].Cells["Date"].Value.ToString());
                if (dataGridView == dataGridViewMaterial)
                {
                    textBoxQty.Text = dataGridView.SelectedRows[0].Cells["Qty"].Value.ToString();
                    textBoxRate.Text = dataGridView.SelectedRows[0].Cells["Rate"].Value.ToString();
                    comboBoxItems.Text = dataGridView.SelectedRows[0].Cells["Item"].Value.ToString();
                    textBoxTotal.Text = dataGridView.SelectedRows[0].Cells["Amount"].Value.ToString();
                    textBoxExtra.Text = dataGridView.SelectedRows[0].Cells["Extra"].Value.ToString();
                    textBoxUnit.Text = dataGridView.SelectedRows[0].Cells["Unit"].Value.ToString();
                    dataGridViewPayment.ClearSelection();
                }
                else if (dataGridView == dataGridViewPayment)
                {
                    textBoxPmtId.Text = dataGridView.SelectedRows[0].Cells["Info"].Value.ToString();
                    comboBoxPmtMode.Text = dataGridView.SelectedRows[0].Cells["Mode"].Value.ToString();
                    textBoxAmount.Text = dataGridView.SelectedRows[0].Cells["Amount"].Value.ToString();
                    dataGridViewMaterial.ClearSelection();
                }
            }
            catch (Exception errSelectedIndex)
            {
                MessageBox.Show("Eror in selecting row : " + errSelectedIndex.Message + "");
            }
        }

        private void RateQty_TextChanged(object sender, EventArgs e)
        {
            double total = 0.00;
            double qty = 0.00;
            double rate = 0.00;
            double extra = 0.00;
            if (textBoxQty.Text.Equals("") || textBoxRate.Text.Equals(""))
            {
                textBoxTotal.Text = total.ToString();
                return;
            }
            try
            {
                qty = Convert.ToDouble(textBoxQty.Text);
                rate = Convert.ToDouble(textBoxRate.Text);
            }
            catch (Exception errPrice)
            {
                MessageBox.Show("Incorrect Rate or Quantity (error = " + errPrice.Message + ")");
            }
            total = rate * qty;
            if (textBoxExtra.Text.Equals(""))
            {
                extra = 0.00;
            }
            else
            {
                try
                {
                    extra = Convert.ToDouble(textBoxExtra.Text);
                }
                catch (Exception errPrice)
                {
                    MessageBox.Show("Incorrect Extra Amount (error = " + errPrice.Message + ")");
                }
            }
            total += extra;
            textBoxTotal.Text = total.ToString();
        }

        private bool ValidateCommonData()
        {
            if (comboBoxAccountId.Text.Equals(""))
            {
                MessageBox.Show("Account name is empty", "Account not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (dateTimePickerTransaction.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Date cannot be greator than today", "Date incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ( (comboBoxItems.SelectedIndex == 0) && (textBoxAmount.Text.Equals("")))
            {
                MessageBox.Show("Nothing to do.", "No action needed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidatePaymentData()
        {
            try
            {
                double total = Convert.ToDouble(textBoxTotal.Text);
                if (total == 0)
                {
                    MessageBox.Show("Total Price is Zero. No use of inserting");
                    return false;
                }
            }
            catch (Exception errPrice)
            {
                MessageBox.Show("Incorrect Rate or Quantity (error = " + errPrice.Message + ")");
                return false;
            }

            if (comboBoxPmtMode.Text.Equals(""))
            {
                MessageBox.Show("Payment Mode not provided");
                return false;
            }

            if (textBoxPmtId.Text.Equals(""))
            {
                textBoxPmtId.Text = "NOT PROVIDED";
            }

            return true;
        }

        private bool ValidateMaterialData()
        {
            if (comboBoxItems.Text.Equals(""))
            {
                MessageBox.Show("Item cannot be Empty", "Item incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (comboBoxItems.Text.Equals("SELECT ITEM"))
            {
                MessageBox.Show("Item not selected", "Item not selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            if (textBoxUnit.Text.Equals(""))
            {
                MessageBox.Show("Item unit not provided.", "Unit not provided", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            if (!ValidateCommonData())
            {
                return;
            }
            if (comboBoxItems.SelectedIndex > 0)
            {
                if (!ValidateMaterialData())
                {
                    return;
                }
            }
            if (!textBoxAmount.Text.Equals(""))
            {
                if (!ValidatePaymentData())
                {
                    return;
                }
            }
            bool isSuccessResult = true;
            MySqlConnection mySqlConnection = new MySqlConnection(mySqlConStr);
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception erropen)
            {
                MessageBox.Show("connection cannot be opened because " + erropen.Message + "");
            }
            MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.Transaction = mySqlTransaction;
            mySqlCommand.CommandType = CommandType.Text;
            mySqlCommand.Parameters.AddWithValue("@var_id", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_acc_number", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_date", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_item", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_unit", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_rate", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_qty", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_extra", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_total", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_mode", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_amount", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_info", "Text");
            if (comboBoxItems.SelectedIndex > 0)
            {
                mySqlCommand.CommandText = "INSERT INTO `materials` (`mtrl_id`, `mtrl_acc_number`, `mtrl_date`, `mtrl_item`, `mtrl_unit`, `mtrl_rate`, `mtrl_qty`, `mtrl_total`, `mtrl_extra`)" +
                        " VALUES (@var_id, @var_acc_number, @var_date, @var_item, @var_unit, @var_rate, @var_qty, @var_total, @var_extra) ";

                mySqlCommand.Prepare();
                mySqlCommand.Parameters["@var_id"].Value = DateTime.Now.ToString("yyyyMMddHHmmss");
                mySqlCommand.Parameters["@var_acc_number"].Value = comboBoxAccountId.Text;
                mySqlCommand.Parameters["@var_date"].Value = dateTimePickerTransaction.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                mySqlCommand.Parameters["@var_item"].Value = comboBoxItems.Text;
                mySqlCommand.Parameters["@var_unit"].Value = textBoxUnit.Text;
                mySqlCommand.Parameters["@var_rate"].Value = textBoxRate.Text;
                mySqlCommand.Parameters["@var_qty"].Value = textBoxQty.Text;
                mySqlCommand.Parameters["@var_total"].Value = textBoxTotal.Text;
                mySqlCommand.Parameters["@var_extra"].Value = textBoxExtra.Text;
                try
                {
                    mySqlCommand.ExecuteNonQuery();
                }
                catch (Exception errquery)
                {
                    MessageBox.Show("Query " + mySqlCommand.CommandText + " \n cannot be executed because " + errquery.Message + "");
                    isSuccessResult = false;
                }
            }
            if (!textBoxAmount.Text.Equals("") && isSuccessResult)
            {
                mySqlCommand.CommandText = "INSERT INTO `payments` (`pmt_id`, `pmt_acc_number`, `pmt_date`, `pmt_mode`, `pmt_amount`, `pmt_info`)" +
                        " VALUES (@var_id, @var_acc_number, @var_date, @var_mode, @var_amount, @var_info) ";

                mySqlCommand.Prepare();
                mySqlCommand.Parameters["@var_id"].Value = DateTime.Now.ToString("yyyyMMddHHmmss");
                mySqlCommand.Parameters["@var_acc_number"].Value = comboBoxAccountId.Text;
                mySqlCommand.Parameters["@var_date"].Value = dateTimePickerTransaction.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                mySqlCommand.Parameters["@var_mode"].Value = comboBoxPmtMode.Text;
                mySqlCommand.Parameters["@var_amount"].Value = textBoxAmount.Text;
                mySqlCommand.Parameters["@var_info"].Value = textBoxPmtId.Text;
                try
                {
                    mySqlCommand.ExecuteNonQuery();
                }
                catch (Exception errquery)
                {
                    MessageBox.Show("Query " + mySqlCommand.CommandText + " \n cannot be executed because " + errquery.Message + "");
                    isSuccessResult = false;
                }
            }
            try
            {
                if (isSuccessResult)
                {
                    mySqlTransaction.Commit();
                }
                else
                {
                    mySqlTransaction.Rollback();
                    mySqlTransaction.Dispose();
                }
            }
            catch (Exception errcommit)
            {
                MessageBox.Show("Error in commiting the transaction because:\n" + errcommit.Message, "Error in Commiting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                mySqlConnection.Close();
            }
            catch (Exception errclose)
            {
                MessageBox.Show("Eror in closing connection : " + errclose.Message + "");
            }
            ComboBoxAccountId_SelectedIndexChanged(null, null);
        }

        private void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            int rowsCount = dataGridViewMaterial.SelectedRows.Count + dataGridViewPayment.SelectedRows.Count;
            if (rowsCount == 0)
            {
                MessageBox.Show("No Row selected.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Delete " + rowsCount.ToString() + " selected data from Tables?", "DELETE ROW FROM TABLE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes)
            {
                MessageBox.Show("Not Deleting.", "No Row Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            bool isSuccessResult = true;
            MySqlConnection mySqlConnection = new MySqlConnection(mySqlConStr);
            try
            {
                mySqlConnection.Open();
            }
            catch (Exception erropen)
            {
                MessageBox.Show("connection cannot be opened because " + erropen.Message + "");
            }
            MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.Transaction = mySqlTransaction;
            mySqlCommand.CommandType = CommandType.Text;
            mySqlCommand.Parameters.AddWithValue("@var_id", "Text");
            
            mySqlCommand.CommandText = "DELETE FROM `materials` WHERE `mtrl_id` = @var_id";
            mySqlCommand.Prepare();
            for (int counter = 0; counter < dataGridViewMaterial.SelectedRows.Count; counter++)
            {
                try
                {
                    mySqlCommand.Parameters["@var_id"].Value = dataGridViewMaterial.SelectedRows[counter].Cells["Id"].Value.ToString();
                    mySqlCommand.ExecuteNonQuery();
                }
                catch (Exception errquery)
                {
                    MessageBox.Show("Query " + mySqlCommand.CommandText + " \n cannot be executed because " + errquery.Message + "");
                    isSuccessResult = false;
                    break;
                }
            }
            if (isSuccessResult)
            {
                mySqlCommand.CommandText = "DELETE FROM `payments` WHERE `pmt_id` = @var_id";
                mySqlCommand.Prepare();
                for (int counter = 0; counter < dataGridViewPayment.SelectedRows.Count; counter++)
                {
                    try
                    {
                        mySqlCommand.Parameters["@var_id"].Value = dataGridViewPayment.SelectedRows[counter].Cells["Id"].Value.ToString();
                        mySqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception errquery)
                    {
                        MessageBox.Show("Query " + mySqlCommand.CommandText + " \n cannot be executed because " + errquery.Message + "");
                        isSuccessResult = false;
                        break;
                    }
                }
            }
            try
            {
                if (isSuccessResult)
                {
                    mySqlTransaction.Commit();
                }
                else
                {
                    mySqlTransaction.Rollback();
                    mySqlTransaction.Dispose();
                }
            }
            catch (Exception errcommit)
            {
                MessageBox.Show("Error in commiting the transaction because:\n" + errcommit.Message, "Error in Commiting", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                mySqlConnection.Close();
            }
            catch (Exception errclose)
            {
                MessageBox.Show("Eror in closing connection : " + errclose.Message + "");
            }
            ComboBoxAccountId_SelectedIndexChanged(null, null);
        }

        private void ButtonReset_Clicked(object sender, EventArgs e)
        {
            comboBoxAccountId.SelectedIndex = 0;
        }
    }
}
