using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public class ListManager
    {
        public string ListTitle { get; set; }
        public string ListCategory { get; set; }
        public int ListId { get; init; }
        public List<TaskManager> Tasks { get; set; }

        public static void CreateList()
        {
            Console.Write("Enter new list name: ");
            string listTitle = Console.ReadLine();

            foreach (ListManager list in ProgramManager.Lists)
            {
                if (list.ListTitle == listTitle)
                {
                    Console.WriteLine("List with the same name already exists. Try again");

                    CreateList();

                    return;
                }
            }

            Console.Write("Enter new list category: ");
            string listCategory = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(listTitle) || String.IsNullOrWhiteSpace(listCategory))
            {
                Console.WriteLine("List title can not be empty, try again");

                CreateList();

                return;
            }

            var listId = 1;

            foreach (ListManager list in ProgramManager.Lists)
            {
                if (list.ListId >= listId)
                {
                    listId = list.ListId + 1;
                }
            }

            ListManager newList = new()
            {
                ListTitle = listTitle,
                Tasks = new List<TaskManager>(),
                ListId = listId,
                ListCategory = listCategory
            };

            ProgramManager.Lists.Add(newList);

            ProgramManager.UpdateAllLists();

            AllListsOverview.AllLists();
        }

        public static void EditList(int listPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            Console.Clear();
            Console.WriteLine($"Old title: {currentList.ListTitle}");
            Console.Write("Enter the new title or leave empty to keep the old title: ");
            string newTitle = Console.ReadLine();

            foreach (ListManager list in ProgramManager.Lists)
            {
                if (list.ListTitle == newTitle)
                {
                    Console.WriteLine("List with the same name already exists. Try again");

                    CreateList();

                    return;
                }
            }

            Console.WriteLine($"Old category: {currentList.ListCategory}");
            Console.Write("Enter the new category or leave empty to keep the old category: ");
            string newCategory = Console.ReadLine();

            ProgramManager.AreYouSure("Are you sure you want to edit this list? y/N: ");

            if (!String.IsNullOrWhiteSpace(newTitle))
            {
                currentList.ListTitle = newTitle;
            }

            if (!String.IsNullOrWhiteSpace(newCategory))
            {
                currentList.ListCategory = newCategory;
            }

            ProgramManager.UpdateAllLists();
        }

        public static void DeleteList()
        {
            if (ProgramManager.Lists.Count == 0)
            {
                Console.WriteLine("No list to remove. Returning");

                Thread.Sleep(2000);

                Console.Clear();
                AllListsOverview.AllLists();
                return;
            }

            int listPosition;

            try
            {
                Console.Write("Enter the position of the list you want to delete: ");
                listPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                DeleteList();

                return;
            }

            ProgramManager.AreYouSure("Are you sure you want to delete this list? y/N: ");

            try
            {
                int listId = ProgramManager.Lists[listPosition - 1].ListId;
                HistoryManager.DeleteIdFromHistory(listId);

                ProgramManager.Lists.RemoveAt(listPosition - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                DeleteList();

                return;
            }

            ProgramManager.UpdateAllLists();

            Console.Clear();
            AllListsOverview.AllLists();
        }


        public static void DeleteSpecificList(int listPosition)
        {
            try
            {
                int listId = ProgramManager.Lists[listPosition - 1].ListId;
                HistoryManager.DeleteIdFromHistory(listId);

                ProgramManager.Lists.RemoveAt(listPosition - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                DeleteSpecificList(listPosition);

                return;
            }

            ProgramManager.UpdateAllLists();
        }

        public static void ViewList()
        {
            if (ProgramManager.Lists.Count == 0)
            {
                Console.WriteLine("No list to view, create one first. Returning");

                Thread.Sleep(2000);

                Console.Clear();

                AllListsOverview.AllLists();
                return;
            }

            int listPosition;

            try
            {
                Console.Write("Enter the position of the list you want to view: ");
                listPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                ViewList();

                return;
            }

            try
            {
                ProgramManager.RecentList.Insert(0, ProgramManager.Lists[listPosition - 1].ListId);
                ProgramManager.UpdateRecent();

                ListOverview.ViewTasksInList(listPosition);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                ViewList();

                return;
            }
        }

        public static void ViewList(int listPosition)
        {
            if (ProgramManager.Lists.Count == 0)
            {
                Console.WriteLine("No list to view, create one first. Returning");

                Thread.Sleep(2000);

                Console.Clear();

                AllListsOverview.AllLists();
                return;
            }

            try
            {
                ProgramManager.RecentList.Insert(0, ProgramManager.Lists[listPosition - 1].ListId);
                ProgramManager.UpdateRecent();

                ListOverview.ViewTasksInList(listPosition);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                ViewList();

                return;
            }
        }

        public static void ArchiveThisList(int listPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            var listExists = false;

            var archiveListPosition = 0;

            foreach (ListManager list in ProgramManager.ArchiveLists)
            {
                if (list.ListTitle == currentList.ListTitle)
                {
                    listExists = true;
                    archiveListPosition = ProgramManager.ArchiveLists.IndexOf(list);
                    break;
                }
            }

            Console.Write("Are you sure you want to archive this list? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    ListOverview.ViewTasksInList(listPosition);

                    return;
            }

            if (listExists)
            {
                Console.WriteLine("List already exists in archive.");
                Console.WriteLine();
                Console.WriteLine("[N] To create a new archive list with a new name");
                Console.WriteLine("[A] To add tasks to existing archive list");

                Console.Write("What do you want to do: ");

                switch (Console.ReadLine().ToUpper())
                {
                    case "N":
                        ArchiveList.CreateArchiveListIfDouble(listPosition);

                        break;
                    case "A":
                        ArchiveList.AddTasksToArchiveList(listPosition, archiveListPosition);

                        break;
                }
            }
            else
            {
                int listId = ProgramManager.Lists[listPosition - 1].ListId;
                HistoryManager.DeleteIdFromHistory(listId);

                ProgramManager.ArchiveLists.Add(currentList);
            }


            DeleteSpecificList(listPosition);
        }

        public static void CreateListIfDouble(int archiveListPosition)
        {
            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition - 1];

            Console.Write("Enter the new title of the duplicate list: ");
            string newTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("New title can not be empty. Try again");

                CreateListIfDouble(archiveListPosition);

                return;
            }

            foreach (ListManager list in ProgramManager.Lists)
            {
                if (list.ListTitle == newTitle)
                {
                    Console.WriteLine("List title already exists in archive. Try again");

                    CreateListIfDouble(archiveListPosition);

                    return;
                }
            }

            ListManager newList = new()
            {
                ListTitle = newTitle,
                ListCategory = currentArchiveList.ListCategory,
                Tasks = currentArchiveList.Tasks
            };

            ProgramManager.Lists.Add(newList);

            ProgramManager.UpdateAllLists();
        }

        public static void AddTasksToList(int listPosition, int archiveListPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition];
            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition - 1];

            foreach (TaskManager archiveTask in currentArchiveList.Tasks)
            {
                foreach (TaskManager task in currentList.Tasks)
                {
                    if (task.TaskTitle == archiveTask.TaskTitle)
                    {
                        Console.WriteLine($"Task [{archiveTask.TaskTitle}] already exists in the archive list. Creating a new list to prevent doubles");
                        CreateListIfDouble(archiveListPosition);
                        return;
                    }
                }
                currentList.Tasks.Add(archiveTask);
            }

            ProgramManager.UpdateArchive();
        }
    }
}
