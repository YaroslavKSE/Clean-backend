namespace Web.Domain;

public class ChatMessage
{
    public Guid Id { get; private set; }
    public Guid GameId { get; private set; } // The game session this message belongs to.
    public Guid SenderUserId { get; private set; } // The user who sent the message.
    public string MessageText { get; private set; }
    public DateTime Timestamp { get; private set; } // The time the message was sent.

    public ChatMessage(Guid gameId, Guid senderUserId, string messageText)
    {
        Id = Guid.NewGuid();
        GameId = gameId;
        SenderUserId = senderUserId;
        MessageText = messageText;
        Timestamp = DateTime.UtcNow;
    }

    // You could add methods to validate or modify the message if necessary.
}