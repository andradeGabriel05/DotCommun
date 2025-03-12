using Application.Interface;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    public async Task<List<Message>> GetMessagesByUsers(Guid idSender, Guid idReceiver, int pageNumber, int pageSize)
    {
        return await  _messageRepository.GetMessagesByUsers(idSender, idReceiver, pageNumber, pageSize);
    }
}