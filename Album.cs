using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApp
{
    internal class Album
    {
        private string _name;
        private List<Song> _songs = new List<Song>();
        private string _artist;

        public Album(string name, string artist)
        {
            _name = name;
            _artist = artist;
        }

        public string Name { get { return _name; } }
        public string Artist { get { return _artist; } }
        public List<Song> Songs { get { return _songs; } }

        public void AddSong(Song song)
        {
            _songs.Add(song);
        }
    }
}
