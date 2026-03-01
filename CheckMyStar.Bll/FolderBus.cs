using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class FolderBus(IUserContextService userContext, IActivityBus activityBus, IFolderDal folderDal, IFolderStatusDal folderStatusDal, IAccommodationDal accommodationDal, IAccommodationTypeDal accommodationTypeDal, IUserDal userDal, IAddressDal addressDal, IMapper mapper) : IFolderBus
    {
        public async Task<FolderResponse> GetIdentifier(CancellationToken ct)
        {
            var folder = await folderDal.GetNextIdentifier(ct);

            return mapper.Map<FolderResponse>(folder);
        }

        public async Task<FolderResponse> GetFolder(int folderIdentifier, CancellationToken ct)
        {
            FolderResponse folderResponse = new FolderResponse();

            var folder = await folderDal.GetFolder(folderIdentifier, ct);

            if (folder.IsSuccess && folder.Folder != null)
            {
                var folderModel = await LoadFolder(folder.Folder, ct);

                folderResponse.IsSuccess = true;
                folderResponse.Message = folder.Message;
                folderResponse.Folder = folderModel;
            }
            else
            {
                folderResponse.IsSuccess = false;
                folderResponse.Message = folder.Message;
            }

            return folderResponse;
        }

        public async Task<FoldersResponse> GetFoldersByInspector(int inspectorIdentifier, string? accommodationName, string? ownerLastName, string? inspectorLastName, int? folderStatus, CancellationToken ct)
        {
            FoldersResponse foldersResponse = new FoldersResponse();

            var folders = await folderDal.GetFoldersByInspector(inspectorIdentifier, accommodationName, ownerLastName, inspectorLastName, folderStatus, ct);

            if (folders.IsSuccess && folders.Folders != null)
            {
                var foldesrModel = await LoadFolders(folders.Folders, ct);

                foldersResponse.IsSuccess = true;
                foldersResponse.Message = folders.Message;
                foldersResponse.Folders = foldesrModel;
            }
            else
            {
                foldersResponse.IsSuccess = false;
                foldersResponse.Message = folders.Message;
            }

            return foldersResponse;

        }

        public async Task<FoldersResponse> GetFolders(string? accommodationName, string? ownerLastName, string? inspectorLastName, int? folderStatus, CancellationToken ct)
        {
            FoldersResponse foldersResponse = new FoldersResponse();

            var folders = await folderDal.GetFolders(accommodationName, ownerLastName, inspectorLastName, folderStatus, ct);

            if (folders.IsSuccess && folders.Folders != null)
            {
                var folderModels = await LoadFolders(folders.Folders, ct);

                foldersResponse.IsSuccess = true;
                foldersResponse.Message = folders.Message;
                foldersResponse.Folders = folderModels;
            }
            else
            {
                foldersResponse.IsSuccess = false;
                foldersResponse.Message = folders.Message;
            }

            return foldersResponse;
        }

        public async Task<BaseResponse> AddFolder(FolderModel folderModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var folder = await folderDal.GetFolder(folderModel.Identifier, ct);

            if (folder.IsSuccess)
            {
                if (folder.Folder == null)
                {
                    if (folderModel.Accommodation != null && folderModel.Owner != null && folderModel.Inspector != null && folderModel.FolderStatus != null && folderModel.Accommodation.AccommodationType != null)
                    {
                        var accommodationResult = await accommodationDal.GetAccommodation(folderModel.Accommodation.Identifier, ct);

                        if (accommodationResult.IsSuccess)
                        {
                            if (accommodationResult.Accommodation == null)
                            {
                                var accommodationTypeResponse = await accommodationTypeDal.GetAccommodationType(folderModel.Accommodation.AccommodationType.Identifier, ct);

                                if (!accommodationTypeResponse.IsSuccess || accommodationTypeResponse.AccommodationType == null)
                                {
                                    result.IsSuccess = false;
                                    result.Message = "Le type d'hébergement spécifié n'existe pas";

                                    return result;
                                }

                                var ownerUserResponse = await userDal.GetUser(folderModel.Owner.Identifier, ct);

                                if (!ownerUserResponse.IsSuccess || ownerUserResponse.User == null)
                                {
                                    result.IsSuccess = false;
                                    result.Message = "L'utilisateur propriétaire spécifié n'existe pas";

                                    return result;
                                }

                                var inspectorUserResponse = await userDal.GetUser(folderModel.Inspector.Identifier, ct);

                                if (!inspectorUserResponse.IsSuccess || inspectorUserResponse.User == null)
                                {
                                    result.IsSuccess = false;
                                    result.Message = "L'utilisateur inspecteur spécifié n'existe pas";

                                    return result;
                                }

                                var folderStatusResponse = await folderStatusDal.GetFolderStatus(folderModel.FolderStatus.Identifier, ct);

                                if (!folderStatusResponse.IsSuccess || folderStatusResponse.FolderStatus == null)
                                {
                                    result.IsSuccess = false;
                                    result.Message = "Le statut du dossier spécifié n'existe pas";

                                    return result;
                                }

                                var dateTime = DateTime.Now;

                                var addressEntity = mapper.Map<Address>(folderModel.Accommodation.Address);

                                var addressResult = await addressDal.AddAddress(addressEntity, ct);

                                if (addressResult.IsSuccess)
                                {
                                    var accommodationEntity = mapper.Map<Accommodation>(folderModel.Accommodation);

                                    var baseAccommodationResult = await accommodationDal.AddAccommodation(accommodationEntity, ct);

                                    if (baseAccommodationResult.IsSuccess)
                                    {
                                        var folderEntity = mapper.Map<Folder>(folderModel);

                                        folderEntity.AccommodationTypeIdentifier = folderModel.Accommodation.AccommodationType.Identifier;
                                        folderEntity.OwnerUserIdentifier = folderModel.Owner.Identifier;
                                        folderEntity.InspectorUserIdentifier = folderModel.Inspector.Identifier;

                                        var folderResult = await folderDal.AddFolder(folderEntity, ct);

                                        if (folderResult.IsSuccess)
                                        {
                                            result.IsSuccess = true;
                                            result.Message = folderResult.Message;
                                        }
                                        else
                                        {
                                            result.IsSuccess = false;
                                            result.Message = folderResult.Message;
                                        }

                                        await activityBus.AddActivity(folderResult.Message, dateTime, currentUser, folderResult.IsSuccess, ct);
                                    }
                                    else
                                    {
                                        result.IsSuccess = false;
                                        result.Message = baseAccommodationResult.Message;
                                    }
                                }
                                else
                                {
                                    result.IsSuccess = false;
                                    result.Message = addressResult.Message;
                                }
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message = accommodationResult.Message;
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "L'hébergement existe déjà";
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Impossible d'ajouter le dossier";
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Le dossier existe déjà";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = folder.Message;
            }

            return result;
        }

        public async Task<BaseResponse> UpdateFolder(FolderModel folderModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var folderResult = await folderDal.GetFolder(folderModel.Identifier, ct);

            if (folderResult.IsSuccess)
            {
                if (folderResult.Folder != null)
                {
                    var dateTime = DateTime.Now;

                    if (folderModel.Accommodation != null)
                    {
                        var accommodationResult = await accommodationDal.GetAccommodation(folderModel.Accommodation.Identifier, ct);

                        if (accommodationResult.IsSuccess)
                        {
                            if (accommodationResult.Accommodation != null)
                            {
                                var accommodationEntity = mapper.Map<Accommodation>(folderModel.Accommodation);

                                accommodationEntity.CreatedDate = accommodationResult.Accommodation.CreatedDate;
                                accommodationEntity.UpdatedDate = dateTime;

                                var accommodationBaseResult = await accommodationDal.UpdateAccommodation(accommodationEntity, ct);

                                if (accommodationBaseResult.IsSuccess)
                                {
                                    result.IsSuccess = true;
                                    result.Message = accommodationBaseResult.Message;

                                    if (folderModel.Accommodation.Address != null)
                                    {
                                        var addressResult = await addressDal.GetAddress(folderModel.Accommodation.Address.Identifier, ct);

                                        if (addressResult.IsSuccess)
                                        {
                                            if (addressResult.Address != null)
                                            {
                                                var addressEntity = mapper.Map<Address>(folderModel.Accommodation.Address);

                                                addressEntity.CreatedDate = addressResult.Address.CreatedDate;                                                    
                                                addressEntity.UpdatedDate = dateTime;

                                                var addressBaseResult = await addressDal.UpdateAddress(addressEntity, ct);

                                                if (addressBaseResult.IsSuccess)
                                                {
                                                    result.IsSuccess = true;
                                                    result.Message = result.Message + "<br>" + addressBaseResult.Message;
                                                }
                                                else
                                                {
                                                    result.IsSuccess = false;
                                                    result.Message = result.Message + "<br>" + addressBaseResult.Message;
                                                }

                                                await activityBus.AddActivity(addressBaseResult.Message, dateTime, currentUser, addressBaseResult.IsSuccess, ct);
                                            }
                                            else
                                            {
                                                result.IsSuccess = false;
                                                result.Message = "Impossible de trouver l'adresse";
                                            }
                                        }
                                        else
                                        {
                                            result.IsSuccess = false;
                                            result.Message = addressResult.Message;
                                        }
                                    }
                                    else
                                    {
                                        result.IsSuccess = false;
                                        result.Message = "Impossible de trouver l'adresse";
                                    }

                                    await activityBus.AddActivity(accommodationBaseResult.Message, dateTime, currentUser, accommodationBaseResult.IsSuccess, ct);
                                }
                                else
                                {
                                    result.IsSuccess = false;
                                    result.Message = accommodationBaseResult.Message;
                                }
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message = "Impossible de trouver l'hébergement";
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = accommodationResult.Message;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Impossible de trouver l'hébergement";
                    }

                    if (folderModel.Accommodation != null && folderModel.Accommodation.AccommodationType != null && folderModel.Owner != null && folderModel.Inspector != null)
                    {
                        var folderEntity = mapper.Map<Folder>(folderModel);

                        folderEntity.AccommodationTypeIdentifier = folderModel.Accommodation.AccommodationType.Identifier;
                        folderEntity.OwnerUserIdentifier = folderModel.Owner.Identifier;
                        folderEntity.InspectorUserIdentifier = folderModel.Inspector.Identifier;
                        folderEntity.CreatedDate = folderResult.Folder.CreatedDate;
                        folderEntity.UpdatedDate = dateTime;

                        var folderBaseResult = await folderDal.UpdateFolder(folderEntity, ct);

                        if (folderBaseResult.IsSuccess)
                        {
                            result.IsSuccess = true;
                            result.Message = result.Message + "<br>" + folderBaseResult.Message;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = result.Message + "<br>" + folderBaseResult.Message;
                        }

                        await activityBus.AddActivity(folderBaseResult.Message, dateTime, currentUser, folderBaseResult.IsSuccess, ct);
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = "Impossible de modifier le dossier";
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Impossible de trouver le dossier";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = folderResult.Message;
            }

            return result;
        }

        public async Task<BaseResponse> DeleteFolder(int folderIdentifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var folderResult = await folderDal.GetFolder(folderIdentifier, ct);

            if (folderResult.IsSuccess)
            {                
                if (folderResult.Folder != null)
                {
                    var dateTime = DateTime.Now;

                    var folderBaseResult = await folderDal.DeleteFolder(folderResult.Folder, ct);

                    if (folderBaseResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = folderBaseResult.Message;

                        var accommodationResult = await accommodationDal.GetAccommodation(folderResult.Folder.AccommodationIdentifier, ct);

                        if (accommodationResult.IsSuccess)
                        {
                            if (accommodationResult.Accommodation != null)
                            {
                                var accommodationBaseResult = await accommodationDal.DeleteAccommodation(accommodationResult.Accommodation, ct);

                                if (accommodationBaseResult.IsSuccess)
                                {
                                    result.IsSuccess = true;
                                    result.Message = result.Message + "<br>" + accommodationBaseResult.Message;

                                    var addressResult = await addressDal.GetAddress(accommodationResult.Accommodation.AddressIdentifier, ct);

                                    if (addressResult.IsSuccess)
                                    {
                                        if (addressResult.Address != null)
                                        {
                                            var addressBaseResult = await addressDal.DeleteAddress(addressResult.Address, ct);

                                            if (addressBaseResult.IsSuccess)
                                            {
                                                result.IsSuccess = true;
                                                result.Message = result.Message + "<br>" + addressBaseResult.Message;
                                            }
                                            else
                                            {
                                                result.IsSuccess = false;
                                                result.Message = result.Message + "<br>" + addressBaseResult.Message;
                                            }

                                            await activityBus.AddActivity(addressBaseResult.Message, dateTime, currentUser, addressBaseResult.IsSuccess, ct);
                                        }
                                        else
                                        {
                                            result.IsSuccess = false;
                                            result.Message = "Impossible de trouver l'adresse";
                                        }
                                    }
                                    else
                                    {
                                        result.IsSuccess = false;
                                        result.Message = addressResult.Message;
                                    }

                                }
                                else
                                {
                                    result.IsSuccess = false;
                                    result.Message = result.Message + "<br>" + accommodationBaseResult.Message;
                                }

                                await activityBus.AddActivity(accommodationBaseResult.Message, dateTime, currentUser, accommodationBaseResult.IsSuccess, ct);
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message = "Impossible de trouver l'hébergement";
                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = accommodationResult.Message;
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = folderBaseResult.Message;
                    }

                    await activityBus.AddActivity(folderResult.Message, dateTime, currentUser, folderResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Le dossier spécifié n'existe pas";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = folderResult.Message;
            }

            return result;
        }

        private async Task<List<FolderModel>> LoadFolders(List<Folder> folders, CancellationToken ct)
        {
            List<FolderModel> folderModels = new List<FolderModel>();

            foreach (var folder in folders)
            {
                var folderModel = mapper.Map<FolderModel>(folder);

                if (folderModel != null)
                {
                    folderModels.Add(await LoadFolder(folder, ct));
                }
            }

            return folderModels;
        }

        private async Task<FolderModel> LoadFolder(Folder folder, CancellationToken ct)
        {
            var folderModel = mapper.Map<FolderModel>(folder);

            var accommodationResponse = await accommodationDal.GetAccommodation(folder.AccommodationIdentifier, ct);

            if (accommodationResponse.IsSuccess && accommodationResponse.Accommodation != null)
            {
                folderModel.Accommodation = mapper.Map<AccommodationModel>(accommodationResponse.Accommodation);

                var addressResponse = await addressDal.GetAddress(accommodationResponse.Accommodation.AddressIdentifier, ct);

                if (addressResponse.IsSuccess && addressResponse.Address != null)
                {
                    folderModel.Accommodation.Address = mapper.Map<AddressModel>(addressResponse.Address);
                }

                var accommodationTypeResponse = await accommodationTypeDal.GetAccommodationType(folder.AccommodationTypeIdentifier, ct);

                if (accommodationTypeResponse.IsSuccess && accommodationTypeResponse.AccommodationType != null)
                {
                    folderModel.Accommodation.AccommodationType = mapper.Map<AccommodationTypeModel>(accommodationTypeResponse.AccommodationType);
                }
            }

            var ownerUserResponse = await userDal.GetUser(folder.OwnerUserIdentifier, ct);

            if (ownerUserResponse.IsSuccess && ownerUserResponse.User != null)
            {
                folderModel.Owner = mapper.Map<UserModel>(ownerUserResponse.User);
            }

            var inspectorUserResponse = await userDal.GetUser(folder.InspectorUserIdentifier, ct);

            if (inspectorUserResponse.IsSuccess && inspectorUserResponse.User != null)
            {
                folderModel.Inspector = mapper.Map<UserModel>(inspectorUserResponse.User);
            }

            var folderStatusResponse = await folderStatusDal.GetFolderStatus(folder.FolderStatusIdentifier, ct);

            if (folderStatusResponse.IsSuccess && folderStatusResponse.FolderStatus != null)
            {
                folderModel.FolderStatus = mapper.Map<FolderStatusModel>(folderStatusResponse.FolderStatus);
            }

            return folderModel;
        }
    }
}
