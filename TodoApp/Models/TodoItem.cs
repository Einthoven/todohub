namespace TodoApp.Models
{
    public enum Priority { Low, Medium, High }
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }

        public bool IsHidden { get; set; }
    }
}
