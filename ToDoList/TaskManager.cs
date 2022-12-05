namespace ToDoListApp
{
    public class TaskManager
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public int TaskId { get; init; }
        public bool Completed { get; set; }
        public int Priority { get; set; }
        public string DateCreated { get; set; }
        public List<SubTask> SubTasks { get; set; }

        public static void CreateTask(int listPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            Console.Write("Enter a new task: ");
            string newTitle = Console.ReadLine();

            foreach (TaskManager task in tasks)
            {
                if (task.TaskTitle == newTitle)
                {
                    Console.WriteLine("Task already exists. Try again with another name.");

                    CreateTask(listPosition);

                    return;
                }
            }

            Console.Write("Enter the task description: ");
            string newDescription = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle) || String.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("Task title or description can not be empty. Try again");

                CreateTask(listPosition);

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

                CreateTask(listPosition);

                return;
            }

            var taskId = 1;

            foreach (TaskManager task in tasks)
            {
                if (task.TaskId >= taskId)
                {
                    taskId = task.TaskId + 1;
                }
            }

            TaskManager newTask = new()
            {
                TaskTitle = newTitle,
                TaskDescription = newDescription,
                TaskId = taskId,
                Completed = false,
                Priority = newPriority,
                DateCreated = DateTime.Now.ToString("G"),
                SubTasks = new List<SubTask>()
            };

            tasks.Add(newTask);

            ProgramManager.UpdateAllLists();
        }

        public static void EditTask(int listPosition, int taskPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            TaskManager currentTask = tasks[taskPosition - 1];

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

                EditTask(listPosition, taskPosition);

                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Old title: {currentTask.TaskTitle}");
            Console.Write("Enter the new title or leave empty to keep old title: ");
            string newTitle = Console.ReadLine();

            foreach (TaskManager task in tasks)
            {
                if (task.TaskTitle == newTitle)
                {
                    Console.WriteLine("Task with the same name already exists. Try again");

                    EditTask(listPosition, taskPosition);

                    return;
                }
            }

            Console.WriteLine($"Old description: {currentTask.TaskDescription}");
            Console.Write("Enter the new description or leave empty to keep old description: ");
            string newDescription = Console.ReadLine();



            ProgramManager.AreYouSure("Are you sure you want to edit this task? y/N: ");

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

        public static void DeleteTask(int listPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            int taskPosition;

            try
            {
                Console.Write("Enter the position of the task you want to delete: ");
                taskPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                DeleteTask(listPosition);

                return;
            }

            ProgramManager.AreYouSure("Are you sure you want to delete this task? y/N: ");

            try
            {
                tasks.RemoveAt(taskPosition - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                DeleteTask(listPosition);

                return;
            }

            ProgramManager.UpdateAllLists();
        }

        public static void DeleteTask(int listPosition, int taskPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            tasks.RemoveAt(taskPosition - 1);


            ProgramManager.UpdateAllLists();
        }

        public static void ViewTask(int listPosition)
        {
            int taskPosition;

            try
            {
                Console.Write("Enter the position of the task you want to view: ");
                taskPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                ViewTask(listPosition);

                return;
            }

            try
            {
                TaskOverview.ViewIndividualTask(listPosition, taskPosition);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                ViewTask(listPosition);

                return;
            }
        }

        public static void ArchiveTask(int listPosition, int taskPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            TaskManager currentTask = tasks[taskPosition - 1];

            var listExists = false;
            var taskExists = false;

            var archiveListPosition = 0;
            List<TaskManager> currentArchiveTasks;

            foreach (ListManager list in ProgramManager.ArchiveLists)
            {
                if (list.ListTitle == currentList.ListTitle)
                {
                    listExists = true;
                    archiveListPosition = ProgramManager.ArchiveLists.IndexOf(list);
                    break;
                }
            }



            if (listExists)
            {
                currentArchiveTasks = ProgramManager.ArchiveLists[archiveListPosition].Tasks;

                foreach (TaskManager task in currentArchiveTasks)
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

                    DeleteTask(listPosition, taskPosition);
                    return;
                }

                ProgramManager.ArchiveLists[archiveListPosition].Tasks.Add(currentTask);
            }
            else
            {
                ListManager newList = new()
                {
                    ListTitle = currentList.ListTitle,
                    Tasks = new List<TaskManager>()
                };

                ProgramManager.ArchiveLists.Add(newList);

                int archiveNewListId = ProgramManager.ArchiveLists.IndexOf(newList);

                ProgramManager.ArchiveLists[archiveNewListId].Tasks.Add(currentTask);
            }

            ProgramManager.AreYouSure("Are you sure you want to archive this task? y/N: ");

            ProgramManager.UpdateArchive();

            DeleteTask(listPosition, taskPosition);

            ListOverview.ViewTasksInList(listPosition);
        }

        public static void ToggleComplete(int listPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            int taskPosition;

            try
            {
                Console.Write("Enter the position of the list: ");
                taskPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number, try again.");

                ToggleComplete(listPosition);

                return;
            }

            TaskManager currentTask;

            try
            {
                currentTask = tasks[taskPosition - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist, try again.");

                ToggleComplete(listPosition);

                return;
            }

            currentTask.Completed = !currentTask.Completed;

            ProgramManager.UpdateAllLists();
        }
    }
}