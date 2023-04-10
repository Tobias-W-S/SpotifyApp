using System;
using System.Collections.Generic;

namespace SpotifyApp
{
    internal class Player
    {
        private static Random _random = new Random();

        private Song currentSong;
        private List<Song> currentSongList;

        private bool isPlaying = true;

        public Player()
        {
            currentSongList = new List<Song>();
        }

        public Song setCurrentSong
        {
            get { return currentSong; }
            set { currentSong = value; }
        }

        public List<Song> ShowQueue()
        {
            return currentSongList;
        }

        public void ShuffleQueue()
        {
            int n = currentSongList.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                Song song = currentSongList[k];
                currentSongList[k] = currentSongList[n];
                currentSongList[n] = song;
            }
        }

        public void ClearQueue()
        {
            currentSongList.Clear();
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
                Console.WriteLine("Press S to go to the main menu, P to pauze the song, < to restart song and > to skip song");
                
                int maxTime = song.Length;
                int currentTime = 0;

                while (currentTime <= maxTime && isPlaying)
                {
                    /* The program updates the current time of the song in the player, when the song reaches the max time it
                     * continues to the next song in the list, the user can still press the keys to perform a action.
                     This action will happen after the delay. This way the user can still perform action whilst listening*/

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
                        else if ( key.Key == ConsoleKey.LeftArrow)
                        { 
                            currentTime = 0;
                        }
                        else if ( key.Key == ConsoleKey.RightArrow)
                        {
                            break;
                        }
                    }
                }
                currentSongIndex++;
            }
            currentSongList.Clear();

            Console.WriteLine("Playback complete.");
        }

        //Simply exist to turn the time into minutes and seconds, making it easier to store the time.

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
