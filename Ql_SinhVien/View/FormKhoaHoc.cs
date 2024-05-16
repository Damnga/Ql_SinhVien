using System;

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
    public partial class FormKhoaHoc : Form
    {
        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlConnection conn;
        SqlCommand cmd;
       
        public FormKhoaHoc()
        {
            InitializeComponent();
        }

        private void FormKhoaHoc_Load(object sender, EventArgs e)
        {
                Hien();
            }
          
        
        void Hien()
        {
            conn = new SqlConnection(nguon);
            sql = @"select * from KhoaHoc ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private bool IsDataDuplicate(string newData)
        {

            try
            {
                conn = new SqlConnection(nguon);
                sql = "SELECT COUNT(*) FROM KhoaHoc WHERE TenKhoa = @TenKhoa";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenKhoa", newData);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                conn.Close();
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi  trùng lặp: " + ex.Message);
                return false;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"insert into KhoaHoc (TenKhoa) values (@TenKhoa) ";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenKhoa", textBox1.Text);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!");
                    Hien();
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu không thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"update KhoaHoc set TenKhoa=@TenKhoa where MaKhoa=@MaKhoa";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenKhoa", textBox1.Text);
                cmd.Parameters.AddWithValue("@MaKhoa", SqlDbType.NVarChar).Value = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());  
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật dữ liệu thành công!");
                    Hien();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào được cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(nguon);
                sql = @"delete from KhoaHoc where MaKhoa=@MaKhoa";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenKhoa", textBox1.Text);
                cmd.Parameters.AddWithValue("@MaKhoa", SqlDbType.NVarChar).Value = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
             DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hien();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn cần phải xóa ngành học thuộc khóa này trước!","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string tenKhoa = row.Cells["TenKhoa"].Value.ToString();
                textBox1.Text = tenKhoa;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            textBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hien();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}



