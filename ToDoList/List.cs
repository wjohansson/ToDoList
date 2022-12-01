using System.Threading.Tasks;

namespace ToDoListApp
{
    public class List
    {
        public string ListTitle { get; set; }
        public string ListCategory { get; set; }
        public List<Task> Tasks { get; set; }

        public static void CreateList()
        {
            Console.Write("Enter new list name: ");
            string listTitle = Console.ReadLine();

            Console.Write("Enter new list category: ");
            string listCategory = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(listTitle) || String.IsNullOrWhiteSpace(listCategory))
            {
                Console.WriteLine("List title can not be empty, try again");

                CreateList();

                return;
            }

            List newList = new()
            {
                ListTitle = listTitle,
                Tasks = new List<Task>(),
                ListCategory = listCategory
            };

            ProgramManager.Lists.Add(newList);

            ProgramManager.UpdateAllLists();

            AllListsOverview.AllLists();
        }

        public static void EditList(int listId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            Console.Clear();
            Console.WriteLine($"Old title: {currentList.ListTitle}");
            Console.Write("Enter the new title or leave empty to keep the old title: ");
            string newTitle = Console.ReadLine();

            Console.WriteLine($"Old categoy: {currentList.ListTitle}");
            Console.Write("Enter the new category or leave empty to keep the old category: ");
            string newCategory = Console.ReadLine();

            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

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

            int listId;

            try
            {
                Console.Write("Enter the id of the list you want to delete: ");
                listId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                DeleteList();

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
                ProgramManager.Lists.RemoveAt(listId - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                DeleteList();

                return;
            }

            ProgramManager.UpdateAllLists();

            Console.Clear();
            AllListsOverview.AllLists();
        }


        public static void DeleteSpecificList(int listId)
        {
            try
            {
                ProgramManager.Lists.RemoveAt(listId - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                DeleteList();

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

            int listId;

            try
            {
                Console.Write("Enter the id of the list you want to view: ");
                listId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                ViewList();

                return;
            }

            try
            {
                ListOverview.ViewTasksInList(listId);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                ViewList();

                return;
            }
        }

        public static void ArchiveThisList(int listId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            var listExists = false;

            var archiveListId = 0;

            foreach (List list in ProgramManager.ArchiveLists)
            {
                if (list.ListTitle == currentList.ListTitle)
                {
                    listExists = true;
                    archiveListId = ProgramManager.ArchiveLists.IndexOf(list);
                    break;
                }
            }

            if (listExists)
            {
                Console.WriteLine("List already exists in archive.");
                Console.WriteLine();
                Console.WriteLine("[N] To create a new archive list with a new name");
                Console.WriteLine("[A] To add tasks to existing archive list");

                switch (Console.ReadLine().ToUpper())
                {
                    case "N":
                        ArchiveList.CreateArchiveListIfDouble(listId);

                        break;
                    case "A":
                        ArchiveList.AddTasksToArchiveList(listId, archiveListId);

                        break;
                }
            }
            else
            {
                ProgramManager.ArchiveLists.Add(currentList);
            }


            DeleteSpecificList(listId);
        }
        }
    }
