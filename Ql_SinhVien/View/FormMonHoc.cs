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
    public partial class FormMonHoc : Form
    {
        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader rdr;
        public FormMonHoc()
        {
            InitializeComponent();
        }

        private void FormMonHoc_Load(object sender, EventArgs e)
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
            sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,HocKy.MaHK,TenHK,HinhThuc.MaHT,HinhThuc,ID_Mon,MaMon,TenMon,SoGioLT,SoGioTH  
            from KhoaHoc, NganhHoc, HocKy, HinhThuc,MonHoc 
            where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
            AND NganhHoc.MaNganh = HocKy.MaNganh 
            AND HocKy.MaHK = HinhThuc.MaHK and HinhThuc.MaHT=MonHoc.MaHT";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private bool IsDataDuplicate(string newData, string tenngang, string tenki, string hinhthuc, string tenmon)
        {

            try
            {
                conn = new SqlConnection(nguon);
                sql = "SELECT COUNT(*) FROM KhoaHoc,NganhHoc,HocKy,HinhThuc,MonHoc WHERE KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=HocKy.MaNganh and HocKy.MaHK=HinhThuc.MaHK and HinhThuc.MaHT=MonHoc.MaHT and TenKhoa=@ten and TenNganh=@tennganh and TenHK = @tENhk and HinhThuc = @HinhThuc and MaMon=@monhoc";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", newData);
                cmd.Parameters.AddWithValue("@tennganh", tenngang);
                cmd.Parameters.AddWithValue("@tENhk", tenki);
                cmd.Parameters.AddWithValue("@HinhThuc", hinhthuc);
                cmd.Parameters.AddWithValue("@monhoc", tenmon);
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
            private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text,textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"insert into MonHoc (MaMon,TenMon,SoGioLT,SoGioTH,MaHT) values (@MaMon,@TenMon,@SoGioLT,@SoGioTH,@MaHT)";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaMon", textBox1.Text);
                cmd.Parameters.AddWithValue("@TenMon", textBox2.Text);
                cmd.Parameters.AddWithValue("@SoGioLT", numericUpDown2.Text);
                cmd.Parameters.AddWithValue("@SoGioTH", numericUpDown1.Text);
                cmd.Parameters.AddWithValue("@MaHT", selectIdCombobox(comboBox4, "MaHT"));
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"update MonHoc  set MaMon=@MaMon,TenMon=@TenMon,SoGioLT=@SoGioLT,SoGioTH=@SoGioTH,MaHT=@MaHT where ID_Mon = @ID_Mon";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaMon", textBox1.Text);
                cmd.Parameters.AddWithValue("@TenMon", textBox2.Text);
                cmd.Parameters.AddWithValue("@SoGioLT", numericUpDown2.Text);
                cmd.Parameters.AddWithValue("@SoGioTH", numericUpDown1.Text);
                cmd.Parameters.AddWithValue("@MaHT", int.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString()));
                cmd.Parameters.AddWithValue("@ID_Mon", int.Parse(dataGridView1.CurrentRow.Cells[8].Value.ToString()));

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
                sql = @"delete from MonHoc where ID_Mon=@id";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", int.Parse(dataGridView1.CurrentRow.Cells[8].Value.ToString()));
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
                MessageBox.Show("Bạn cần phải xóa điểm thuộc môn học này trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hien();
        }

        private void button6_Click(object sender, EventArgs e)
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

                        

                        sql = @"select KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc, ID_Mon, MaMon, TenMon, SoGioLT, SoGioTH  
                from KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc 
                where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
                AND NganhHoc.MaNganh = HocKy.MaNganh 
                AND HocKy.MaHK = HinhThuc.MaHK 
                AND HinhThuc.MaHT = MonHoc.MaHT 
                AND KhoaHoc.MaKhoa = @MaKhoa";
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

                   

                    sql = @"select KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc, ID_Mon, MaMon, TenMon, SoGioLT, SoGioTH  
                from KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc 
                where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
                AND NganhHoc.MaNganh = HocKy.MaNganh 
                AND HocKy.MaHK = HinhThuc.MaHK 
                AND HinhThuc.MaHT = MonHoc.MaHT 
                AND KhoaHoc.MaKhoa = @MaKhoa";
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
       
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    string sql = @"select * from HinhThuc where MaHK = @TenHK";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHK", selectIdCombobox(comboBox3, "MaHK"));
                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter1.Fill(table);

                        comboBox4.DataSource = table;
                        comboBox4.DisplayMember = "HinhThuc";
                        comboBox4.ValueMember = "MaHT";
                    }

                   
                    sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,HocKy.MaHK,TenHK,HinhThuc.MaHT,HinhThuc,ID_Mon,MaMon,TenMon,SoGioLT,SoGioTH  
            from KhoaHoc, NganhHoc, HocKy, HinhThuc,MonHoc 
            where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
            AND NganhHoc.MaNganh = HocKy.MaNganh 
            AND HocKy.MaHK = HinhThuc.MaHK and HinhThuc.MaHT=MonHoc.MaHT and KhoaHoc.MaKhoa=@MaKhoa";
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
        
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                   

                    sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,HocKy.MaHK,TenHK,HinhThuc.MaHT,HinhThuc,ID_Mon,MaMon,TenMon,SoGioLT,SoGioTH  
                    from KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc 
                    where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
                    AND NganhHoc.MaNganh = HocKy.MaNganh 
                    AND HocKy.MaHK = HinhThuc.MaHK 
                    AND HinhThuc.MaHT = MonHoc.MaHT 
                    AND HinhThuc.MaHT = @maht";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maht", selectIdCombobox(comboBox4, "MaHT"));
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
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
                string mamon = row.Cells["MaMon"].Value.ToString();
                string tenmon = row.Cells["TenMon"].Value.ToString();
                string lt = row.Cells["SoGioLT"].Value.ToString();
                string th = row.Cells["SoGioTH"].Value.ToString();
                comboBox1.Text = tenKhoa;
                comboBox2.Text = tenNganh;
                comboBox3.Text = tenHK;
                comboBox4.Text = hinhthuc;
                textBox1.Text = mamon;
                textBox2.Text = tenmon;
                numericUpDown2.Text = lt;
                numericUpDown1.Text = th;
            }
        }
    }
}
