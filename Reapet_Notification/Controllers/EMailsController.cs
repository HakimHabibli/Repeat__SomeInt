using Microsoft.AspNetCore.Mvc;
using Reapet_Notification.Services;
using Shared.Dtos.Emails;

namespace Reapet_Notification.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EMailsController : ControllerBase
{
    private readonly IMailService _mailService;

    public EMailsController(IMailService mailService)
    {
        _mailService = mailService;
    }
    [HttpPost]
    public async Task<IActionResult> SendMail(EmailDtoBody emailBody)
    {
        try
        {
            await _mailService.SendEmailAsync(emailBody);
            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
}
