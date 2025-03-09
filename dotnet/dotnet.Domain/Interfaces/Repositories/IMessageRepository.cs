using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<Message> PostMessage(Message message);
    Task<List<Message>> GetMessage(Guid idSender, Guid idReceiver);
    
    
}