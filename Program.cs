
using System;
using System.Linq;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variable Dictionary
            int guessMax = 10; //Max number of guesses the player gets
            int answerLen = 4; //Length of the answer
            //
            bool answerFound = false; //Control variable for if the player finds the answer
            bool validInput = true; //Control variable for testing the player's input
            string currGuess = ""; //Player's current guess
            int guess = 0; //Player's current guess number
            int[] answer = new int[answerLen]; //The answer to the game.
            int[] answerCopy;  //Deep copy of the answer for manipulation
            int[] answerCompared; //Array used for comparing player input to answer
            string pluses; //String for printing out pluses
            string minuses; //String for printing out minuses


            //Init
            Console.WriteLine("Hello, welcome to Simplified Mastermind!");
            Console.WriteLine("\nInstructions: "
            + "\nWhen you guess a number, I will print out a + for every digit you get correct in the correct spot."
            + "\nAnd I will print out a - for every digit you get correct in the wrong spot.\n");

            //Randomizing answer
            RandomizeAnswer(answer, answerLen, 7);

            //////If you would like to see the answer while playing//////
            //PrintAnswer(answer);
            /////////////////////////////////////////////////////////////


            Console.WriteLine("");
            while (guess < guessMax && !answerFound) {//Gameplay loop exited if player finds the answer or 

                //Ask Player for input
                Console.WriteLine($"The answer is {answerLen} digits long.");
                Console.Write($"Please enter guess number {guess + 1}: ");

                //Read Player input
                currGuess = Console.ReadLine();

                validInput = true;
                validInput = CheckInput(currGuess, answerLen); //Validate input

                if (validInput) {//If valid input begin testing input

                    
                    answerCopy = CopyAnswer(answer, answerLen); //Init deep copy of answer
                    
                    //Init answer compared array
                    answerCompared = new int[answerLen];
                    for (int n = 0; n < answerLen; n++) { answerCompared[n] = -1; }

                    //Test Answer
                    TestAnswer(answerCopy, answerCompared, answerLen, currGuess);
                    
                    //Reset output strings
                    pluses = "";
                    minuses = "";

                    //Iterate through answerCompared and build the strings with it
                    for (int j = 0; j < answerLen; j++) {
                        if (answerCompared[j] == j) {//Initialized index in correct spot is a plus
                            pluses += "+";
                        }
                        else if (answerCompared[j] > -1) {//Initialized index in wrong spot is a minus
                            minuses += "-";
                        }
                    }

                    //Print to player
                    Console.WriteLine(minuses);
                    Console.WriteLine(pluses);
                    
                    //Test win condition
                    if (pluses.Length == answerLen) { answerFound = true; }
                    
                    //Iterate game loop
                    guess++;
                }
            }

            if (answerFound == true) {//If player won, congratulate player.
                Console.WriteLine("Congratulations! You Won!");
            } else {//If player lost, give answer.
                Console.WriteLine($"\nI'm sorry, you lost. The answer was {String.Join("", answer)}");
            }

            //Terminate
            Console.WriteLine("\n<<< Normal Termination >>>");
        }


        //Helper method to see the answer.
        private static void PrintAnswer(int[] answer) {

            Console.Write("Answer: ");
            foreach (int a in answer) {
                Console.Write($"{a}");
            }
            Console.WriteLine("");
        }

        //Helper method for randomizing the answer.
        //highBound is the high boundary for the digits in the answer (exlusive).
        private static void RandomizeAnswer(int[] answer, int answerLen, int highBound) {

            Random randy = new Random(); //Random generator

            for (int i = 0; i < answerLen; i++) {
                answer[i] = randy.Next(1, highBound);
            }

        }

        //Helper method to validate input
        private static bool CheckInput(string input, int answerLen) {

            if (input.Length != answerLen) { //Test length
                Console.WriteLine($"\n>>>I'm sorry, your guess needs to be {answerLen} numbers no spaces.<<<\n");
                return false;
            }

            foreach (char myChar in input) { //Test that all chars are digits
                if (myChar < '0' || myChar > '9') {
                    Console.WriteLine($"\n>>>I'm sorry, please use only numbers.<<<\n");
                    return false;
                }
            }

            return true;
        }

        //Help method for deep copying answer
        private static int[] CopyAnswer(int[] answer, int answerLen) {

            int[] copy = new int[answerLen];
            for (int i = 0; i < answerLen; i++) {
                copy[i] = answer[i];
            }

            return copy;

        }

        //Helper method for testing the answer
        private static void TestAnswer(int[] answerCopy, int[] answerCompared, int answerLen, string currGuess) {

            for (int i = 0; i < answerLen; i++) {//Iterate through currGuess chars

                int currNum = (int)Char.GetNumericValue(currGuess, i);//Turn char into int
                int index = Array.IndexOf(answerCopy, currNum); //Search for int in answerCopy

                if (index > -1) { //If found
                    answerCompared[i] = index; //Update answerCompared
                    answerCopy[index] = -1;    //Remove that int from answerCopy to correctly assess douplicates
                }

            }

        }

    }
}