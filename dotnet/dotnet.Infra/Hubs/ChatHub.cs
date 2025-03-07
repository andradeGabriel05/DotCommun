using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.SignalR;

namespace Infra.Hubs;

public class ChatHub : Hub
{
    public Task SendMessage(string emailTo, string message)
    {
        return Clients.User(emailTo).SendAsync("ReceiveMessage", emailTo, message);
    }
    
    public override async Task OnConnectedAsync()
    {
        var user = Context.User;
        if (user == null)
        {
            Console.WriteLine("❌ Usuário não autenticado.");
        }
        else
        {
            foreach (var claim in user.Claims)
            {
                Console.WriteLine($"🔹 Claim: {claim.Type} = {claim.Value}");
            }
        }

        await base.OnConnectedAsync();
    }
}