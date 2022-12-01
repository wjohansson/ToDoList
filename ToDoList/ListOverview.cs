using System.Threading.Tasks;

namespace ToDoListApp
{
    public class ListOverview
    {
        public static void ViewTasksInList(int listId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            Console.Clear();

            Console.WriteLine("LIST MENU");
            Console.WriteLine();

            Console.WriteLine($"List Title - {currentList.ListTitle} (Category: {currentList.ListCategory})");
            Console.WriteLine();

            List<Task> tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks in this list. You need to add one.");

                Task.CreateTask(listId);

                ViewTasksInList(listId);

                return;
            }

            ProgramManager.UpdateAllLists();

            foreach (Task task in tasks)
            {
                if (task.Completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"    Task ID #{tasks.IndexOf(task) + 1}");
                Console.WriteLine($"        Title - {task.TaskTitle} (Prio: {task.Priority})");
                Console.WriteLine($"            ¤ {task.TaskDescription}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
            }

            TasksOption(listId);

            ViewTasksInList(listId);
        }

        public static void TasksOption(int listId)
        {
            Console.WriteLine("[E] To edit this list.");
            Console.WriteLine("[A] To archive this list.");
            Console.WriteLine("[V] To view a task.");
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
                    List.EditList(listId);

                    break;
                case "A":
                    List.ArchiveThisList(listId);
                    AllListsOverview.AllLists();

                    return;
                case "V":
                    Task.ViewTask(listId);

                    return;
                case "D":
                    Task.DeleteSpecificTask(listId);

                    break;
                case "N":
                    Task.CreateTask(listId);

                    break;
                case "T":
                    Task.ToggleComplete(listId);

                    break;
                case "S":
                    TaskSort.SortTasks(listId);

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