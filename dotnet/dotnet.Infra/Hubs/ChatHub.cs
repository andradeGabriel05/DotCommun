using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using Infra.Data;
using Microsoft.AspNetCore.SignalR;

namespace Infra.Hubs;

public class ChatHub : Hub
{
    private readonly UserDbContext _userDbContext;
    

    public ChatHub(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task SendMessage(Guid idEmailTo, string emailTo, string message)
    {
        var emailFrom = Context.User?.FindFirst(ClaimTypes.Email)?.Value;

        var idEmailFrom = Context.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        
        Console.WriteLine("Passou pelo id e email");
        Console.WriteLine($"Id: {idEmailTo}");
        Console.WriteLine($"Email: {emailTo}");
        Console.WriteLine($"Message: {message}");
        Console.WriteLine($"From: {emailFrom}");
        Console.WriteLine($"Id From: {idEmailFrom}");

        if (Guid.TryParse(idEmailFrom, out Guid parsedIdEmailFrom))
        {
            Console.WriteLine("Passou pelo guid");
            var messageChat = new Message(message, DateTime.UtcNow, parsedIdEmailFrom, idEmailTo);
        
            _userDbContext.Messages.Add(messageChat);
            await _userDbContext.SaveChangesAsync();
            Console.WriteLine("Passou pelo db");
        }
        await Clients.Users(emailFrom, emailTo).SendAsync("ReceiveMessage", emailFrom, message);
    }

}