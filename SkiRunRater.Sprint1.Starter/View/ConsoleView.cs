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

        public static int GetSkiRunID(List<SkiRun> skiRuns)
        {
            int skiRunID = -1;

            DisplayAllSkiRuns(skiRuns);

            DisplayMessage("");
            DisplayPromptMessage("Enter the ski run ID: ");

            int idTemp = ConsoleUtil.ValidateIntegerResponse("Please enter a valid ski run ID : ", Console.ReadLine());

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

        #endregion
    }
}
