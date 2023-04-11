using Bunifu.UI.WinForms;
using Microsoft.EntityFrameworkCore;
using Spotify.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify
{
    public partial class ListItem : UserControl
    {
        public int[] ids { get; set; }

        public string? NameList { get; set; }

        public string? ImageLoad { get; set; }
        public ListItem()
        {
            InitializeComponent();
        }

        private void bunifuLabel2_MouseEnter(object sender, EventArgs e)
        {
            BunifuLabel label = (BunifuLabel)sender;
            label.Font = new System.Drawing.Font("Segoe UI", label.Font.SizeInPoints, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);

        }

        private void bunifuLabel2_MouseLeave(object sender, EventArgs e)
        {
            BunifuLabel label = (BunifuLabel)sender;
            label.Font = new System.Drawing.Font("Segoe UI", label.Font.SizeInPoints, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

        }

        private void ListItem_Load(object sender, EventArgs e)
        {
            bunifuLabel1.Text = NameList;
            using (var db = new MusicAppContext())
            {
              
              
                var models = db.Musics.Where(m => ids.Contains(m.Id)).Take(6).ToList();

                item1.NameItem = models[0].Name;
                var artist1 = db.Artists.FirstOrDefault(a => a.Id == models[0].ArtistId);
                item1.NameArtist = artist1.Name;
                item2.NameItem = models[1].Name;
                var artist2 = db.Artists.FirstOrDefault(a => a.Id == models[1].ArtistId);
                item2.NameArtist = artist2.Name;
                item3.NameItem = models[2].Name;
                var artist3 = db.Artists.FirstOrDefault(a => a.Id == models[2].ArtistId);
                item3.NameArtist = artist3.Name;
                item4.NameItem = models[3].Name;
                var artist4 = db.Artists.FirstOrDefault(a => a.Id == models[3].ArtistId);
                item4.NameArtist = artist4.Name;
                item5.NameItem = models[4].Name;
                var artist5 = db.Artists.FirstOrDefault(a => a.Id == models[4].ArtistId);
                item5.NameArtist = artist5.Name;
                item6.NameItem = models[5].Name;
                var artist6 = db.Artists.FirstOrDefault(a => a.Id == models[5].ArtistId);
                item6.NameArtist = artist6.Name;

            }

        }
    }
}
