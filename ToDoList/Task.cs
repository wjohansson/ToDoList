namespace ToDoListApp
{
    public class Task
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool Completed { get; set; }
        public int Priority { get; set; }
        public string DateCreated { get; set; }

        public static void CreateTask(int listId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            List<Task> tasks = currentList.Tasks;

            Console.Write("Enter a new task: ");
            string newTitle = Console.ReadLine();

            Console.Write("Enter the task description: ");
            string newDescription = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle) || String.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("Task title or description can not be empty. Try again");

                CreateTask(listId);

                return;
            }

            int newPriority;

            try
            {
                Console.Write("Enter priority of the task as a number between 1 and 5: ");
                newPriority = Convert.ToInt32(Console.ReadLine());

                if (newPriority > 5 || newPriority < 1)
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {

                Console.WriteLine("Priority must be a number between 1 and 5. Try again.");

                CreateTask(listId);

                return;
            }

            Task task = new()
            {
                TaskTitle = newTitle,
                TaskDescription = newDescription,
                Completed = false,
                Priority = newPriority,
                DateCreated = DateTime.Now.ToString("G"),
            };

            tasks.Add(task);

            ProgramManager.UpdateAllLists();
        }

        public static void EditTask(int listId, int taskId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            List<Task> tasks = currentList.Tasks;

            Task currentTask = tasks[taskId - 1];

            string newPriorityInput;
            int newPriority;

            try
            {
                Console.WriteLine($"Old priority: {currentTask.Priority}");
                Console.Write("Enter the new priority or leave empty to keep old priority: ");
                newPriorityInput = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(newPriorityInput))
                {
                    newPriority = Convert.ToInt32(newPriorityInput);

                    if (newPriority > 5 || newPriority < 1)
                    {
                        throw new FormatException();
                    }

                    currentTask.Priority = newPriority;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Priority must be a number between 1 and 5. Try again");

                EditTask(listId, taskId);

                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Old title: {currentTask.TaskTitle}");
            Console.Write("Enter the new title or leave empty to keep old title: ");
            string newTitle = Console.ReadLine();

            Console.WriteLine($"Old description: {currentTask.TaskDescription}");
            Console.Write("Enter the new description or leave empty to keep old description: ");
            string newDescription = Console.ReadLine();

            

            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            if (!String.IsNullOrWhiteSpace(newTitle))
            {
                currentTask.TaskTitle = newTitle;
            }

            if (!String.IsNullOrWhiteSpace(newDescription))
            {
                currentTask.TaskDescription = newDescription;
            }


            ProgramManager.UpdateAllLists();
        }

        public static void DeleteSpecificTask(int listId)
        {
            List currentList = ProgramManager.Lists[listId - 1];

            List<Task> tasks = currentList.Tasks;

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
            List currentList = ProgramManager.Lists[listId - 1];

            List<Task> tasks = currentList.Tasks;

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
            List currentList = ProgramManager.Lists[listId - 1];

            List<Task> tasks = currentList.Tasks;

            Task currentTask = tasks[taskId - 1];

            var listExists = false;
            var taskExists = false;

            var archiveListId = 0;
            List<Task> currentArchiveTasks;

            foreach (List list in ProgramManager.ArchiveLists)
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

                foreach (Task task in currentArchiveTasks)
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
                List newList = new()
                {
                    ListTitle = currentList.ListTitle,
                    Tasks = new List<Task>()
                };

                ProgramManager.ArchiveLists.Add(newList);

                int archiveNewListId = ProgramManager.ArchiveLists.IndexOf(newList);

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
            List currentList = ProgramManager.Lists[listId - 1];

            List<Task> tasks = currentList.Tasks;

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