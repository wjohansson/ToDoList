namespace ToDoListApp
{
    public class Task
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool Completed { get; set; }

        public static void CreateTask(int listId)
        {
            var currentList = ProgramManager.Lists[listId - 1];

            var tasks = currentList.Tasks;

            Console.Write("Enter a new task: ");
            var newTitle = Console.ReadLine();

            Console.Write("Enter the task description: ");
            var newDescription = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle) || String.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("Task title or description can not be empty. Try again");

                CreateTask(listId);

                return;
            }

            var task = new Task()
            {
                TaskTitle = newTitle,
                TaskDescription = newDescription,
                Completed = false
            };

            tasks.Add(task);

            ProgramManager.UpdateAllLists();
        }

        public static void EditTask(int listId, int taskId)
        {
            var currentList = ProgramManager.Lists[listId - 1];

            var tasks = currentList.Tasks;

            var currentTask = tasks[taskId - 1];

            Console.WriteLine();
            Console.WriteLine($"Old title: {currentTask.TaskTitle}");
            Console.Write("Enter the new title: ");
            var newTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("The title can not be empty. Try again.");

                EditTask(listId, taskId);

                return;
            }

            Console.WriteLine($"Old description: {currentTask.TaskDescription}");
            Console.Write("Enter the new description: ");
            var newDescription = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("The description can not be empty. Try again.");

                EditTask(listId, taskId);

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

            currentTask.TaskTitle = newTitle;
            currentTask.TaskDescription = newDescription;

            ProgramManager.UpdateAllLists();
        }

        public static void DeleteSpecificTask(int listId)
        {
            var currentList = ProgramManager.Lists[listId - 1];

            var tasks = currentList.Tasks;

            int taskId;

            try
            {
                Console.Write("Enter the id of the task you want to delete: ");
                taskId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                DeleteSpecificTask(listId);

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
                tasks.RemoveAt(taskId - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                DeleteSpecificTask(listId);

                return;
            }

            ProgramManager.UpdateAllLists();
        }

        public static void DeleteTask(int listId, int taskId)
        {
            var currentList = ProgramManager.Lists[listId - 1];

            var tasks = currentList.Tasks;

            tasks.RemoveAt(taskId - 1);
            

            ProgramManager.UpdateAllLists();
        }

        public static void ViewTask(int listId)
        {
            int taskId;

            try
            {
                Console.Write("Enter the id of the task you want to view: ");
                taskId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                ViewTask(listId);

                return;
            }

            try
            {
                TaskOverview.ViewIndividualTask(listId, taskId);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                ViewTask(listId);

                return;
            }
        }

        public static void ArchiveTask(int listId, int taskId)
        {
            //Kolla om denna lista som innehåller tasken finns i arkiv listan, om den finns så ska den även kolla om tasken redan finns, 
            // om tasken också finns så ska man inte göra nånting mer, eftersom det inte ska finnas dubletter av någonting
            // om tasken däremot inte finns men listan finns så måste man lägga till tasken i den redan befintliga listan
            // om varken tasken eller listan finns så måste man skapa en ny lista med samma namn. Och sedan måste man då lägga
            // till den nya tasken i listan

            var currentList = ProgramManager.Lists[listId - 1];

            var tasks = currentList.Tasks;

            var currentTask = tasks[taskId - 1];

            var listExists = false;
            var taskExists = false;

            int archiveListId = 0;
            List<Task> currentArchiveTasks;

            foreach (var list in ProgramManager.ArchiveLists)
            {
                if (list.ListTitle == currentList.ListTitle)
                {
                    listExists = true;
                    archiveListId = ProgramManager.ArchiveLists.IndexOf(list);
                    break;
                }
            }



            if (listExists)
            {
                currentArchiveTasks = ProgramManager.ArchiveLists[archiveListId].Tasks;

                foreach (var task in currentArchiveTasks)
                {
                    if (task.TaskTitle == currentTask.TaskTitle)
                    {
                        taskExists = true;
                        break;
                    }
                }

                if (taskExists)
                {
                    Console.WriteLine("Task already exists in archive");

                    Console.Write("Do you want to remove this task? y/N: ");

                    switch (Console.ReadLine().ToUpper())
                    {
                        case "Y":
                            break;
                        default:
                            return;
                    }

                    DeleteTask(listId, taskId);
                    return;
                }

                ProgramManager.ArchiveLists[archiveListId].Tasks.Add(currentTask);
            }
            else
            {
                var newList = new List()
                {
                    ListTitle = currentList.ListTitle,
                    Tasks = new List<Task>()
                };

                ProgramManager.ArchiveLists.Add(newList);

                var archiveNewListId = ProgramManager.ArchiveLists.IndexOf(newList);

                ProgramManager.ArchiveLists[archiveNewListId].Tasks.Add(currentTask);
            }

            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            ProgramManager.UpdateArchive();

            DeleteTask(listId, taskId);

            ListOverview.ViewTasksInList(listId);
        }

        public static void ToggleComplete(int listId)
        {
            var currentList = ProgramManager.Lists[listId - 1];

            var tasks = currentList.Tasks;

            int taskId;

            try
            {
                Console.Write("Enter the id of the list: ");
                taskId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                ToggleComplete(listId);

                return;
            }

            Task currentTask;

            try
            {
                currentTask = tasks[taskId - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Id does not exist, try again.");

                ToggleComplete(listId);

                return;
            }

            currentTask.Completed = !currentTask.Completed;

            ProgramManager.UpdateAllLists();
        }
    }
}