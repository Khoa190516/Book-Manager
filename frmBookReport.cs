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
    public partial class frmBookReport : Form
    {
        public DataTable dtBook { get; set; }
        public frmBookReport()
        {
            InitializeComponent();
            
        }

        public frmBookReport(DataTable dtBok):this()
        {
            dtBook = dtBok;
            DataTable dt2 = new DataTable();
            dt2 = dtBook;
            DataView dv = dt2.DefaultView;
            dv.Sort = "[BookPrice] DESC";
            dt2 = dv.ToTable();
            dgvBookReport.DataSource = dt2;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvBookReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvBookReport.DataSource = dtBook;
        }

        private void frmBookReport_Load(object sender, EventArgs e)
        {

        }
    }
}
