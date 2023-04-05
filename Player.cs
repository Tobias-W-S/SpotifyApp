using System;
using System.Collections.Generic;

namespace SpotifyApp
{
    internal class Player
    {
        private Song currentSong;
        private List<Song> currentSongList;

        private bool isPlaying = true;

        private Song testsong = new Song("test", 10, "Thierry Tomaat", "Pop");
        private Song testsong2 = new Song("test", 16, "Thierry Tomaat", "Pop");

        public Player()
        {
            currentSongList = new List<Song>();
            addSongToQueue(testsong);
            addSongToQueue(testsong2);

        }

        public Song setCurrentSong
        {
            get { return currentSong; }
            set { currentSong = value; }
        }

        public void playCurrentSong()
        {
            isPlaying = true;
            if (currentSongList.Count == 0)
            {
                Console.WriteLine("No songs in queue.");
                return;
            }

            int currentSongIndex = 0;
            while (currentSongIndex < currentSongList.Count && isPlaying)
            {
                Song song = currentSongList[currentSongIndex];
                Console.Clear();
                Console.WriteLine($"Currently playing: {song.Name} by {song.Artist}");
                Console.WriteLine("Press S to go to the main menu, P to pauze the song, < to restart song and > to skip a song");
                int maxTime = song.Length;
                int currentTime = 0;

                while (currentTime <= maxTime && isPlaying)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"[ {convertTime(currentTime)} / {convertTime(maxTime)} ] ");
                    Task.Delay(1000).Wait();
                    currentTime++;

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.P)
                        {
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.WriteLine("\nPlayback paused.");
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.ReadKey(true);
                        }
                        else if (key.Key == ConsoleKey.S)
                        {
                            Console.WriteLine("\nPlayback stopped.");
                            isPlaying = false;
                            return;
                        }
                        else if (key.Key == ConsoleKey.LeftArrow && currentSongIndex > 0)
                        {
                            currentSongIndex--;
                            break;
                        }
                    }
                }
                currentSongIndex++;
            }

            Console.WriteLine("Playback complete.");
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
