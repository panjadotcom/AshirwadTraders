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
    public partial class AccountsPage : Form
    {
        static string mySqlConStr = Properties.Settings.Default.MySqlConStr;
        public AccountsPage()
        {
            InitializeComponent();
        }

        private void AccountsPage_Load(object sender, EventArgs e)
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
                    dataRow["number"] = "";
                    dataSet.Tables["ID_COMBO_BOX"].Rows.InsertAt(dataRow, 0);
                    //dataRow = dataSet.Tables["ID_COMBO_BOX"].NewRow();
                    //dataRow["id"] = "1";
                    //dataRow["number"] = "panjadotcom";
                    //dataSet.Tables["ID_COMBO_BOX"].Rows.InsertAt(dataRow, 1);
                    //dataRow = dataSet.Tables["ID_COMBO_BOX"].NewRow();
                    //dataRow["id"] = "2";
                    //dataRow["number"] = "yadavdotcom";
                    //dataSet.Tables["ID_COMBO_BOX"].Rows.InsertAt(dataRow, 2);
                    //dataRow = dataSet.Tables["ID_COMBO_BOX"].NewRow();
                    //dataRow["id"] = "3";
                    //dataRow["number"] = "nehadotcom";
                    //dataSet.Tables["ID_COMBO_BOX"].Rows.InsertAt(dataRow, 3);
                    comboBoxAccountNumber.DataSource = dataSet.Tables["ID_COMBO_BOX"];
                    comboBoxAccountNumber.ValueMember = "id";
                    comboBoxAccountNumber.DisplayMember = "number";
                    comboBoxAccountNumber.SelectedIndex = 0;
                }
            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + queryString +" \n cannot be executed because " + errquery.Message + "");
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

        private void ComboBoxAccountNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxAdditionalInfo.Text = "";
            textBoxAddress.Text = "";
            textBoxName.Text = "";
            textBoxPhone.Text = "";
            if (comboBoxAccountNumber.SelectedIndex == 0)
            {
                buttonUpdate.Text = "CREATE";
                return;
            }
            buttonUpdate.Text = "UPDATE";
            string queryString = "SELECT * FROM `account` WHERE `acc_id` = " + comboBoxAccountNumber.SelectedIndex.ToString();
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
                if (dataSet != null)
                {
                    textBoxName.Text = dataSet.Tables["ID_DATA"].Rows[0]["acc_name"].ToString();
                    textBoxPhone.Text = dataSet.Tables["ID_DATA"].Rows[0]["acc_phone"].ToString();
                    textBoxAddress.Text = dataSet.Tables["ID_DATA"].Rows[0]["acc_address"].ToString();
                    textBoxAdditionalInfo.Text = dataSet.Tables["ID_DATA"].Rows[0]["acc_info"].ToString();
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

        private bool ValidateData()
        {
            if (comboBoxAccountNumber.SelectedIndex == 0)
            {
                if (string.Equals(comboBoxAccountNumber.Text, ""))
                {
                    MessageBox.Show("Account ID Field cannot be empty", "Acc Id Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (string.Equals(textBoxName.Text,""))
            {
                MessageBox.Show("Name Field cannot be empty", "Name Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.Equals(textBoxPhone.Text, ""))
            {
                textBoxPhone.Text = "NOT FILLED";
            }

            if (string.Equals(textBoxAddress.Text, ""))
            {
                textBoxAddress.Text = "NOT FILLED";
            }

            if (string.Equals(textBoxAdditionalInfo.Text, ""))
            {
                textBoxAdditionalInfo.Text = "NOT FILLED";
            }
            return true;
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
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
            MySqlTransaction mySqlTransaction = mySqlConnection.BeginTransaction();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.Transaction = mySqlTransaction;
            mySqlCommand.CommandType = CommandType.Text;
            mySqlCommand.Parameters.AddWithValue("@var_id", 1);
            mySqlCommand.Parameters.AddWithValue("@var_number", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_name", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_phone", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_address", "Text");
            mySqlCommand.Parameters.AddWithValue("@var_info", "Text");
            if (comboBoxAccountNumber.SelectedIndex > 0)
            {
                mySqlCommand.CommandText = "UPDATE `account` SET `acc_name` = @var_name, `acc_phone` = @var_phone, `acc_address` = @var_address, `acc_info` = @var_info WHERE `acc_id` = @var_id ";
            }
            else
            {
                mySqlCommand.CommandText = "INSERT INTO `account` (`acc_number`, `acc_name`, `acc_phone`, `acc_address`, `acc_info`)" +
                    " VALUES (@var_number, @var_name, @var_phone, @var_address, @var_info) ";
            }
            mySqlCommand.Prepare();
            mySqlCommand.Parameters["@var_id"].Value = Convert.ToInt32(comboBoxAccountNumber.SelectedValue);
            mySqlCommand.Parameters["@var_number"].Value = comboBoxAccountNumber.Text;
            mySqlCommand.Parameters["@var_name"].Value = textBoxName.Text;
            mySqlCommand.Parameters["@var_phone"].Value = textBoxPhone.Text;
            mySqlCommand.Parameters["@var_address"].Value = textBoxAddress.Text;
            mySqlCommand.Parameters["@var_info"].Value = textBoxAdditionalInfo.Text;
            try
            {
                mySqlCommand.ExecuteNonQuery();
                mySqlTransaction.Commit();
                if (comboBoxAccountNumber.SelectedIndex > 0)
                {
                    MessageBox.Show("Account : " + comboBoxAccountNumber.Text + " updated successfully");
                }
                else
                {
                    MessageBox.Show("New Account " + comboBoxAccountNumber.Text + " created successfully");
                }
                
            }
            catch (Exception errquery)
            {
                MessageBox.Show("Query " + mySqlCommand.CommandText + " \n cannot be executed because " + errquery.Message + "");
            }
            try
            {
                mySqlConnection.Close();
            }
            catch (Exception errclose)
            {
                MessageBox.Show("Eror in closing connection : " + errclose.Message + "");
            }
            Close();
        }
    }
}
