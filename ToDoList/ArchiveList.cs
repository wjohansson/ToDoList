namespace ToDoListApp
{
    public class ArchiveList
    {
        public static void DeleteArchiveList()
        {
            if (ProgramManager.ArchiveLists.Count == 0)
            {
                Console.WriteLine("No archived list to remove. Returning");

                Thread.Sleep(2000);

                Console.Clear();
                AllArchiveListsOverview.AllArchiveLists();
                return;
            }

            int listId;

            try
            {
                Console.Write("Enter the id of the archived list you want to delete: ");
                listId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                DeleteArchiveList();

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
                ProgramManager.ArchiveLists.RemoveAt(listId - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                DeleteArchiveList();

                return;
            }

            ProgramManager.UpdateArchive();

            Console.Clear();
            AllArchiveListsOverview.AllArchiveLists();
        }

        public static void ViewArchiveList()
        {
            if (ProgramManager.ArchiveLists.Count == 0)
            {
                Console.WriteLine("No archived list to view, create one first. Returning");

                Thread.Sleep(2000);

                Console.Clear();

                AllArchiveListsOverview.AllArchiveLists();
                return;
            }

            int listId;

            try
            {
                Console.Write("Enter the id of the archived list you want to view: ");
                listId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                ViewArchiveList();

                return;
            }

            try
            {
                ArchiveListOverview.ViewTasksInArchiveList(listId);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                ViewArchiveList();

                return;
            }
        }

        public static void CreateArchiveListIfDouble(int listId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            Console.Write("Enter the new title of the duplicate list: ");
            string newTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("New title can not be empty. Try again");

                CreateArchiveListIfDouble(listId);

                return;
            }

            foreach (List list in ProgramManager.ArchiveLists)
            {
                if (list.ListTitle == newTitle)
                {
                    Console.WriteLine("List title already exists in archive. Try again");

                    CreateArchiveListIfDouble(listId);

                    return;
                }
            }

            List newList = new()
            {
                ListTitle = newTitle,
                ListCategory = currentList.ListCategory,
                Tasks = currentList.Tasks
            };

            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            ProgramManager.ArchiveLists.Add(newList);

            ProgramManager.UpdateArchive();
        }

        public static void AddTasksToArchiveList(int listId, int archiveListId)
        {
            List currentList = ProgramManager.Lists[listId - 1];
            List currentArchiveList = ProgramManager.ArchiveLists[archiveListId - 1];

            foreach (Task task in currentList.Tasks)
            {
                foreach (Task archiveTask in currentArchiveList.Tasks)
                {
                    if (task.TaskTitle == archiveTask.TaskTitle)
                    {
                        Console.WriteLine($"Task [{task.TaskTitle}] already exists in the archive list. Creating a new list to prevent doubles");
                        CreateArchiveListIfDouble(listId);
                        return;
                    }
                }
                currentArchiveList.Tasks.Add(task);
            }

            ProgramManager.UpdateArchive();
        }
    }
}
