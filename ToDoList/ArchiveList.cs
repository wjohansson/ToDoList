using System.Collections.Generic;

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

            int archiveListPosition;

            try
            {
                Console.Write("Enter the position of the archived list you want to delete: ");
                archiveListPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                DeleteArchiveList();

                return;
            }

            ProgramManager.AreYouSure("Are you sure you want to delete the list? y/N: ");

            try
            {
                ProgramManager.ArchiveLists.RemoveAt(archiveListPosition - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                DeleteArchiveList();

                return;
            }

            ProgramManager.UpdateArchive();

            Console.Clear();
            AllArchiveListsOverview.AllArchiveLists();
        }

        public static void DeleteSpecificArchiveList(int archiveListPosition)
        {
            try
            {
                ProgramManager.ArchiveLists.RemoveAt(archiveListPosition - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                DeleteSpecificArchiveList(archiveListPosition);

                return;
            }

            ProgramManager.UpdateArchive();
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

            int archiveListPosition;

            try
            {
                Console.Write("Enter the position of the archived list you want to view: ");
                archiveListPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                ViewArchiveList();

                return;
            }

            try
            {
                ArchiveListOverview.ViewTasksInArchiveList(archiveListPosition);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                ViewArchiveList();

                return;
            }
        }

        public static void RestoreSpecificArchiveList()
        {
            int archiveListPosition;
            try
            {
                Console.Write("Position of the list you want to restore: ");
                archiveListPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number. Try again");

                RestoreSpecificArchiveList();
                return;
            }

            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition - 1];

            var listExists = false;

            var listPosition = 0;

            foreach (ListManager list in ProgramManager.Lists)
            {
                if (list.ListTitle == currentArchiveList.ListTitle)
                {
                    listExists = true;
                    listPosition = ProgramManager.Lists.IndexOf(list);
                    break;
                }
            }

            Console.Write("Are you sure you want to restore the list? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    ArchiveListOverview.ViewTasksInArchiveList(archiveListPosition);
                    return;
            }

            if (listExists)
            {
                Console.WriteLine("List already exists.");
                Console.WriteLine();
                Console.WriteLine("[N] To create a new list with a new name");
                Console.WriteLine("[A] To add tasks to existing list");

                Console.Write("What do you want to do: ");

                switch (Console.ReadLine().ToUpper())
                {
                    case "N":
                        ListManager.CreateListIfDouble(archiveListPosition);

                        break;
                    case "A":
                        ListManager.AddTasksToList(listPosition, archiveListPosition);

                        break;
                }
            }
            else
            {
                ProgramManager.Lists.Add(currentArchiveList);
            }


            DeleteSpecificArchiveList(archiveListPosition);
        }

        public static void CreateArchiveListIfDouble(int listPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            Console.Write("Enter the new title of the duplicate list: ");
            string newTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("New title can not be empty. Try again");

                CreateArchiveListIfDouble(listPosition);

                return;
            }

            foreach (ListManager list in ProgramManager.ArchiveLists)
            {
                if (list.ListTitle == newTitle)
                {
                    Console.WriteLine("List title already exists in archive. Try again");

                    CreateArchiveListIfDouble(listPosition);

                    return;
                }
            }

            ListManager newList = new()
            {
                ListTitle = newTitle,
                ListCategory = currentList.ListCategory,
                Tasks = currentList.Tasks
            };

            ProgramManager.ArchiveLists.Add(newList);

            ProgramManager.UpdateArchive();
        }

        public static void AddTasksToArchiveList(int listPosition, int archiveListPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];
            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition];

            foreach (TaskManager task in currentList.Tasks)
            {
                foreach (TaskManager archiveTask in currentArchiveList.Tasks)
                {
                    if (task.TaskTitle == archiveTask.TaskTitle)
                    {
                        Console.WriteLine($"Task [{task.TaskTitle}] already exists in the archive list. Creating a new list to prevent doubles");
                        CreateArchiveListIfDouble(listPosition);
                        return;
                    }
                }
                currentArchiveList.Tasks.Add(task);
            }

            ProgramManager.UpdateArchive();
        }
    }
}
