namespace ProjectManagementApp
{
    public interface ITrackable
    {
        //Method to start task by a user
        bool StartTask(string username);
        //Method to complete task by a user
        bool CompleteTask(string username);
        //Method to update status
        bool UpdateStatus(TaskStatus newStatus);
    }
}