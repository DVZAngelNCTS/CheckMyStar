using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IFolderDal
    {
        Task<FolderResult> GetNextIdentifier(CancellationToken ct);
        Task<FolderResult> GetFolder(int folderIdentifier, CancellationToken ct);
        Task<FoldersResult> GetFoldersByInspector(int inspectorIdentifier, string? accommodationName, string? ownerLastName, string? inspectorLastName, int? folderStatus, CancellationToken ct);
        Task<FoldersResult> GetFolders(string? accommodationName, string? ownerLastName, string? inspectorLastName, int? folderStatus, CancellationToken ct);
        Task<BaseResult> AddFolder(Folder folder, CancellationToken ct);
        Task<BaseResult> UpdateFolder(Folder folder, CancellationToken ct);
        Task<BaseResult> DeleteFolder(Folder folder, CancellationToken ct);
    }
}
