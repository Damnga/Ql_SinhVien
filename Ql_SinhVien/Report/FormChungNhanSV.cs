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
    public partial class FormChungNhanSV : Form
    {
        public FormChungNhanSV()
        {
            InitializeComponent();
        }

        private void FormChungNhanSV_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string query = "SELECT KhoaHoc.* , NganhHoc.* ,LopHoc.* , SinhVien.* FROM KhoaHoc,NganhHoc,LopHoc,SinhVien where SinhVien.MaSV = @ma";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ma", ClassLogin.MaSV);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            var reportDataSource = new ReportDataSource("DataSetChungNhanSV", dataTable);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
           
        }
    }
}
