// See https://aka.ms/new-console-template for more information
using SpotifyApp;

Song TakeOnMe = new Song("Take on me", 225, "a-ha", "Pop");

Player player = new Player();
player.addSongToQueue(TakeOnMe);

User user = new User("Tobias");

user.CreatePlaylist("Examples");
user.CreatePlaylist("test2");
user.AddSongToPlaylist("Examples", TakeOnMe);

string[] menuOptions = { "Play Song", "Playlists", "Quit" };

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
            Console.WriteLine("To create a new playlist, Press C. To Show playlists, press S");
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

                ShowPlaylists(tempPlaylist, cursorPosition);
            }
        }
    }

    void ShowPlaylists(List<Playlist> playlists, int cursorPosition)
    {
        ConsoleKeyInfo keyInfo;
        bool isActive = true;

        while (isActive)
        {
            Console.Clear();
            Console.WriteLine("To exit, press E. To show all the songs, press Enter.\n");

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

                playlists[cursorPosition].GetSongs().ForEach(s => Console.WriteLine(s.Name));

                Console.ReadKey(true);
            }
        }
    }


}