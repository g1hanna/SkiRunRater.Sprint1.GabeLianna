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

        private static List<SkiRun> OrderSkiRunsList(List<SkiRun> skiRuns)
        {
            List<SkiRun> sortedList = skiRuns;

            sortedList.Sort((skiRun1, skiRun2) =>
            {
                if (skiRun1.ID > skiRun2.ID) return 1;
                else if (skiRun1.ID < skiRun2.ID) return -1;
                else return 0;
            });

            return sortedList;
        }

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

                    int skiRunID = -1;
                    SkiRun skiRun = null;
                    string message = string.Empty;

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
                            skiRuns.RemoveAll((s) => s.ID == skiRunID);
                            skiRunRepository.DeleteSkiRun(skiRunID);
                            ConsoleView.DisplayReset();
                            ConsoleView.DisplayMessage($"Ski run {skiRunID} has been deleted.");
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.AddSkiRun:
                            skiRun = ConsoleView.AddSkiRun(skiRuns);
                            skiRunRepository.InsertSkiRun(skiRun);
                            skiRuns.Add(skiRun);
                            skiRuns = OrderSkiRunsList(skiRuns);
                            skiRunRepository.WriteSkiRunsData();
                            ConsoleView.DisplayReset();
                            ConsoleView.DisplayMessage($"New ski run {skiRun.ID} added.");
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.UpdateSkiRun:
                            //
                            // TODO: Implement update ski run method
                            //
                            skiRun = ConsoleView.UpdateSkiRun(skiRuns, out skiRunID);
                            skiRunRepository.UpdateSkiRun(skiRun);
                            skiRuns.RemoveAll((s) => { return s.ID == skiRunID; });
                            skiRuns.Add(skiRun);
                            skiRuns = OrderSkiRunsList(skiRuns);
                            skiRunRepository.WriteSkiRunsData();
                            ConsoleView.DisplayReset();
                            ConsoleView.DisplayMessage($"Ski run {skiRun.ID} has been updated.");
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.QuerySkiRunsByVertical:
                            //
                            // TODO: Implement query ski run method
                            //
                            ConsoleView.QuerySkiRunsByVertical(skiRuns);
                            ConsoleView.DisplayContinuePrompt();
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
