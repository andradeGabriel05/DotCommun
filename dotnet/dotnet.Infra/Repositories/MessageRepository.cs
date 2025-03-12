using Azure;
using Domain.Entities;
using Infra.Data;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly UserDbContext _userDbContext;
    
    public MessageRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }
    
    public async Task<List<Message>> GetMessagesByUsers(Guid idSender, Guid idReceiver, int pageNumber, int pageSize)
    {
        return await _userDbContext.Messages.Where(m => m.SenderId == idSender && m.ReceiverId == idReceiver).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }
}