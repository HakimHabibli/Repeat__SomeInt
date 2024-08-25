namespace Reapet_Notification.Models;

public class EmailBody 
{
	public string To { get; set; } = null!;
	public string From { get; set; } = null!;
	public string? Bcc { get; set; }
	public string? Cc { get; set; }
	public string Subject { get; set; } = null!;
	public string? Body { get; set; }
	public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

}
