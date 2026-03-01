using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IFolderBus
    {
        Task<FolderResponse> GetIdentifier(CancellationToken ct);
        Task<FoldersResponse> GetFolders(string? accommodationName, string? ownerLastName, string? inspectorLastName, int? folderStatus, CancellationToken ct);
        Task<FolderResponse> GetFolder(int folderIdentifier, CancellationToken ct);
        Task<FoldersResponse> GetFoldersByInspector(int inspectorIdentifier, string? accommodationName, string? ownerLastName, string? inspectorLastName, int? folderStatus, CancellationToken ct);
        Task<BaseResponse> AddFolder(FolderModel folderModel, int currentUser, CancellationToken ct);
        Task<BaseResponse> DeleteFolder(int folderIdentifier, int currentUser, CancellationToken ct);
    }
}
