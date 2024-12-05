using Riptide;
using ValorRise.Enums;

namespace ValorRise.Models;

public class ItemDB : IMessageSerializable
{
    public byte SlotId { get; set; }
    public ItemName ItemName { get; set; }
    public byte Amount { get; set; }
    public byte? Level { get; set; }

    public void Serialize(Message message)
    {
        message
            .AddByte(SlotId)
            .AddByte((byte)ItemName)
            .AddByte(Amount);

        message.AddBool(Level.HasValue); // Flag
        if (Level.HasValue) message.AddByte(Level.Value);
    }

    public void Deserialize(Message message)
    {
        SlotId = message.GetByte();
        ItemName = (ItemName)message.GetByte();
        Amount = message.GetByte();

        if (message.GetBool()) Level = message.GetByte();
    }
}
