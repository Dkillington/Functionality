namespace Functionality
{
    public class Text
    {
        // [Variables]
        const int defaultTypewriterSpeed = 80; // Typewriter speed in miliseconds (Default: 100)


        // [Functions]
        // Asks user for input
        public static string Ask(string message, int typewriterMiliseconds = 0, bool clearAfter = true)
        {
            Typewrite(message, typewriterMiliseconds); // Write message
            Console.WriteLine("\n");
            string playerResponse = Console.ReadLine(); // Collect user input
            if (clearAfter) // Clear screen after if desired
            {
                Console.Clear();
            }

            return playerResponse; // Return user input
        }
        // Will write out a message
        public static void Say(string message, int typewriterMiliseconds = 0, bool clearAfter = true, bool pressAnyKeyMessage = false)
        {
            Typewrite(message, typewriterMiliseconds); // Write message

            if (pressAnyKeyMessage) // Display a message to 'press any key' if desired
            {
                Console.WriteLine("\n\n(Press Any Key To Continue . . .)\n");
            }

            Console.ReadKey(); // Await input

            if (clearAfter) // Clear the screen if desired
            {
                Console.Clear();
            }
        }


        // [Tools]
        // Writes text in a "Typewriter" fashion
        public static void Typewrite(string messageToType, int speedMiliseconds = defaultTypewriterSpeed)
        {
            bool skipped = false;
            // Wait for the input miliseconds (Absolute value to prevent error)
            foreach (char character in messageToType)
            {
                Console.Write(character);
                System.Threading.Thread.Sleep(Math.Abs(speedMiliseconds));

                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    skipped = true;
                    break;
                }
                //ReadAvailableKeys();
            }

            if (skipped)
            {
                Console.Clear();
                Console.Write(messageToType);
            }
        }

        // Eliminates all user input
        public static void ReadAvailableKeys()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        // Takes in a string and returns it with only the first letter of each word capitalized
        public static string CapitalizeEachFirstLetter(string sentence)
        {
            string sentenceToReturn = "";

            sentence = sentence.ToLower(); // Convert entire word to lowercase

            //capitalizedName = char.ToUpper(capitalizedName[0]) + capitalizedName.Substring(1); // Uppercase only the first letter

            bool capitalizeNext = true;
            for (int i = 0; i < sentence.Length - 1; i++)
            {
                if (capitalizeNext && char.IsLetter(sentence[i]))
                {
                    sentenceToReturn += char.ToUpper(sentence[i]) + sentence.Substring(i + 1, ReturnLength(sentence, i));
                    capitalizeNext = false;
                }
                else
                {
                    if (char.IsWhiteSpace(sentence[i]))
                    {
                        capitalizeNext = true;
                    }

                }
            }

            // Returns length of string by starting in string and going until finding a space or string ends
            int ReturnLength(string wordToFindNextSpaceIn, int indexStart)
            {
                int length = 0;
                for (int i = indexStart; i < wordToFindNextSpaceIn.Length - 1; i++)
                {
                    if (char.IsWhiteSpace(wordToFindNextSpaceIn[i]))
                    {
                        break;
                    }

                    length++;
                }

                return length;
            }

            return sentenceToReturn; // Return name to return
        }
        public static void ColorBackgroundAndForeground(string message, ConsoleColor backgroundColor = ConsoleColor.Black, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            // Color the console's current background and foreground to given colors
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;

            // Write message
            Console.Write(message);

            // Return console's background/foreground to default colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Take in a value and correctly return it as a proper string
        public static string FormatNumericValueToString(float number)
        {
            if (number % 1 == 0) // If number is whole (Doesn't have a remainder)
            {
                if (number > 10)
                {
                    return string.Format("{0:0,0.##}", number);
                }
                else
                {
                    return string.Format("{0:0.##}", number);
                }
            }
            else
            {
                if (number > 10)
                {
                    return string.Format("{0:0,0.00}", number);
                }
                else
                {
                    return string.Format("{0:0.00}", number);
                }
            }
        }

        // Returns true/false depending on if the player confirms the given message
        public bool AskToConfirm(string messageToUse, bool clearAfter = true)
        {
            bool confirmed = false;

            bool choosing = true;
            while (choosing)
            {
                Console.Write(messageToUse + " (Y/N)\n\n");
                string answer = Console.ReadLine().ToLower().Trim();
                if (answer == "yes" || answer == "y")
                {
                    confirmed = true;
                    choosing = false;
                }
                else if (answer == "no" || answer == "n")
                {
                    confirmed = false;
                    choosing = false;
                }
                else
                {

                }

                if (clearAfter)
                {
                    Console.Clear();
                }
            }

            return confirmed;
        }



        public void PressAnyKey()
        {
            Console.WriteLine("\nPress Any Key . . .\n\n");
            Console.ReadKey();
            Console.Clear();
        }

        public void CancelInput()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

    }
}
