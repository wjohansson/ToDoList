using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ToDoListApp
{
    public class ToDoList
    {
        public string ListTitle { get; set; }
        public List<Task> Tasks { get; set; }

        public static void CreateList()
        {

            var lists = Overview.GetAllLists();

            Console.WriteLine("Enter new list name: ");
            var listTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(listTitle))
            {
                Console.WriteLine("List title can not be empty, try again");
                CreateList();
            }

            var list = new ToDoList()
            {
                ListTitle = listTitle,
                Tasks = new List<Task>()
            };

            lists.Add(list);

            Overview.UpdateAllLists(lists);
        }

        public static void EditList()
        {
            var lists = Overview.GetAllLists();

            if (lists.Count == 0)
            {
                Console.WriteLine("No list to edit, create one first");

                Thread.Sleep(2000);

                Overview.AllLists();
            }

            Console.WriteLine("Enter the id of the list you want to edit:");
            var listId = Convert.ToInt32(Console.ReadLine());

            if (listId > lists.Count || listId <= 0)
            {
                Console.WriteLine("ID Doesnt exist, try again");

                EditList();

                return;
            }

            Console.Clear();
            Console.WriteLine($"Old title: {lists[listId - 1].ListTitle}");
            Console.WriteLine("Enter the new title:");
            var newTitle = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("The title can not be empty. Try again");

                EditList();

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

            lists[listId - 1].ListTitle = newTitle;

            Overview.UpdateAllLists(lists);
        }

        public static void DeleteList()
        {
            var lists = Overview.GetAllLists();

            if (lists.Count == 0)
            {
                Console.WriteLine("No list to remove, returning");

                Thread.Sleep(2000);

                Overview.AllLists();
            }

            Console.WriteLine("Enter the id of the list you want to delete:");
            var listId = Convert.ToInt32(Console.ReadLine());

            if (listId > lists.Count || listId <= 0)
            {
                Console.WriteLine("ID Doesnt exist, try again");
                DeleteList();
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

            lists.RemoveAt(listId - 1);

            Overview.UpdateAllLists(lists);

        }

        public static void ViewList()
        {
            var lists = Overview.GetAllLists();

            if (lists.Count == 0)
            {
                Console.WriteLine("No list to view, create one first. Returning");

                Thread.Sleep(2000);

                Overview.AllLists();
            }

            Console.WriteLine("Enter the id of the list you want to view:");
            var listId = Convert.ToInt32(Console.ReadLine());

            if (listId > lists.Count || listId <= 0)
            {
                Console.WriteLine("ID Doesnt exist, try again");

                ViewList();

                return;
            }

            Task.ViewTasksInList(listId);
        }
    }
}
