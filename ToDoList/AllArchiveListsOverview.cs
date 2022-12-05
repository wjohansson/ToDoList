namespace ToDoListApp
{
    public class AllArchiveListsOverview
    {
        public static void AllArchiveLists()
        {
            Console.Clear();

            Console.WriteLine("ARCHIVE OVEWVIEW MENU");
            Console.WriteLine();


            if (ProgramManager.ArchiveLists.Count == 0)
            {
                Console.WriteLine("No existing archived lists");
                Console.WriteLine();

                ArchiveListOption();

                return;
            }

            Console.WriteLine("Current existing archived lists:");
            Console.WriteLine();

            foreach (ListManager list in ProgramManager.ArchiveLists)
            {
                var allTasksCompleted = true;

                if (list.Tasks.Count == 0)
                {
                    allTasksCompleted = false;
                }

                foreach (TaskManager task in list.Tasks)
                {
                    if (!task.Completed)
                    {
                        allTasksCompleted = false;
                    }
                }

                if (allTasksCompleted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"List Position #{ProgramManager.ArchiveLists.IndexOf(list) + 1}");
                Console.WriteLine($"    Title - {list.ListTitle}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
            }

            ArchiveListOption();
        }

        public static void AllArchiveListsAndToDo()
        {
            Console.Clear();

            Console.WriteLine("ARCHIVE OVEWVIEW MENU");
            Console.WriteLine();

            if (ProgramManager.ArchiveLists.Count == 0)
            {
                Console.WriteLine("No existing archived lists and tasks.");
                Console.WriteLine();

                ArchiveListOption();

                return;
            }

            Console.WriteLine("Current existing archived lists and tasks:");
            Console.WriteLine();

            foreach (ListManager list in ProgramManager.ArchiveLists)
            {
                Console.WriteLine($"List Position #{ProgramManager.ArchiveLists.IndexOf(list) + 1}");
                Console.WriteLine($"    Title - {list.ListTitle} (Category: {list.ListCategory})");

                foreach (TaskManager task in list.Tasks)
                {
                    if (task.Completed)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine($"        Task Position #{list.Tasks.IndexOf(task) + 1}");
                    Console.WriteLine($"            Title - {task.TaskTitle} (Prio: {task.Priority})");
                    Console.WriteLine($"                ¤ {task.TaskDescription}");

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
            Console.WriteLine("[R] To restore an archived lists.");
            Console.WriteLine("[V] To view a arvchived list.");
            Console.WriteLine("[D] To delete a arvchived list.");
            Console.WriteLine("[B] To go back to startpage.");
            Console.WriteLine("[DELARCHIVE] To go delete all tasks and lists in archive.");
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
                case "R":
                    ArchiveList.RestoreSpecificArchiveList();

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
                case "DELARCHIVE":
                    ProgramManager.ClearAllArchiveLists();

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