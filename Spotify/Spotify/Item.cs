using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify
{
    public partial class item : UserControl
    {

        public int Id { get; set; }

        public string? NameItem { get; set; }
        public string? NameArtist { get; set; }


        public string? ImageLoad { get; set; }
        public item()
        {
            InitializeComponent();
        }
        public void LLoad()
        {
            String name = NameItem;
            bunifuLabel1.Text = name;
            bunifuLabel2.Text = NameArtist;
        }
        private void Item_Load(object sender, EventArgs e)
        {
          
            LLoad();

        }

        private void Item_MouseHover(object sender, EventArgs e)
        {
            String name = NameItem;
            bunifuLabel1.Text = name;
            bunifuLabel2.Text = NameArtist;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
        }

        private void item_MouseLeave(object sender, EventArgs e)
        {
            String name = NameItem;
            bunifuLabel1.Text = name;
            bunifuLabel2.Text = NameArtist;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));

        }

       
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
          
        }
    }
}
