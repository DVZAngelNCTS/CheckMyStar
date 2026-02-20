using CheckMyStar.Dal.Results;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IFolderStatusDal
    {
        Task<FolderStatusResult> GetFolderStatus(int folderStatusIdentifier, CancellationToken ct);
    }
}
