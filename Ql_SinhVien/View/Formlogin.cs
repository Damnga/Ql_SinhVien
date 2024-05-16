using Ql_SinhVien.Report;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ql_SinhVien
{
    
    public partial class Formlogin : Form
    {






        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader rdr;
        public Formlogin()
        {
            InitializeComponent();
        }

        private void Formlogin_Load(object sender, EventArgs e)
        {

        }
        private bool IsDataDuplicate(string tenlop, string mk )
        {
                conn = new SqlConnection(nguon);
                sql = "SELECT COUNT(*) FROM TaiKhoan WHERE  TenTK = @tenLop and MatKhau=@mk";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tenlop", tenlop);
                cmd.Parameters.AddWithValue("@mk", mk);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();
                return count > 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(IsDataDuplicate(textBox1.Text, textBox2.Text))
            {
                conn = new SqlConnection(nguon);
                sql = "SELECT PhanQuyen FROM TaiKhoan WHERE  TenTK = @tenLop and MatKhau=@mk";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tenlop", textBox1.Text);
                cmd.Parameters.AddWithValue("@mk", textBox2.Text);
                    conn.Open();
                    var phanQuyen = cmd.ExecuteScalar();
                    conn.Close();

                    if (phanQuyen != null && phanQuyen.ToString() == "admin")
                    {
                        ClassLogin.loginadmin = true;
                        MessageBox.Show("Admin đăng nhập thành công!","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Close();
                       
                    }
                    else
                    {
                        ClassLogin.loginuser = true;
                        MessageBox.Show("Sinh Viên đã đăng nhập thành công!","Thông Báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClassLogin.MaSV = textBox1.Text;
                    this.Close();
                       
                }
            }
            else
            {
                ClassLogin.loginadmin = true;
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
            
        }
    }
}
