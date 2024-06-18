using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionality
{
    public class Options
    {
        // Variables
        public int exitValue = -1; // This is the number that is returned when the user wants to exit


        // Methods



        // Return an Action based on given text/action pairs (NO exit functionality)
        public Action ActionOptions(string titleMessage, Dictionary<string, Action> actions)
        {
            Action returnableAction = null;
            while(returnableAction == null)
            {
                int index = WriteOptions_GetValidResponse(titleMessage, ReturnStringListOfOptions());

                returnableAction = actions.ElementAt(index).Value;
            }

            return returnableAction;


            List<string> ReturnStringListOfOptions()
            {
                List<string> textList = new List<string>();
                foreach (var action in actions)
                {
                    textList.Add(action.Key);
                }

                return textList;
            }
        }

        // Return an Action based on given text/action pairs (Yes exit functionality)
        // EXAMPLE:
            // Unlike the similar method above, this one takes in a referenced 'exitBoolean'
            // This 'exitBoolean' is one being used in a loop in a program where the function is called, for example:
                // bool inLoop = true
                // while(inLoop)
                //{ 
                // ActionOptions(Options, ref inLoop)
                //}
            // Therefore, the exitBoolean should be set to true before using
            // If the 'exit' choice is picked, it will return this exitBoolean as false, ending the loop
        public Action ActionOptions(string titleMessage, Dictionary<string, Action> actions, ref bool exitBoolean)
        {
            Action returnableAction = null;
            while (returnableAction == null)
            {
                int index = WriteOptions_GetValidResponse(titleMessage, ReturnStringListOfOptions(), true);

                if(index == exitValue)
                {
                    exitBoolean = false;
                    returnableAction = Exit;
                }
                else
                {
                    returnableAction = actions.ElementAt(index).Value;
                }
            }

            return returnableAction;

            void Exit()
            {
                // Does nothing!
            }
            List<string> ReturnStringListOfOptions()
            {
                List<string> textList = new List<string>();
                foreach (var action in actions)
                {
                    textList.Add(action.Key);
                }

                return textList;
            }
        }


        public async Task<Action> AsyncActionOptions(string titleMessage, Dictionary<string, Action> actions)
        {
            Action returnableAction = null;
            while (returnableAction == null)
            {
                int index = WriteOptions_GetValidResponse(titleMessage, ReturnStringListOfOptions());

                returnableAction = actions.ElementAt(index).Value;
            }

            return returnableAction;


            List<string> ReturnStringListOfOptions()
            {
                List<string> textList = new List<string>();
                foreach (var action in actions)
                {
                    textList.Add(action.Key);
                }

                return textList;
            }
        }

        // Simple method for taking in list of options, waiting for user to pick a valid option, and then returning the desired index
        // NOTE: If the call allows for exiting, and the user picks the exit option, this code will return the 'exitValue' above ^
        public int WriteOptions_GetValidResponse(string titleMessage, List<string> opts, bool allowExit = false)
        {
            int validNumber = 0;

            bool awaitingValidResponse = true;
            while(awaitingValidResponse)
            {
                Display();
                bool isValid = ValidResponse(out int number);
                Console.Clear();

                if (isValid)
                {
                    validNumber = number;
                    awaitingValidResponse = false;
                }
            }

            return validNumber;




            // Display options
            void Display()
            {
                // Add titleMessage if available
                if(!string.IsNullOrWhiteSpace(titleMessage))
                {
                    Console.WriteLine(titleMessage + "\n---------------------------------------------------------------------------------");
                }

                // List all options
                int counter = 0;
                foreach (string choice in opts)
                {
                    counter++;
                    Console.WriteLine($"[{counter}] {choice}");
                }
                Console.WriteLine();

                // If allowing for exit functionality, show it
                if (allowExit)
                {
                    Console.WriteLine("[E] Exit\n");
                }
            }

            // Verifies that user's response is a number within the range of number of options
            bool ValidResponse(out int value)
            {
                // Get answer
                string answer = Console.ReadLine().ToLower();

                // Sees if answer is a valid number
                bool isNum = int.TryParse(answer, out int number);

                // If its a number, proceed
                if (isNum)
                {
                    // Convert number from what is on the screen (1, 2, 3) to a proper index value (0, 1, 2)
                    int validIndex = (number - 1);

                    // If this proper index value falls within the list of options, proceed
                    if (validIndex >= 0 && validIndex < opts.Count)
                    {
                        value = validIndex;
                        return true;
                    }
                    else
                    {
                        value = 0;
                        return false;
                    }
                }
                // Else, if it isnt a number...
                else
                {
                    // But it is 'e' and exit functionality is allowed, return the 'exit' value
                    if(answer == "e" && allowExit)
                    {
                        value = exitValue;
                        return true;
                    }
                    else
                    {
                        value = 0;
                        return false;
                    }
                }
            }
        }
    }
}
