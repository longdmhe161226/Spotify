using System;
using System.Collections.Generic;

namespace Spotify.DataAccess;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Music> Musics { get; } = new List<Music>();
}
