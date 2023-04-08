using SpotifyApp;

//Create all songs for app and additional testdata for the software.

List<Song> songList = new List<Song>();
List<Album> albumList = new List<Album>();

Song Testsong = new Song("Test", 10, "Example", "Experimental");

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

//Initialize both the Player and User, as only 1 of both will exist in the program.

Player player = new Player();
player.addSongToQueue(Testsong);

User user = new User("Tobias");

user.CreatePlaylist("Examples");
user.AddSongToPlaylist("Examples", TakeOnMe);
user.AddSongToPlaylist("Examples", TrainOfThought);


//Prints out a menu you can navigate using up and down arrows en select the rest with other keys that are noted in the page

string[] menuOptions = { "Play Song", "Current Queue", "Playlists", "Albums", "Quit" };

int cursorPosition = 0;

while (true)
{
    Console.Clear();

    Console.WriteLine("Main Menu, up and down to navigate and enter to go to the selected page\n");

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
                //This goes to the song player's main function
                player.playCurrentSong();
                break;
            case 1:
                //Shows the current queue of the player and allows the user to clear or shuffle the queue
                PlayerQueue();
                break;
            case 2:
                //Shows all the user created playlists and all the functionality to make, edit and delete playlists
                List<Playlist> tempPlaylist = user.GetPlaylists();

                ShowPlaylists(tempPlaylist);
                break;
            case 3:
                //Shows all the albums of the program and the songs.
                ShowAlbums();
                break;
            case 4:
                return;
        }
    }

    void PlayerQueue()
    {
        Console.Clear();
        ConsoleKeyInfo keyInfo;
        List<Song> queue = player.ShowQueue();
        Console.WriteLine("Here are the current songs in the queue in order:\n");

        foreach (Song song in queue)
        {
            Console.WriteLine(song.Name + " by " + song.Artist);
        }

        Console.WriteLine("\nTo shuffle the songs, press S. To clear queue, press C. Press any other key to leave");
        keyInfo = Console.ReadKey(true);

        if (keyInfo.Key == ConsoleKey.S)
        {
            player.ShuffleQueue();
            Console.WriteLine("Queue shuffled");
            Console.ReadKey();
        }
        else if (keyInfo.Key == ConsoleKey.C)
        {
            player.ClearQueue();
            Console.WriteLine("Queue cleared");
            Console.ReadKey();
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
            Console.WriteLine("To exit, press E. To show all the songs, press Enter. To delete playlist, press D. To add playlist to queue, press Q. To create playlist, press C\n");

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
            else if (keyInfo.Key == ConsoleKey.D)
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
            else if (keyInfo.Key == ConsoleKey.Q)
            {
                try { player.addSongToQueue(playlists[cursorPosition].GetSongs()); }
                catch { return; }
                Console.Clear();
                Console.WriteLine("Songs added");
                Console.ReadKey(true);
            }
            if (keyInfo.Key == ConsoleKey.C)
            {
                Console.WriteLine("Enter the playlist name");
                user.CreatePlaylist(Console.ReadLine());
                Console.WriteLine("Succesfully created, press any button to go back");
                Console.ReadKey(true);
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
            Console.WriteLine("To go back, press E. To remove selected song, press C. \nTo add a song to the playlist, press S. To add the selected song to the queue, press Q");

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
            else if (keyInfo.Key == ConsoleKey.Q)
            {
                try { player.addSongToQueue(playlist.GetSongs()[cursorPosition]); }
                catch { return; }
                Console.Clear();
                Console.WriteLine("Song added");
                Console.ReadKey(true);
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
            Console.WriteLine("To exit, press E. To show all the songs, press Enter. To add songs to queue press Q\n");

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
                ShowAlbumSongs(albumList[cursorPosition]);
            }
            else if (keyInfo.Key == ConsoleKey.Q)
            {
                player.addSongToQueue(albumList[cursorPosition].Songs);
                Console.Clear();
                Console.WriteLine("Songs added");
                Console.ReadKey(true);
            }
        }
    }

    void ShowAlbumSongs(Album album) {
        ConsoleKeyInfo keyInfo;
        bool isActive = true;
        int cursorPosition = 0;

        while (isActive)
        {
            Console.Clear();
            Console.WriteLine("To exit, press E. To show all the songs, press Enter. To add songs to queue press Q\n");

            for (int i = 0; i < album.Songs.Count; i++)
            {
                if (i == cursorPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(">> " + album.Songs[i].Name + " by " + album.Songs[i].Artist);
                }
                else
                {
                    Console.WriteLine("   " + album.Songs[i].Name + " by " + album.Songs[i].Artist);
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
            else if (keyInfo.Key == ConsoleKey.Q)
            {
                player.addSongToQueue(album.Songs[cursorPosition]);
                Console.Clear();
                Console.WriteLine("Song added");
                Console.ReadKey(true);
            }
        }
    }
}
