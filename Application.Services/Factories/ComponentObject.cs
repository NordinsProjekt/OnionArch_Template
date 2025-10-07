using Application.Services.Factories.Interfaces;
using Domain.Entities;
using Domain.Entities.Types;

namespace Application.Services.Factories;

public class ComponentObject : IComponentObject
{
    internal FlexDirectionType FlexDirectionType { get; set; } = FlexDirectionType.None;
    internal bool FlexBox { get; set; }
    internal List<BasicComponent> BasicComponents { get; set; } = new();

    public IComponentObject SetFlexBox(bool flexbox = true)
    {
        FlexBox = flexbox;

        return this;
    }

    public IComponentObject SetFlexBoxDirection(FlexDirectionType direction = FlexDirectionType.Row)
    {
        FlexBox = direction != FlexDirectionType.None;
        FlexDirectionType = direction;

        return this;
    }

    public IComponentObject AddBasicButton()
    {
        BasicComponents.Add(new ButtonBasicComponent { Text = "Ny knapp" });

        return this;
    }

    public IComponentObject AddBasicTextBox()
    {
        BasicComponents.Add(new TextBasicComponent { Text = "Ny textbasic komponent" });

        return this;
    }

    public IComponentObject AddBasicTextArea()
    {
        BasicComponents.Add(new TextAreaBasicComponent { Text = "Ny textarea komponent" });

        return this;
    }
}