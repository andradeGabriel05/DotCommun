using Domain.Entities;

namespace Application.Interface;

public interface IMessageService
{
     Task<List<Message>> GetMessagesByUsers(Guid idSender, Guid idReceiver, int pageNumber, int pageSize);

}