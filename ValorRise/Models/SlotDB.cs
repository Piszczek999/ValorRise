using Riptide;
using ValorRise.Enums;

namespace ValorRise.Models;

public class SlotDB : IMessageSerializable
{
    public byte SlotId { get; set; }
    public ItemName ItemName { get; set; }
    public byte Quantity { get; set; }
    public byte Level { get; set; }

    public void Serialize(Message message) => message
            .AddByte(SlotId)
            .AddByte((byte)ItemName)
            .AddByte(Quantity)
            .AddByte(Level);

    public void Deserialize(Message message)
    {
        SlotId = message.GetByte();
        ItemName = (ItemName)message.GetByte();
        Quantity = message.GetByte();
        Level = message.GetByte();
    }
}