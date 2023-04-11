using System;
using System.Collections.Generic;

namespace Spotify.DataAccess;

public partial class Music
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Local { get; set; }

    public int? ArtistId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Artist? Artist { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<Playlist> Playlists { get; } = new List<Playlist>();
}
