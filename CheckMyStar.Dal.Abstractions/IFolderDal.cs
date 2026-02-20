using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IFolderDal
    {
        Task<FolderResult> GetNextIdentifier(CancellationToken ct);
        Task<FolderResult> GetFolder(int folderIdentifier, CancellationToken ct);
        Task<FoldersResult> GetFolders(CancellationToken ct);
        Task<BaseResult> AddFolder(Folder folder, CancellationToken ct);
        Task<BaseResult> UpdateFolder(Folder folder, CancellationToken ct);
        Task<BaseResult> DeleteFolder(Folder folder, CancellationToken ct);
    }
}
