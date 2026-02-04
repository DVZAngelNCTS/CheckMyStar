using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Enumerations;
using System.Data;

namespace CheckMyStar.Bll
{
    public partial class UserBus(IUserDal userDal, ICivilityDal civilityDal, IRoleDal roleDal, IAddressDal addressDal, ICountryDal countryDal, IMapper mapper) : IUserBus
    {
        public async Task<UserResponse> GetUser(string login, string password, CancellationToken ct)
        {
            UserResponse userResult = new UserResponse();

            var user = await userDal.GetUser(login, password, ct);

            if (user.IsSuccess && user.User != null)
            {
                userResult.User = await LoadUser(user.User, ct);
            }

            userResult.IsSuccess = user.IsSuccess;
            userResult.Message = user.Message;

            return userResult;
        }

        public async Task<UsersResponse> GetUsers(string lastName, string firstName, string society, string email, string phone, string address, int? role, CancellationToken ct)
        {
            var users = await userDal.GetUsers(lastName, firstName, society, email, phone, address, role, ct);

            return mapper.Map<UsersResponse>(users);
        }

        public async Task<BaseResponse> AddUser(UserModel userModel, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var user = await userDal.GetUser(userModel.Identifier, ct);

            if (user.IsSuccess)
            {
                if (user.User == null)
                {
                    user = await userDal.GetUser(userModel.LastName, userModel.FirstName, userModel.Society, userModel.Email, userModel.Phone, ct);

                    if (user.IsSuccess)
                    {
                        if (user.User == null)
                        {
                            var userEntity = mapper.Map<User>(userModel);

                            result = mapper.Map<BaseResponse>(await userDal.AddUser(userEntity, ct));
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = "L'utilisateur existe déjà";
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = user.Message;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'utilisateur existe déjà";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = user.Message;
            }

            return result;
        }

        public async Task<BaseResponse> UpdateUser(UserModel userModel, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var user = await userDal.GetUser(userModel.Identifier, ct);

            if (user.IsSuccess)
            {
                var userEntity = mapper.Map<User>(userModel);

                return mapper.Map<BaseResponse>(await userDal.UpdateUser(userEntity, ct));
            }

            result.IsSuccess = false;
            result.Message = "L'utilisateur' n'existe pas, impossible de le modifié";

            return result;
        }

        public async Task<BaseResponse> DeleteUser(int identifier, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var user = await userDal.GetUser(identifier, ct);

            if (user.IsSuccess)
            {
                var userEntity = mapper.Map<User>(user.User);

                return mapper.Map<BaseResponse>(await userDal.DeleteUser(userEntity, ct));
            }

            result.IsSuccess = false;
            result.Message = "L'utilisateur n'existe pas, impossible de le supprimer";

            return result;
        }

        private async Task<UserModel?> LoadUser(User user, CancellationToken ct)
        {
            var userModel = mapper.Map<UserModel>(user);

            if (userModel != null)
            {
                var civilites = await civilityDal.GetCivilities(ct);

                if (civilites.IsSuccess && civilites.Civilities != null)
                {
                    var civility = civilites.Civilities.First(c => c.Identifier == user.CivilityIdentifier);

                    userModel.Civility = civility.Identifier.ToEnum<EnumCivility>();
                }

                var roles = await roleDal.GetRoles(null, ct);

                if (roles.IsSuccess && roles.Roles != null)
                {
                    var role = roles.Roles.First(r => r.Identifier == user.RoleIdentifier);

                    userModel.Role = role.Identifier.ToEnum<EnumRole>();
                }

                if (user.AddressIdentifier != null)
                {
                    var address = await addressDal.GetAddress(user.AddressIdentifier.Value, ct);

                    if (address.IsSuccess && address.Address != null)
                    {
                        userModel.Address = mapper.Map<AddressModel>(address.Address);

                        var countries = await countryDal.GetCountries(ct);

                        if (countries.IsSuccess && countries.Countries != null)
                        {
                            var country = countries.Countries.First(c => c.Identifier == address.Address.CountryIdentifier);

                            userModel.Address.Country = mapper.Map<CountryModel>(country);
                        }
                    }
                }
            }

            return userModel;
        }
    }
}
