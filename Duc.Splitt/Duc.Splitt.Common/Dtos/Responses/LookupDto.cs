namespace Duc.Splitt.Common.Dtos.Responses
{
    public class LookupDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
    public class LookupDocumentDto : LookupGuidDto
    {
        public string? Description { get; set; }
        public required List<string> SupportedFileTypes { get; set; }
        public required int MaxFileSizeKb { get; set; }
        public bool IsRequired { get; set; }
    }

    public class LookupWithTotalDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public int TotalRecords { get; set; }
    }

    public class LookupGuidDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
    }



}

