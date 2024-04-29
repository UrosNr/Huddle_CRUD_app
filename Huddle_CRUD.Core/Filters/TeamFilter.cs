namespace Huddle_CRUD.Core.Filters
{
    public class TeamFilter
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int? Page {  get; set; } 
        public int? PageSize { get; set; }
        public string SortBy { get; set; } = "asc";
    }
}
