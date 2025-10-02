using Domain.Entities.Interfaces;
using Domain.Entities.Types;

namespace Domain.Entities;

public abstract class BasicComponent : IEntity, IVisible
{
    public Guid Id { get; set; }
    public Guid ApiComponentId { get; set; }
    public Guid ComponentId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Visible { get; set; }

    public BasicComponentType Type { get; set; }
}