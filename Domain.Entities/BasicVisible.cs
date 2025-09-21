using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class BasicComponent : IEntity, IVisible
{
    public Guid Id { get; set; }
    public Guid ApiComponentId { get; set; }
    public Guid ComponentId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Visible { get; set; }
}