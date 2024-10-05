namespace ValorRise;

[AttributeUsage(AttributeTargets.Class)]
internal class MessageAttribute : Attribute
{
  public ushort MessageId { get; set; }

  public MessageAttribute(ushort messageId)
  {
    MessageId = messageId;
  }
}