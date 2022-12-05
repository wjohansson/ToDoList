using System.Collections.Generic;

namespace ToDoListApp
{
    public class TaskSort
    {
        public static void SortTasks(int listPosition)
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
                    NameSort(listPosition);

                    break;
                case "D":
                    DateSort(listPosition);

                    break;
                case "P":
                    PrioritySort(listPosition);

                    break;
            }
        }

        public static void NameSort(int listPosition)
        {
            ProgramManager.Lists[listPosition - 1].Tasks = ProgramManager.Lists[listPosition - 1].Tasks.OrderBy(o => o.TaskTitle).ToList();

            ProgramManager.UpdateAllLists();
        }

        public static void DateSort(int listPosition)
        {
            ProgramManager.Lists[listPosition - 1].Tasks = ProgramManager.Lists[listPosition - 1].Tasks.OrderBy(o => o.DateCreated).ToList();

            ProgramManager.UpdateAllLists();
        }

        private static void PrioritySort(int listPosition)
        {
            ProgramManager.Lists[listPosition - 1].Tasks = ProgramManager.Lists[listPosition - 1].Tasks.OrderBy(o => o.Priority).ToList();

            ProgramManager.UpdateAllLists();
        }
    }
}