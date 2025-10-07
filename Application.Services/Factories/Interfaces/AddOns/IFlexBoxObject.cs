using Domain.Entities.Types;

namespace Application.Services.Factories.Interfaces.AddOns;

public interface IFlexBoxObject
{
    IComponentObject SetFlexBox(bool flexbox = true);
    IComponentObject SetFlexBoxDirection(FlexDirectionType direction = FlexDirectionType.Row);
}