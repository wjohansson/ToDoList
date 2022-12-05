using System.Threading.Tasks;

namespace ToDoListApp
{
    public class TaskOverview
    {
        public static void ViewIndividualTask(int listPosition, int taskPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                ListOverview.ViewTasksInList(listPosition);
                return;
            }

            TaskManager currentTask = tasks[taskPosition - 1];

            Console.Clear();

            Console.WriteLine("TASK MENU");
            Console.WriteLine();

            if (currentTask.Completed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine($"Task Position #{tasks.IndexOf(currentTask) + 1}");
            Console.WriteLine($"    Title - {currentTask.TaskTitle} (Prio: {currentTask.Priority})");
            Console.WriteLine($"        Description - {currentTask.TaskDescription}");
            Console.WriteLine();

            foreach (SubTask subTask in currentTask.SubTasks)
            {
                if (subTask.Completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"            Sub-task Position #{currentTask.SubTasks.IndexOf(subTask) + 1}");
                Console.WriteLine($"                Title - {subTask.SubTaskTitle}");
                Console.WriteLine($"                    Description - {subTask.SubTaskDescription}");

                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            TaskOption(listPosition, taskPosition);
        }

        public static void TaskOption(int listPosition, int taskPosition)
        {
            Console.WriteLine("[E] To edit this task.");
            Console.WriteLine("[A] To archive this task.");
            Console.WriteLine("[D] To delete a sub-task.");
            Console.WriteLine("[ES] To edit a sub-task.");
            Console.WriteLine("[N] To create a new sub-task.");
            Console.WriteLine("[B] To go back to list overview.");
            Console.WriteLine("[Q] To quit the program.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "E":
                    TaskManager.EditTask(listPosition, taskPosition);

                    break;
                case "A":
                    TaskManager.ArchiveTask(listPosition, taskPosition);

                    break;
                case "D":
                    SubTask.DeleteSubTask(listPosition, taskPosition);

                    break;
                case "ES":
                    SubTask.EditSubTask(listPosition, taskPosition);

                    break;

                case "N":
                    SubTask.CreateSubTask(listPosition, taskPosition);

                    break;
                case "B":
                    ListOverview.ViewTasksInList(listPosition);

                    break;
                case "Q":
                    ProgramManager.QuitProgram();

                    break;
            }

            ViewIndividualTask(listPosition, taskPosition);
        }
    }
}