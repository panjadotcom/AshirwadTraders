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
using System.IO;
using System.Diagnostics;

namespace AshirwadTraders
{
    public partial class SettingsPage : Form
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void SettingsPage_Load(object sender, EventArgs e)
        {
            string connectionString = Properties.Settings.Default.MySqlConStr;
            textBoxMySqlConStr.Text = connectionString;
            string[] tokens = connectionString.Split(';');
            foreach (var token in tokens)
            {
                if (token.Contains("server="))
                {
                    textBoxMySqlServerIp.Text = token.Substring("server=".Length);
                }
                if (token.Contains("user id="))
                {
                    textBoxMySqlUserName.Text = token.Substring("user id=".Length);
                }
                if (token.Contains("password="))
                {
                    textBoxMySqlPassword.Text = token.Substring("password=".Length);
                }
                if (token.Contains("port="))
                {
                    textBoxMySqlServerPort.Text = token.Substring("port=".Length);
                }
                if (token.Contains("database="))
                {
                    textBoxMySqlSchema.Text = token.Substring("database=".Length);
                }
            }
            textBoxMySqlDirectory.Text = Properties.Settings.Default.MySqlWorkingDirectory;
        }

        private void ButtonMySqlUpdateSettings_Clicked(object sender, EventArgs e)
        {
            Properties.Settings.Default.MySqlConStr = textBoxMySqlConStr.Text;
            Properties.Settings.Default.MySqlWorkingDirectory = textBoxMySqlDirectory.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("MySql details Saved", "MySql details Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ButtonTestConnection_Clicked(object sender, EventArgs e)
        {
            string mysqlConStr = textBoxMySqlConStr.Text;
            MySqlConnection mysqlConn = new MySqlConnection(mysqlConStr);
            try
            {
                mysqlConn.Open();
                try
                {
                    mysqlConn.Close();
                    MessageBox.Show("Databse connected successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception errclose)
                {
                    MessageBox.Show("connection cannot be closed because " + errclose.Message + "", "Error in Closing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception erropen)
            {
                MessageBox.Show("connection cannot be opened because " + erropen.Message + "", "Error in opening", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonExit_Clicked(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonMySqlDirectory_Clicked(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = textBoxMySqlDirectory.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxMySqlDirectory.Text = folderBrowserDialog.SelectedPath;
            }
            else
            {
                textBoxMySqlDirectory.Text = "Path not selected";
            }
        }

        private void ButtonRestoreDB_Clicked(object sender, EventArgs e)
        {
            string path = Properties.Settings.Default.MySqlWorkingDirectory;
            if (Directory.Exists(path))
            {
                if (!File.Exists(path + "\\mysql.exe"))
                {
                    MessageBox.Show("File : \"" + path + "\\mysql.exe\" not exists. \n Install MySql client first then retry.\n OR \n Change MySql directory path in Settings.", "MySql file not exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Path : \"" + path + "\" not exists. \n Install MySql client first then retry.\n OR \n Change MySql directory path in Settings.", "MySql Path not exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string connectionString = Properties.Settings.Default.MySqlConStr;
            string server = "";
            string userId = "";
            string passwd = "";
            string port = "";
            string database = "";
            string[] tokens = connectionString.Split(';');
            foreach (var token in tokens)
            {
                if (token.Contains("server="))
                {
                    server = token.Substring("server=".Length);
                }
                if (token.Contains("user id="))
                {
                    userId = token.Substring("user id=".Length);
                }
                if (token.Contains("password="))
                {
                    passwd = token.Substring("password=".Length);
                }
                if (token.Contains("port="))
                {
                    port = token.Substring("port=".Length);
                }
                if (token.Contains("database="))
                {
                    database = token.Substring("database=".Length);
                }
            }
            string command;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MySql Script(*.sql)|*.sql";
            openFileDialog.DefaultExt = "sql";
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                command = "mysql -u " + userId + " -p" + passwd + " -h " + server + " -P " + port + " " + database + " < \"" + openFileDialog.FileName + "\"";
            }
            else
            {
                return;
            }
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = path;
            process.Start();
            StreamReader streamReader = process.StandardOutput;
            StreamWriter streamWriter = process.StandardInput;
            streamWriter.WriteLine(command);
            streamWriter.Close();
            process.WaitForExit();
            process.Close();
        }

        private void UpdateConnectionString(object sender, EventArgs e)
        {
            string connectionString = "";
            if (textBoxMySqlServerIp.Text.Length > 0)
            {
                connectionString = "server=" + textBoxMySqlServerIp.Text;
            }
            if (textBoxMySqlServerPort.Text.Length > 0)
            {
                if (connectionString.Length > 0)
                {
                    connectionString = connectionString + ";port=" + textBoxMySqlServerPort.Text;
                }
                else
                {
                    connectionString = "port=" + textBoxMySqlServerPort.Text;
                }
            }
            if (textBoxMySqlUserName.Text.Length > 0)
            {
                if (connectionString.Length > 0)
                {
                    connectionString = connectionString + ";user id=" + textBoxMySqlUserName.Text;
                }
                else
                {
                    connectionString = "user id=" + textBoxMySqlUserName.Text;
                }
            }
            if (textBoxMySqlPassword.Text.Length > 0)
            {
                if (connectionString.Length > 0)
                {
                    connectionString = connectionString + ";password=" + textBoxMySqlPassword.Text;
                }
                else
                {
                    connectionString = "password=" + textBoxMySqlPassword.Text;
                }
            }
            if (textBoxMySqlSchema.Text.Length > 0)
            {
                if (connectionString.Length > 0)
                {
                    connectionString = connectionString + ";database=" + textBoxMySqlSchema.Text;
                }
                else
                {
                    connectionString = "database=" + textBoxMySqlSchema.Text;
                }
            }
            if (connectionString.Length > 0)
            {
                textBoxMySqlConStr.Text = connectionString + ";SslMode=none";
            }
            else
            {
                textBoxMySqlConStr.Text = "";
            }
        }
    }
}
