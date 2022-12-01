namespace ToDoListApp
{
    public class ListSort
    {
        public static void SortLists()
        {
            Console.Clear();
            Console.WriteLine("[N] To sort by name.");
            Console.WriteLine("[C] To sort by category.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "N":
                    NameSort();

                    break;
                case "C":
                    CategorySort();

                    break;
            }
        }

        public static void NameSort()
        {
            ProgramManager.Lists = ProgramManager.Lists.OrderBy(o => o.ListTitle).ToList();

            ProgramManager.UpdateAllLists();
        }

        public static void CategorySort()
        {
            ProgramManager.Lists = ProgramManager.Lists.OrderBy(o => o.ListCategory).ToList();

            ProgramManager.UpdateAllLists();
        }
    }
}