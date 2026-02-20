using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IFolderService
    {
        Task<FolderResponse> GetNextIdentifier(CancellationToken ct);
        Task<FoldersResponse> GetFolders(CancellationToken ct);
        Task<BaseResponse> CreateFolder(FolderSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateFolder(FolderSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteFolder(int folderIdentifier, CancellationToken ct);
    }
}
