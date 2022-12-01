using System.Text.Json;

namespace ToDoListApp
{
    public class ProgramManager
    {
        private static string _currentDir = Environment.CurrentDirectory;
        private static string _path = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoList.json";
        private static string _archivePath = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoListArchive.json";

        public static List<List> Lists { get; set; } = GetAllLists();
        public static List<List> ArchiveLists { get; set; } = GetArchive();

        public static void CreateFiles()
        {
            if (!File.Exists(_path) || String.IsNullOrEmpty(File.ReadAllText(_path)))
            {
                using (FileStream fs = File.Create(_path)) { }

                File.WriteAllText(_path, "[]");
            }

            if (!File.Exists(_archivePath) || String.IsNullOrEmpty(File.ReadAllText(_archivePath)))
            {
                using (FileStream fs = File.Create(_archivePath)) { }

                File.WriteAllText(_archivePath, "[]");
            }
        }

        public static string GetPath() => _path;

        public static string GetArchivePath() => _archivePath;

        public static List<List> GetAllLists()
        {
            string jsonData = File.ReadAllText(_path);

            List<List> lists = JsonSerializer.Deserialize<List<List>>(jsonData);

            return lists;
        }

        public static void UpdateAllLists()
        {

            string jsonData = JsonSerializer.Serialize(Lists);

            File.WriteAllText(_path, jsonData);
        }

        public static List<List> GetArchive()
        {

            string jsonData = File.ReadAllText(_archivePath);

            List<List> lists = JsonSerializer.Deserialize<List<List>>(jsonData);

            return lists;
        }

        public static void UpdateArchive()
        {
            string jsonData = JsonSerializer.Serialize(ArchiveLists);

            File.WriteAllText(_archivePath, jsonData);
        }

        public static void QuitProgram()
        {
            Console.Write("Are you sure? Y/n: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "N":
                    break;
                default:
                    Environment.Exit(-1);

                    break;
            }
        }

        public static void ClearAllLists()
        {
            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            Lists.Clear();
            UpdateAllLists();
        }

        public static void ClearAllArchiveLists()
        {
            Console.Write("Are you sure? y/N: ");

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }

            ArchiveLists.Clear();
            UpdateArchive();
        }
    }
}