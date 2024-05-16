
using System;
using System.Collections;
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
    public partial class FormHinhThuc : Form
    {
        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader rdr;
        public FormHinhThuc()
        {
            
            InitializeComponent();
        }



        private void FormHinhThuc_Load(object sender, EventArgs e)
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
            sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,HocKy.MaHK,TenHK,MaHT,HinhThuc 
            from KhoaHoc, NganhHoc, HocKy, HinhThuc 
            where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
            AND NganhHoc.MaNganh = HocKy.MaNganh 
            AND HocKy.MaHK = HinhThuc.MaHK";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(nguon);

                if (IsDataDuplicate(comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return; 
                }

                sql = @"insert into HinhThuc (HinhThuc,MaHK) values (@HinhThuc,@MaHK)";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HinhThuc", comboBox4.Text);
                cmd.Parameters.AddWithValue("@MaHK", selectIdCombobox(comboBox3, "MaHK"));
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

        private bool IsDataDuplicate(string newData, string tenngang, string tenki,string hinhthuc)
        {
           
            try
            {
                conn = new SqlConnection(nguon);
                sql = "SELECT COUNT(*) FROM KhoaHoc,NganhHoc,HocKy,HinhThuc WHERE KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=HocKy.MaNganh and HocKy.MaHK=HinhThuc.MaHK and TenKhoa=@ten and TenNganh=@tennganh and TenHK = @tENhk and HinhThuc = @HinhThuc";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", newData);
                cmd.Parameters.AddWithValue("@tennganh", tenngang);
                cmd.Parameters.AddWithValue("@tENhk", tenki);
                cmd.Parameters.AddWithValue("@HinhThuc", hinhthuc);
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
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"update HinhThuc set HinhThuc=@HinhThuc ,MaHK=@MaHK where MaHT=@MaHT";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HinhThuc", comboBox4.Text);
                cmd.Parameters.AddWithValue("@MaHK", int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()));
                cmd.Parameters.AddWithValue("@MaHT", int.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString()));
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
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(nguon);
                sql = @"delete from HinhThuc where MaHT=@MaHT";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaHT", int.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString()));
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
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
                MessageBox.Show("Bạn cần phải xóa môn học thuộc hình thức này trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Hien();
        }
        private void button2_Click(object sender, EventArgs e)
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
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    string sql = @"select * from NganhHoc where MaKhoa = @TenKhoa";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter1.Fill(table);
                        comboBox2.DataSource = table;
                        comboBox2.DisplayMember = "TenNganh";
                        comboBox2.ValueMember = "MaNganh";
                    }

                   
                    sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,HocKy.MaHK,TenHK,MaHT,HinhThuc 
                    from KhoaHoc, NganhHoc, HocKy, HinhThuc 
                    where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
                    AND NganhHoc.MaNganh = HocKy.MaNganh 
                    AND HocKy.MaHK = HinhThuc.MaHK 
                    and KhoaHoc.MaKhoa=@MaKhoa";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
     
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    string sql = @"select * from HocKy where MaNganh = @TenNganh";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNganh", selectIdCombobox(comboBox2, "MaNganh"));
                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter1.Fill(table);

                        comboBox3.DataSource = table;
                        comboBox3.DisplayMember = "TenHK";
                        comboBox3.ValueMember = "MaHK";
                    }

                    

                    sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,HocKy.MaHK,TenHK,MaHT,HinhThuc 
                    from KhoaHoc, NganhHoc, HocKy, HinhThuc 
                    where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
                    AND NganhHoc.MaNganh = HocKy.MaNganh 
                    AND HocKy.MaHK = HinhThuc.MaHK and KhoaHoc.MaKhoa=@MaKhoa and NganhHoc.MaNganh=@manganh";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                        cmd.Parameters.AddWithValue("@manganh", selectIdCombobox(comboBox2, "MaNganh"));
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
     
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    sql = @"select KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, MaHT, HinhThuc 
                    from KhoaHoc, NganhHoc, HocKy, HinhThuc 
                    where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
                    AND NganhHoc.MaNganh = HocKy.MaNganh 
                    AND HocKy.MaHK = HinhThuc.MaHK 
                    and KhoaHoc.MaKhoa=@MaKhoa and  NganhHoc.MaNganh =@manganh and HocKy.MaHK=@mahk
                   ";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                    adapter.SelectCommand.Parameters.AddWithValue("@manganh", selectIdCombobox(comboBox2, "MaNganh"));
                    adapter.SelectCommand.Parameters.AddWithValue("@mahk", selectIdCombobox(comboBox3, "MaHK"));
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string tenKhoa = row.Cells["TenKhoa"].Value.ToString();
                string tenNganh = row.Cells["TenNganh"].Value.ToString();
                string tenHK = row.Cells["TenHK"].Value.ToString();
                string hinhthuc = row.Cells["HinhThuc"].Value.ToString();
                comboBox1.Text = tenKhoa;
                comboBox2.Text = tenNganh;
                comboBox3.Text = tenHK;
                comboBox4.Text = hinhthuc;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
