using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiRunRater;

namespace SkiRunRater
{
    public static class ConsoleView
    {
        #region ENUMERABLES


        #endregion

        #region FIELDS

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS
        // View/Screen functions
        /// <summary>
        /// method to display the manager menu and get the user's choice
        /// </summary>
        /// <returns></returns>
        public static AppEnum.ManagerAction GetUserActionChoice()
        {
            AppEnum.ManagerAction userActionChoice = AppEnum.ManagerAction.None;
            //
            // set a string variable with a length equal to the horizontal margin and filled with spaces
            //
            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            //
            // set up display area
            //
            DisplayReset();

            //
            // display the menu
            //
            DisplayMessage("Ski Manager Menu");
            DisplayMessage("");
            Console.WriteLine(
                leftTab + "1. List All Ski Runs" + Environment.NewLine +
                leftTab + "2. Display a Ski Run's Details" + Environment.NewLine +
                leftTab + "3. Delete a Ski Run" + Environment.NewLine +
                leftTab + "4. Add a Ski Run" + Environment.NewLine +
                leftTab + "5. Update a Ski Run" + Environment.NewLine +
                leftTab + "6. Query Ski Runs by Vertical" + Environment.NewLine + 
                leftTab + "E. Quit" + Environment.NewLine);

            DisplayMessage("");
            DisplayPromptMessage("Enter the number/letter for the menu choice.");
            ConsoleKeyInfo userResponse = Console.ReadKey(true);

            switch (userResponse.KeyChar)
            {
                case '1':
                    userActionChoice = AppEnum.ManagerAction.ListAllSkiRuns;
                    break;
                case '2':
                    userActionChoice = AppEnum.ManagerAction.DisplaySkiRunDetail;
                    break;
                case '3':
                    userActionChoice = AppEnum.ManagerAction.DeleteSkiRun;
                    break;
                case '4':
                    userActionChoice = AppEnum.ManagerAction.AddSkiRun;
                    break;
                case '5':
                    userActionChoice = AppEnum.ManagerAction.UpdateSkiRun;
                    break;
                case '6':
                    userActionChoice = AppEnum.ManagerAction.QuerySkiRunsByVertical;
                    break;
                case 'E':
                case 'e':
                    userActionChoice = AppEnum.ManagerAction.Quit;
                    break;
                default:
                    Console.WriteLine(
                        "It appears you have selected an incorrect choice." + Environment.NewLine +
                        "Press any key to try again or the ESC key to exit.");

                    userResponse = Console.ReadKey(true);
                    if (userResponse.Key == ConsoleKey.Escape)
                    {
                        userActionChoice = AppEnum.ManagerAction.Quit;
                    }
                    break;
            }

            return userActionChoice;
        }

        /// <summary>
        /// Deletes a ski run of the user's choice
        /// </summary>
        /// <param name="skiRuns">List of ski runs to delete from</param>
        public static int DeleteSkiRun(List<SkiRun> skiRuns)
        {
            int skiRunID = -1;

            while (true)
            {
                DisplayReset();

                DisplayMessage("Existing Ski Runs:");
                DisplayMessage();

                DisplayAllSkiRunsWithoutResettingDisplay(skiRuns);

                string prompt = "Please enter the ID of a ski run to delete.";

                DisplayMessage("\n");
                DisplayMessage(prompt);

                skiRunID = GetSkiRunID(skiRuns);

                if (skiRunID == -1)
                {
                    DisplayMessage("");
                    DisplayMessage("The ID you specified does not match any of our records. Please try again.");
                    Console.ReadKey(true);
                }
                else
                {
                    break;
                }
            }

            return skiRunID;
        }

        /// <summary>
        /// View the details of a specific skiRun from a list
        /// </summary>
        /// <param name="skiRuns">List of ski runs</param>
        public static void DisplaySkiRunDetail(List<SkiRun> skiRuns)
        {
            int skiRunID = -1;

            DisplayReset();

            DisplayMessage("Choose a Ski Run to Display the Details of:");
            DisplayMessage("");

            DisplayAllSkiRunsWithoutResettingDisplay(skiRuns);

            string prompt = "Please enter the ID number of the ski run of your choosing: ";

            DisplayMessage("");
            DisplayMessage("");
            DisplayPromptMessage(prompt);

            skiRunID = ConsoleUtil.ValidateIntegerResponse(prompt, Console.ReadLine());

            SkiRun skiRunWeWant;

            //foreach (var skiRun in skiRuns)
            //{
            //    if (skiRun.ID == skiRunID)
            //    {
            //        skiRunWeWant = skiRun;
            //    }
            //}

            //
            // lambda expression (=>) and expression bodies
            //
            skiRunWeWant = skiRuns.Find((SkiRun run) => (run.ID == skiRunID));

            StringBuilder details = new StringBuilder();
            //string separator = Environment.NewLine;
            //string separator = "\n";


            //details += "ID: ";
            //details += skiRunWeWant.ID.ToString();
            //details += separator;
            //details += "Name: ";
            //details += skiRunWeWant.Name;
            //details += separator;
            //details += "Vertical: ";
            //details += skiRunWeWant.Vertical;
            
            details.AppendLine();

            DisplayReset();
            DisplayMessage($"ID: {skiRunWeWant.ID.ToString()}");
            DisplayMessage($"Name: {skiRunWeWant.Name}");
            DisplayMessage($"Vertical: {skiRunWeWant.Vertical}");
        }

        /// <summary>
        /// Display all ski runs without switching to a new screen
        /// </summary>
        /// <param name="skiRuns">The list of ski runs to display</param>
        private static void DisplayAllSkiRunsWithoutResettingDisplay(List<SkiRun> skiRuns)
        {
            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(8));
            columnHeader.Append("Ski Run".PadRight(25));
            //columnHeader.Append("Vertical in Feet".PadRight(5));

            DisplayMessage(columnHeader.ToString());

            foreach (SkiRun skiRun in skiRuns)
            {
                StringBuilder skiRunInfo = new StringBuilder();

                skiRunInfo.Append(skiRun.ID.ToString().PadRight(8));
                skiRunInfo.Append(skiRun.Name.PadRight(25));
                //skiRunInfo.Append(skiRun.Vertical.ToString().PadRight(5));

                DisplayMessage(skiRunInfo.ToString());
            }
        }

        /// <summary>
        /// method to display all ski run info
        /// </summary>
        public static void DisplayAllSkiRuns(List<SkiRun> skiRuns)
        {
            DisplayReset();

            DisplayMessage("All of the existing ski runs are displayed below;");
            DisplayMessage("");

            DisplayAllSkiRunsWithoutResettingDisplay(skiRuns);
        }

        /// <summary>
        /// Adds a ski run to the list of ski runs
        /// </summary>
        /// <param name="skiRuns">The ski run list to check against for ID errors</param>
        public static SkiRun AddSkiRun(List<SkiRun> skiRuns)
        {
            SkiRun result = new SkiRun(-1, "", 0);

            //
            // get SkiRun.ID
            //
            while (true)
            {
                int idTemp = GetNewSkiRunID(skiRuns);
                
                if (idTemp == -1)
                {
                    DisplayReset();

                    DisplayMessage("");
                    DisplayMessage("There is an object with that ID.");
                    DisplayMessage("Please provide an ID that does not already exist.");
                }
                else if (idTemp == -2)
                {
                    DisplayReset();

                    DisplayMessage("");
                    DisplayMessage("That is an invalid ID.");
                    DisplayMessage("Please provide a positive non-zero integer ID.");
                }
                else
                {
                    result.ID = idTemp;
                    break;
                }
            }

            DisplayContinuePrompt();

            //
            // get SkiRun.Name
            //
            result.Name = GetNewSkiRunName();
            DisplayContinuePrompt();

            //
            // get SkiRun.Vertical
            //
            while(true)
            {
                bool success;
                result.Vertical = GetNewSkiRunVertical(out success);

                if (!success)
                {
                    DisplayMessage("");
                    DisplayMessage("That is an invalid vertical height.");
                    DisplayMessage("Please provide a non-negative integer height.");
                }
                else break;
            }
            DisplayContinuePrompt();


            return result;
        }

        /// <summary>
        /// Prompts the user for the ID of a ski run to change and new details for it.
        /// </summary>
        /// <param name="skiRuns">The list of ski runs to use and update</param>
        /// <returns></returns>
        public static SkiRun UpdateSkiRun(List<SkiRun> skiRuns, out int skiRunID)
        {
            //
            // Get a ski run ID
            //
            int idTemp = -1;
            while (true)
            {
                idTemp = GetSkiRunID(skiRuns);

                if (idTemp != -1) break;
                else
                {
                    DisplayReset();

                    DisplayMessage("");
                    DisplayMessage("That ID does not match any ski run in the list.");
                    DisplayMessage("Please provide an ID of a ski run that exists.");
                }
            }

            skiRunID = idTemp;

            // use a predicate to retreive the desired object
            SkiRun skiRun = skiRuns.Find((sr) => (sr.ID == idTemp));

            //
            // get SkiRun.Name
            //
            skiRun.Name = GetNewSkiRunName();
            DisplayContinuePrompt();

            //
            // get SkiRun.Vertical
            //
            int verticalTemp;
            while (true)
            {
                bool success;
                verticalTemp = GetNewSkiRunVertical(out success);

                if (!success)
                {
                    DisplayMessage("");
                    DisplayMessage("That is an invalid vertical height.");
                    DisplayMessage("Please provide a non-negative integer height.");
                }
                else break;
            }
            skiRun.Vertical = verticalTemp;

            DisplayContinuePrompt();
            
            return skiRun;
        }


        public static void QuerySkiRunsByVertical(List<SkiRun> skiRuns)
        {
            int minimumVertical;
            int maximumVertical;

            // loop until valid range provided
            while (true)
            {
                // Get a minimum height
                DisplayReset();
                DisplayPromptMessage("Enter a minimum vertical height: ");

                minimumVertical = ConsoleUtil.ValidateIntegerResponse("Please enter a minimum integer vertical height: ", Console.ReadLine());

                DisplayContinuePrompt();

                // Get a maximum height
                DisplayReset();
                DisplayPromptMessage("Enter a maximum vertical height: ");

                maximumVertical = ConsoleUtil.ValidateIntegerResponse("Please enter a minimum integer vertical height: ", Console.ReadLine());

                DisplayContinuePrompt();

                // Display new range
                DisplayReset();

                DisplayMessage($"Minimum run height: {minimumVertical}");
                DisplayMessage($"Maximum run height: {maximumVertical}");

                DisplayMessage("");

                if (maximumVertical < minimumVertical)
                {
                    // if error in range, ask fro request
                    DisplayMessage("An invalid range has been entered.");
                    DisplayMessage("The minimum height exceeds the maximum height.");
                    DisplayMessage("Please enter an appropriate range to query from.");

                    Console.ReadKey(true);
                }
                else
                {
                    DisplayContinuePrompt();
                    break;
                }
            }

            // display list of ski runs in the range
            List<SkiRun> skiRunsQuery = new List<SkiRun>();

            skiRunsQuery = skiRuns.FindAll((skiRun) => (skiRun.Vertical >= minimumVertical && skiRun.Vertical <= maximumVertical));

            DisplayReset();

            if (skiRunsQuery.Count != 0)
            {
                DisplayMessage($"Below are the ski runs between {minimumVertical} and {maximumVertical}.");
                DisplayMessage();

                DisplayAllSkiRunsWithoutResettingDisplay(skiRunsQuery);
            }
            else
            {
                DisplayMessage($"There were no ski runs between {minimumVertical} and {maximumVertical}.");
            }
        }


        // Helper functions
        /// <summary>
        /// Prompts the user to select a ski run by ID
        /// </summary>
        /// <param name="skiRuns">The list of ski runs to get an ID from</param>
        /// <returns></returns>
        public static int GetSkiRunID(List<SkiRun> skiRuns)
        {
            int skiRunID = -1;

            DisplayAllSkiRuns(skiRuns);

            DisplayMessage("");
            DisplayPromptMessage("Enter the ID of a ski run: ");

            int idTemp = ConsoleUtil.ValidateIntegerResponse("Please enter a valid ski run ID: ", Console.ReadLine());

            foreach (SkiRun s in skiRuns)
            {
                if (s.ID == idTemp)
                {
                    skiRunID = idTemp;
                    break;
                }
            }

            return skiRunID;
        }

        /// <summary>
        /// Prompts the user to enter the ID for a new ski run.
        /// The ID must be unique and cannot be in the list or be lower than 1.
        /// </summary>
        /// <param name="skiRuns">The list of ski runs to add to</param>
        /// <returns></returns>
        public static int GetNewSkiRunID(List<SkiRun> skiRuns)
        {
            DisplayReset();
            DisplayPromptMessage("Enter the ID for a new ski run: ");

            int idTemp = ConsoleUtil.ValidateIntegerResponse(
                "Please enter the ID for a new ski run (make sure your ID is not already used): ",
                Console.ReadLine());

            if (idTemp > 0)
            {
                foreach (SkiRun s in skiRuns)
                {
                    if (s.ID == idTemp)
                    {
                        idTemp = -1;
                        break;
                    }
                }
            }
            else
            {
                idTemp = -2;
            }

            return idTemp;
        }

        /// <summary>
        /// Prompts the user to enter the name for a new ski run.
        /// The string shouldn't be empty or contain commas.
        /// </summary>
        /// <returns></returns>
        public static string GetNewSkiRunName()
        {
            DisplayReset();
            DisplayPromptMessage("Enter the name for a new ski run: ");

            string nameTemp = ConsoleUtil.ValidateStringResponse("Please enter a non-empty name for the string (remove any commas): ",
                Console.ReadLine().Trim(),
                (s) => { return (!s.Contains(",") && !string.IsNullOrEmpty(s)); } );
            
            return nameTemp;
        }

        /// <summary>
        /// Prompts the user to enter the height for a new ski run
        /// </summary>
        /// <param name="success">Is true if the user's input is valid</param>
        /// <returns></returns>
        public static int GetNewSkiRunVertical(out bool success)
        {
            DisplayReset();
            DisplayPromptMessage("Enter the vertical for a new ski run: ");

            int idTemp = ConsoleUtil.ValidateIntegerResponse(
                "Please enter the vertical height for a new ski run (must be a positive integer): ",
                Console.ReadLine());

            success = (idTemp >= 0);

            if (!success)
                return -1;
            else
                return idTemp;
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Ski Run Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message = "")
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// display a message in the message area without a new line for the prompt
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayPromptMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            //
            // create a list of strings to hold the wrapped text message
            //
            List<string> messageLines;

            //
            // call utility method to wrap text and loop through list of strings to display
            //
            messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

            for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
            {
                Console.WriteLine(messageLines[lineNumber]);
            }

            Console.Write(messageLines[messageLines.Count() - 1]);
        }


        #endregion
    }
}
