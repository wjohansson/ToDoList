namespace ToDoListApp
{
    public class List
    {
        public string ListTitle { get; set; }
        public List<Task> Tasks { get; set; }

        public static void CreateList()
        {
            Console.Write("Enter new list name: ");
            var listTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(listTitle))
            {
                Console.WriteLine("List title can not be empty, try again");

                CreateList();
                return;
            }

            var list = new List()
            {
                ListTitle = listTitle,
                Tasks = new List<Task>()
            };

            ProgramManager.Lists.Add(list);

            ProgramManager.UpdateAllLists();

            AllListsOverview.AllLists();
        }

        public static void EditList(int listId)
        {
            var currentList = ProgramManager.Lists[listId - 1];

            Console.Clear();
            Console.WriteLine($"Old title: {currentList.ListTitle}");
            Console.Write("Enter the new title: ");
            var newTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("The title can not be empty. Try again");

                EditList(listId);

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

            currentList.ListTitle = newTitle;

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
    }
}
