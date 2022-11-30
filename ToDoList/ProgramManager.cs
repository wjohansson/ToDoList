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
                using (var fs = File.Create(_path)) { }

                File.WriteAllText(_path, "[]");
            }

            if (!File.Exists(_archivePath) || String.IsNullOrEmpty(File.ReadAllText(_archivePath)))
            {
                using (var fs = File.Create(_archivePath)) { }

                File.WriteAllText(_archivePath, "[]");
            }
        }

        public static string GetPath() => _path;

        public static string GetArchivePath() => _archivePath;

        public static List<List> GetAllLists()
        {
            var jsonData = File.ReadAllText(_path);

            var lists = JsonSerializer.Deserialize<List<List>>(jsonData);

            return lists;
        }

        public static void UpdateAllLists()
        {

            var jsonData = JsonSerializer.Serialize(Lists);

            File.WriteAllText(_path, jsonData);
        }

        public static List<List> GetArchive()
        {

            var jsonData = File.ReadAllText(_archivePath);

            var lists = JsonSerializer.Deserialize<List<List>>(jsonData);

            return lists;
        }

        public static void UpdateArchive()
        {
            var jsonData = JsonSerializer.Serialize(ArchiveLists);

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
    }
}