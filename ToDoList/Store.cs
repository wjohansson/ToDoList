namespace ToDoListApp
{
    public class Store
    {
        private static string _currentDir = Environment.CurrentDirectory;
        private static string _path = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\ToDoList.json";

        public static void CreateSaveFile()
        {
            if(!File.Exists(_path))
            {
                File.Create(_path);
            }
        }

        public static string GetPath()
        {
            return _path;
        }
    }
}