using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Kitaplik_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\ASUS\Desktop\Kitaplik.mdb");
        void listele()
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter("select *from Kitaplar", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        string durum = "";
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into Kitaplar(KitapAd,Yazar,Tur,Sayfa,Durum)values(@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKitapAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komut.Parameters.AddWithValue("@p3", CmbTur.Text);
            komut.Parameters.AddWithValue("@p4", TxtSayfa.Text);
            komut.Parameters.AddWithValue("@p5",durum);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

              
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtKitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

        }

        private void BtnSil_Click(object sender, EventArgs e) {

            baglanti.Open();
            OleDbCommand sil = new OleDbCommand("Delete from Kitaplar where Kitapid=@a1", baglanti);
            sil.Parameters.AddWithValue("@a1", TxtKitapid.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Silinmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand g = new OleDbCommand("update Kitaplar set KitapAd=@d1,Yazar=@d2,Tur=@d3,Sayfa=@d4,Durum=@d5 where Kitapid=@d6 ", baglanti);
            g.Parameters.AddWithValue("@d1", TxtKitapAd.Text);
            g.Parameters.AddWithValue("@d2", TxtYazar.Text);
            g.Parameters.AddWithValue("@d3", CmbTur.Text);
            g.Parameters.AddWithValue("@d4", TxtSayfa.Text);
            if (radioButton1.Checked == true)
            {
                g.Parameters.AddWithValue("@d5", durum);
            }
            if (radioButton2.Checked == true)
            {
                g.Parameters.AddWithValue("@d5", durum);
            }
            g.Parameters.AddWithValue("@d6", TxtKitapid.Text);
            g.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Bilgileri güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            OleDbCommand bul = new OleDbCommand("select *from Kitaplar where KitapAd=@s1", baglanti);
            bul.Parameters.AddWithValue("@s1", TxtKitapBul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(bul);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void TxtKitapBul_TextChanged(object sender, EventArgs e)
        {
            //Textboxa yazdıkça datagridde bulur.
            OleDbCommand bul = new OleDbCommand("select *from Kitaplar where KitapAd like '%"+TxtKitapBul.Text+"%'", baglanti);

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(bul);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {

            //Ara butonuna bastıkça datagridde bulur.
            OleDbCommand bul = new OleDbCommand("select *from Kitaplar where KitapAd like '%" + TxtKitapBul.Text + "%'", baglanti);

            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(bul);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
