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
    public partial class frmMaintainBooks : Form
    {
        BookStoreMng bs = new BookStoreMng();
        DataTable dtBooks;
        public frmMaintainBooks()
        {
            InitializeComponent();
            getBooks();
        }

        

        private void frmMaintainBooks_Load(object sender, EventArgs e)
        {
            getBooks();
        }

        private void getBooks()
        {
            dtBooks = bs.getBooks();
            txtBookID.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();
            //put data to form
            txtBookID.DataBindings.Add("Text", dtBooks, "BookID");
            txtBookName.DataBindings.Add("Text", dtBooks, "BookName");
            txtBookPrice.DataBindings.Add("Text", dtBooks, "BookPrice");
            dgvBookStores.DataSource = dtBooks;
            lbTotalPrice.Text = "Total Price :" + dtBooks.Compute("SUM(BookPrice)", string.Empty);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtBookID.Text);
            string Name = txtBookName.Text;
            float Price = float.Parse(txtBookPrice.Text);

            Book newBook = new Book
            {
                BookID = ID,
                BookName = Name,
                BookPrice = Price
            };
            bool rs = bs.addNewBook(newBook);
            string s = (rs == true ? "success" : "fail");
            MessageBox.Show("Add " + s);
            getBooks();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtBookID.Text);
            string Name = txtBookName.Text;
            float Price = float.Parse(txtBookPrice.Text);

            Book newBook = new Book
            {
                BookID = ID,
                BookName = Name,
                BookPrice = Price
            };
            bool rs = bs.updateBook(newBook);
            string s = (rs == true ? "success" : "fail");
            MessageBox.Show("Update " + s);
            getBooks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(txtBookID.Text);
            bool rs = bs.deleteBook(ID);
            string s = (rs == true ? "success" : "fail");
            MessageBox.Show("Delete " + s);
            getBooks();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            
            DataView dv = new DataView(dtBooks);
            dv.RowFilter = dgvBookStores.Columns[1].HeaderText.ToString() + " LIKE '%" + txtBookName.Text + "%'";
            dgvBookStores.DataSource = dv;
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmBookReport bookReport = new frmBookReport(dtBooks);
            DialogResult frmBookResult = bookReport.ShowDialog();
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            dgvBookStores.ClearSelection();
            dgvBookStores.Rows[0].Selected = true;
            
        }

        private void btnLast_Click(object sender, EventArgs e)
        {

            dgvBookStores.ClearSelection();
            int TotalRows = dgvBookStores.Rows.Count-1;
            dgvBookStores.Rows[TotalRows-1].Selected = true;
            
        }

        int rowIndex;

        private void btnPrevious_Click(object sender, EventArgs e)
        {

            rowIndex = dgvBookStores.SelectedCells[0].OwningRow.Index;
            DataRow row = dtBooks.NewRow();

            row[0] = int.Parse(dgvBookStores.Rows[rowIndex].Cells[0].Value.ToString());
            row[1] = dgvBookStores.Rows[rowIndex].Cells[1].Value.ToString();
            row[2] = float.Parse(dgvBookStores.Rows[rowIndex].Cells[2].Value.ToString());

            if(rowIndex > 0)
            {
                dtBooks.Rows.RemoveAt(rowIndex);
                dtBooks.Rows.InsertAt(row, rowIndex - 1);
                dgvBookStores.ClearSelection();
                dgvBookStores.Rows[rowIndex - 1].Selected = true;
            }
            

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            rowIndex = dgvBookStores.SelectedCells[0].OwningRow.Index;
            DataRow row = dtBooks.NewRow();

            row[0] = int.Parse(dgvBookStores.Rows[rowIndex].Cells[0].Value.ToString());
            row[1] = dgvBookStores.Rows[rowIndex].Cells[1].Value.ToString();
            row[2] = float.Parse(dgvBookStores.Rows[rowIndex].Cells[2].Value.ToString());

            if (rowIndex < dgvBookStores.Rows.Count-2)
            {
                dtBooks.Rows.RemoveAt(rowIndex);
                dtBooks.Rows.InsertAt(row, rowIndex + 1);
                dgvBookStores.ClearSelection();
                dgvBookStores.Rows[rowIndex + 1].Selected = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            getBooks();
        }
    }
}
