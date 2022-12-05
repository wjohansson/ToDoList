namespace ToDoListApp
{
    public class HistoryManager
    {
        public static void ViewListFromHistory(int listId)
        {
            int listPosition = -1;

            foreach (ListManager list in ProgramManager.Lists)
            {
                if (listId == list.ListId)
                {
                    listPosition = ProgramManager.Lists.IndexOf(list) + 1;
                }
            }

            ListManager.ViewList(listPosition);
        }

        public static void DeleteIdFromHistory(int listId)
        {
            ProgramManager.RecentList.RemoveAll(id => id == listId);

            ProgramManager.UpdateRecent();
        }
    }
}