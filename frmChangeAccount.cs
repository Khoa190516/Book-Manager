using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace workshop4
{
    public partial class frmChangeAccount : Form
    {
        public Employee employee { get; set; }
        public frmChangeAccount()
        {
            InitializeComponent();
        }

        public frmChangeAccount(Employee emp):this()
        {
            employee = emp;
            initData();
        }

        BookStoreMng bs = new BookStoreMng();

        private void initData()
        {
            txtEmpID.Text = employee.EmpID.ToString();
            txtEmpID.Enabled = false;
            txtPwd.Text = employee.EmpPass.ToString();
            if(employee.Role)
            {
                cbRole.Checked=true;
            }
            cbRole.Enabled = false;
        }

        private void btnUpdatePwd_Click(object sender, EventArgs e)
        {
            int EmpID = int.Parse(txtEmpID.Text.Trim());
            string pwd = txtPwd.Text.Trim();
            bool Result = bs.changePassword(EmpID, pwd);
            string s = (Result == true ? "successfull" : "fail");
            MessageBox.Show("Update password " + s);
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
