using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prints_Of_Donets_KursovayaBD_
{
    public partial class AuthorizationForm : Form
    {
        static public MainForm mainForm;
        public AuthorizationForm()
        {
            InitializeComponent();
            
        }

        private void AuthorizationForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "" && textBoxPassword.Text == "")
            {
                Visible = false;
                mainForm = new MainForm();
                mainForm.Visible = true;
            }
            else
            {
                MessageBox.Show("Повторите попытку","Ошибка ваторизации");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
