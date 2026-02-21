using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions.ForService
{
    public interface IFolderBusForService
    {
        Task<FolderResponse> GetNextIdentifier(CancellationToken ct);
        Task<FoldersResponse> GetFolders(FolderGetRequest request, CancellationToken ct);
        Task<FolderResponse> GetFolder(FolderGetRequest request, CancellationToken ct);
        Task<FoldersResponse> GetFoldersByInspector(FolderGetRequest request, CancellationToken ct);
        Task<BaseResponse> CreateFolder(FolderSaveRequest request, CancellationToken ct);
        Task<BaseResponse> UpdateFolder(FolderSaveRequest request, CancellationToken ct);
        Task<BaseResponse> DeleteFolder(FolderDeleteRequest request, CancellationToken ct);
    }
}
