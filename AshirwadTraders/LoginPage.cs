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
                MDIParent mDIParent = new MDIParent();
                textBoxPassword.Text = "";
                Hide();
                mDIParent.ShowDialog();
                Close();
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
    }
}
