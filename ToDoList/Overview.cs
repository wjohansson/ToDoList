using System.IO;
using System.Text.Json;

namespace ToDoListApp
{
    public class Overview
    {
        public static void AllLists()
        {
            Console.Clear();
            Console.WriteLine("Current existing lists:");
            Console.WriteLine();

            var lists = GetAllLists();

            foreach (var list in lists)
            {
                Console.WriteLine($"-{lists.IndexOf(list) + 1}- {list.ListTitle}");
                Console.WriteLine();
            }

            ListOption();
        }

        public static void ListOption()
        {
            Console.WriteLine("To view a list: 'V'");
            Console.WriteLine("To delete a list: 'D'");
            Console.WriteLine("To edit a list: 'E'");
            Console.WriteLine("To create a new list: 'N'");
            Console.WriteLine("To quit the program: 'Q'");

            switch (Console.ReadLine().ToUpper())
            {
                case "V":
                    ToDoList.ViewList();
                    return;
                case "D":
                    ToDoList.DeleteList();
                    
                    break;
                case "E":
                    ToDoList.EditList();

                    break;
                case "N":
                    ToDoList.CreateList();

                    break;
                case "Q":
                    QuitProgram();

                    break;
            }

            Console.Clear();
            AllLists();
        }

        public static List<ToDoList> GetAllLists()
        {
            var path = Store.GetPath();

            var jsonData = File.ReadAllText(path);

            var lists = JsonSerializer.Deserialize<List<ToDoList>>(jsonData);

            return lists;
        }

        public static void UpdateAllLists(List<ToDoList> lists)
        {
            var path = Store.GetPath();

            var jsonData = JsonSerializer.Serialize(lists);

            File.WriteAllText(path, jsonData);
        }

        public static void QuitProgram()
        {
            Console.WriteLine("Are you sure? Y/n");

            switch (Console.ReadLine().ToUpper())
            {
                case "N":
                    break;
                default:
                    Environment.Exit(0);
                    
                    break;
            }
        }
    }
}