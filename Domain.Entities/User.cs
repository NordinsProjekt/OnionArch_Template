using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
    public ICollection<Component> Component { get; set; } = new List<Component>();
}