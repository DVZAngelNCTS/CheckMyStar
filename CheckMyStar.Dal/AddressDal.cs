using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class AddressDal(ICheckMyStarDbContext dbContext) : IAddressDal
    {
        public async Task<AddressResult> GetNextIdentifier(CancellationToken ct)
        {
            AddressResult addressResult = new AddressResult();

            try
            {
                var existingIdentifiers = await (from r in dbContext.Addresses.AsNoTracking()
                                                 orderby r.Identifier
                                                 select r.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    addressResult.IsSuccess = true;
                    addressResult.Address = new Address { Identifier = 1 };
                    addressResult.Message = "Identifiant récupéré avec succès";
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

                    addressResult.IsSuccess = true;
                    addressResult.Address = new Address { Identifier = nextIdentifier };
                    addressResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                addressResult.IsSuccess = false;
                addressResult.Message = ex.Message;
            }

            return addressResult;
        }

        public async Task<AddressResult> GetAddress(int addressIdentifier, CancellationToken ct)
        {
            AddressResult addressResult = new AddressResult();

            try
            {
                var address = await (from a in dbContext.Addresses.AsNoTracking()
                                     where a.Identifier == addressIdentifier
                                     select a).FirstOrDefaultAsync(ct);

                addressResult.IsSuccess = true;
                addressResult.Address = address;
            }
            catch (Exception ex)
            {
                addressResult.IsSuccess = false;
                addressResult.Message = ex.Message;
            }

            return addressResult;
        }

        public async Task<BaseResult> AddAddress(Address address, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(address, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = "Adresse ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = "Impossible d'ajouter l'adresse";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible d'ajouter l'adresse : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> UpdateAddress(Address address, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.UpdateAsync(address, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = "Adresse modifié avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = "Impossible de modifier l'adresse";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible de modifier l'adresse : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> DeleteAddress(Address address, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(address, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = "Adresse supprimé avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = "Impossible de supprimer l'adresse";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible de supprimer l'adresse : " + ex.Message;
            }

            return baseResult;
        }
    }
}
