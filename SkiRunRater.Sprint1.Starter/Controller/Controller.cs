using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiRunRater
{
    public class Controller
    {
        #region FIELDS

        bool active = true;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            SkiRunRepository skiRunRepository = new SkiRunRepository();

            ConsoleView.DisplayWelcomeScreen();

            using (skiRunRepository)
            {
                List<SkiRun> skiRuns = skiRunRepository.GetSkiAllRuns();

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;

                    int skiRunID;
                    SkiRun skiRun;
                    string message;

                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.ListAllSkiRuns:
                            ConsoleView.DisplayAllSkiRuns(skiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.DisplaySkiRunDetail:
                            ConsoleView.DisplaySkiRunDetail(skiRuns);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.DeleteSkiRun:
                            skiRunID = ConsoleView.DeleteSkiRun(skiRuns);
                            skiRunRepository.DeleteSkiRun(skiRunID);
                            ConsoleView.DisplayReset();
                            ConsoleView.DisplayMessage($"Ski run {skiRunID} has been deleted.");
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            //
                            // TODO: Implement add ski run method
                            //
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            //
                            // TODO: Implement update ski run method
                            //
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            //
                            // TODO: Implement query ski run method
                            //
                            break;
                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;
                        case AppEnum.ManagerAction.None:
                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        #endregion

    }
}
