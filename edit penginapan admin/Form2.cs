using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using System.Text.RegularExpressions;

namespace edit_penginapan_admin
{
    public partial class Form2 : Form
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=Pariwisata;User Id=postgres;Password=timotius;");
        public Form2()
        {
            InitializeComponent();
        }
        string imglocation = "";
        SqlCommand cmd;

        private void Form2_Load(object sender, EventArgs e)
        {

        }

       
        private void pb_foto_penginapan_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imglocation = dialog.FileName.ToString();
                pb_foto_penginapan.ImageLocation = imglocation;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            byte[] images = null;
            

            FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            images = brs.ReadBytes((int)stream.Length);

            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into edit_penginapan (judul, keterangan, kapasitas, nomor_kamar, fasilitas, harga, foto) values ('" + tb_judul.Text + "','" + tb_keterangan.Text + "','" + tb_kapasitas.Text + "','" + tb_nomor_kamar.Text + "','" + tb_fasilitas.Text + "','" + tb_harga.Text + "',@images) ";
            cmd.Parameters.Add(new NpgsqlParameter("@images", images));
            cmd.ExecuteNonQuery();
            conn.Close();


            if (tb_judul.Text == "")
            {
                MessageBox.Show("Masukan Judul Terlebih Dahulu");

            }
            else if (tb_keterangan.Text == "")
            {
                MessageBox.Show("Masukan Keterangan Terlebih Dahulu");
            }
            else if (tb_kapasitas.Text == "")
            {
                MessageBox.Show("Masukan Kapasitas Terlebih Dahulu");
            }
            else if (tb_nomor_kamar.Text == "")
            {
                MessageBox.Show("Masukan Nomor Kamar Terlebih Dahulu");
            }
            else if (tb_fasilitas.Text == "")
            {
                MessageBox.Show("Masukan Fasilitas Terlebih Dahulu");
            }
            else if (tb_harga.Text == "")
            {
                MessageBox.Show("Masukan Harga Terlebih Dahulu");
            }
            else
            {
                //cmd.ExecuteNonQuery();
                MessageBox.Show("Data Berhasil Disimpan");
                tb_judul.Text = "";
                tb_keterangan.Text = "";
                tb_kapasitas.Text = "";
                tb_nomor_kamar.Text = "";
                tb_fasilitas.Text = "";
                tb_harga.Text = "";
            }
            


        }

        public void button1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from edit_penginapan where judul = '" + tb_judul.Text + "', keterangan = '"+tb_keterangan.Text+"', kapasitas = '"+tb_kapasitas.Text+"',nomor_kamar = '"+tb_nomor_kamar.Text+"', fasilitas = '"+tb_fasilitas.Text+"', harga = '"+tb_harga.Text+"', foto = @images ";
            //cmd.ExecuteNonQuery();
            //koneksi.Close();
            //tb_hapus_idstaf.Text = "";

            if (tb_judul.Text == "")
            {
                MessageBox.Show("Masukan Judul Terlebih Dahulu");

            }
            else if (tb_keterangan.Text == "")
            {
                MessageBox.Show("Masukan Keterangan Terlebih Dahulu");
            }
            else if (tb_kapasitas.Text == "")
            {
                MessageBox.Show("Masukan Kapasitas Terlebih Dahulu");
            }
            else if (tb_nomor_kamar.Text == "")
            {
                MessageBox.Show("Masukan Nomor Kamar Terlebih Dahulu");
            }
            else if (tb_fasilitas.Text == "")
            {
                MessageBox.Show("Masukan Fasilitas Terlebih Dahulu");
            }
            else if (tb_harga.Text == "")
            {
                MessageBox.Show("Masukan Harga Terlebih Dahulu");
            }
            else
            {
                cmd.ExecuteNonQuery(); 
                MessageBox.Show("Data Berhasil Dihapus");
                this.Close();
            }
            conn.Close();
        }
    }
}
