using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace workshop4
{
    public partial class frmLogin : Form
    {
        BookStoreMng bs = new BookStoreMng();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int EmpID=0;
            try
            {
                 EmpID = int.Parse(txtEmpID.Text);
            }
            catch(FormatException ex)
            {
                MessageBox.Show("ID must be number !!");
            }
            
            string Pass = txtPassword.Text;
            if(bs.checkLogin(EmpID, Pass))
            {
                
                if (bs.checkRole(EmpID))
                {
                    
                    frmMaintainBooks BookStore = new frmMaintainBooks();
                    DialogResult frmMain = BookStore.ShowDialog();
                    
                }
                else
                {
                    
                    Employee emp = new Employee(EmpID, Pass, false);
                    frmChangeAccount changeAccount = new frmChangeAccount(emp);
                    DialogResult frmChangeAccount = changeAccount.ShowDialog();
                    
                }
                
            }
            else
            {
                MessageBox.Show("Invalid ID or Password");
                txtPassword.Text = "";
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEmpID.Text = "";
            txtPassword.Text = "";
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
