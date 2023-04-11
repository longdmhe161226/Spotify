using System;
using System.Collections.Generic;

namespace Spotify.DataAccess;

public partial class Playlist
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Music> Musics { get; } = new List<Music>();
}
