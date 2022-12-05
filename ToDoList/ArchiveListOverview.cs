namespace ToDoListApp
{
    public class ArchiveListOverview
    {
        public static void ViewTasksInArchiveList(int archiveListPosition)
        {
            ListManager currentArchiveList = ProgramManager.ArchiveLists[archiveListPosition - 1];

            List<TaskManager> archiveTasks = currentArchiveList.Tasks;

            if (archiveTasks.Count == 0)
            {
                ProgramManager.ArchiveLists.RemoveAt(archiveListPosition - 1);

                ProgramManager.UpdateArchive();

                AllArchiveListsOverview.AllArchiveLists();

                return;
            }

            Console.Clear();

            Console.WriteLine("ARCHIVE LIST MENU");
            Console.WriteLine();

            Console.WriteLine($"List Title: {currentArchiveList.ListTitle}");
            Console.WriteLine();

            ProgramManager.UpdateArchive();

            foreach (TaskManager task in archiveTasks)
            {
                if (task.Completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"    Task Position #{archiveTasks.IndexOf(task) + 1}");
                Console.WriteLine($"        Title - {task.TaskTitle} (Prio: {task.Priority})");
                Console.WriteLine($"            ¤ {task.TaskDescription}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
            }

            ArchiveTasksOption(archiveListPosition);

            ViewTasksInArchiveList(archiveListPosition);
        }

        public static void ArchiveTasksOption(int archiveListPosition)
        {
            Console.WriteLine("[R] To restore task.");
            Console.WriteLine("[D] To delete an archived task.");
            Console.WriteLine("[B] To go back to archive start page.");
            Console.WriteLine("[Q] To quit the program.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "R":
                    ArchiveTask.RestoreSpecificTask(archiveListPosition);

                    break;
                case "D":
                    ArchiveTask.DeleteSpecificArchiveTask(archiveListPosition);

                    break;
                case "B":
                    Console.Clear();
                    AllArchiveListsOverview.AllArchiveLists();

                    break;
                case "Q":
                    ProgramManager.QuitProgram();

                    break;
            }
        }
    }
}