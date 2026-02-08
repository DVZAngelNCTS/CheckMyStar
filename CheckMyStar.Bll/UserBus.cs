using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Enumerations;
using CheckMyStar.Security;

namespace CheckMyStar.Bll
{
    public partial class UserBus(IUserContextService userContext, IActivityBus activityBus, IUserDal userDal, ICivilityDal civilityDal, IRoleDal roleDal, IAddressDal addressDal, ICountryDal countryDal, IActivityDal activityDal, IMapper mapper) : IUserBus
    {
        public async Task<UserResponse> GetIdentifier(CancellationToken ct)
        {
            var user = await userDal.GetNextIdentifier(ct);

            return mapper.Map<UserResponse>(user);
        }

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
            UsersResponse usersResponse = new UsersResponse();

            usersResponse.Users = new List<UserModel>();

            var users = await userDal.GetUsers(lastName, firstName, society, email, phone, address, role, ct);

            if (users.IsSuccess && users.Users != null)
            {
                foreach (User user in users.Users)
                {
                    var userModel = mapper.Map<UserModel>(user);

                    if (user.AddressIdentifier != null)
                    {
                        var addressReponse = await addressDal.GetAddress(user.AddressIdentifier.Value, ct);

                        if (addressReponse.IsSuccess && addressReponse.Address != null)
                        {
                            userModel.Address = mapper.Map<AddressModel>(addressReponse.Address);

                            var countryResponse = await countryDal.GetCountry(addressReponse.Address.CountryIdentifier, ct);

                            userModel.Address.Country = mapper.Map<CountryModel>(countryResponse.Country);
                        }
                    }

                    usersResponse.Users.Add(userModel);
                }
            }

            return usersResponse;
        }

        public async Task<BaseResponse> AddUser(UserModel userModel, int currentUser, CancellationToken ct)
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
                            userModel.Password = SecurityHelper.HashPassword(userModel.Password);

                            var dateTime = DateTime.Now;

                            userModel.Address.CreatedDate = dateTime;
                            userModel.Address.UpdatedDate = dateTime;

                            var addressEntity = mapper.Map<Address>(userModel.Address);

                            var addressResult = mapper.Map<BaseResponse>(await addressDal.AddAddress(addressEntity, ct));

                            if (addressResult.IsSuccess)
                            {
                                userModel.CreatedDate = dateTime;
                                userModel.UpdatedDate = dateTime;

                                var userEntity = mapper.Map<User>(userModel);

                                var userResult = mapper.Map<BaseResponse>(await userDal.AddUser(userEntity, ct));

                                if (userResult.IsSuccess)
                                {
                                    result.IsSuccess = true;
                                    result.Message = userResult.Message + "<br>" + addressResult.Message;
                                }
                                else
                                {
                                    result.IsSuccess = false;
                                    result.Message = userResult.Message + "<br>" + addressResult.Message;
                                }

                                await activityBus.AddActivity(userResult.Message, dateTime, currentUser, userResult.IsSuccess, ct);
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message = addressResult.Message;
                            }

                            await activityBus.AddActivity(addressResult.Message, dateTime, currentUser, addressResult.IsSuccess, ct);

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

        public async Task<BaseResponse> UpdateUser(UserModel userModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var user = await userDal.GetUser(userModel.Identifier, ct);

            if (user.IsSuccess)
            {
                if (user.User != null)
                {
                    if (userModel.Password == string.Empty)
                    {
                        userModel.Password = user.User.Password;
                    }

                    var dateTime = DateTime.Now;

                    userModel.CreatedDate = dateTime;
                    userModel.UpdatedDate = dateTime;

                    var userEntity = mapper.Map<User>(userModel);

                    var userResult = await userDal.UpdateUser(userEntity, ct);

                    if (userResult.IsSuccess)
                    {
                        userModel.Address.CreatedDate = dateTime;
                        userModel.Address.UpdatedDate = dateTime;

                        var addressEntity = mapper.Map<Address>(userModel.Address);

                        var addressResult = await addressDal.UpdateAddress(addressEntity, ct);

                        if (addressResult.IsSuccess)
                        {
                            result.IsSuccess = true;
                            result.Message = userResult.Message + "<br>" + addressResult.Message;
                        }
                        else
                        {
                            result.IsSuccess = false;
                            result.Message = userResult.Message + "<br>" + addressResult.Message;
                        }

                        await activityBus.AddActivity(addressResult.Message, dateTime, currentUser, addressResult.IsSuccess, ct);
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = userResult.Message;
                    }

                    await activityBus.AddActivity(userResult.Message, dateTime, currentUser, userResult.IsSuccess, ct);

                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'utilisateur' n'existe pas, impossible de le modifié";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = user.Message;
            }

            return result;
        }

        public async Task<BaseResponse> DeleteUser(int identifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var user = await userDal.GetUser(identifier, ct);

            if (user.IsSuccess)
            {
                if (user.User != null)
                {
                    var userEntity = mapper.Map<User>(user.User);

                    var userResult = await userDal.DeleteUser(userEntity, ct);

                    if (userResult.IsSuccess)
                    {
                        result.IsSuccess = true;
                        result.Message = userResult.Message;

                        if (user.User.AddressIdentifier != null)
                        {
                            var addressResult = await addressDal.GetAddress(user.User.AddressIdentifier.Value, ct);

                            if (addressResult.IsSuccess)
                            {
                                if (addressResult.Address != null)
                                {
                                    var baseResult = await addressDal.DeleteAddress(addressResult.Address, ct);

                                    if (baseResult.IsSuccess)
                                    {
                                        result.Message += "<br>" + addressResult.Message;
                                    }
                                    else
                                    {
                                        result.IsSuccess = false;
                                        result.Message += "<br>" + baseResult.Message;
                                    }

                                    await activityBus.AddActivity(baseResult.Message, DateTime.Now, currentUser, baseResult.IsSuccess, ct);
                                }
                                else
                                {
                                    result.IsSuccess = false;
                                    result.Message += "<br>" + addressResult.Message;
                                }
                            }
                            else
                            {
                                result.IsSuccess = false;
                                result.Message += "<br>" + addressResult.Message;
                            }
                        }
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.Message = userResult.Message;
                    }

                    await activityBus.AddActivity(userResult.Message, DateTime.Now, currentUser, userResult.IsSuccess, ct);
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "L'utilisateur n'existe pas, impossible de le supprimer";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = user.Message;
            }

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
