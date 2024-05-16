using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ql_SinhVien.Report
{
    public partial class FormDanhSachSV : Form
    {
        public FormDanhSachSV()
        {
            InitializeComponent();
        }

        private void FormDanhSachSV_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT TenLop FROM SinhVien INNER JOIN LopHoc ON SinhVien.MaLop = LopHoc.MaLop WHERE SinhVien.MaSV = @ma";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ma", ClassLogin.MaSV);
                string tenLop = command.ExecuteScalar()?.ToString();
                string query = "SELECT KhoaHoc.*, NganhHoc.*, LopHoc.*, SinhVien.* FROM  KhoaHoc, NganhHoc, LopHoc, SinhVien WHERE  SinhVien.MaLop = LopHoc.MaLop AND KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=LopHoc.MaNganh and LopHoc.TenLop = @tenLop";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenLop", tenLop);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
             
                var reportDataSource = new ReportDataSource("DataSetDanhSach", dataTable);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                this.reportViewer1.RefreshReport();
            }
           
        }
    }
}
