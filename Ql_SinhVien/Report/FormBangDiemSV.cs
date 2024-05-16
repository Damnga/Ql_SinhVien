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

namespace Ql_SinhVien
{
    public partial class FormBangDiemSV : Form
    {
        public FormBangDiemSV()
        {
            InitializeComponent();
        }

        private void FormBangDiemSV_Load(object sender, EventArgs e)
        {
          
            string connectionString = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT TenLop FROM SinhVien INNER JOIN LopHoc ON SinhVien.MaLop = LopHoc.MaLop WHERE SinhVien.MaSV = @ma";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ma", ClassLogin.MaSV);
                string tenLop = command.ExecuteScalar()?.ToString();
                string query = "SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem      FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem       WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa         AND NganhHoc.MaNganh = HocKy.MaNganh         AND HocKy.MaHK = HinhThuc.MaHK         AND HinhThuc.MaHT = MonHoc.MaHT       AND MonHoc.ID_Mon = Diem.MaMon        AND NganhHoc.MaNganh = LopHoc.MaNganh      AND LopHoc.MaLop = SinhVien.MaLop        AND SinhVien.ID_SV = Diem.MaSV and TenLop = @tenLop";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenLop", tenLop);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);
                var reportDataSource = new ReportDataSource("DataSetBangDiemSV", dataTable);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
