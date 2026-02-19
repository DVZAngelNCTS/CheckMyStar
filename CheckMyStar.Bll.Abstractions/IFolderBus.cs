using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IFolderBus
    {
        Task<FolderResponse> GetIdentifier(CancellationToken ct);
        Task<FoldersResponse> GetFolders(CancellationToken ct);
        Task<BaseResponse> AddFolder(FolderCreateModel folderCreateModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteFolder(int folderIdentifier, int currentUser, CancellationToken ct);
    }
}
