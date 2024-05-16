
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ql_SinhVien
{

    public partial class FormHocKy : Form
    {
        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader rdr;
        public FormHocKy()
        {
            InitializeComponent();
        }

        private void FormHocKy_Load(object sender, EventArgs e)
        {
            
            conn = new SqlConnection(nguon);
            sql = @"select * from KhoaHoc";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable table = new DataTable();
            adapter.Fill(table);
            conn.Close();
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "TenKhoa";
            comboBox1.ValueMember = "MaKhoa";
            Hien();

        }
        void Hien()
        {
            conn = new SqlConnection(nguon);
            sql = @"select KhoaHoc.MaKhoa ,TenKhoa,NganhHoc.MaNganh,TenNganh,MaHK,TenHK from KhoaHoc,NganhHoc,HocKy where KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=HocKy.MaNganh";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private bool IsDataDuplicate(string newData,string tenngang,string tenki)
        {
            try
            {
                conn = new SqlConnection(nguon);
                sql = "SELECT COUNT(*) FROM KhoaHoc,NganhHoc,HocKy WHERE KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=HocKy.MaNganh and TenKhoa=@ten and TenNganh=@tennganh and TenHK = @tENhk";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", newData);
                cmd.Parameters.AddWithValue("@tennganh", tenngang);
                cmd.Parameters.AddWithValue("@tENhk", tenki);
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(comboBox1.Text,comboBox2.Text,textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"insert into HocKy (TenHK,MaNganh) values (@TenHK,@MaNganh)";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenHK", textBox1.Text);
                cmd.Parameters.AddWithValue("@MaNganh", selectIdCombobox(comboBox2,"MaNganh"));
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
                MessageBox.Show("Đã xảy ra lỗi them : " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(comboBox1.Text, comboBox2.Text, textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"update HocKy set TenHK=@TenHK,MaNganh=@MaNganh where MaHK=@MaHK";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenHK", textBox1.Text);
                cmd.Parameters.AddWithValue("@MaNganh", int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString()));
                cmd.Parameters.AddWithValue("@MaHK", int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()));
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

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(nguon);
                sql = @"delete from HocKy where MaHK=@MaHK";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHK", int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()));
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
                MessageBox.Show("Bạn cần phải xóa hình thức học thuộc học kì này trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hien();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int selectIdCombobox(System.Windows.Forms.ComboBox x, string y)
        {
            DataRowView rowView = (DataRowView)x.SelectedItem;
            return Convert.ToInt32(rowView[y]);
        }

      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                conn.Open();
                sql = @"select * from NganhHoc where MaKhoa = @TenKhoa";
                 cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenKhoa", selectIdCombobox(comboBox1,"MaKhoa"));
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                adapter1.SelectCommand = cmd;
                DataTable table = new DataTable();
                adapter1.Fill(table);
                conn.Close();
                comboBox2.DataSource = table;
                comboBox2.DisplayMember = "TenNganh";
                comboBox2.ValueMember = "MaNganh";
               
                conn = new SqlConnection(nguon);
                conn.Open();
                sql = @"select KhoaHoc.MaKhoa ,TenKhoa,NganhHoc.MaNganh,TenNganh,MaHK,TenHK from KhoaHoc,NganhHoc,HocKy where KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=HocKy.MaNganh and KhoaHoc.MaKhoa=@MaKhoa";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                conn.Close();
            }
        }
       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            conn = new SqlConnection(nguon);
            conn.Open();
            sql = @"select KhoaHoc.MaKhoa ,TenKhoa,NganhHoc.MaNganh,TenNganh,MaHK,TenHK from KhoaHoc,NganhHoc,HocKy where KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=HocKy.MaNganh and KhoaHoc.MaKhoa=@MaKhoa and NganhHoc.MaNganh=@ma";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1,"MaKhoa"));
            adapter.SelectCommand.Parameters.AddWithValue("@ma", selectIdCombobox(comboBox2, "MaNganh"));
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string tenKhoa = row.Cells["TenKhoa"].Value.ToString();
                string tenNganh = row.Cells["TenNganh"].Value.ToString();
                string tenHK = row.Cells["TenHK"].Value.ToString();
                comboBox1.Text = tenKhoa;
                comboBox2.Text = tenNganh;
                textBox1.Text = tenHK;
            }
        }
    }
}
           
    

