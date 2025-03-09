using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public class MessageRepository : IMessageRepository
{
    public Task<Message> PostMessage(Message message)
    {
        throw new NotImplementedException();

    }

    public Task<List<Message>> GetMessage(Guid idSender, Guid idReceiver)
    {
        throw new NotImplementedException();
    }
}