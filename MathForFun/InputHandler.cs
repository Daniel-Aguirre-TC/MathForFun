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
                playerOneName = Console.ReadLine();
                // if playerOneName has been changed will check name, otherwise back through while loop to ask name again
                if(playerOneName != "")
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
                    playerTwoName = Console.ReadLine();
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
            while (GameManager.questionType == QuestionGenerator.QuestionType.None)
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
                        GameManager.questionType = QuestionGenerator.QuestionType.Addition;
                        break;
                    case '2':
                        GameManager.questionType = QuestionGenerator.QuestionType.Subtraction;
                        break;
                    case '3':
                        GameManager.questionType = QuestionGenerator.QuestionType.Multiplication;
                        break;
                    case '4':
                        GameManager.questionType = QuestionGenerator.QuestionType.Division;
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
                        if (GameManager.questionType == QuestionGenerator.QuestionType.Division)
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
                        if (GameManager.questionType == QuestionGenerator.QuestionType.Division)
                        {
                            GameManager.firstMaxNumber += 50;
                        }
                        break;
                    case '3':
                        GameManager.currentDifficulty = GameManager.Difficulty.Hard;
                        GameManager.currentDifficulty = GameManager.Difficulty.Medium;
                        GameManager.firstMinNumber = 0;
                        GameManager.firstMaxNumber = 15;
                        GameManager.secondMinNumber = 1;
                        GameManager.secondMaxNumber = 15;
                        if (GameManager.questionType == QuestionGenerator.QuestionType.Division)
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
                "You have selected the difficulty: {currentDifficulty}\n", "Press y/n to confirm."
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
            GameManager.questionType = QuestionGenerator.QuestionType.None;
            GameManager.currentDifficulty = GameManager.Difficulty.None;
            GetNumberOfPlayers();
            GetPlayerNames();
            GetMathCategory();
            OfferDifficulty();
        }
      

        /*
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

        */
                     // return the players score, logic dependant on difficulty and the sum of the opperands for the problem solved.


        /*
                void CheckAnswer()
                {
                    // if can parse set result
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        // if result is correct
                        if (result == currentAnswer)
                        {
                            // score booster is based on numOne + numTwo so that subtraction score increases with higher numbers even if difference is  low number.
                            scoreBooster += Convert.ToInt32(Math.Floor((QuestionGenerator.numOne + QuestionGenerator.numTwo) / 10m));
                            // increase correctCount, notify player, offer continue.
                            correctCount++;
                            // Notify user their answer was correct, increase Range of possible opperands for next question, offer continue playing
                            Console.WriteLine($"\nYou answered correctly! Your score is now: {CalculateScore()}.\n\nYour total correct answers so far is {correctCount}\n");
                            IncreaseNumberRange();
                            OfferContinue();
                        }
                        // else means answer given was incorrect
                        else
                        {
                            // notify player they lost and offer continue.
                            Console.WriteLine($"\nI'm sorry, {result} is not the correct answer.\n\n{QuestionGenerator.numOne} {QuestionGenerator.currentOperator} {QuestionGenerator.numTwo} = {currentAnswer}.\n");
                            Console.WriteLine($"Your final score is {CalculateScore()}, with {correctCount} correct answers.\n");
                            // reset correctCount to 0 if got wrong answer.
                            correctCount = 0;
                            scoreBooster = 0;
                            OfferContinue();
                        }
                    }
                    // else the parse failed so CheckAnswer again to get a new entry
                    else
                    {
                        Console.WriteLine($"Invalid entry. Please enter a number.");
                        CheckAnswer();
                    }
                }
                // increase number range that problems are created from



        

                */


    }
}
