using System;
using Microsoft.VisualBasic.FileIO;

namespace RussianRoulette
{
    internal class Program
    {

    static void Main(string[] args)
    {
        // loop that will launch the game and re-run it if user wants to play again 
        while (true)
        {
            // create game object
            Game gameState = new Game();
            
            gameState.RunGame(); 
            
            // ask user if he wants to play again, if not, exit
            bool playerChoice = GetAgreement("Play the game again?(Y/n)?: ");
            
            if (!playerChoice)
            {
                Console.Clear();
                break;
            }
            
        }
    }
    // get yes or no from user and return his choice as bool
    static bool GetAgreement(string prompt)
    {
        string choice = "";

        // get correct input from user & handle incorrect one
        while (choice != "y" && choice != "n")
        {
            Console.Write(prompt);
            choice = Console.ReadLine().ToLower();

            if (choice != "y" && choice != "n")
            {
                Console.WriteLine("\u001b[31mPlease, type \"y\" or \"n\"!\u001b[0m");
            }
        }

        if (choice == "y")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    }

    class Game
    {
        // declare game's most important data fields and create random number generator

        private string DirectoryPath { get; set; }
        private string DirectoryName { get; set; }
        private Random NumberGenerator { get; } = new Random();

        private const int RoundsCount = 3;
        private int ChoosedChamber { get; set; }
        
        // run all necessary game's functions
        public void RunGame()
        { 
            Console.Clear();
            GetDirectory();
            PrintWelcomeMessage();
            PickGameMode();
        }
        
        private void PrintWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("Welcome to the game 'Russian roulette'");
            Console.WriteLine("This game has two difficulty modes:");
            Console.WriteLine("1. Normal: Only 1 of 6 chambers is loaded with a bullet.");
            Console.WriteLine("2. Extreme: 5 of 6 chambers are loaded with a bullet.");
            Console.WriteLine("Player must pick a revolver's chamber, number between 1 and 6 both for Normal and Extreme  mode.");
            Console.WriteLine("If the player is lucky and selects a chamber without a bullet, they can continue playing with a new round.");
            Console.WriteLine("However, if the player chooses a loaded chamber, the folder containing their files will be permanently deleted.");
            Console.WriteLine("There will be three rounds. If the player withstands all of them, they can keep their folder.");
            Console.WriteLine("Good Luck!");
            Console.WriteLine("+----------------------------------------------------------------------------------------------------------------+");
        }

        // pick game mode depending on user's choice
        private void PickGameMode()
        {
            int choice = GetInRangeNumber(1, 2, "Pick difficulty(1 or 2): ");

            if (choice == 1)
            {
                NormalGameMode();
            }
            else
            {
                ExtremeGameMode();
            }
        }

        private void NormalGameMode()
        {
            int loadedChamber = GenerateRevolverChambers();

            for (int i = 1; i <= RoundsCount; i++)
            {
                ChoosedChamber = HandleGameRound(i);
                if (ChoosedChamber != loadedChamber)
                {
                    HandleRoundSurvival(i);
                }
                else
                {
                    HandlePlayerLose(loadedChamber, false);
                    return;
                }
            }
            // print out congratulation message if player survives all game rounds
            HandlePlayerWin(loadedChamber, false);
        }

        private void ExtremeGameMode()
        {
            int unloadedChamber = GenerateRevolverChambers();

            for (int i = 1; i <= RoundsCount; i++)
            {
                ChoosedChamber = HandleGameRound(i);
                if (ChoosedChamber != unloadedChamber) 
                {
                    HandlePlayerLose(unloadedChamber, true);
                    return;
                }
                else
                {
                    HandleRoundSurvival(i);
                }
            }
            HandlePlayerWin(unloadedChamber, true);
        }

        // generate a number in range 1-6
        private int GenerateRevolverChambers()
        {
            return NumberGenerator.Next(1, 7);
        }

        private int HandleGameRound(int currentRound)
        {
            Console.WriteLine($"\u001b[35mRound ({currentRound}/{RoundsCount})\u001b[0m");
            
            return GetInRangeNumber(1, 6, "Choose a chamber(1-6): ");
        }
        
        // print out message for normal & extreme game modes
        
        private void HandlePlayerLose(int chamber, bool isExtremeMode)
        {
            if (isExtremeMode)
            {
                Console.WriteLine($"\u001b[31mTo no one's surprise, you're dead now. Game will delete \"{DirectoryName}\" folder as you won't need it anyway.\u001b[0m");
                Console.WriteLine($"P.S Chamber \u2116{chamber} was unloaded.");
                DeleteDirectory();
            }
            else
            {
                Console.WriteLine($"\u001b[31mUnfortunately, you're dead now. Game will delete \"{DirectoryName}\" folder as you won't need it anyway.\u001b[0m");
                Console.WriteLine($"P.S Chamber \u2116{chamber} was loaded.");
                DeleteDirectory();
            }
        }
        
        private void HandlePlayerWin(int chamber, bool isExtremeMode)
        {
            if (isExtremeMode)
            {
                Console.WriteLine($"\u001b[32mWow, you somehow survived Extreme mode. Congratulations! Now you can keep your \"{DirectoryName}\" folder (and your life)!\u001b[0m");
                Console.WriteLine($"P.S Chamber \u2116{chamber} was unloaded.");
            }
            else
            {
                Console.WriteLine($"\u001b[32mCongratulations! You survived all three rounds of my game! You can keep your \"{DirectoryName}\" folder (and your life)!\u001b[0m");
                Console.WriteLine($"P.S Chamber \u2116{chamber} was loaded.");
            }
        }

        private void HandleRoundSurvival(int currentRound)
        {
            Console.WriteLine($"\u001b[92mCongratulations! You've survived round \u2116{currentRound}\u001b[0m");
            Thread.Sleep(1000);
            Console.Clear();
        }

        private string GetDirectory()
        {
            string path;
            while (true)
            {
                Console.Write("Please type path to your most important folder: ");
                path = Console.ReadLine();

                // get name of user's directory and check if it is valid
                if (Directory.Exists(path))
                {
                    string directoryName = new DirectoryInfo(path).Name;

                    if (directoryName != null)
                    {
                        DirectoryPath = path;
                        DirectoryName = directoryName;
                        return path;
                    }
                    else
                    {
                        Console.WriteLine("\u001b[31mUnable to get directory name. Please provide a valid path.\u001b[0m");
                    }
                }
                else
                {
                    Console.WriteLine("\u001b[31mProvided folder doesn't exist. Please provide correct path to your folder!\u001b[0m");
                }
            }
        }

        private void DeleteDirectory()
        {
            Directory.Delete(DirectoryPath);
        }

        // get number in specific range from the user and handle all exceptions
        private int GetInRangeNumber(int lowerBound, int upperBound, string prompt)
        {
            int result;
            
            while (true)
            {
                Console.Write(prompt);

                try
                {
                    result = Convert.ToInt32(Console.ReadLine());

                    if (result >= lowerBound && result <= upperBound)
                    {
                        Console.Clear();
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"\u001b[31mPlease, type a number in range {lowerBound}-{upperBound}.\u001b[0m");
                    }
                    
                }
                catch (FormatException)
                {
                    Console.WriteLine("\u001b[31mPlease, type a valid number.\u001b[0m");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"\u001b[31mProvided number is too large or to small. Please, type a number in range {lowerBound}-{upperBound}.\u001b[0m");
                }
            }
        }
    }
}