
CREATE TABLE Account(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50),
    Password NVARCHAR(50)
);
CREATE TABLE Playlist(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    AccountId INT FOREIGN KEY REFERENCES Account(Id)
);
CREATE TABLE Artist(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50)
);
CREATE TABLE Category(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    Image NVARCHAR(100)
);
CREATE TABLE Music(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50),
    Image NVARCHAR(100),
    Local NVARCHAR(100),
    ArtistId INT FOREIGN KEY REFERENCES Artist(Id),
    CategoryId INT FOREIGN KEY REFERENCES Category(Id)
);
CREATE TABLE Liked(
    AccountId INT FOREIGN KEY REFERENCES Account(Id),
    MusicId INT FOREIGN KEY REFERENCES Music(Id),
    PRIMARY KEY(AccountId, MusicId)
);
CREATE TABLE ListSongs(
    PlaylistId INT FOREIGN KEY REFERENCES Playlist(Id),
    MusicId INT FOREIGN KEY REFERENCES Music(Id),
    PRIMARY KEY(PlaylistId, MusicId)
);
