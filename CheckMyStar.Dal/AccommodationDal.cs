using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AccommodationDal(ICheckMyStarDbContext dbContext) : IAccommodationDal
    {
        public async Task<AccommodationResult> GetNextIdentifier(CancellationToken ct)
        {
            AccommodationResult accommodationResult = new AccommodationResult();

            try
            {
                var existingIdentifiers = await (from a in dbContext.Accommodations.AsNoTracking()
                                                 orderby a.Identifier
                                                 select a.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    accommodationResult.IsSuccess = true;
                    accommodationResult.Accommodation = new Accommodation { Identifier = 1 };
                    accommodationResult.Message = "Identifiant récupéré avec succès";
                }
                else
                {
                    int nextIdentifier = 1;

                    foreach (var id in existingIdentifiers)
                    {
                        if (id == nextIdentifier)
                        {
                            nextIdentifier++;
                        }
                        else if (id > nextIdentifier)
                        {
                            break;
                        }
                    }

                    accommodationResult.IsSuccess = true;
                    accommodationResult.Accommodation = new Accommodation { Identifier = nextIdentifier };
                    accommodationResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                accommodationResult.IsSuccess = false;
                accommodationResult.Message = ex.Message;
            }

            return accommodationResult;
        }

        public async Task<AccommodationResult> GetAccommodation(int accommodationIdentifier, CancellationToken ct)
        {
            AccommodationResult accommodationResult = new AccommodationResult();

            try
            {
                var accommodation = await (from a in dbContext.Accommodations.AsNoTracking()
                                          where a.Identifier == accommodationIdentifier
                                          select a).FirstOrDefaultAsync(ct);

                accommodationResult.IsSuccess = true;
                accommodationResult.Accommodation = accommodation;
            }
            catch (Exception ex)
            {
                accommodationResult.IsSuccess = false;
                accommodationResult.Message = ex.Message;
            }

            return accommodationResult;
        }

        public async Task<AccommodationsResult> GetAccommodations(CancellationToken ct)
        {
            AccommodationsResult accommodationsResult = new AccommodationsResult();

            try
            {
                var accommodations = await (from a in dbContext.Accommodations.AsNoTracking()
                                           where a.IsActive == true
                                           select a).ToListAsync(ct);

                accommodationsResult.IsSuccess = true;
                accommodationsResult.Accommodations = accommodations;
                accommodationsResult.Message = $"{accommodations.Count} hébergement(s) récupéré(s) avec succès";
            }
            catch (Exception ex)
            {
                accommodationsResult.IsSuccess = false;
                accommodationsResult.Message = ex.Message;
            }

            return accommodationsResult;
        }

        public async Task<BaseResult> AddAccommodation(Accommodation accommodation, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(accommodation, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Hébergement {accommodation.AccommodationName} ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible d'ajouter l'hébergement {accommodation.AccommodationName}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible d'ajouter l'hébergement {accommodation.AccommodationName} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> UpdateAccommodation(Accommodation accommodation, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.UpdateAsync(accommodation, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Hébergement {accommodation.AccommodationName} modifié avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de modifier l'hébergement {accommodation.AccommodationName}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de modifier l'hébergement {accommodation.AccommodationName} : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> DeleteAccommodation(Accommodation accommodation, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(accommodation, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = $"Hébergement {accommodation.AccommodationName} supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = $"Impossible de supprimer l'hébergement {accommodation.AccommodationName}";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = $"Impossible de supprimer l'hébergement {accommodation.AccommodationName} : " + ex.Message;
            }

            return baseResult;
        }
    }
}
