using SpotifyApp;

List<Song> songList = new List<Song>();
List<Album> albumList = new List<Album>();

Song TakeOnMe = new Song("Take on me", 225, "a-ha", "Pop");
Song TrainOfThought = new Song("Train of Thought", 254, "a-ha", "Pop");
Song LoveIsReason = new Song("Love is reason", 187, "a-ha", "Pop");
Song TheBlueSky = new Song("The blue sky", 156, "a-ha", "Pop");

Song NeverGonnaGiveYouUp = new Song("Never gonna give you up", 213, "Rick Astley", "Pop");
Song TogetherForever = new Song("Together forever", 205, "Rick Astley", "Pop");
Song SlippinAway = new Song("Slipping Away", 232, "Rick Astley", "Pop");

Album HuntingHighAndLow = new Album("Hunting high and low", "a-ha");
HuntingHighAndLow.AddSong(TakeOnMe);
HuntingHighAndLow.AddSong(TrainOfThought);
HuntingHighAndLow.AddSong(LoveIsReason);
HuntingHighAndLow.AddSong(TheBlueSky);

Album WheneverYouNeedSomebody = new Album("Whenever you need somebody", "Rick Astley");
WheneverYouNeedSomebody.AddSong(NeverGonnaGiveYouUp);
WheneverYouNeedSomebody.AddSong(TogetherForever);
WheneverYouNeedSomebody.AddSong(SlippinAway);


void SetSongAndAlbumList()
{
    //Adding songs in the main list will be done in a separate function in order to navigate throught the program.cs easier.
    albumList.Add(HuntingHighAndLow);
    songList.Add(TakeOnMe);
    songList.Add(TrainOfThought);
    songList.Add(LoveIsReason);
    songList.Add(TheBlueSky);

    albumList.Add(WheneverYouNeedSomebody);
    songList.Add(NeverGonnaGiveYouUp);
    songList.Add(TogetherForever);
    songList.Add(SlippinAway);
}

SetSongAndAlbumList();

Player player = new Player();
player.addSongToQueue(TakeOnMe);

User user = new User("Tobias");

user.CreatePlaylist("Examples");
user.CreatePlaylist("test2");
user.AddSongToPlaylist("Examples", TakeOnMe);
user.AddSongToPlaylist("Examples", TrainOfThought);


string[] menuOptions = { "Play Song", "Playlists", "Albums", "Quit" };

int cursorPosition = 0;

while (true)
{
    Console.Clear();

    Console.WriteLine("Main Menu\n");

    for (int i = 0; i < menuOptions.Length; i++)
    {
        if (i == cursorPosition)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(">> " + menuOptions[i]);
        }
        else
        {
            Console.WriteLine("   " + menuOptions[i]);
        }
        Console.ResetColor();
    }

    ConsoleKeyInfo keyInfo = Console.ReadKey();

    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        cursorPosition--;

        if (cursorPosition < 0)
        {
            cursorPosition = menuOptions.Length - 1;
        }
    }

    if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        cursorPosition++;

        if (cursorPosition >= menuOptions.Length)
        {
            cursorPosition = 0;
        }
    }

    if (keyInfo.Key == ConsoleKey.Enter)
    {
        switch (cursorPosition)
        {
            case 0:
                player.playCurrentSong();
                break;
            case 1:
                PlayListMenu();
                break;
            case 2:
                ShowAlbums();
                break;
            case 3:
                return;
        }
    }

    void PlayListMenu()
    {
        List<Playlist> tempPlaylist;
        bool isActive = true;
        ConsoleKeyInfo userInput;

        while (isActive)
        {
            Console.Clear();
            Console.WriteLine("To create a new playlist, press C. To show playlists, press S");
            Console.WriteLine("To exit, press E");
            userInput = Console.ReadKey(true);

            if (userInput.Key == ConsoleKey.E)
            {
                isActive = false;
            } 
            if (userInput.Key == ConsoleKey.C)
            {
                Console.WriteLine("Enter the playlist name");
                user.CreatePlaylist(Console.ReadLine());
                Console.WriteLine("Succesfully created, press any button to go back");
                Console.ReadKey(true);
            }
            else if (userInput.Key == ConsoleKey.S)
            {
                tempPlaylist = user.GetPlaylists();

                ShowPlaylists(tempPlaylist);
            }
        }
    }

    void ShowPlaylists(List<Playlist> playlists)
    {
        ConsoleKeyInfo keyInfo;
        bool isActive = true;
        int cursorPosition = 0;

        while (isActive)
        {
            Console.Clear();
            Console.WriteLine("To exit, press E. To show all the songs, press Enter. To delete playlist, press C\n");

            for (int i = 0; i < playlists.Count; i++)
            {
                if (i == cursorPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(">> " + playlists[i]._name);
                }
                else
                {
                    Console.WriteLine("   " + playlists[i]._name);
                }
                Console.ResetColor();
            }

            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.E)
            {
                isActive = false;
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                cursorPosition--;
                if (cursorPosition < 0)
                {
                    cursorPosition = playlists.Count - 1;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                cursorPosition++;
                if (cursorPosition >= playlists.Count)
                {
                    cursorPosition = 0;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Playlist selected: " + playlists[cursorPosition]._name);

                ShowPlaylistSongs(playlists[cursorPosition]);
            }
            else if (keyInfo.Key == ConsoleKey.C)
            {
                Console.Clear();
                Console.WriteLine("Are you sure you want to delete playlist " + playlists[cursorPosition]._name + "? (y/n)");
                ConsoleKeyInfo confirmation = Console.ReadKey(true);
                if (confirmation.Key == ConsoleKey.Y)
                {
                    playlists.RemoveAt(cursorPosition);
                    if (cursorPosition >= playlists.Count)
                    {
                        cursorPosition = playlists.Count - 1;
                    }
                }
            }
        }
    }

    void ShowPlaylistSongs(Playlist playlist)
    {
        int cursorPosition = 0;
        ConsoleKeyInfo keyInfo;
        bool isActive = true;

        while (isActive)
        {
            Console.Clear();
            Console.WriteLine("To go back, press E. To remove song, press C. To add a song, press S");

            for (int i = 0; i < playlist.GetSongs().Count; i++)
            {
                if (i == cursorPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(">> " + playlist.GetSongs()[i].Name);
                }
                else
                {
                    Console.WriteLine("   " + playlist.GetSongs()[i].Name);
                }
                Console.ResetColor();
            }

            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.E)
            {
                isActive = false;
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                cursorPosition--;
                if (cursorPosition < 0)
                {
                    cursorPosition = playlist.GetSongs().Count - 1;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                cursorPosition++;
                if (cursorPosition >= playlist.GetSongs().Count)
                {
                    cursorPosition = 0;
                }
            }
            else if (keyInfo.Key == ConsoleKey.C)
            {
                Console.WriteLine("Are you sure you want to delete song " + playlist.GetSongs()[cursorPosition].Name.ToString() + "? (y/n)");
                ConsoleKeyInfo confirmation = Console.ReadKey(true);

                if (confirmation.Key == ConsoleKey.Y)
                {
                    playlist.RemoveSongByName(playlist.GetSongs()[cursorPosition].Name.ToString());
                    if (cursorPosition >= playlist.GetSongs().Count)
                    {
                        cursorPosition = playlist.GetSongs().Count - 1;
                    }
                }
            }
            else if (keyInfo.Key == ConsoleKey.S)
            {
                Console.Clear();
                Console.WriteLine("Enter the name of one of the following songs/albums to add it to the playlist: \n");
                foreach (Song song in songList)
                {
                    Console.WriteLine(song.Name + " by " + song.Artist);
                }
                Console.WriteLine("\nALBUMS ");
                foreach (Album album in albumList)
                {
                    Console.WriteLine(album.Name + " by " + album.Artist);
                }
                Console.WriteLine();

                string songToAdd = Console.ReadLine();

                bool songAlreadyInPlaylist = false;
                foreach (Song song in playlist.GetSongs())
                {
                    if (song.Name == songToAdd)
                    {
                        songAlreadyInPlaylist = true;
                        Console.WriteLine(song.Name + " is already in the playlist.");
                        break;
                    }
                }

                if (!songAlreadyInPlaylist)
                {
                    Album album = albumList.FirstOrDefault(a => a.Name == songToAdd);
                    if (album != null)
                    {
                        foreach (Song song in album.Songs)
                        {
                            if (!playlist.GetSongs().Contains(song))
                            {
                                playlist.AddSong(song);
                                Console.WriteLine(song.Name + " has been added to the playlist.");
                            }
                        }
                    }
                    else
                    {
                        Song song = songList.FirstOrDefault(s => s.Name == songToAdd);
                        if (song != null)
                        {
                            playlist.AddSong(song);
                            Console.WriteLine(song.Name + " has been added to the playlist.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid song or album name.");
                        }
                    }
                }

                Console.ReadKey(true);
                cursorPosition = playlist.GetSongs().Count - 1;
            }
        }
    }

    void ShowAlbums()
    {
        List<Album> albums = albumList;
        ConsoleKeyInfo keyInfo;
        bool isActive = true;
        int cursorPosition = 0;

        while (isActive)
        {
            Console.Clear();
            Console.WriteLine("To exit, press E. To show all the songs, press Enter.\n");

            for (int i = 0; i < albumList.Count; i++)
            {
                if (i == cursorPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(">> " + albumList[i].Name + " by " + albumList[i].Artist);
                }
                else
                {
                    Console.WriteLine("   " + albumList[i].Name + " by " + albumList[i].Artist);
                }
                Console.ResetColor();
            }

            keyInfo = Console.ReadKey(true);
            if (keyInfo.Key == ConsoleKey.E)
            {
                isActive = false;
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                cursorPosition--;
                if (cursorPosition < 0)
                {
                    cursorPosition = albumList.Count - 1;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                cursorPosition++;
                if (cursorPosition >= albumList.Count)
                {
                    cursorPosition = 0;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Showing results of: " + albumList[cursorPosition].Name + "\n");

                foreach (Song song in albumList[cursorPosition].Songs)
                {
                    Console.WriteLine(song.Name + " Genre: " + song.Genre);
                }
                Console.ReadKey();
            }
        }
    }
}