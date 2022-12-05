namespace ToDoListApp
{
    public class ArchiveTask
    {
        public static void DeleteSpecificArchiveTask(int archiveListPosition)
        {
            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition - 1];

            List<TaskManager> archiveTasks = currentArchiveList.Tasks;

            int archiveTaskPosition;

            try
            {
                Console.Write("Enter the Position of the archived task you want to delete: ");
                archiveTaskPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                DeleteSpecificArchiveTask(archiveListPosition);

                return;
            }

            ProgramManager.AreYouSure("Are you sure you want to delete this archive task? y/N: ");

            try
            {
                archiveTasks.RemoveAt(archiveTaskPosition - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                DeleteSpecificArchiveTask(archiveListPosition);

                return;
            }

            ProgramManager.UpdateArchive();
        }

        public static void DeleteArchiveTask(int archiveListPosition, int archiveTaskPosition)
        {
            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition - 1];

            List<TaskManager> archiveTasks = currentArchiveList.Tasks;

            archiveTasks.RemoveAt(archiveTaskPosition - 1);

            ProgramManager.UpdateArchive();
        }

        public static void RestoreSpecificTask(int archiveListPosition)
        {
            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition - 1];

            List<TaskManager> archiveTasks = currentArchiveList.Tasks;

            int archiveTaskPosition;

            try
            {
                Console.Write("Enter the position of the archived task you want to restore: ");
                archiveTaskPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                RestoreSpecificTask(archiveListPosition);

                return;
            }

            TaskManager currentTask = archiveTasks[archiveTaskPosition - 1];

            var listExists = false;
            var taskExists = false;

            var currentListId = 0;
            List<TaskManager> currentTasks;

            foreach (ListManager list in ProgramManager.Lists)
            {
                if (list.ListTitle == currentArchiveList.ListTitle)
                {
                    listExists = true;
                    currentListId = ProgramManager.Lists.IndexOf(list);
                    break;
                }
            }

            if (listExists)
            {
                currentTasks = ProgramManager.Lists[currentListId].Tasks;

                foreach (TaskManager task in currentTasks)
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

                    DeleteArchiveTask(archiveListPosition, archiveTaskPosition);
                    return;
                }

                ProgramManager.Lists[currentListId].Tasks.Add(currentTask);
            }
            else
            {
                ListManager newList = new ListManager()
                {
                    ListTitle = currentArchiveList.ListTitle,
                    Tasks = new List<TaskManager>()
                };

                ProgramManager.Lists.Add(newList);

                int newListId = ProgramManager.Lists.IndexOf(newList);

                ProgramManager.Lists[newListId].Tasks.Add(currentTask);
            }

            ProgramManager.AreYouSure("Are you sure you want to restore this archive task? y/N: ");

            ProgramManager.UpdateAllLists();

            DeleteArchiveTask(archiveListPosition, archiveTaskPosition);

            ArchiveListOverview.ViewTasksInArchiveList(archiveListPosition);
        }
    }
}
