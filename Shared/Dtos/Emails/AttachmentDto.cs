namespace Shared.Dtos.Emails;

public class AttachmentDto
{
    public string FileName { get; set; } = string.Empty;
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
}
