// See https://aka.ms/new-console-template for more information
using SpotifyApp;

Player player = new Player();

// Create menu options
string[] menuOptions = { "Play Song", "Quit" };

// Set initial cursor position
int cursorPosition = 0;

// Loop until user selects "Quit"
while (true)
{
    Console.Clear();

    // Display menu options
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

    // Get user input
    ConsoleKeyInfo keyInfo = Console.ReadKey();

    // Handle up arrow
    if (keyInfo.Key == ConsoleKey.UpArrow)
    {
        cursorPosition--;

        if (cursorPosition < 0)
        {
            cursorPosition = menuOptions.Length - 1;
        }
    }

    // Handle down arrow
    if (keyInfo.Key == ConsoleKey.DownArrow)
    {
        cursorPosition++;

        if (cursorPosition >= menuOptions.Length)
        {
            cursorPosition = 0;
        }
    }

    // Handle enter key
    if (keyInfo.Key == ConsoleKey.Enter)
    {
        switch (cursorPosition)
        {
            case 0:
                player.playCurrentSong();
                break;
            case 1:
                return;
        }
    }
}