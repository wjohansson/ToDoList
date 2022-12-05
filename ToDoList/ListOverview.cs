using System.Threading.Tasks;

namespace ToDoListApp
{
    public class ListOverview
    {
        public static void ViewTasksInList(int listPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            Console.Clear();

            Console.WriteLine("LIST MENU");
            Console.WriteLine();

            Console.WriteLine($"List Title - {currentList.ListTitle} (Category: {currentList.ListCategory})");
            Console.WriteLine();

            List<TaskManager> tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks in this list. You need to add one.");

                TaskManager.CreateTask(listPosition);

                ViewTasksInList(listPosition);

                return;
            }

            ProgramManager.UpdateAllLists();

            foreach (TaskManager task in tasks)
            {
                if (task.Completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"    Task Position #{tasks.IndexOf(task) + 1}");
                Console.WriteLine($"        Title - {task.TaskTitle} (Prio: {task.Priority})");
                Console.WriteLine($"            ¤ {task.TaskDescription}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
            }

            TasksOption(listPosition);

            ViewTasksInList(listPosition);
        }

        public static void TasksOption(int listPosition)
        {
            Console.WriteLine("[E] To edit this list.");
            Console.WriteLine("[A] To archive this list.");
            Console.WriteLine("[V] To view a task and its sub-tasks.");
            Console.WriteLine("[D] To delete a task.");
            Console.WriteLine("[N] To create a new task.");
            Console.WriteLine("[T] To toggle completion of a task.");
            Console.WriteLine("[S] To sort tasks.");
            Console.WriteLine("[B] To go back to start page.");
            Console.WriteLine("[Q] To quit the program.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "E":
                    ListManager.EditList(listPosition);

                    break;
                case "A":
                    ListManager.ArchiveThisList(listPosition);
                    AllListsOverview.AllLists();

                    return;
                case "V":
                    TaskManager.ViewTask(listPosition);

                    return;
                case "D":
                    TaskManager.DeleteTask(listPosition);

                    break;
                case "N":
                    TaskManager.CreateTask(listPosition);

                    break;
                case "T":
                    TaskManager.ToggleComplete(listPosition);

                    break;
                case "S":
                    TaskSort.SortTasks(listPosition);

                    break;
                case "B":
                    Console.Clear();
                    AllListsOverview.AllLists();

                    break;
                case "Q":
                    ProgramManager.QuitProgram();

                    break;
            }
        }
    }
}