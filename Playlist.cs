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

        public void AddSong(List<Song> songs)
        {
            foreach (Song song in songs)
            {
                AddSong(song);
            }
        }

        public void RemoveSongByName(string songName)
        {
            Song song = _songs.FirstOrDefault(s => s.Name == songName);

            if (song != null)
            {
                _songs.Remove(song);
            }
        }

        public static List<Song> GetCommonSongs(Playlist playlist1, Playlist playlist2)
        {
            List<Song> commonSongs = new List<Song>();

            foreach (Song song1 in playlist1.GetSongs())
            {
                foreach (Song song2 in playlist2.GetSongs())
                {
                    if (song1.Name == song2.Name)
                    {
                        commonSongs.Add(song1);
                    }
                }
            }

            return commonSongs;
        }

    }
}