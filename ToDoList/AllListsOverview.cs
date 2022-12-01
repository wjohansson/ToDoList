using System.Text.Json;
using System.Threading.Tasks;

namespace ToDoListApp
{
    public class AllListsOverview
    {

        public static void AllLists()
        {
            Console.Clear();

            Console.WriteLine("OVEWVIEW MENU");
            Console.WriteLine();

            if (ProgramManager.Lists.Count == 0)
            {
                Console.WriteLine("No existing lists");
                Console.WriteLine();

                ListOption();

                return;
            }

            Console.WriteLine("Current existing lists:");
            Console.WriteLine();

            foreach (List list in ProgramManager.Lists)
            {
                Console.WriteLine($"List Id #{ProgramManager.Lists.IndexOf(list) + 1}");
                Console.WriteLine($"    Title - {list.ListTitle}");
                Console.WriteLine();
            }

            ListOption();
        }

        public static void AllListsAndToDo()
        {
            Console.Clear();

            Console.WriteLine("OVEWVIEW MENU");
            Console.WriteLine();

            if (ProgramManager.Lists.Count == 0)
            {
                Console.WriteLine("No existing lists and tasks.");
                Console.WriteLine();

                ListOption();

                return;
            }
            Console.WriteLine("Current existing lists and tasks:");
            Console.WriteLine();

            foreach (List list in ProgramManager.Lists)
            {
                Console.WriteLine($"List Id #{ProgramManager.Lists.IndexOf(list) + 1}");
                Console.WriteLine($"    Title - {list.ListTitle} (Category: {list.ListCategory})");

                foreach (Task task in list.Tasks)
                {
                    if (task.Completed)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine($"        Task ID #{list.Tasks.IndexOf(task) + 1}");
                    Console.WriteLine($"            Title - {task.TaskTitle} (Prio: {task.Priority})");
                    Console.WriteLine($"                ¤ {task.TaskDescription}");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }

            ListOption();
        }

        public static void ListOption()
        {
            Console.WriteLine("[E] To expand all lists.");
            Console.WriteLine("[C] To collapse all lists.");
            Console.WriteLine("[V] To view a list.");
            Console.WriteLine("[L] To view latest list.");
            Console.WriteLine("[D] To delete a list.");
            Console.WriteLine("[N] To create a new list.");
            Console.WriteLine("[S] To sort all lists.");
            Console.WriteLine("[A] To view archived tasks.");
            Console.WriteLine("[DELALL] To delete all lists and tasks.");
            Console.WriteLine("[Q] To quit the program.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "E":
                    AllListsAndToDo();

                    break;
                case "C":
                    AllLists();

                    break;
                case "V":
                    List.ViewList();

                    return;
                case "L":
                    try
                    {
                        ListOverview.ViewTasksInList(ProgramManager.Lists.Count);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("No lists to view, create one first. Returning");

                        Thread.Sleep(2000);
                        break;
                    }

                    break;
                case "D":
                    List.DeleteList();

                    break;
                case "N":
                    List.CreateList();

                    break;
                case "S":
                    ListSort.SortLists();

                    break;
                case "A":
                    AllArchiveListsOverview.AllArchiveLists();

                    break;
                case "DELALL":
                    ProgramManager.ClearAllLists();

                    break;
                case "Q":
                    ProgramManager.QuitProgram();

                    break;
            }

            Console.Clear();
            AllLists();
        }
    }
}