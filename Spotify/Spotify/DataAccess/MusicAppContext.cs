using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Spotify.DataAccess;

public partial class MusicAppContext : DbContext
{
    public MusicAppContext()
    {
    }

    public MusicAppContext(DbContextOptions<MusicAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123;database=MusicApp;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC0782222263");

            entity.ToTable("Account");

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasMany(d => d.Musics).WithMany(p => p.Accounts)
                .UsingEntity<Dictionary<string, object>>(
                    "Liked",
                    r => r.HasOne<Music>().WithMany()
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Liked__MusicId__440B1D61"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Liked__AccountId__4316F928"),
                    j =>
                    {
                        j.HasKey("AccountId", "MusicId").HasName("PK__Liked__258221A6ECB66C54");
                        j.ToTable("Liked");
                    });
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Artist__3214EC07A6B85D2B");

            entity.ToTable("Artist");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07938B594C");

            entity.ToTable("Category");

            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Music>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Music__3214EC0715F04C98");

            entity.ToTable("Music");

            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Local).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Artist).WithMany(p => p.Musics)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Music__ArtistId__3F466844");

            entity.HasOne(d => d.Category).WithMany(p => p.Musics)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Music__CategoryI__403A8C7D");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3214EC07B9E85353");

            entity.ToTable("Playlist");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Playlist__Accoun__38996AB5");

            entity.HasMany(d => d.Musics).WithMany(p => p.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                    "ListSong",
                    r => r.HasOne<Music>().WithMany()
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ListSongs__Music__47DBAE45"),
                    l => l.HasOne<Playlist>().WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ListSongs__Playl__46E78A0C"),
                    j =>
                    {
                        j.HasKey("PlaylistId", "MusicId").HasName("PK__ListSong__A21EE3A0E4D31054");
                        j.ToTable("ListSongs");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
