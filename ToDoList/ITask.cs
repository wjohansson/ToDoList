namespace ToDoListApp
{
    public interface ITask
    {
        string TaskTitle { get; set; }
        string TaskDescription { get; set; }
        bool Completed { get; set; }
        int Priority { get; set; }
        string DateCreated { get; set; }
        List<SubTask> SubTasks { get; set; }

        void CreateTask();

        void EditTask();

        void DeleteTask();

        void ViewTask();

        void ArchiveTask();

        void ToggleComplete();


    }
}