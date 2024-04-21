namespace Web.Domain;

public class ChatMessage
{
    public Guid GameSessionId { get; private set; } 
    public string Sender { get; private set; }
    public string MessageText { get; private set; }
    public DateTime Timestamp { get; private set; } 

    public ChatMessage(Guid gameSessionId, string sender, string messageText)
    {
        GameSessionId = gameSessionId;
        Sender = sender;
        MessageText = messageText;
        Timestamp = DateTime.UtcNow;
    }
}