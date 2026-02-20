using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Responses
{
    public class FolderResponse : BaseResponse
    {
        public FolderModel? Folder { get; set; }
    }
}
