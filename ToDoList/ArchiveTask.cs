﻿using ToDoListApp;

namespace ToDoList
{
    public class ArchiveTask
    {
        public static void DeleteSpecificArchiveTask(int listId)
        {
            var currentList = ProgramManager.ArchiveLists[listId - 1];

            var tasks = currentList.Tasks;

            int taskId;

            try
            {
                Console.Write("Enter the id of the archived task you want to delete: ");
                taskId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                DeleteSpecificArchiveTask(listId);

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

                DeleteSpecificArchiveTask(listId);

                return;
            }

            ProgramManager.UpdateArchive();
        }

        public static void DeleteArchiveTask(int listId, int taskId)
        {
            var currentList = ProgramManager.ArchiveLists[listId - 1];

            var tasks = currentList.Tasks;

            tasks.RemoveAt(taskId - 1);

            ProgramManager.UpdateArchive();
        }

        public static void RestoreSpecificTask(int listId)
        {
            //Kolla om denna lista som innehåller tasken finns i arkiv listan, om den finns så ska den även kolla om tasken redan finns, 
            // om tasken också finns så ska man inte göra nånting mer, eftersom det inte ska finnas dubletter av någonting
            // om tasken däremot inte finns men listan finns så måste man lägga till tasken i den redan befintliga listan
            // om varken tasken eller listan finns så måste man skapa en ny lista med samma namn. Och sedan måste man då lägga
            // till den nya tasken i listan

            var currentList = ProgramManager.ArchiveLists[listId - 1];

            var tasks = currentList.Tasks;

            int taskId;

            try
            {
                Console.Write("Enter the id of the archived task you want to restore: ");
                taskId = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Id must be a number, try again.");

                RestoreSpecificTask(listId);

                return;
            }

            var currentTask = tasks[taskId - 1];

            var listExists = false;
            var taskExists = false;

            var currentListId = 0;
            List<ToDoListApp.Task> currentTasks;

            foreach (var list in ProgramManager.Lists)
            {
                if (list.ListTitle == currentList.ListTitle)
                {
                    listExists = true;
                    currentListId = ProgramManager.Lists.IndexOf(list);
                    break;
                }
            }



            if (listExists)
            {
                currentTasks = ProgramManager.Lists[currentListId].Tasks;

                foreach (var task in currentTasks)
                {
                    if (task.TaskTitle == currentTask.TaskTitle)
                    {
                        taskExists = true;
                        break;
                    }
                }

                if (taskExists)
                {
                    Console.WriteLine("Task already exists");

                    Console.Write("Do you want to remove this task? y/N: ");

                    switch (Console.ReadLine().ToUpper())
                    {
                        case "Y":
                            break;
                        default:
                            return;
                    }

                    DeleteArchiveTask(listId, taskId);
                    return;
                }

                ProgramManager.Lists[currentListId].Tasks.Add(currentTask);
            }
            else
            {
                var newList = new List()
                {
                    ListTitle = currentList.ListTitle,
                    Tasks = new List<ToDoListApp.Task>()
                };

                ProgramManager.Lists.Add(newList);

                var newListId = ProgramManager.Lists.IndexOf(newList);

                ProgramManager.Lists[newListId].Tasks.Add(currentTask);
            }

            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            ProgramManager.UpdateAllLists();

            DeleteArchiveTask(listId, taskId);

            ArchiveListOverview.ViewTasksInArchiveList(listId);
        }
    }
}
