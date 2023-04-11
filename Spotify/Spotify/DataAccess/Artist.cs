using System;
using System.Collections.Generic;

namespace Spotify.DataAccess;

public partial class Artist
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Music> Musics { get; } = new List<Music>();
}
