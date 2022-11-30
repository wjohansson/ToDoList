using System.Threading.Tasks;

namespace ToDoListApp
{
    public class ListOverview
    {
        public static void ViewTasksInList(int listId)
        {
            var currentList = ProgramManager.Lists[listId - 1];

            Console.Clear();

            Console.WriteLine($"List Title: {currentList.ListTitle}");
            Console.WriteLine();

            var tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks in this list. You need to add one.");

                Task.CreateTask(listId);

                ViewTasksInList(listId);

                return;
            }

            ProgramManager.UpdateAllLists();

            foreach (var task in tasks)
            {
                if (task.Completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"-{tasks.IndexOf(task) + 1}- {task.TaskTitle}");
                Console.WriteLine($"    - {task.TaskDescription}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
            }

            TasksOption(listId);

            ViewTasksInList(listId);
        }

        public static void TasksOption(int listId)
        {
            Console.WriteLine("To edit this list: 'E'");
            Console.WriteLine("To view a task: 'V'");
            Console.WriteLine("To delete a task: 'D'");
            Console.WriteLine("To create a new task: 'N'");
            Console.WriteLine("To toggle completion of a task: 'T'");
            Console.WriteLine("To go back to start page: 'B'");
            Console.WriteLine("To quit the program: 'Q'");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "E":
                    List.EditList(listId);

                    break;
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