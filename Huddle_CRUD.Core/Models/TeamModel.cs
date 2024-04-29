using System.ComponentModel.DataAnnotations;

namespace Huddle_CRUD.Core.Models
{
    public class TeamModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
