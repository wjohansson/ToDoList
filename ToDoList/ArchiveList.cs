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
    }
}
