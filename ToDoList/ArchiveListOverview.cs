using ToDoList;

namespace ToDoListApp
{
    public class ArchiveListOverview
    {
        public static void ViewTasksInArchiveList(int listId)
        {
            var currentList = ProgramManager.ArchiveLists[listId - 1];

            Console.Clear();

            Console.WriteLine($"List Title: {currentList.ListTitle}");
            Console.WriteLine();

            var tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                //Kanske inte fungerar, dubbelkolla denna
                ProgramManager.ArchiveLists.Remove(currentList);

                ViewTasksInArchiveList(listId);

                return;
            }

            ProgramManager.UpdateArchive();

            foreach (var task in tasks)
            {
                if (task.Completed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"-{tasks.IndexOf(task) + 1}- {task.TaskTitle}");
                Console.WriteLine($"    - {task.TaskDescription}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
            }

            ArchiveTasksOption(listId);

            ViewTasksInArchiveList(listId);
        }

        public static void ArchiveTasksOption(int listId)
        {
            Console.WriteLine("[R] To restore task.");
            Console.WriteLine("[D] To delete an arhived task.");
            Console.WriteLine("[B] To go back to archive start page.");
            Console.WriteLine("[Q] To quit the program.");

            Console.WriteLine();
            Console.Write("What do you want to do: ");
            switch (Console.ReadLine().ToUpper())
            {
                case "R":
                    ArchiveTask.RestoreTask(listId);

                    break;
                case "D":
                    ArchiveTask.DeleteSpecificArchiveTask(listId);

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