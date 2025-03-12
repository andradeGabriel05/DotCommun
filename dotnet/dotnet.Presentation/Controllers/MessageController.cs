using System.Text.Json;
using Application.Interface;
using Application.Models.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    
    public MessageController(IMessageService messageService)
    { 
        _messageService = messageService;
    } 
    
    [HttpGet]
    public async Task<IActionResult> GetMessagesByUsers([FromQuery] Guid idSender, Guid idReceiver, int pageNumber = 1, int pageSize = 20)
    {
        var messages = await _messageService.GetMessagesByUsers(idSender, idReceiver, pageNumber, pageSize);
        return Ok(messages);
    }
}