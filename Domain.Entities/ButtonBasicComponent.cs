using Domain.Entities.Interfaces;
using Domain.Entities.Types;

namespace Domain.Entities;

public class ButtonBasicComponent : BasicComponent, IBasicComponentReference
{
    public string Text { get; set; } = string.Empty;
    public bool Disable { get; set; }
    public string ClassInfo { get; set; } = string.Empty;
    public string IdInfo { get; set; } = string.Empty;
    public TriggerType TriggerType { get; set; }
    public FunctionType FunctionType { get; set; }
}
