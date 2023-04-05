using System;
using System.Collections.Generic;

namespace SpotifyApp
{
    internal class Player
    {
        private Song currentSong;
        private List<Song> currentSongList;

        private bool isPlaying = true;
        private bool isPaused = false;

        private Song testsong = new Song("test", 120, "Thierry Tomaat", "Pop");

        public Player()
        {

        }

        public Song setCurrentSong
        {
            get { return currentSong; }
            set { currentSong = value; }
        }

        public void playCurrentSong()
        {
            string maxStringTime = convertTime(testsong.Length);
            string currentStringTime;
            int currentTime = 0;

            while (isPlaying)
            {
                while (isPaused)
                {
                    Console.Write("\r[PAUSED] Press 'p' to resume or 's' to stop...");
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.P)
                        isPaused = false;
                    else if (key.Key == ConsoleKey.S)
                        isPlaying = false;
                }

                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"Currently playing: {testsong.Name} by {testsong.Artist}");
                currentStringTime = convertTime(currentTime);
                Console.Write($"[ {currentStringTime} / {maxStringTime} ] ");

                if (currentTime >= testsong.Length)
                {
                    isPlaying = false;
                }

                Task.Delay(1000).Wait();
                currentTime++;

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.P)
                        isPaused = true;
                    else if (key.Key == ConsoleKey.S)
                        isPlaying = false;
                }
            }
        }

        private string convertTime(int time)
        {
            int minute = (int)(time / 60);
            int second = (int)(time % 60);

            string secondCheck = "";

            if (second < 10)
                secondCheck = "0";

            return $"{minute}:{secondCheck}{second}";
        }

        public void addSongToQueue(Song song)
        {
            currentSongList.Add(song);
        }
        public void addSongToQueue(List<Song> songs)
        {
            foreach (Song song in songs)
            {
                currentSongList.Add(song);
            }
        }
    }
}
