namespace SpotifyApp
{
    internal class Friend
    {
        private string _name;
        private List<Playlist> _playlists;

        public Friend(string name)
        {
            _name = name;
            _playlists = new List<Playlist>();
        }

        public string Name
        {
            get { return _name; }
        }

        public List<Playlist> Playlists { get { return _playlists; } }

        public void AddPlaylist(string name)
        {
            Playlist playlist = _playlists.FirstOrDefault(p => p._name == name);

            if (playlist == null)
            {
                _playlists.Add(new Playlist(name, new List<Song>()));
            }
        }

        public void AddPlaylist(string name, List<Song> songs)
        {
            Playlist playlist = _playlists.FirstOrDefault(p => p._name == name);

            if (playlist == null)
            {
                _playlists.Add(new Playlist(name, songs));
            }
        }

        public void AddSongToPlaylist(string playlistName, Song song)
        {
            Playlist playlist = _playlists.FirstOrDefault(p => p._name == playlistName);

            if (playlist != null)
            {
                playlist.AddSong(song);
            }
        }

        public void AddSongsToPlaylist(string playlistName, List<Song> songs)
        {
            Playlist playlist = _playlists.FirstOrDefault(p => p._name == playlistName);

            if (playlist != null)
            {
                playlist.AddSong(songs);
            }
        }
    }
}
