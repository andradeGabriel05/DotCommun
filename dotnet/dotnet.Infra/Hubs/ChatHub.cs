using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Infra.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(string emailTo, string message)
    {
        var emailFrom = Context.User?.FindFirst(ClaimTypes.Email)?.Value;
        
        Console.WriteLine($"📩 {emailFrom} enviou uma mensagem para {emailTo}: {message}");
        
        return Clients.Users(emailFrom, emailTo).SendAsync("ReceiveMessage", emailFrom, message);
    }
    
}