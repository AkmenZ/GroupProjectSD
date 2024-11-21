public class Task
{
    // props
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? AssignedTo { get; set; } // nullable prop
    public string Status { get; set; } = "New"; // initial status

    // constructor
    public Task(int id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    // method
    public override string ToString()
    {
        return $"Task Id: {Id}, Title: {Title}, Status: {Status}";
    }
}
