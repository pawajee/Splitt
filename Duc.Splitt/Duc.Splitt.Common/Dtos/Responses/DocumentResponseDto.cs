namespace Duc.Splitt.Common.Dtos.Responses
{
    public class DocumentResponseDto
    {
        public string? DocumentConfigurationName { get; set; }
        public Guid? DocumentLibraryId { get; set; }
        public string? URL { get; set; }
        public byte[]? AttachmentByte { get; set; }
    }
}
