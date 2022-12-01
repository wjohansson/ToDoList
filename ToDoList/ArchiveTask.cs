namespace ToDoListApp
{
    public class ArchiveTask
    {
        public static void DeleteSpecificArchiveTask(int listId)
        {
            List currentList = ProgramManager.ArchiveLists[listId - 1];

            List<Task> tasks = currentList.Tasks;

            int taskId;

            try
            {
                Console.Write("Enter the id of the archived task you want to delete: ");
                taskId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                DeleteSpecificArchiveTask(listId);

                return;
            }

            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            try
            {
                tasks.RemoveAt(taskId - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                DeleteSpecificArchiveTask(listId);

                return;
            }

            ProgramManager.UpdateArchive();
        }

        public static void DeleteArchiveTask(int listId, int taskId)
        {
            List currentList = ProgramManager.ArchiveLists[listId - 1];

            List<Task> tasks = currentList.Tasks;

            tasks.RemoveAt(taskId - 1);

            ProgramManager.UpdateArchive();
        }

        public static void RestoreSpecificTask(int listId)
        {
            List currentList = ProgramManager.ArchiveLists[listId - 1];

            List<Task> tasks = currentList.Tasks;

            int taskId;

            try
            {
                Console.Write("Enter the id of the archived task you want to restore: ");
                taskId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                RestoreSpecificTask(listId);

                return;
            }

            Task currentTask = tasks[taskId - 1];

            var listExists = false;
            var taskExists = false;

            var currentListId = 0;
            List<Task> currentTasks;

            foreach (List list in ProgramManager.Lists)
            {
                if (list.ListTitle == currentList.ListTitle)
                {
                    listExists = true;
                    currentListId = ProgramManager.Lists.IndexOf(list);
                    break;
                }
            }

            if (listExists)
            {
                currentTasks = ProgramManager.Lists[currentListId].Tasks;

                foreach (Task task in currentTasks)
                {
                    if (task.TaskTitle == currentTask.TaskTitle)
                    {
                        taskExists = true;
                        break;
                    }
                }

                if (taskExists)
                {
                    Console.WriteLine("Task already exists");

                    Console.Write("Do you want to remove this task? y/N: ");

                    switch (Console.ReadLine().ToUpper())
                    {
                        case "Y":
                            break;
                        default:
                            return;
                    }

                    DeleteArchiveTask(listId, taskId);
                    return;
                }

                ProgramManager.Lists[currentListId].Tasks.Add(currentTask);
            }
            else
            {
                List newList = new List()
                {
                    ListTitle = currentList.ListTitle,
                    Tasks = new List<Task>()
                };

                ProgramManager.Lists.Add(newList);

                int newListId = ProgramManager.Lists.IndexOf(newList);

                ProgramManager.Lists[newListId].Tasks.Add(currentTask);
            }

            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            ProgramManager.UpdateAllLists();

            DeleteArchiveTask(listId, taskId);

            ArchiveListOverview.ViewTasksInArchiveList(listId);
        }
    }
}
