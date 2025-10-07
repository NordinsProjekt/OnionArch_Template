using Domain.Entities.Interfaces;
using Domain.Entities.Types;

namespace Domain.Entities;

public class Component : IEntity, IVisible, IDisplayProperties, IFlexBox
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Visible { get; set; }

    public int Height { get; set; }
    public int Width { get; set; }
    public int ZIndex { get; set; }

    public bool FlexBox { get; set; }
    public FlexDirectionType FlexDirection { get; set; }

    public ICollection<BasicComponent> BasicComponents { get; set; } = new List<BasicComponent>();
}