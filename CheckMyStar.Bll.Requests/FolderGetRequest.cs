namespace CheckMyStar.Bll.Requests
{
    public class FolderGetRequest
    {
        public string? AccommodationName { get; set; }
        public string? OwnerLastName { get; set; }
        public string? InspectorLastName { get; set; }
        public int? FolderStatus { get; set; }
        public int? FolderIdentifier { get; set; }
        public int? InspectorIdentifier { get; set; }
    }
}
