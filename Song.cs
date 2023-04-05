using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApp
{
    internal class Song
    {
        private string _name;
        private int _length;
        private string _artist;
        private string _genre;
        public Song(string name, int length, string artist, string genre) { 
            _name = name;
            _length = length;
            _artist = artist;
            _genre = genre;
        }

        public string Name { get { return _name; } }
        public int Length { get { return _length; } }
        public string Artist { get { return _artist; } }
        public string Genre { get { return _genre; } }

    }
}
