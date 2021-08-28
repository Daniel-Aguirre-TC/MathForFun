using System;
using System.Collections.Generic;
using System.Text;

namespace MathForFun
{
    public static class InputHandler
    {

        // Ask the user how many players will be enjoying our game.
        public static void GetNumberOfPlayers()
        {
            while (GameManager.playerCount == GameManager.NumberOfPlayers.None)
            {
                Console.Clear();
                ConsoleHandler.CenterMidScreenAndPrintArray(new string[] {
                    "  Please select one of the following:\n\n",
                    " 1) One Player\n",
                    "  2) Two Players\n\n"                    
                }) ;
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        GameManager.playerCount = GameManager.NumberOfPlayers.One;
                        ConsoleHandler.CenterMidScreenAndPrintArray(new string[] {
                        " You have selected One Player.\n",
                        "Press any key to continue."
                        });
                        break;
                    case '2':
                        Console.Clear();
                        GameManager.playerCount = GameManager.NumberOfPlayers.Two;
                        ConsoleHandler.CenterMidScreenAndPrintArray(new string[] {
                        " You have selected Two Players.\n",
                        "Press any key to continue."
                        });
                        break;
                    default:
                        Console.Clear();
                        ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { " Invalid Response\n", "Please enter '1' or '2'." });
                        ConsoleHandler.ClearAfterReadKey();
                        break;
                }
            }
            ConsoleHandler.ClearAfterReadKey();
        }

        // Depending on if playing one player or two, will ask for one name or two.
        public static void GetPlayerNames()
        {
            // always get first player name when this is called.
            string playerOneName = "";
            // "" not acceptable for player name, and will keep user in loop until changed.
            while (playerOneName == "")
            {
                Console.Clear();
                ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { "Player One, please enter your name." });
                Console.Write("".PadLeft(Console.WindowWidth / 2));
                playerOneName = Console.ReadLine();
                // if playerOneName has been changed will check name, otherwise back through while loop to ask name again
                if (playerOneName.Length > 15)
                {
                    Console.Clear();
                    ConsoleHandler.CenterMidScreenAndPrintArray(new string[] {
                        "I'm sorry, this game is not designed to handle a name that long.",
                        "Please enter a name less than 15 characters long." });
                    playerOneName = "";
                    ConsoleHandler.ClearAfterReadKey();
                }
                if (playerOneName != "")
                {
                    Console.Clear();
                    ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { $" \"{playerOneName}\" has been entered for Player One.\n", "Is this correct? y/n" });
                    switch (Console.ReadKey().KeyChar)
                    {
                        case (char)ConsoleKey.Enter:
                        case 'y':
                            Console.Clear();
                            ConsoleHandler.playerOneName = playerOneName;
                            ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { $"Name \"{playerOneName}\" saved." });
                            ConsoleHandler.ClearAfterReadKey();
                            break;
                        case 'n':
                        default:
                            playerOneName = "";
                            break;
                    }
                }                
            }
            // if twoPlayer then get second player name in the same way that we get player one name.
            if (GameManager.playerCount == GameManager.NumberOfPlayers.Two)
            {
                string playerTwoName = "";
                while (playerTwoName == "")
                {
                    Console.Clear();
                    ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { "Player Two, it's your turn! Please enter your name." });
                    Console.Write("".PadLeft(Console.WindowWidth / 2));
                    playerTwoName = Console.ReadLine();
                    if (playerTwoName.Length > 15)
                    {
                        Console.Clear();
                        ConsoleHandler.CenterMidScreenAndPrintArray(new string[] {
                        "I'm sorry, this game is not designed to handle a name that long.",
                        "Please enter a name less than 15 characters long." });
                        playerTwoName = "";
                        ConsoleHandler.ClearAfterReadKey();
                    }
                    if (playerTwoName != "")
                    {
                        Console.Clear();
                        ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { $" \"{playerTwoName}\" has been entered for Player Two.\n", "Is this correct? y/n" });
                        switch (Console.ReadKey().KeyChar)
                        {
                            case (char)ConsoleKey.Enter:
                            case 'y':
                                Console.Clear();
                                ConsoleHandler.playerTwoName = playerTwoName;
                                ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { $"Name \"{playerTwoName}\" saved." });
                                ConsoleHandler.ClearAfterReadKey();
                                break;
                            case 'n':
                            default:
                                playerTwoName = "";
                                break;
                        }
                    }                   
                }              
            }         
        }

        // Ask the user what type of math question to present then assign questionType
        public static void GetMathCategory()
        {
            while (GameManager.questionType == GameManager.QuestionType.None)
            {
                Console.Clear();
                ConsoleHandler.CenterMidScreenAndPrintArray(new string[] {
                " Please select a category by entering the corresponding number.\n",
                "1) Addition      ",
                "2) Subtraction   ",
                "3) Multiplication",
                "4) Division      "
                });
                Console.WriteLine();
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        GameManager.questionType = GameManager.QuestionType.Addition;
                        ConsoleHandler.currentOperator = "+";
                        break;
                    case '2':
                        GameManager.questionType = GameManager.QuestionType.Subtraction;
                        ConsoleHandler.currentOperator = "-";
                        break;
                    case '3':
                        GameManager.questionType = GameManager.QuestionType.Multiplication;
                        ConsoleHandler.currentOperator = "x";
                        break;
                    case '4':
                        GameManager.questionType = GameManager.QuestionType.Division;
                        ConsoleHandler.currentOperator = "/";
                        break;
                    // if default, invalid response, so no difficulty set which will cause while loop to re-run.
                    default:
                        ConsoleHandler.CenterAndPrintArray(new string[] { " Invalid response, Please enter a number 1-4.\n","Press any key to return." });
                        ConsoleHandler.ClearAfterReadKey();
                        break;
                }
            }
            Console.Clear();
            ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { $" You have selected the category: {GameManager.questionType}\n", "Press any key to continue."});
 
            ConsoleHandler.ClearAfterReadKey();
        }

        // Ask the user what difficulty they want and then assign currentDifficulty
        public static void OfferDifficulty()
        {            
            // assign initial values for number range at start of game

            // use while loop to offer difficulty again if player inputs invalid entry. This prevents user from continuing without setting a difficulty.
            while (GameManager.currentDifficulty == GameManager.Difficulty.None)
            {
                ConsoleHandler.CenterMidScreenAndPrintArray(new string[]
                {
                    " Please select a difficulty by entering the corresponding number.\n",
                    "1) Easy  ",
                    "2) Medium",
                    "3) Hard  "
                });                
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        GameManager.currentDifficulty = GameManager.Difficulty.Easy;
                        // assign initial number range based on difficulty. This will be updated as gameplay continues but will be reset if we get a wrong answer.
                        GameManager.firstMinNumber = 0;
                        GameManager.firstMaxNumber = 5;
                        GameManager.secondMinNumber = 1;
                        GameManager.secondMaxNumber = 5;
                        if (GameManager.questionType == GameManager.QuestionType.Division)
                        {
                            GameManager.firstMaxNumber += 25;
                        }
                        break;
                    case '2':
                        GameManager.currentDifficulty = GameManager.Difficulty.Medium;
                        GameManager.firstMinNumber = 0;
                        GameManager.firstMaxNumber = 10;
                        GameManager.secondMinNumber = 1;
                        GameManager.secondMaxNumber = 10;
                        if (GameManager.questionType == GameManager.QuestionType.Division)
                        {
                            GameManager.firstMaxNumber += 50;
                        }
                        break;
                    case '3':
                        GameManager.currentDifficulty = GameManager.Difficulty.Hard;
                        GameManager.firstMinNumber = 0;
                        GameManager.firstMaxNumber = 15;
                        GameManager.secondMinNumber = 1;
                        GameManager.secondMaxNumber = 15;
                        if (GameManager.questionType == GameManager.QuestionType.Division)
                        {
                            GameManager.firstMaxNumber += 75;
                        }
                        break;
                        // if default, invalid response, so no difficulty set which will cause while loop to re-run.
                        default:
                        ConsoleHandler.CenterMidScreenAndPrintArray(new string[]
                        {
                            " Invalid response\n", "Please enter a number 1-3."
                        });
                        ConsoleHandler.ClearAfterReadKey();
                        break;
                }
            }
            Console.Clear();
            ConsoleHandler.CenterMidScreenAndPrintArray(new string[]
            {
                $" You have selected the difficulty: {GameManager.currentDifficulty}\n", "Press y/n to confirm."
            });
            switch (Console.ReadKey().KeyChar)
            {
                case (char)ConsoleKey.Enter:
                case 'y':
                    break;
                case 'n':
                default:
                    GameManager.currentDifficulty = GameManager.Difficulty.None;
                    OfferDifficulty();
                    break;
            }
        }
        
        // Get number of players, names, category, and difficulty
        public static void OfferChangeSettings()
        {
            GameManager.playerCount = GameManager.NumberOfPlayers.None;
            GameManager.questionType = GameManager.QuestionType.None;
            GameManager.currentDifficulty = GameManager.Difficulty.None;
            GetNumberOfPlayers();
            GetPlayerNames();
            GetMathCategory();
            OfferDifficulty();
        }
                      
        // Offer to restart the game, or close application if declined.
        public static void OfferContinue()
        {
            Console.Clear();
            ConsoleHandler.CenterMidScreenAndPrintArray(new string[] {
                "Do you wish to continue playing?\n", "Please enter y/n"
            });
            switch (Console.ReadKey().KeyChar)
            {
                // if yes, spacebar, or enter key then continue
                case 'y':
                case (char)ConsoleKey.Enter:
                case (char)ConsoleKey.Spacebar:
                    if (!GameManager.stillPlaying)
                    {
                        // set questionType and currentDifficulty back to none so that we can offer new settings if correctCount == 0
                        OfferChangeSettings();
                    }
                    break;
                // if n then set stillPlaying to false to break gameplay loop.
                case 'n':
                    GameManager.CloseApplication();
                    break;
                // if anything else then invalid entry and offer continue again.
                default:
                    Console.WriteLine("\n\nInvalid Entry. Please Select 'y' or 'n'.\n");
                    OfferContinue();
                    break;
            }
        }
        
        // check if answer is correct and call next steps based on outcome.
        public static void CheckAnswer()
        {
            Console.Write("".PadLeft(Console.WindowWidth / 2));
            // if can parse set result
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                // if result is correct
                if (result == GameManager.currentAnswer)
                {
                    // score booster is based on numOne + numTwo so that subtraction score increases with higher numbers even if difference is  low number.
                    GameManager.scoreBooster += Convert.ToInt32(Math.Floor((GameManager.numOne + GameManager.numTwo) / 10m));
                    // if two player
                    if (GameManager.playerCount == GameManager.NumberOfPlayers.Two)
                    {
                        // if it's player one's turn during a two player game
                        if (GameManager.turnCount % 2 != 0)
                        {
                            ConsoleHandler.PrintCorrectAnswerReceived(ConsoleHandler.playerOneName);
                            ConsoleHandler.playerOneScore = GameManager.CalculateScore(ConsoleHandler.playerOneScore);
                        }
                        else
                        // else if it's player two's turn
                        {
                            ConsoleHandler.PrintCorrectAnswerReceived(ConsoleHandler.playerTwoName);
                            ConsoleHandler.playerTwoScore = GameManager.CalculateScore(ConsoleHandler.playerTwoScore);
                            //increase number range after player twos turn since it is now been both players turns.
                            GameManager.IncreaseNumberRange();
                        }
                    }
                    // if correct answer for a one player game
                    else
                    {
                        ConsoleHandler.PrintCorrectAnswerReceived(ConsoleHandler.playerOneName);
                        ConsoleHandler.playerOneScore = GameManager.CalculateScore(ConsoleHandler.playerOneScore);
                        GameManager.IncreaseNumberRange();
                    }
                    
                }
                // else incorrect answer received
                else
                {
                    Console.Clear();
                    // tell player wrong answer and show final scores
                    ConsoleHandler.CenterMidScreenAndPrintArray(new string[] { "I'm sorry, the correct answer is:\n", $" {GameManager.numOne} {ConsoleHandler.currentOperator} {GameManager.numTwo} = {GameManager.currentAnswer}\n" });
                    if (GameManager.playerCount == GameManager.NumberOfPlayers.Two)
                    {
                        ConsoleHandler.CenterAndPrintArray(new string[] { $"{ConsoleHandler.playerOneName}'s Final Score: {ConsoleHandler.playerOneScore}", $"{ConsoleHandler.playerTwoName}'s Final Score: {ConsoleHandler.playerTwoScore}" });
                    }
                    else Console.WriteLine(ConsoleHandler.CenterText($"{ConsoleHandler.playerOneName}'s Final Score: {ConsoleHandler.playerOneScore}"));
                    Console.ReadLine();
                    // offer continue playing
                    OfferContinue();      
                }

                // increase turn count now that turn is over.
                GameManager.turnCount++;

            }
            // else the parse failed so CheckAnswer again to get a new entry
            else
            {
                Console.WriteLine($"Invalid entry. Please enter a number.");
                CheckAnswer();
            }
        }
                
    }
}
