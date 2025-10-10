using Domain.Entities.Types;

namespace Application.Services.Factories.Interfaces.AddOns;

public interface IFlexBoxObject
{
    IGenericComponent SetFlexBox(bool flexbox = true);
    IGenericComponent SetFlexBoxDirection(FlexDirectionType direction = FlexDirectionType.Row);
}