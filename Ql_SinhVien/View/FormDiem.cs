using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Ql_SinhVien
{
    public partial class FormDiem : Form
    {
        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader rdr;
        public FormDiem()
        {
            InitializeComponent();
        }

        private void FormDiem_Load(object sender, EventArgs e)
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
            sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
       ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
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
                    sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
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

                    string sql1 = @"SELECT * FROM LopHoc WHERE MaNganh = @TenNganh";
                    using (SqlCommand cmd = new SqlCommand(sql1, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNganh", selectIdCombobox(comboBox2, "MaNganh"));
                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter1.Fill(table);

                        comboBox6.DataSource = table;
                        comboBox6.DisplayMember = "TenLop";
                        comboBox6.ValueMember = "MaLop";
                    }
                    sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
        AND KhoaHoc.MaKhoa = @MaKhoa 
        and NganhHoc.MaNganh = @manganh";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa",selectIdCombobox(comboBox1,"MaKhoa"));
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


                    sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
        AND KhoaHoc.MaKhoa = @MaKhoa  and NganhHoc.MaNganh = @manganh and HocKy.MaHK=@MA";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                        cmd.Parameters.AddWithValue("@manganh", selectIdCombobox(comboBox2, "MaNganh"));
                        cmd.Parameters.AddWithValue("@MA", selectIdCombobox(comboBox3, "MaHK"));
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
                    string sql = @"select * from MonHoc where MaHT = @TenHT";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenHT", selectIdCombobox(comboBox4, "MaHT"));
                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter1.Fill(table);

                        comboBox5.DataSource = table;
                        comboBox5.DisplayMember = "TenMon";
                        comboBox5.ValueMember = "ID_Mon";
                    }
                    sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
        AND KhoaHoc.MaKhoa = @MaKhoa  and NganhHoc.MaNganh = @manganh and HocKy.MaHK=@MA AND HinhThuc.MaHT = @MAHT";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                        cmd.Parameters.AddWithValue("@manganh", selectIdCombobox(comboBox2, "MaNganh"));
                        cmd.Parameters.AddWithValue("@MA", selectIdCombobox(comboBox3, "MaHK"));
                        cmd.Parameters.AddWithValue("@MAHT", selectIdCombobox(comboBox4, "MaHT"));
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
    
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem != null)
            {
                sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
        AND KhoaHoc.MaKhoa = @MaKhoa  and NganhHoc.MaNganh = @manganh and HocKy.MaHK=@MA AND HinhThuc.MaHT = @MAHT and ID_Mon=@ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                    cmd.Parameters.AddWithValue("@manganh", selectIdCombobox(comboBox2, "MaNganh"));
                    cmd.Parameters.AddWithValue("@MA", selectIdCombobox(comboBox3, "MaHK"));
                    cmd.Parameters.AddWithValue("@MAHT", selectIdCombobox(comboBox4, "MaHT"));
                    cmd.Parameters.AddWithValue("@ma", selectIdCombobox(comboBox5, "ID_Mon"));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
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
                string monhoc = row.Cells["TenMon"].Value.ToString();
                string lop = row.Cells["TenLop"].Value.ToString();
                string TENSV = row.Cells["TenSV"].Value.ToString();
                string diem = row.Cells["Diem"].Value.ToString();
                comboBox1.Text = tenKhoa;
                comboBox2.Text = tenNganh;
                comboBox3.Text = tenHK;
                comboBox4.Text = hinhthuc;
                comboBox5.Text = monhoc;
                comboBox6.Text = lop;
                comboBox7.Text = TENSV;
                numericUpDown1.Text = diem;
            }
        }

        private void comboBox6_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem != null)
            {
                comboBox7.DataSource = null;
                conn = new SqlConnection(nguon);
                conn.Open();
                sql = @"select * from SinhVien where MaLop=@ma";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ma", selectIdCombobox(comboBox6, "MaLop"));
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable DT = new DataTable();
                adt.Fill(DT);
                comboBox7.DataSource = DT;
                comboBox7.DisplayMember = "TenSV";
                comboBox7.ValueMember = "ID_SV";
            }
            sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
        AND KhoaHoc.MaKhoa = @MaKhoa and NganhHoc.MaNganh = @manganh and LopHoc.MaLop=@malop ";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                cmd.Parameters.AddWithValue("@manganh", selectIdCombobox(comboBox2, "MaNganh"));
                cmd.Parameters.AddWithValue("@malop", selectIdCombobox(comboBox6, "Malop"));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void comboBox7_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, HocKy.MaHK, TenHK, HinhThuc.MaHT, HinhThuc,MonHoc.ID_Mon, MonHoc.MaMon, TenMon, SoGioLT, SoGioTH, LopHoc.MaLop, TenLop,SinhVien.MaSV, TenSV,Diem.MaDiem,Diem
        FROM KhoaHoc, NganhHoc, HocKy, HinhThuc, MonHoc, LopHoc, SinhVien, Diem
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = HocKy.MaNganh 
        AND HocKy.MaHK = HinhThuc.MaHK 
        AND HinhThuc.MaHT = MonHoc.MaHT
        AND MonHoc.ID_Mon = Diem.MaMon 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND SinhVien.ID_SV = Diem.MaSV 
                AND KhoaHoc.MaKhoa = @MaKhoa  and NganhHoc.MaNganh = @manganh and LopHoc.MaLop=@ma and SinhVien.ID_SV=@AAA";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                cmd.Parameters.AddWithValue("@manganh", selectIdCombobox(comboBox2, "MaNganh"));
                cmd.Parameters.AddWithValue("@ma", selectIdCombobox(comboBox6, "Malop"));
                cmd.Parameters.AddWithValue("@AAA", selectIdCombobox(comboBox7, "ID_SV"));
               
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(nguon);
                sql = @"insert into Diem (MaMon,MaSV,Diem) values(@MaMon,@MaSV,@Diem)";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Diem", numericUpDown1.Text);
                cmd.Parameters.AddWithValue("@MaSV", selectIdCombobox(comboBox7, "ID_SV"));
                cmd.Parameters.AddWithValue("@MaMon", selectIdCombobox(comboBox5, "ID_Mon"));
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
                conn = new SqlConnection(nguon);
                sql = @"update Diem set MaMon=@MaMon,MaSV=@MaSV,Diem=@Diem where MaDiem=@MaDiem";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Diem", numericUpDown1.Text);
                cmd.Parameters.AddWithValue("@MaSV", selectIdCombobox(comboBox7, "ID_SV"));
                cmd.Parameters.AddWithValue("@MaMon", selectIdCombobox(comboBox5, "ID_Mon"));
                cmd.Parameters.AddWithValue("@MaDiem", int.Parse(dataGridView1.CurrentRow.Cells[17].Value.ToString()));
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
                sql = @"delete from Diem where MaDiem=@MaDiem";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDiem", int.Parse(dataGridView1.CurrentRow.Cells[17].Value.ToString()));
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
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            numericUpDown1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hien();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
