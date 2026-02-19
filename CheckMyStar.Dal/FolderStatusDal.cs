using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class FolderStatusDal(ICheckMyStarDbContext dbContext) : IFolderStatusDal
    {
        public async Task<FolderStatusResult> GetFolderStatus(int folderStatusIdentifier, CancellationToken ct)
        {
            FolderStatusResult folderStatusResult = new FolderStatusResult();

            try
            {
                var folderStatus = await (from fs in dbContext.FolderStatuses.AsNoTracking()
                                         where fs.Identifier == folderStatusIdentifier
                                         select fs).FirstOrDefaultAsync(ct);

                folderStatusResult.IsSuccess = true;
                folderStatusResult.FolderStatus = folderStatus;
            }
            catch (Exception ex)
            {
                folderStatusResult.IsSuccess = false;
                folderStatusResult.Message = ex.Message;
            }

            return folderStatusResult;
        }
    }
}
