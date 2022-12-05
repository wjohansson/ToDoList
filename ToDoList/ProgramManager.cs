using System.Text.Json;

namespace ToDoListApp
{
    public class ProgramManager
    {
        private static string _currentDir = Environment.CurrentDirectory;
        private static string _path = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoList.json";
        private static string _archivePath = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoListArchive.json";
        private static string _historyPath = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoListHistory.json";

        public static List<ListManager> Lists { get; set; }
        public static List<ListManager> ArchiveLists { get; set; }
        public static List<int> RecentList { get; set; }

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

            if (!File.Exists(_historyPath) || String.IsNullOrEmpty(File.ReadAllText(_archivePath)))
            {
                using (FileStream fs = File.Create(_historyPath)) { }

                File.WriteAllText(_historyPath, "[]");
            }
        }

        public static string GetPath() => _path;

        public static string GetArchivePath() => _archivePath;

        public static string GetRecentPath() => _historyPath;

        public static List<ListManager> GetAllLists()
        {
            string jsonData = File.ReadAllText(_path);

            List<ListManager> lists = JsonSerializer.Deserialize<List<ListManager>>(jsonData);

            return lists;
        }

        public static void UpdateAllLists()
        {

            string jsonData = JsonSerializer.Serialize(Lists);

            File.WriteAllText(_path, jsonData);
        }

        public static List<ListManager> GetArchive()
        {

            string jsonData = File.ReadAllText(_archivePath);

            List<ListManager> lists = JsonSerializer.Deserialize<List<ListManager>>(jsonData);

            return lists;
        }

        public static void UpdateArchive()
        {
            string jsonData = JsonSerializer.Serialize(ArchiveLists);

            File.WriteAllText(_archivePath, jsonData);
        }

        public static List<int> GetRecent()
        {
            string jsonData = File.ReadAllText(_historyPath);

            List<int> lists = JsonSerializer.Deserialize<List<int>>(jsonData);

            return lists;
        }

        public static void UpdateRecent()
        {
            string jsonData = JsonSerializer.Serialize(RecentList);

            File.WriteAllText(_historyPath, jsonData);
        }

        public static void QuitProgram()
        {
            Console.Write("Are you sure you want to quit the program? Y/n: ");

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
            AreYouSure("Are you sure you want to REMOVE all the lists and tasks? This can not be undone. y/N: ");

            Lists.Clear();
            UpdateAllLists();
        }

        public static void ClearAllArchiveLists()
        {
            AreYouSure("Are you sure you want to REMOVE all archive lists and tasks? This can not be undone. y/N: ");

            ArchiveLists.Clear();
            UpdateArchive();
        }

        public static void AreYouSure(string message)
        {
            Console.Write(message);

            switch (Console.ReadLine().ToUpper())
            {
                case "Y":
                    break;
                default:
                    return;
            }
        }

    }
}