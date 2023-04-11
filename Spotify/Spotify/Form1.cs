using Bunifu.UI.WinForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Spotify.DataAccess;
using System.Windows.Forms;

namespace Spotify
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlayList_Load()
        {
            flowLayoutPanel2.Controls.Clear();
            using (var context = new MusicAppContext())
            {
                List<Playlist> list = new List<Playlist>();
                list = context.Playlists.Where(p => p.AccountId == 1).ToList();
                foreach (var playlist in list)
                {
                    BunifuLabel lb = new BunifuLabel();
                    lb.Text = playlist.Name;
                    lb.ForeColor = Color.White;
                    lb.Click += new EventHandler(Playlist_Click);
                    lb.Tag = playlist.Id;
                    flowLayoutPanel2.Controls.Add(lb);
                }

            }
        }

        private void Playlist_Click(object? sender, EventArgs e)
        {
            PlayList_Load();

            BunifuLabel lb = (BunifuLabel)sender;
            int playlistid = (int)lb.Tag;
            using (var context = new MusicAppContext())
            {
                var playlist = context.Playlists.FirstOrDefault(x => x.Id == playlistid);
                Label Name = new Label();
                Name.Text = playlist.Name;
                Name.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                Name.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                //Name.Location = new System.Drawing.Point(445, 87);
                Name.Size = new System.Drawing.Size(1000, 86);
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(Name);
                Button btn = new Button();
                btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
                btn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                btn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                btn.Margin = new System.Windows.Forms.Padding(5);
                btn.Size = new System.Drawing.Size(105, 41);
                btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                btn.Text = "Rename";
                flowLayoutPanel1.Controls.Add(btn);
                Button btndel = new Button();
                btndel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
                btndel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                btndel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                btndel.Margin = new System.Windows.Forms.Padding(5);
                btndel.Size = new System.Drawing.Size(105, 41);
                btndel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                btndel.Text = "Delete";
                flowLayoutPanel1.Controls.Add(btndel);
                btn.Tag = playlistid;
                btndel.Tag = playlistid;

                btndel.Click += new EventHandler(Delete);
                btn.Click += new EventHandler(RenameButton_Click);



                var musics = context.Musics.FromSqlRaw("SELECT m.* FROM Music m JOIN ListSongs ls ON m.ID = ls.MusicID WHERE ls.PlaylistID = " + playlistid).ToList();

                foreach (var music in musics)
                {

                    item it = new item();
                    it.NameItem = music.Name;
                    var artist = context.Artists.FirstOrDefault(a => a.Id == music.ArtistId);
                    it.NameArtist = artist.Name;
                    flowLayoutPanel1.Controls.Add(it);
                }

            }
        }

        private void Delete(object? sender, EventArgs e)
        {
            Button btndel = sender as Button;
            int playlistId = (int)btndel.Tag;
            using (var context = new MusicAppContext())
            {
                var playlist = context.Playlists.FirstOrDefault(x => x.Id == playlistId);
                if (playlist != null)
                {
                    context.Playlists.Remove(playlist);
                    context.SaveChanges();
                }
            }
            PlayList_Load();
            Home_Load();
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int playlistId = (int)btn.Tag;
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter new name", "Rename playlist", "");
            if (!string.IsNullOrEmpty(input))
            {
                using (var context = new MusicAppContext())
                {
                    var playlist = context.Playlists.FirstOrDefault(p => p.Id == playlistId);
                    if (playlist != null)
                    {
                        playlist.Name = input;
                        context.SaveChanges();
                    }
                }
            }
            PlayList_Load();
            using (var context = new MusicAppContext())
            {
                var playlist = context.Playlists.FirstOrDefault(a => a.Id == playlistId);
                Label Name = new Label();
                Name.Text = playlist.Name;
                Name.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                Name.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                Name.Location = new System.Drawing.Point(445, 87);
                Name.Size = new System.Drawing.Size(208, 86);
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(Name);
                flowLayoutPanel1.Controls.Add(btn);
                btn.Click += new EventHandler(RenameButton_Click);

                var musics = context.Musics.FromSqlRaw("SELECT m.* FROM Music m JOIN ListSongs ls ON m.ID = ls.MusicID WHERE ls.PlaylistID = " + playlistId).ToList();

                foreach (var itemm in musics)
                {
                    item it = new item();
                    it.NameItem = itemm.Name;
                    var artist = context.Artists.FirstOrDefault(a => a.Id == itemm.ArtistId);
                    it.NameArtist = artist.Name;
                    flowLayoutPanel1.Controls.Add(it);
                }
            }

        }

        private void Home_Load()
        {
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.VerticalScroll.Visible = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            flowLayoutPanel1.Controls.Clear();
            bunifuFlatButton1.Iconimage = global::Spotify.Properties.Resources.homeclicked1;
            bunifuFlatButton2.Iconimage = global::Spotify.Properties.Resources.search;
            bunifuFlatButton3.Iconimage = global::Spotify.Properties.Resources.lib;
            bunifuFlatButton5.Iconimage = global::Spotify.Properties.Resources.heart;
            PlayList_Load();

            ListItem listItemTop10 = new ListItem();
            listItemTop10.NameList = "Top 10";
            // 1, 12, 23, 8, 42, 3, 20, 7, 9, 34 
            listItemTop10.ids = new[] { 1, 12, 23, 8, 42, 3 };
            flowLayoutPanel1.Controls.Add(listItemTop10);
            ListItem listItemTopViews = new ListItem();
            listItemTopViews.NameList = "Top Views";
            listItemTopViews.ids = new[] { 12, 38, 14, 7, 3, 23 };
            flowLayoutPanel1.Controls.Add(listItemTopViews);
            using (var context = new MusicAppContext())
            {
                var accountId = 1;
                var sql = $"SELECT * FROM Music WHERE ID IN (SELECT MusicID FROM Liked WHERE AccountID = {accountId})";
                var musics = context.Musics.FromSqlRaw(sql).ToList();
                ListItem listItemLikedSong = new ListItem();
                listItemLikedSong.NameList = "Liked Song";
                listItemLikedSong.ids = musics.Select(m => m.Id).ToArray();
                flowLayoutPanel1.Controls.Add(listItemLikedSong);
            }



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Home_Load();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
           
            Home_Load();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

            PlayList_Load();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.VerticalScroll.Visible = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            bunifuFlatButton1.Iconimage = global::Spotify.Properties.Resources.home;
            bunifuFlatButton2.Iconimage = global::Spotify.Properties.Resources.search;
            bunifuFlatButton3.Iconimage = global::Spotify.Properties.Resources.lib;
            bunifuFlatButton5.Iconimage = global::Spotify.Properties.Resources.heartclicked;
            flowLayoutPanel1.Controls.Clear();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            using (var context = new MusicAppContext())
            {
                var maxPlaylistId = context.Playlists.ToList().Count;

                var newPlaylist = new Playlist
                {
                    Name = "My Playlist #" + (maxPlaylistId),
                    AccountId =1
                };

                context.Playlists.Add(newPlaylist);
                context.SaveChanges();

                PlayList_Load();


                int playlistid = (int)context.Playlists.Max(p => p.Id);

                var playlist = context.Playlists.FirstOrDefault(x => x.Id == playlistid);
                Label Name = new Label();
                Name.Text = playlist.Name;
                Name.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                Name.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
                //Name.Location = new System.Drawing.Point(445, 87);
                Name.Size = new System.Drawing.Size(1000, 86);
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(Name);
                Button btn = new Button();
                btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
                btn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                btn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                btn.Margin = new System.Windows.Forms.Padding(5);
                btn.Size = new System.Drawing.Size(105, 41);
                btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                btn.Text = "Rename";
                flowLayoutPanel1.Controls.Add(btn);
                Button btndel = new Button();
                btndel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
                btndel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                btndel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
                btndel.Margin = new System.Windows.Forms.Padding(5);
                btndel.Size = new System.Drawing.Size(105, 41);
                btndel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                btndel.Text = "Delete";
                flowLayoutPanel1.Controls.Add(btndel);
                btn.Tag = playlistid;
                btndel.Tag = playlistid;

                btndel.Click += new EventHandler(Delete);
                btn.Click += new EventHandler(RenameButton_Click);



                var musics = context.Musics.FromSqlRaw("SELECT m.* FROM Music m JOIN ListSongs ls ON m.ID = ls.MusicID WHERE ls.PlaylistID = " + playlistid).ToList();

                foreach (var music in musics)
                {

                    item it = new item();
                    it.NameItem = music.Name;
                    var artist = context.Artists.FirstOrDefault(a => a.Id == music.ArtistId);
                    it.NameArtist = artist.Name;
                    flowLayoutPanel1.Controls.Add(it);


                }
            }
        }


        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            PlayList_Load();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.VerticalScroll.Visible = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            bunifuFlatButton1.Iconimage = global::Spotify.Properties.Resources.home;
            bunifuFlatButton2.Iconimage = global::Spotify.Properties.Resources.searchclicked;
            bunifuFlatButton3.Iconimage = global::Spotify.Properties.Resources.lib;
            bunifuFlatButton5.Iconimage = global::Spotify.Properties.Resources.heart;
            flowLayoutPanel1.Controls.Clear();
            using (var context = new MusicAppContext())
            {
                List<Spotify.DataAccess.Category> categories = context.Categories.ToList();
                foreach (var cate in categories)
                {
                    PictureBox pictureBox = new PictureBox();
                    string ch = '"' + "";
                    pictureBox.Image = Image.FromFile(cate.Image.Replace(ch, ""));
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox.Tag = cate.Id;
                    pictureBox.Click += new EventHandler(Category_Click);

                    flowLayoutPanel1.Controls.Add(pictureBox);

                }
            }
        }
        private void Category_Click(object sender, EventArgs e)
        {


            PlayList_Load();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.VerticalScroll.Visible = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            bunifuFlatButton1.Iconimage = global::Spotify.Properties.Resources.home;
            bunifuFlatButton2.Iconimage = global::Spotify.Properties.Resources.searchclicked;
            bunifuFlatButton3.Iconimage = global::Spotify.Properties.Resources.lib;
            bunifuFlatButton5.Iconimage = global::Spotify.Properties.Resources.heart;
            flowLayoutPanel1.Controls.Clear();
            PictureBox pictureBox = sender as PictureBox;
            int categoryId = (int)pictureBox.Tag;
            using (var context = new MusicAppContext())
            {
                List<Music> musics = context.Musics.Where(m => m.CategoryId == categoryId).ToList(); ;
                foreach (var song in musics)
                {
                    item it = new item();
                    it.NameItem = song.Name;
                    var artist = context.Artists.FirstOrDefault(a => a.Id == song.ArtistId);
                    it.NameArtist = artist.Name;
                    flowLayoutPanel1.Controls.Add(it);
                }

            }
        }



        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            PlayList_Load();

            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.VerticalScroll.Visible = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            bunifuFlatButton1.Iconimage = global::Spotify.Properties.Resources.home;
            bunifuFlatButton2.Iconimage = global::Spotify.Properties.Resources.search;
            bunifuFlatButton3.Iconimage = global::Spotify.Properties.Resources.libclicked;
            bunifuFlatButton5.Iconimage = global::Spotify.Properties.Resources.heart;
            flowLayoutPanel1.Controls.Clear();

            using (var context = new MusicAppContext())
            {
                var playlists = context.Playlists.Where(p => p.AccountId == 1).ToList();
                foreach (var itemm in playlists)
                {
                    item it = new item();
                    it.NameItem = itemm.Name;

                    flowLayoutPanel1.Controls.Add(it);
                }
            }

        }


        //private void bunifuImageButton_MouseEnter(object sender, EventArgs e)
        //{
        //    bunifuImageButton1.BackColor = Color.Gray;
        //}

        private void bunifuImageButton_MouseLeave(object sender, EventArgs e)
        {
            BunifuImageButton btn = (BunifuImageButton)sender;
            btn.BackColor = Color.Black;
        }
        private void bunifuImageButton_MouseHover(object sender, EventArgs e)
        {
            BunifuImageButton btn = (BunifuImageButton)sender;
            if (btn.Name == "bunifuImageButton3")
            {
                bunifuImageButton3.BackColor = Color.Red;
            }
            else if (btn.Name == "bunifuImageButton1" || btn.Name == "bunifuImageButton2")
            {
                btn.BackColor = Color.Gray;
            }
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {

            Environment.Exit(0);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PlayList_Load();
            flowLayoutPanel1.Controls.Clear();
            string searchValue = textBox1.Text.Trim();

            using (var context = new MusicAppContext())
            {
                var musics = context.Musics
                    .Where(x => x.Name.Contains(searchValue))
                    .ToList();

                foreach (var itemm in musics)
                {
                    item it = new item();
                    it.NameItem = itemm.Name;
                    var artist = context.Artists.FirstOrDefault(a => a.Id == itemm.ArtistId);
                    it.NameArtist = artist.Name;
                    flowLayoutPanel1.Controls.Add(it);
                }
            }
        }

    }
}