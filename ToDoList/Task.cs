using System.Collections.Generic;

namespace ToDoListApp
{
    public class Task
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public bool Completed { get; set; }

        public static void CreateTask(int listId)
        {
            var lists = Overview.GetAllLists();

            var currentList = lists[listId - 1];

            var tasks = currentList.Tasks;


            Console.WriteLine("Enter a new task:");
            var newTitle = Console.ReadLine();

            Console.WriteLine("Enter the task description");
            var newDescription = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle) || String.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("Task title or description can not be empty. Try again");
                CreateTask(listId);
            }

            var task = new Task()
            {
                TaskTitle = newTitle,
                TaskDescription = newDescription,
                Completed = false
            };

            tasks.Add(task);

            Overview.UpdateAllLists(lists);
        }

        public static void EditTask(int listId)
        {
            var lists = Overview.GetAllLists();

            var currentList = lists[listId - 1];

            var tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                Console.WriteLine("No task to edit, create one first");

                Thread.Sleep(2000);

                ViewTasksInList(listId);
            }

            Console.WriteLine("Enter the id of the list you want to edit:");
            var taskId = Convert.ToInt32(Console.ReadLine());

            if (taskId > tasks.Count || taskId <= 0)
            {
                Console.WriteLine("ID Doesnt exist, try again");

                EditTask(listId);

                return;
            }

            var currentTask = tasks[listId - 1];

            Console.Clear();
            Console.WriteLine($"Old title: {currentTask.TaskTitle}");
            Console.WriteLine("Enter the new title:");
            var newTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("The title can not be empty. Try again");

                EditTask(listId);

                return;
            }

            Console.WriteLine($"Old description: {currentTask.TaskDescription}");
            Console.WriteLine("Enter the new description:");
            var newDescription = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newDescription))
            {
                Console.WriteLine("The description can not be empty. Try again");

                EditTask(listId);

                return;
            }

            Console.WriteLine("Are you sure? y/N");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            tasks[taskId - 1].TaskTitle = newTitle;
            tasks[taskId - 1].TaskDescription = newDescription;

            Overview.UpdateAllLists(lists);
        }

        public static void DeleteTask(int listId)
        {
            var lists = Overview.GetAllLists();

            var currentList = lists[listId - 1];

            var tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                Console.WriteLine("No task to delete, create one first");

                Thread.Sleep(2000);

                ViewTasksInList(listId);
            }

            Console.WriteLine("Enter the id of the list you want to delete:");
            var taskId = Convert.ToInt32(Console.ReadLine());

            if (taskId > tasks.Count || taskId <= 0)
            {
                Console.WriteLine("ID Doesnt exist, try again");

                EditTask(listId);

                return;
            }


            Console.WriteLine("Are you sure? y/N");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            tasks.RemoveAt(taskId - 1);

            Overview.UpdateAllLists(lists);
        }

        public static void ViewTask(int listId)
        {

        }

        public static void ViewTasksInList(int listId)
        {
            var lists = Overview.GetAllLists();

            var currentList = lists[listId - 1];

            Console.Clear();

            Console.WriteLine($"List Title: {currentList.ListTitle}");
            Console.WriteLine();

            var tasks = currentList.Tasks;

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks in this list. You need to add one");

                CreateTask(listId);

                ViewTasksInList(listId);

                return;
            }


            Overview.UpdateAllLists(lists);

            foreach (var task in tasks)
            {
                Console.WriteLine($"-{tasks.IndexOf(task) + 1}- {task.TaskTitle}");
                Console.WriteLine($"        {task.TaskDescription}");
                Console.WriteLine();
            }

            TaskOption(listId);

            ViewTasksInList(listId);
        }

        public static void TaskOption(int listId)
        {
            Console.WriteLine("To view a task: 'V'");
            Console.WriteLine("To delete a task: 'D'");
            Console.WriteLine("To edit a task: 'E'");
            Console.WriteLine("To create a new task: 'N'");
            Console.WriteLine("To go back to overview: 'B'");
            Console.WriteLine("To quit the program: 'Q'");

            switch (Console.ReadLine().ToUpper())
            {
                case "V":
                    ViewTask(listId);

                    return;
                case "D":
                    DeleteTask(listId);

                    break;
                case "E":
                    EditTask(listId);

                    break;
                case "N":
                    CreateTask(listId);

                    break;
                case "B":
                    Overview.AllLists();

                    break;
                case "Q":
                    Overview.QuitProgram();

                    break;
            }
        }
    }
}