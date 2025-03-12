using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<List<Message>> GetMessagesByUsers(Guid idSender, Guid idReceiver, int pageNumber, int pageSize);
    
    
}