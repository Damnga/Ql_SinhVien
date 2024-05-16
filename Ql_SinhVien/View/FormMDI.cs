using Ql_SinhVien.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ql_SinhVien
{
    
    public partial class FormMDI : Form
    {
       
        public FormMDI()         
        {
            InitializeComponent();
        }
       
        private void FormMDI_Load(object sender, EventArgs e)
        {
            this.khoaHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.ngànhHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.họcKỳToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.hìnhThứcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.mônHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.điểmToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.sinhVIÊNToolStripMenuItem.Enabled=ClassLogin.loginadmin;
            this.lớpToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.bảngĐiểmToolStripMenuItem.Enabled = ClassLogin.loginuser;
            this.giấyChứngNhậnSinhViênToolStripMenuItem.Enabled = ClassLogin.loginuser;
            this.danhSáchSinhViênToolStripMenuItem.Enabled = ClassLogin.loginuser;
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
          
        }

        private void khoaHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKhoaHoc fkh = new FormKhoaHoc();
            fkh.MdiParent = this;
            fkh.Show();
        }
        string ten;
     
        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formlogin flg = new Formlogin();
            flg.MdiParent = this;
            flg.Show();
            
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void ngànhHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNganhHoc fnh= new FormNganhHoc();
            fnh.MdiParent = this;
            fnh.Show();
        }

        private void họcKỳToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHocKy fhk= new FormHocKy();
            fhk.MdiParent = this;
            fhk.Show();
        }

        private void hìnhThứcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHinhThuc fht= new FormHinhThuc();
            fht.MdiParent = this;
            fht.Show();
        }

        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMonHoc fmh = new FormMonHoc();
            fmh.MdiParent = this;
            fmh.Show();
        }

        private void điểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDiem fd= new FormDiem();
            fd.MdiParent = this;
            fd.Show();
        }

        private void sinhVIÊNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSinhVien fsv = new FormSinhVien();
            fsv.MdiParent = this;
            fsv.Show();
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLopHoc flh= new FormLopHoc();
            flh.MdiParent = this;
            flh.Show();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassLogin.loginadmin = false;
            ClassLogin.loginuser = false;
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }
            this.khoaHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.ngànhHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.họcKỳToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.hìnhThứcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.mônHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.điểmToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.sinhVIÊNToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.lớpToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.bảngĐiểmToolStripMenuItem.Enabled = ClassLogin.loginuser;
            this.giấyChứngNhậnSinhViênToolStripMenuItem.Enabled = ClassLogin.loginuser;
            this.danhSáchSinhViênToolStripMenuItem.Enabled = ClassLogin.loginuser;

        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            this.bảngĐiểmToolStripMenuItem.Enabled = ClassLogin.loginuser;
            this.giấyChứngNhậnSinhViênToolStripMenuItem.Enabled = ClassLogin.loginuser;
            this.danhSáchSinhViênToolStripMenuItem.Enabled = ClassLogin.loginuser;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.khoaHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.ngànhHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.họcKỳToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.hìnhThứcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.mônHọcToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.điểmToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.sinhVIÊNToolStripMenuItem.Enabled = ClassLogin.loginadmin;
            this.lớpToolStripMenuItem.Enabled = ClassLogin.loginadmin;
        }

        private void bảngĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
           FormBangDiemSV fbd = new FormBangDiemSV();
            fbd.MdiParent = this;
            fbd.Show();
        }

        private void giấyChứngNhậnSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChungNhanSV FCN = new FormChungNhanSV();
            FCN.MdiParent = this;
            FCN.Show();
        }

        private void danhSáchSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDanhSachSV fds = new FormDanhSachSV();
            fds.MdiParent = this;
            fds.Show();
        }
    }
}
