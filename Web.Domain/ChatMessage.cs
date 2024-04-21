namespace Web.Domain;

public class ChatMessage
{
    public Guid Id { get; private set; }
    public Guid GameId { get; private set; } 
    public Guid SenderUserId { get; private set; }
    public string MessageText { get; private set; }
    public DateTime Timestamp { get; private set; } 

    public ChatMessage(Guid gameId, Guid senderUserId, string messageText)
    {
        Id = Guid.NewGuid();
        GameId = gameId;
        SenderUserId = senderUserId;
        MessageText = messageText;
        Timestamp = DateTime.UtcNow;
    }
}