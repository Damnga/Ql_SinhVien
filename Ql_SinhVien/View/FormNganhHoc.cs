
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
    public partial class FormNganhHoc : Form
    {
        string nguon = @"Data Source=DESKTOP-B0TRKC5;Initial Catalog=QLSV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        string sql = @"";
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader rdr;
        public FormNganhHoc()
        {
            InitializeComponent();

        }

        public void FormNganhHoc_Load(object sender, EventArgs e)
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
            sql = @"select KhoaHoc.MaKhoa ,TenKhoa,MaNganh,TenNganh from KhoaHoc,NganhHoc where KhoaHoc.MaKhoa=NganhHoc.MaKhoa";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private bool IsDataDuplicate(string newData,string tenkhoa)
        {

            try
            {
                conn = new SqlConnection(nguon);
                sql = "SELECT COUNT(*) FROM KhoaHoc,NganhHoc WHERE KhoaHoc.MaKhoa=NganhHoc.MaKhoa and  TenNganh = @TenNganh and TenKhoa=@TenKhoa";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenNganh", newData);
                cmd.Parameters.AddWithValue("@TenKhoa", tenkhoa);
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
                if (IsDataDuplicate(textBox2.Text,comboBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    string sql = @"insert into NganhHoc (TenNganh, MaKhoa) values (@TenNganh, @MaKhoa) ";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNganh", textBox2.Text);
                        cmd.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1,"MaKhoa"));
                        int rowsAffected = cmd.ExecuteNonQuery();
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
                if (IsDataDuplicate(textBox2.Text, comboBox1.Text))
                {
                    MessageBox.Show("Dữ liệu đã tồn tại!");
                    return;
                }
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    string sql = @"update NganhHoc set TenNganh=@TenNganh,MaKhoa=@MaKhoa where MaNganh=@MaNganh";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenNganh", textBox2.Text);
                        cmd.Parameters.AddWithValue("@MaKhoa", int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                        cmd.Parameters.AddWithValue("@MaNganh", int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString()));
                        int rowsAffected = cmd.ExecuteNonQuery();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    string sql = @"delete from NganhHoc where MaNganh=@MaNganh";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNganh", int.Parse(dataGridView1.CurrentRow.Cells[2].Value.ToString()));
                        int rowsAffected = cmd.ExecuteNonQuery();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn cần phải xóa học kì thuộc ngành này trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int selectIdCombobox(System.Windows.Forms.ComboBox x, string y)
        {
            DataRowView rowView = (DataRowView)x.SelectedItem;
            return Convert.ToInt32(rowView[y]);
        }
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            conn = new SqlConnection(nguon);
            conn.Open();
            sql = @"select KhoaHoc.MaKhoa ,TenKhoa,MaNganh,TenNganh from KhoaHoc,NganhHoc where KhoaHoc.MaKhoa=NganhHoc.MaKhoa and KhoaHoc.MaKhoa=@MaKhoa";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@MaKhoa", selectIdCombobox(comboBox1,"MaKhoa"));
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
                comboBox1.Text = tenKhoa;
                textBox2.Text = tenNganh;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            comboBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hien();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
                
        }
    

