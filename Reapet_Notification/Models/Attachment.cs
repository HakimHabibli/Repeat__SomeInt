namespace Reapet_Notification.Models;

public class Attachment
{
	public string FileName { get; set; } = string.Empty;
	public byte[] FileContent { get; set; } = Array.Empty<byte>();
}