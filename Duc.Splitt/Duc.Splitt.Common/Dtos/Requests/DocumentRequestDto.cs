namespace Duc.Splitt.Common.Dtos.Requests
{
    public class DocumentRequestDto
    {
        public required Guid DocumentConfigurationId { get; set; }
        public required string MineType { get; set; }
        public required string FileName { get; set; }
        public required byte[] AttachmentByte { get; set; }
    }
}
