using Domain.Entities.Types;

namespace Domain.Entities.Interfaces;

public interface IFlexBox
{
    public bool FlexBox { get; set; }
    public FlexDirectionType FlexDirection { get; set; }
}