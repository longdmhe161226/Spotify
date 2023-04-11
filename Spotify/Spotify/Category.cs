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
    public partial class Category : UserControl
    {

        public int Id { get; set; }

        public string? NameCate { get; set; }

        public string? ImageLoad { get; set; }
        public Category()
        {
            InitializeComponent();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            string ch = '"' +"";
            pictureBox1.Image = Image.FromFile(ImageLoad.Replace(ch,""));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
