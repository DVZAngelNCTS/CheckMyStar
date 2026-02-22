using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll
{
    public partial class FolderBus : IFolderBusForService
    {
        public Task<FolderResponse> GetNextIdentifier(CancellationToken ct)
        {
            return this.GetIdentifier(ct);
        }

        public Task<FoldersResponse> GetFolders(FolderGetRequest request, CancellationToken ct)
        {
            return this.GetFolders(request.AccommodationName, request.OwnerLastName, request.InspectorLastName, request.FolderStatus, ct);
        }

        public Task<FolderResponse> GetFolder(FolderGetRequest request, CancellationToken ct)
        {
            return this.GetFolder(request.FolderIdentifier!.Value, ct);
        }

        public Task<FoldersResponse> GetFoldersByInspector(FolderGetRequest request, CancellationToken ct)
        {
            return this.GetFoldersByInspector(request.InspectorIdentifier!.Value, ct);
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

        public Task<BaseResponse> DeleteFolder(FolderDeleteRequest request, CancellationToken ct)
        {
            var user = userContext.CurrentUser.Identifier;

            return this.DeleteFolder(request.Identifier, user, ct);
        }
    }
}
