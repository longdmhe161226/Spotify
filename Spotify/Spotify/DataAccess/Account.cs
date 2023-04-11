using System;
using System.Collections.Generic;

namespace Spotify.DataAccess;

public partial class Account
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Playlist> Playlists { get; } = new List<Playlist>();

    public virtual ICollection<Music> Musics { get; } = new List<Music>();
}
