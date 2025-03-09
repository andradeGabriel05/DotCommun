namespace Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    
    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; }

    public Message(string content, DateTime date, Guid  senderId, Guid  receiverId)
    {
        Id = Guid.NewGuid();
        Content = content;
        Date = date;
        SenderId = senderId;
        ReceiverId = receiverId;
    }
}
    
    