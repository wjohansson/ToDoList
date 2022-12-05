namespace ToDoListApp
{
    public class SubTask
    {
        public string SubTaskTitle { get; set; }
        public string SubTaskDescription { get; set; }
        public int SubTaskId { get; init; }
        public bool Completed { get; set; }

        public static void CreateSubTask(int listPosition, int taskPosition)
        {
            List<SubTask> subTasks = ProgramManager.Lists[listPosition - 1].Tasks[taskPosition - 1].SubTasks;

            Console.Write("Enter a new sub-task: ");
            string newTitle = Console.ReadLine();

            foreach (SubTask subTask in subTasks)
            {
                if (subTask.SubTaskTitle== newTitle)
                {
                    Console.WriteLine("There is already another sub-task with the same name. Try again");

                    CreateSubTask(listPosition, taskPosition);

                    return;
                }
            }

            Console.Write("Enter the sub-task description: ");
            string newDescription = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle) || String.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("Task title or description can not be empty. Try again");

                CreateSubTask(listPosition, taskPosition);

                return;
            }

            var subTaskId = 1;

            foreach (SubTask subTask in subTasks)
            {
                if (subTask.SubTaskId >= subTaskId)
                {
                    subTaskId = subTask.SubTaskId + 1;
                }
            }

            SubTask newSubTask = new()
            {
                SubTaskTitle = newTitle,
                SubTaskDescription = newDescription,
                SubTaskId = subTaskId,
                Completed = false
            };

            subTasks.Add(newSubTask);

            ProgramManager.UpdateAllLists();
        }

        public static void DeleteSubTask(int listPosition, int taskPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            TaskManager currentTask = tasks[taskPosition - 1];

            List<SubTask> subTasks = currentTask.SubTasks;

            int subTaskPosition;

            try
            {
                Console.Write("Position of the sub-task you want to delete: ");
                subTaskPosition = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number. Try again");

                DeleteSubTask(listPosition, taskPosition);

                return;
            }

            ProgramManager.AreYouSure("Are you sure you want to delete this sub-task? y/N: ");

            try
            {
                subTasks.RemoveAt(subTaskPosition - 1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist. Try again");

                DeleteSubTask(listPosition, taskPosition);

                return;
            }


            ProgramManager.UpdateAllLists();
        }

        public static void EditSubTask(int listPosition, int taskPosition)
        {
            ListManager currentList = ProgramManager.Lists[listPosition - 1];

            List<TaskManager> tasks = currentList.Tasks;

            TaskManager currentTask = tasks[taskPosition - 1];

            List<SubTask> subTasks = currentTask.SubTasks;

            int subTaskPosition;

            try
            {
                Console.Write("Position of the sub-task you want to edit: ");
                subTaskPosition = Convert.ToInt32(Console.ReadLine());                
            }
            catch (FormatException)
            {
                Console.WriteLine("Position must be a number. Try again");

                EditSubTask(listPosition, taskPosition);

                return;
            }

            SubTask currentSubTask;
            
            try
            {
                currentSubTask = subTasks[subTaskPosition - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Position does not exist. Try again");

                EditSubTask(listPosition, taskPosition);

                return;
            }

            Console.WriteLine();
            Console.WriteLine($"Old title: {currentSubTask.SubTaskTitle}");
            Console.Write("Enter the new title or leave empty to keep old title: ");
            string newTitle = Console.ReadLine();

            foreach (SubTask subTask in subTasks)
            {
                if (subTask.SubTaskTitle == newTitle)
                {
                    Console.WriteLine("There is already another sub-task with the same name. Try again");

                    CreateSubTask(listPosition, taskPosition);

                    return;
                }
            }

            Console.WriteLine($"Old description: {currentSubTask.SubTaskDescription}");
            Console.Write("Enter the new description or leave empty to keep old description: ");
            string newDescription = Console.ReadLine();

            ProgramManager.AreYouSure("Are you sure you want to edit this sub-task? y/N: ");

            if (!String.IsNullOrWhiteSpace(newTitle))
            {
                currentSubTask.SubTaskTitle = newTitle;
            }

            if (!String.IsNullOrWhiteSpace(newDescription))
            {
                currentSubTask.SubTaskDescription = newDescription;
            }

            ProgramManager.UpdateAllLists();
        }

        public static void ToggleSubTaskComplete()
        {

        }
    }
}