using Application.Services.Interfaces;
using Domain.Entities;

namespace Application.Services.Factories;

public class ComponentFactory : IComponentFactory
{
    private Guid UserId { get; set; }
    private readonly ComponentObject ComponentObject = new();
    private List<BasicComponent> BasicComponents { get; set; }

    public ComponentFactory(Guid userId)
    {
        if (userId == Guid.Empty) throw new Exception("Must be a valid UserId");

        UserId = userId;
        BasicComponents = new List<BasicComponent>();
    }

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

    public IComponentObject Begin()
    {
        return ComponentObject;
    }
}