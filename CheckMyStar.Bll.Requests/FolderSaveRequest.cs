using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class FolderSaveRequest
    {
        public required FolderCreateModel Folder { get; set; }
    }
}
