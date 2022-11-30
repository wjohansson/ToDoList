namespace ToDoListApp
{
    public class AllArchiveListsOverview
    {
        public static void AllArchiveLists()
        {
            Console.Clear();

            if (ProgramManager.ArchiveLists.Count == 0)
            {
                Console.WriteLine("No existing archived lists");
                Console.WriteLine();

                ArchiveListOption();

                return;
            }

            Console.WriteLine("Current existing archived lists:");
            Console.WriteLine();

            foreach (var list in ProgramManager.ArchiveLists)
            {
                Console.WriteLine($"-{ProgramManager.ArchiveLists.IndexOf(list) + 1}- {list.ListTitle}");
                Console.WriteLine();
            }

            ArchiveListOption();
        }

        public static void AllArchiveListsAndToDo()
        {
            Console.Clear();

            if (ProgramManager.ArchiveLists.Count == 0)
            {
                Console.WriteLine("No existing archived lists and tasks.");
                Console.WriteLine();

                ArchiveListOption();

                return;
            }
            Console.WriteLine("Current existing archived lists and tasks:");
            Console.WriteLine();

            foreach (var list in ProgramManager.ArchiveLists)
            {
                Console.WriteLine($"-{ProgramManager.ArchiveLists.IndexOf(list) + 1}- {list.ListTitle}");

                foreach (var task in list.Tasks)
                {
                    if (task.Completed)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine($"    -{list.Tasks.IndexOf(task) + 1}- {task.TaskTitle}");
                    Console.WriteLine($"            - {task.TaskDescription}");

                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
            ArchiveListOption();
        }

        public static void ArchiveListOption()
        {
            Console.WriteLine("[E] To expand all archived lists.");
            Console.WriteLine("[C] To collapse all arvchived lists.");
            Console.WriteLine("[V] To view a arvchived list.");
            Console.WriteLine("[D] To delete a arvchived list.");
            Console.WriteLine("[B] To go back to startpage.");
            Console.WriteLine("[Q] To quit the program.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "E":
                    AllArchiveListsAndToDo();

                    break;
                case "C":
                    AllArchiveLists();

                    break;
                case "V":
                    ArchiveList.ViewArchiveList();

                    return;
                case "D":
                    ArchiveList.DeleteArchiveList();

                    break;
                case "B":
                    Console.Clear();
                    AllListsOverview.AllLists();

                    break;
                case "Q":
                    ProgramManager.QuitProgram();

                    break;
            }

            Console.Clear();
            AllArchiveLists();

        }
    }
}