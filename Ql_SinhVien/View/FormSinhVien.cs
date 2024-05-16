using Microsoft.AnalysisServices.Tabular;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ql_SinhVien
{
    public partial class FormSinhVien : Form
    {
        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlCommand cmd;
        SqlConnection conn;
        SqlDataReader rdr;
        public FormSinhVien()
        {
            InitializeComponent();
        }

        private void FormSinhVien_Load(object sender, EventArgs e)
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
            sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,LopHoc.MaLop,TenLop,ID_SV,MaSV,TenSV,NgaySinh,GioiTinh,QueQuan,SoThich
            from KhoaHoc, NganhHoc,LopHoc,SinhVien
            where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
            AND NganhHoc.MaNganh = LopHoc.MaNganh
            and LopHoc.MaLop=SinhVien.MaLop ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private bool IsDataDuplicate(string newData, string tenngang, string tenlop,string tensv)
        {
            try
            {
                conn = new SqlConnection(nguon);
                sql = "SELECT COUNT(*) FROM KhoaHoc,NganhHoc,LopHoc,SinhVien WHERE KhoaHoc.MaKhoa=NganhHoc.MaKhoa and NganhHoc.MaNganh=LopHoc.MaNganh and LopHoc.MaLop=SinhVien.MaLop and TenKhoa=@ten and TenNganh=@tennganh and TenLop = @tenLop and MaSV=@masv ";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ten", newData);
                cmd.Parameters.AddWithValue("@tennganh", tenngang);
                cmd.Parameters.AddWithValue("@tenlop", tenlop);
                cmd.Parameters.AddWithValue("@masv",tensv );
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

                        conn.Close();

                        comboBox3.DataSource = table;
                        comboBox3.DisplayMember = "TenNganh";
                        comboBox3.ValueMember = "MaNganh";
                    }

                    conn.Open();
                    sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, LopHoc.MaLop, TenLop, ID_SV, MaSV, TenSV, NgaySinh, GioiTinh, QueQuan, SoThich 
        FROM KhoaHoc, NganhHoc, LopHoc, SinhVien
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
        AND KhoaHoc.MaKhoa = @MaKhoa";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1, "MaKhoa"));
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }

                    conn.Close();
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
                    string sql = @"SELECT * FROM LopHoc WHERE MaNganh = @TenNganh";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNganh", selectIdCombobox(comboBox3, "MaNganh"));
                        SqlDataAdapter adapter1 = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter1.Fill(table);

                        comboBox4.DataSource = table;
                        comboBox4.DisplayMember = "TenLop";
                        comboBox4.ValueMember = "MaLop";
                        comboBox4.Text = "";
                   
                    }

                    sql = @"SELECT KhoaHoc.MaKhoa, TenKhoa, NganhHoc.MaNganh, TenNganh, LopHoc.MaLop, TenLop, ID_SV, MaSV, TenSV, NgaySinh, GioiTinh, QueQuan, SoThich 
        FROM KhoaHoc, NganhHoc, LopHoc, SinhVien
        WHERE KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
        AND NganhHoc.MaNganh = LopHoc.MaNganh
        AND LopHoc.MaLop = SinhVien.MaLop 
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
        
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = new SqlConnection(nguon);
            conn.Open();
            sql = @"select KhoaHoc.MaKhoa,TenKhoa,NganhHoc.MaNganh,TenNganh,LopHoc.MaLop,TenLop,ID_SV,MaSV,TenSV,NgaySinh,GioiTinh,QueQuan,SoThich from KhoaHoc, NganhHoc,LopHoc,SinhVien
            where KhoaHoc.MaKhoa = NganhHoc.MaKhoa 
            AND NganhHoc.MaNganh = LopHoc.MaNganh
            and LopHoc.MaLop=SinhVien.MaLop and LopHoc.MaLop=@MaLop";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@MaLop", selectIdCombobox(comboBox4,"MaLop"));
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
                string tenLOP = row.Cells["Tenlop"].Value.ToString();
                string masv = row.Cells["MaSV"].Value.ToString();
                string tensv = row.Cells["TenSV"].Value.ToString();
                string ngaysinh = row.Cells["NgaySinh"].Value.ToString();
                string gioitinh = row.Cells["GioiTinh"].Value.ToString();
                string quequan = row.Cells["QueQuan"].Value.ToString();
                string sothich = row.Cells["SoThich"].Value.ToString();
                comboBox1.Text = tenKhoa;
                comboBox3.Text = tenNganh;
                comboBox4.Text = tenLOP;
                textBox1.Text= masv;
                textBox2.Text = tensv;
                dateTimePicker1.Text = ngaysinh;
                if (gioitinh == "Nam")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                radioButton1.Text = gioitinh;
                textBox3.Text = quequan;
                if (sothich.Contains("Âm Nhạc"))
                {
                    checkBox1.Checked = true;
                }
                if (sothich.Contains("Thể Thao"))
                {
                    checkBox2.Checked = true;
                }
                if (sothich.Contains("Du Lịch"))
                {
                    checkBox3.Checked = true;
                }
                if (sothich.Contains("Học Bài"))
                {
                    checkBox4.Checked = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(comboBox1.Text, comboBox3.Text, comboBox4.Text, textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"insert into SinhVien (MaSV,TenSV,NgaySinh,GioiTinh,QueQuan,SoThich,MaLop)
                    values(@masv,@ten,@ngaysinh,@gioitinh,@quequan,@sothich,@malop)";
                cmd = new SqlCommand(sql, conn);

                string gioitinh = radioButton1.Checked ? "Nam" : "Nữ";

                List<string> selectedSothich = new List<string>();
                if (checkBox1.Checked)
                {
                    selectedSothich.Add(checkBox1.Text);
                }
                if (checkBox2.Checked)
                {
                    selectedSothich.Add(checkBox2.Text);
                }
                if (checkBox3.Checked)
                {
                    selectedSothich.Add(checkBox3.Text);
                }
                if (checkBox4.Checked)
                {
                    selectedSothich.Add(checkBox4.Text);
                }
                string sothichValue = string.Join(", ", selectedSothich);

                cmd.Parameters.AddWithValue("@masv", textBox1.Text);
                cmd.Parameters.AddWithValue("@ten", textBox2.Text);
                cmd.Parameters.AddWithValue("@ngaysinh", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
                cmd.Parameters.AddWithValue("@quequan", textBox3.Text);
                cmd.Parameters.AddWithValue("@sothich", sothichValue);
                cmd.Parameters.AddWithValue("@malop", selectIdCombobox(comboBox4, "MaLop"));
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
               
                if (rowsAffected > 0)
                {
                    conn = new SqlConnection(nguon);
                    sql = @"insert into TaiKhoan(TenTK,MatKhau,PhanQuyen) values(@TenTK,123,'user')";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TenTK", textBox1.Text);
                    conn.Open();
                    int rowsAffected1 = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAffected1 > 0)
                    {
                        MessageBox.Show("Tạo tài khoản cho sinh vien " + textBox2.Text + " với tên tài khoản là: " + textBox1.Text + " mật khảu: 123 ");
                        Hien();
                    }
                    else
                    {
                        MessageBox.Show("Thêm tài khoản không thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi : " + ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsDataDuplicate(comboBox1.Text, comboBox3.Text, comboBox4.Text, textBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                conn = new SqlConnection(nguon);
                sql = @"update SinhVien set MaSV=@masv,TenSV=@ten,NgaySinh=@ngaysinh,GioiTinh=@gioitinh,QueQuan=@quequan,SoThich=@sothich,MaLop=@malop where ID_SV=@idsv";
                cmd = new SqlCommand(sql, conn);
                string gioitinh = radioButton1.Checked ? "Nam" : "Nữ";
                List<string> selectedSothich = new List<string>();
                if (checkBox1.Checked)
                {
                    selectedSothich.Add(checkBox1.Text);
                }
                if (checkBox2.Checked)
                {
                    selectedSothich.Add(checkBox2.Text);
                }
                if (checkBox3.Checked)
                {
                    selectedSothich.Add(checkBox3.Text);
                }
                if (checkBox4.Checked)
                {
                    selectedSothich.Add(checkBox4.Text);
                }

             
                string sothichValue = string.Join(", ", selectedSothich);

                cmd.Parameters.AddWithValue("@masv", textBox1.Text);
                cmd.Parameters.AddWithValue("@ten", textBox2.Text);
                cmd.Parameters.AddWithValue("@ngaysinh", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
                cmd.Parameters.AddWithValue("@quequan", textBox3.Text);
                cmd.Parameters.AddWithValue("@sothich", sothichValue); 
                cmd.Parameters.AddWithValue("@malop", int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString()));
                cmd.Parameters.AddWithValue("@idsv", int.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString()));

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
                MessageBox.Show("Đã xảy ra lỗi : " + ex.Message);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(nguon);
                sql = @"delete from SinhVien where ID_SV=@idsv";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idsv", int.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString()));
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
                MessageBox.Show("Bạn cần phải xóa điểm của sinh viên này trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox4.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Hien();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
         
        }
        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {

        }
    }
}
