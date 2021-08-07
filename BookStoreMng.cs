using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace workshop4
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public float BookPrice { get; set; }

        public Book()
        {

        }

        public Book(int BookID,string BookName,float BookPrice)
        {
            this.BookID = BookID;
            this.BookName = BookName;
            this.BookPrice = BookPrice;
        }
    }

    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpPass { get; set; }

        public bool Role { get; set; }

        public Employee()
        {

        }

        public Employee(int EmpID,string EmpPass,bool Role)
        {
            this.EmpID = EmpID; ;
            this.EmpPass = EmpPass;
            this.Role = Role;
        }
    }
    public class BookStoreMng
    {
        string strConn;

        public BookStoreMng()
        {
            strConn = getConnectionString();
        }

        public string getConnectionString()
        {
            string strConn = "server=DESKTOP-T678I10;database=BookStores;uid=sa;pwd=sa";
            return strConn;
        }

        public bool checkLogin(int ID, string pass)
        {
            string SQL = "Select * from Employee where EmpID=@EmpID and EmpPass=@EmpPass";
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@EmpID", ID);
            cmd.Parameters.AddWithValue("@EmpPass", pass);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtEmp = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtEmp);
                if(dtEmp.Rows.Count>0)
                {
                    return true;
                }
            }
             catch(SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return false;
        }

        public bool checkRole(int ID)
        {

            string SQL = "Select * from Employee where EmpID=@EmpID";
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@EmpID", ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtEmp = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtEmp);
                if (dtEmp.Rows.Count > 0)
                {
                    DataRow row = dtEmp.Rows[0];
                    bool Role = bool.Parse(row["Role"].ToString());
                    if(Role)
                    {
                        return true;
                    }
                    
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cnn.Close();
            }


            return false;
        }

        public bool changePassword(int ID, string pass)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            string SQL = "Update Employee set EmpPass=@EmpPass where EmpID=@EmpID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@EmpPass", pass);
            cmd.Parameters.AddWithValue("@EmpID", ID);
            if(cnn.State==ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();

            return (count > 0);
        }
        
        public DataTable getBooks()
        {
            string SQL = "select * from Books";
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtBooks = new DataTable();
            try
            {
                if(cnn.State==ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtBooks);
            }
            catch(SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dtBooks;
        }

        public bool addNewBook(Book book)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            string SQL = "Insert Books Values(@BookID,@BookName,@BookPrice)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@BookID", book.BookID);
            cmd.Parameters.AddWithValue("@BookName", book.BookName);
            cmd.Parameters.AddWithValue("@BookPrice", book.BookPrice);
            if(cnn.State==ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);

        }

        public bool updateBook(Book book)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            string SQL = "Update Books set BookName=@BookName,BookPrice=@BookPrice where BookID=@BookID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@BookID", book.BookID);
            cmd.Parameters.AddWithValue("@BookName", book.BookName);
            cmd.Parameters.AddWithValue("@BookPrice", book.BookPrice);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }

        public bool deleteBook(int BookID)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            string SQL = "Delete Books where BookID=@BookID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@BookID", BookID);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }

    }


}
