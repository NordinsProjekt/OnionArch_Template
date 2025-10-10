using Application.Services.Factories.Interfaces;
using Domain.Entities.Types;

namespace Application.Services.Factories;

public class ApiComponentObject : IGenericComponent
{
    public IGenericComponent SetFlexBox(bool flexbox = false)
    {
        throw new NotImplementedException();
    }

    public IGenericComponent SetFlexBoxDirection(FlexDirectionType direction = FlexDirectionType.None)
    {
        throw new NotImplementedException();
    }

    public IGenericComponent AddBasicButton()
    {
        throw new NotImplementedException();
    }

    public IGenericComponent AddBasicTextBox()
    {
        throw new NotImplementedException();
    }

    public IGenericComponent AddBasicTextArea()
    {
        throw new NotImplementedException();
    }
}
