using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class FolderBus(
        IUserContextService userContext,
        IActivityBus activityBus,
        IFolderDal folderDal,
        IFolderStatusDal folderStatusDal,
        IAccommodationDal accommodationDal,
        IAccommodationTypeDal accommodationTypeDal,
        IUserDal userDal,
        IQuoteDal quoteDal,
        IInvoiceDal invoiceDal,
        IAppointmentDal appointmentDal,
        IAddressDal addressDal,
        IMapper mapper) : IFolderBus
    {
        public async Task<FolderResponse> GetIdentifier(CancellationToken ct)
        {
            var folder = await folderDal.GetNextIdentifier(ct);

            return mapper.Map<FolderResponse>(folder);
        }

        public async Task<FoldersResponse> GetFolders(CancellationToken ct)
        {
            FoldersResponse foldersResponse = new FoldersResponse();

            var folders = await folderDal.GetFolders(ct);

            if (folders.IsSuccess && folders.Folders != null)
            {
                foreach (Folder folder in folders.Folders)
                {
                    var folderModel = mapper.Map<FolderModel>(folder);

                    var accommodationTypeResponse = await accommodationTypeDal.GetAccommodationType(folder.AccommodationTypeIdentifier, ct);
                    if (accommodationTypeResponse.IsSuccess && accommodationTypeResponse.AccommodationType != null)
                    {
                        folderModel.AccommodationType = mapper.Map<AccommodationTypeModel>(accommodationTypeResponse.AccommodationType);
                    }

                    var accommodationResponse = await accommodationDal.GetAccommodation(folder.AccommodationIdentifier, ct);
                    if (accommodationResponse.IsSuccess && accommodationResponse.Accommodation != null)
                    {
                        folderModel.Accommodation = mapper.Map<AccommodationModel>(accommodationResponse.Accommodation);

                        var addressResponse = await addressDal.GetAddress(accommodationResponse.Accommodation.AddressIdentifier, ct);
                        if (addressResponse.IsSuccess && addressResponse.Address != null)
                        {
                            folderModel.Accommodation.Address = mapper.Map<AddressModel>(addressResponse.Address);
                        }
                    }

                    var ownerUserResponse = await userDal.GetUser(folder.OwnerUserIdentifier, ct);
                    if (ownerUserResponse.IsSuccess && ownerUserResponse.User != null)
                    {
                        folderModel.OwnerUser = mapper.Map<UserModel>(ownerUserResponse.User);
                    }

                    var inspectorUserResponse = await userDal.GetUser(folder.InspectorUserIdentifier, ct);
                    if (inspectorUserResponse.IsSuccess && inspectorUserResponse.User != null)
                    {
                        folderModel.InspectorUser = mapper.Map<UserModel>(inspectorUserResponse.User);
                    }

                    var folderStatusResponse = await folderStatusDal.GetFolderStatus(folder.FolderStatusIdentifier, ct);
                    if (folderStatusResponse.IsSuccess && folderStatusResponse.FolderStatus != null)
                    {
                        folderModel.FolderStatus = mapper.Map<FolderStatusModel>(folderStatusResponse.FolderStatus);
                    }

                    if (folder.QuoteIdentifier.HasValue)
                    {
                        var quoteResponse = await quoteDal.GetQuote(folder.QuoteIdentifier.Value, ct);
                        if (quoteResponse.IsSuccess && quoteResponse.Quote != null)
                        {
                            folderModel.Quote = mapper.Map<QuoteModel>(quoteResponse.Quote);
                        }
                    }

                    if (folder.InvoiceIdentifier.HasValue)
                    {
                        var invoiceResponse = await invoiceDal.GetInvoice(folder.InvoiceIdentifier.Value, ct);
                        if (invoiceResponse.IsSuccess && invoiceResponse.Invoice != null)
                        {
                            folderModel.Invoice = mapper.Map<InvoiceModel>(invoiceResponse.Invoice);
                        }
                    }

                    if (folder.AppointmentIdentifier.HasValue)
                    {
                        var appointmentResponse = await appointmentDal.GetAppointment(folder.AppointmentIdentifier.Value, ct);
                        if (appointmentResponse.IsSuccess && appointmentResponse.Appointment != null)
                        {
                            folderModel.Appointment = mapper.Map<AppointmentModel>(appointmentResponse.Appointment);
                        }
                    }

                    foldersResponse.Folders.Add(folderModel);
                }

                foldersResponse.IsSuccess = true;
                foldersResponse.Message = folders.Message;
            }
            else
            {
                foldersResponse.IsSuccess = false;
                foldersResponse.Message = folders.Message;
            }

            return foldersResponse;
        }

        public async Task<BaseResponse> AddFolder(FolderCreateModel folderCreateModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var folder = await folderDal.GetFolder(folderCreateModel.Identifier, ct);

            if (folder.IsSuccess)
            {
                if (folder.Folder == null)
                {
                    var accommodationTypeResponse = await accommodationTypeDal.GetAccommodationType(folderCreateModel.AccommodationTypeIdentifier, ct);
                    if (!accommodationTypeResponse.IsSuccess || accommodationTypeResponse.AccommodationType == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "Le type d'hébergement spécifié n'existe pas";
                        return result;
                    }

                    var accommodationResponse = await accommodationDal.GetAccommodation(folderCreateModel.AccommodationIdentifier, ct);
                    if (!accommodationResponse.IsSuccess || accommodationResponse.Accommodation == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "L'hébergement spécifié n'existe pas";
                        return result;
                    }

                    var ownerUserResponse = await userDal.GetUser(folderCreateModel.OwnerUserIdentifier, ct);
                    if (!ownerUserResponse.IsSuccess || ownerUserResponse.User == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "L'utilisateur propriétaire spécifié n'existe pas";
                        return result;
                    }

                    var inspectorUserResponse = await userDal.GetUser(folderCreateModel.InspectorUserIdentifier, ct);
                    if (!inspectorUserResponse.IsSuccess || inspectorUserResponse.User == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "L'utilisateur inspecteur spécifié n'existe pas";
                        return result;
                    }

                    var folderStatusResponse = await folderStatusDal.GetFolderStatus(folderCreateModel.FolderStatusIdentifier, ct);
                    if (!folderStatusResponse.IsSuccess || folderStatusResponse.FolderStatus == null)
                    {
                        result.IsSuccess = false;
                        result.Message = "Le statut du dossier spécifié n'existe pas";
                        return result;
                    }

                    if (folderCreateModel.QuoteIdentifier.HasValue)
                    {
                        var quoteResponse = await quoteDal.GetQuote(folderCreateModel.QuoteIdentifier.Value, ct);
                        if (!quoteResponse.IsSuccess || quoteResponse.Quote == null)
                        {
                            result.IsSuccess = false;
                            result.Message = "Le devis spécifié n'existe pas";
                            return result;
                        }
                    }

                    if (folderCreateModel.InvoiceIdentifier.HasValue)
                    {
                        var invoiceResponse = await invoiceDal.GetInvoice(folderCreateModel.InvoiceIdentifier.Value, ct);
                        if (!invoiceResponse.IsSuccess || invoiceResponse.Invoice == null)
                        {
                            result.IsSuccess = false;
                            result.Message = "La facture spécifiée n'existe pas";
                            return result;
                        }
                    }

                    if (folderCreateModel.AppointmentIdentifier.HasValue)
                    {
                        var appointmentResponse = await appointmentDal.GetAppointment(folderCreateModel.AppointmentIdentifier.Value, ct);
                        if (!appointmentResponse.IsSuccess || appointmentResponse.Appointment == null)
                        {
                            result.IsSuccess = false;
                            result.Message = "Le rendez-vous spécifié n'existe pas";
                            return result;
                        }
                    }

                    var dateTime = DateTime.Now;

                    var folderEntity = new Folder
                    {
                        Identifier = folderCreateModel.Identifier,
                        AccommodationTypeIdentifier = folderCreateModel.AccommodationTypeIdentifier,
                        AccommodationIdentifier = folderCreateModel.AccommodationIdentifier,
                        OwnerUserIdentifier = folderCreateModel.OwnerUserIdentifier,
                        InspectorUserIdentifier = folderCreateModel.InspectorUserIdentifier,
                        FolderStatusIdentifier = folderCreateModel.FolderStatusIdentifier,
                        QuoteIdentifier = folderCreateModel.QuoteIdentifier,
                        InvoiceIdentifier = folderCreateModel.InvoiceIdentifier,
                        AppointmentIdentifier = folderCreateModel.AppointmentIdentifier,
                        CreatedDate = dateTime,
                        UpdatedDate = dateTime
                    };

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

        public async Task<BaseResponse> DeleteFolder(int folderIdentifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var folder = await folderDal.GetFolder(folderIdentifier, ct);

            if (folder.IsSuccess)
            {
                if (folder.Folder != null)
                {
                    var folderEntity = folder.Folder;

                    var folderResult = await folderDal.DeleteFolder(folderEntity, ct);

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

                    var dateTime = DateTime.Now;
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
                result.Message = folder.Message;
            }

            return result;
        }
    }
}
