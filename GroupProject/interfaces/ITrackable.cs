public interface ITrackable
{
    string Status { get; set; }
    // implements method to update status
    void UpdateStatus(string newStatus);
}