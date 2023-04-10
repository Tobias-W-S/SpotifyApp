namespace SpotifyApp
{
    internal class User
    {
        private string _username;
        private List<Playlist> _playlists;

        public User(string username)
        {
            _username = username;
            _playlists = new List<Playlist>();
        }

        public void CreatePlaylist(string name)
        {
            Playlist playlist = _playlists.FirstOrDefault(p => p._name == name);

            if (playlist == null)
            {
                _playlists.Add(new Playlist(name, new List<Song>()));
            }
        }

        public void RemovePlaylistByName(string playlistName)
        {
            Playlist playlist = _playlists.FirstOrDefault(p => p._name == playlistName);

            if (playlist != null)
            {
                _playlists.Remove(playlist);
            }
        }

        public List<Playlist> GetPlaylists()
        {
            return _playlists;
        }

        public Playlist GetPlaylistByName(string playlistName)
        {
            return _playlists.FirstOrDefault(p => p._name == playlistName);
        }

        public void AddSongToPlaylist(string playlistName, Song song)
        {
            Playlist playlist = GetPlaylists().FirstOrDefault(p => p._name == playlistName);

            if (playlist != null)
            {
                playlist.AddSong(song);
            }
            else
            {
                Console.WriteLine($"Playlist '{playlistName}' not found.");
            }
        }
    }
}