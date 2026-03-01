using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Requests
{
    public class FolderSaveRequest
    {
        public required FolderModel Folder { get; set; }
    }
}
