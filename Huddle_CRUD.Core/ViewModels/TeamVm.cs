namespace Huddle_CRUD.Core.ViewModels
{
    public class TeamVm
    {
        public Guid Id { get; set; }
        public string? InitialName { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool EditMode { get; set; } = false;
    }
}
