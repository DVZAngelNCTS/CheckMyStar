using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class FoldersResponse : BaseResponse
    {
        public List<FolderModel> Folders { get; set; } = new List<FolderModel>();
    }
}
