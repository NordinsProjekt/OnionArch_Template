using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class Component : IEntity, IVisible
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Visible { get; set; }

    public ICollection<BasicComponent> BasicComponents { get; set; } = new List<BasicComponent>();
}