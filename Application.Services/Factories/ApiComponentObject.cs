using Application.Services.Factories.Interfaces;
using Domain.Entities.Types;

namespace Application.Services.Factories;

public class ApiComponentObject : IApiComponentObject
{
    public IComponentObject SetFlexBox(bool flexbox = false)
    {
        throw new NotImplementedException();
    }

    public IComponentObject SetFlexBoxDirection(FlexDirectionType direction = FlexDirectionType.None)
    {
        throw new NotImplementedException();
    }

    public IComponentObject AddBasicButton()
    {
        throw new NotImplementedException();
    }

    public IComponentObject AddBasicTextBox()
    {
        throw new NotImplementedException();
    }

    public IComponentObject AddBasicTextArea()
    {
        throw new NotImplementedException();
    }
}
