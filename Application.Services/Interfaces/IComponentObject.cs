using Domain.Entities.Types;

namespace Application.Services.Interfaces;

public interface IComponentObject
{
    IComponentObject SetFlexBox(bool flexbox = true);
    IComponentObject SetFlexBoxDirection(FlexDirectionType direction = FlexDirectionType.Row);
    IComponentObject AddBasicButton();
    IComponentObject AddBasicTextBox();
    IComponentObject AddBasicTextArea();
}