using Domain.Entities.Interfaces;

namespace Domain.Entities;

public class TextAreaBasicComponent : BasicComponent, IBasicComponentReference, IBasicComponentPlaceholder,
    IBasicComponentRowCol
{
    public string Text { get; set; } = string.Empty;
    public int MaxCol { get; set; }
    public int MaxRow { get; set; }
    public string ClassInfo { get; set; } = string.Empty;
    public string IdInfo { get; set; } = string.Empty;
    public string Placeholder { get; set; } = string.Empty;
    public bool Disable { get; set; }
}