using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class TextBasicComponent : BasicComponent, IBasicComponentReference, IBasicComponentPlaceholder
{
    public string Text { get; set; } = string.Empty;
    public string ClassInfo { get; set; } = string.Empty;
    public string IdInfo { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    public bool Disable { get; set; }
}