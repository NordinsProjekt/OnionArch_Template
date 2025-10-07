using Application.Services.Factories.Interfaces;
using Domain.Entities;

namespace Application.Services.Factories;

public class ComponentFactory : IComponentFactory
{
    private Guid UserId { get; set; }
    private ComponentObject ComponentObject { get; } = new();

    public Component Build()
    {
        return new Component
        {
            UserId = UserId,
            BasicComponents = ComponentObject.BasicComponents.ToList(),
            FlexBox = ComponentObject.FlexBox,
            FlexDirection = ComponentObject.FlexDirectionType
        };
    }

    public IComponentObject Begin(Guid userId)
    {
        if (userId == Guid.Empty) throw new Exception("Must be a valid UserId");

        UserId = userId;

        return ComponentObject;
    }
}