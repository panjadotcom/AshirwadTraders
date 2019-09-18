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
    public partial class LoginPage : Form
    {
        bool isPasswordChanging;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            textBoxUsername.Text = Properties.Resources.LoginUserName;
            textBoxUsername.Enabled = false;
            textBoxPassword.Text = "";
            isPasswordChanging = false;
        }

        private void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            if (String.Equals(textBoxPassword.Text, Properties.Settings.Default.Password))
            {
                MySqlConnection mySqlConnection = new MySqlConnection(Properties.Settings.Default.MySqlConStr);
                try
                {
                    mySqlConnection.Open();
                    try
                    {
                        mySqlConnection.Close();
                        MDIParent mDIParent = new MDIParent();
                        textBoxPassword.Text = "";
                        Hide();
                        mDIParent.ShowDialog();
                        Close();
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
            else
            {
                MessageBox.Show("Login Password not matched", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.Text = "";
            }
        }

        private void ButtonChangePasswd_Clicked(object sender, EventArgs e)
        {
            if (isPasswordChanging)
            {
                /* procedure for password change */
                isPasswordChanging = false;
                buttonChangePasswd.Text = "Change Password";
                textBoxUsername.Text = Properties.Resources.LoginUserName;
                buttonLogin.Enabled = true;
                if (string.Equals( textBoxPassword.Text, Properties.Resources.AdminPassword))
                {
                    MessageBox.Show("Password changed to default. As provided by the developer", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.Password = Properties.Resources.LoginDefaultPassword;
                    Properties.Settings.Default.Save();
                    textBoxPassword.Text = "";
                    return;
                }
                if (string.Equals(textBoxPassword.Text, ""))
                {
                    MessageBox.Show("Empty String:\n Password not changed", "Password Not Changed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Properties.Settings.Default.Password = textBoxPassword.Text;
                Properties.Settings.Default.Save();
                this.textBoxPassword.Text = "";
                MessageBox.Show("Password changed successfully!", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (this.textBoxPassword.Text == Properties.Resources.AdminPassword)
                {
                    MessageBox.Show("Password changed to default. As provided by the developer", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.Password = Properties.Resources.LoginDefaultPassword;
                    Properties.Settings.Default.Save();
                    textBoxPassword.Text = "";
                    return;
                }
                if (textBoxPassword.Text == Properties.Settings.Default.Password)
                {
                    MessageBox.Show("Changing login password.\nEnter new password in above field", "Password Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isPasswordChanging = true;
                    buttonChangePasswd.Text = "Update Password";
                    textBoxUsername.Text = "Enter new password";
                    textBoxPassword.Text = "";
                    buttonLogin.Enabled = false;
                    return;
                }
                MessageBox.Show("Password does not match.\nPlease type login password in above field.\nIf forget the login password, then follow the instruction provided by the developer", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPassword.Text = "";
            }
        }

        private void SettingButton_Clicked(object sender, EventArgs e)
        {
            SettingsPage settingsPage = new SettingsPage();
            Hide();
            settingsPage.ShowDialog();
            Show();
        }

        private void TextBoxPassword_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ButtonLogin_Clicked(null, null);
            }
        }
    }
}
