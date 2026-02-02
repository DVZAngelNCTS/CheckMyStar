using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Enumerations;

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
