using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class FolderBus : IFolderBusForService
    {
        Task<FolderResponse> IFolderBusForService.GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        Task<FoldersResponse> IFolderBusForService.GetFolders(CancellationToken ct)
        {
            return this.GetFolders(ct);
        }

        public Task<BaseResponse> CreateFolder(FolderSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.AddFolder(request.Folder, user, ct);
        }

        public Task<BaseResponse> UpdateFolder(FolderSaveRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.UpdateFolder(request.Folder, user, ct);
        }

        public Task<BaseResponse> DeleteFolder(int folderIdentifier, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteFolder(folderIdentifier, user, ct);
        }
    }
}
