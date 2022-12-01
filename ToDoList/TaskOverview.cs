using System.Threading.Tasks;

namespace ToDoListApp
{
    public class TaskOverview
    {
        public static void ViewIndividualTask(int listId, int taskId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            List<Task> tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                ListOverview.ViewTasksInList(listId);
                return;
            }

            Task currentTask = tasks[taskId - 1];

            Console.Clear();

            Console.WriteLine("TASK MENU");
            Console.WriteLine();

            if (currentTask.Completed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine($"Task ID #{tasks.IndexOf(currentTask) + 1}");
            Console.WriteLine($"    Title - {currentTask.TaskTitle} (Prio: {currentTask.Priority})");
            Console.WriteLine($"        ¤ {currentTask.TaskDescription}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            TaskOption(listId, taskId);
        }

        public static void TaskOption(int listId, int taskId)
        {
            Console.WriteLine("[E] To edit this task.");
            Console.WriteLine("[A] To archive this task.");
            Console.WriteLine("[B] To go back to list overview.");
            Console.WriteLine("[Q] To quit the program.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "E":
                    Task.EditTask(listId, taskId);

                    break;
                case "A":
                    Task.ArchiveTask(listId, taskId);

                    break;
                case "B":
                    ListOverview.ViewTasksInList(listId);

                    break;
                case "Q":
                    ProgramManager.QuitProgram();

                    break;
            }

            ViewIndividualTask(listId, taskId);
        }
    }
}