using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        BindingSource albumBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDOA albumsDOA = new AlbumsDOA();
           
            //connect the list to the grid view control
            albumBindingSource.DataSource = albumsDOA.getAllAlbums();
            dataGridView1.DataSource = albumBindingSource;

            //pictureBox1.Load("https://upload.wikimedia.org/wikipedia/en/5/5f/Morgan_Wallen_-_One_Thing_at_a_Time.png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDOA albumsDOA = new AlbumsDOA();

            //connect the list to the grid view control
            albumBindingSource.DataSource = albumsDOA.searchTitles(txt_ImageURL.Text);
            dataGridView1.DataSource = albumBindingSource;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            //get the row number clicked

            int rowClicked = dataGridView.CurrentCell.RowIndex;
           // MessageBox.Show("You clicked  row " + rowClicked);

            String imageURL = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();
           // MessageBox.Show("URL=" + imageURL);

            pictureBox1.Load(imageURL);
              


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //add a new item to the database
            Album album = new Album
            {
                AlbumName = txt_albumName.Text,
                ArtistName = txt_albumArtist.Text,
                Year = Int32.Parse(txt_albumYear.Text),
                ImageURL = txt_ImageURL.Text,
                Description = txt_description.Text,
            };

            AlbumsDOA albumsDOA = new AlbumsDOA();
            int result = albumsDOA.addOneAlbum(album);
            MessageBox.Show(result + " new row(s) instered");
        }
    }
}
