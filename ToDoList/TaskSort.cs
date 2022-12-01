using System.Collections.Generic;

namespace ToDoListApp
{
    public class TaskSort
    {
        public static void SortTasks(int listId)
        {
            Console.Clear();
            Console.WriteLine("[N] To sort by name.");
            Console.WriteLine("[D] To sort by date.");
            Console.WriteLine("[P] To sort by priority.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "N":
                    NameSort(listId);

                    break;
                case "D":
                    DateSort(listId);

                    break;
                case "P":
                    PrioritySort(listId);

                    break;
            }
        }

        public static void NameSort(int listId)
        {
            ProgramManager.Lists[listId - 1].Tasks = ProgramManager.Lists[listId - 1].Tasks.OrderBy(o => o.TaskTitle).ToList();

            ProgramManager.UpdateAllLists();
        }

        public static void DateSort(int listId)
        {
            ProgramManager.Lists[listId - 1].Tasks = ProgramManager.Lists[listId - 1].Tasks.OrderBy(o => o.DateCreated).ToList();

            ProgramManager.UpdateAllLists();
        }

        private static void PrioritySort(int listId)
        {
            ProgramManager.Lists[listId - 1].Tasks = ProgramManager.Lists[listId - 1].Tasks.OrderBy(o => o.Priority).ToList();

            ProgramManager.UpdateAllLists();
        }
    }
}