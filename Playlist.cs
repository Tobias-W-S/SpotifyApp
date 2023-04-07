using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApp
{
    internal class Playlist
    {
        public string _name;
        private List<Song> _songs;

        public Playlist(string name, List<Song> songs)
        {
            _name = name;
            _songs = songs;
        }

        public List<Song> GetSongs()
        {
            return _songs;
        }

        public void AddSong(Song song)
        {
            _songs.Add(song);
        }

        public void RemoveSongByName(string songName)
        {
            var song = _songs.FirstOrDefault(s => s.Name == songName);

            if (song != null)
            {
                _songs.Remove(song);
            }
        }
    }
}