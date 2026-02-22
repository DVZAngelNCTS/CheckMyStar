using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services
{
    public class FolderService(IFolderBusForService folderBusForService) : IFolderService
    {
        public Task<FolderResponse> GetNextIdentifier(CancellationToken ct)
        {
            return folderBusForService.GetNextIdentifier(ct);
        }

        public Task<FoldersResponse> GetFolders(FolderGetRequest request, CancellationToken ct)
        {
            return folderBusForService.GetFolders(request, ct);
        }

        public Task<FolderResponse> GetFolder(FolderGetRequest request, CancellationToken ct)
        {
            return folderBusForService.GetFolder(request, ct);
        }

        public Task<FoldersResponse> GetFoldersByInspector(FolderGetRequest request, CancellationToken ct)
        {
            return folderBusForService.GetFoldersByInspector(request, ct);
        }

        public Task<BaseResponse> CreateFolder(FolderSaveRequest request, CancellationToken ct)
        {
            return folderBusForService.CreateFolder(request, ct);
        }

        public Task<BaseResponse> UpdateFolder(FolderSaveRequest request, CancellationToken ct)
        {
            return folderBusForService.UpdateFolder(request, ct);
        }

        public Task<BaseResponse> DeleteFolder(FolderDeleteRequest request, CancellationToken ct)
        {
            return folderBusForService.DeleteFolder(request, ct);
        }
    }
}
