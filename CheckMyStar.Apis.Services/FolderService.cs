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

        public Task<FoldersResponse> GetFolders(CancellationToken ct)
        {
            return folderBusForService.GetFolders(ct);
        }

        public Task<BaseResponse> CreateFolder(FolderSaveRequest request, CancellationToken ct)
        {
            return folderBusForService.CreateFolder(request, ct);
        }

        public Task<BaseResponse> UpdateFolder(FolderSaveRequest request, CancellationToken ct)
        {
            return folderBusForService.UpdateFolder(request, ct);
        }

        public Task<BaseResponse> DeleteFolder(int folderIdentifier, CancellationToken ct)
        {
            return folderBusForService.DeleteFolder(folderIdentifier, ct);
        }
    }
}
