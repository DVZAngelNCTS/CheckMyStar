using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Enumerations;

namespace CheckMyStar.Bll
{
    public partial class UserBus(IUserContextService userContextService, IUserDal userDal, ICivilityDal civilityDal, IRoleDal roleDal, IAddressDal addressDal, ICountryDal countryDal, IMapper mapper) : IUserBus
    {
        public async Task<UserModel?> GetUser(string login, string password)
        {
            UserModel? userModel = null;

            var user = await userDal.GetUser(login, password);

            if (user != null)
            {
                userModel = await LoadUser(user);
            }

            return userModel;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            List<UserModel> userModels = new List<UserModel>();

            var users = await userDal.GetUsers();

            foreach (var user in users)
            {
                var userModel = await LoadUser(user);

                if (userModel != null)
                {
                    userModels.Add(userModel);
                }
            }

            return userModels;
        }

        private async Task<UserModel?> LoadUser(User user)
        {
            var userModel = mapper.Map<UserModel>(user);

            if (userModel != null)
            {
                var civilites = await civilityDal.GetCivilities();

                if (civilites != null)
                {
                    var civility = civilites.First(c => c.Identifier == user.CivilityIdentifier);

                    userModel.Civility = civility.Identifier.ToEnum<EnumCivility>();
                }

                var roles = await roleDal.GetRoles();

                if (roles != null)
                {
                    var role = roles.First(r => r.Identifier == user.RoleIdentifier);

                    userModel.Role = role.Identifier.ToEnum<EnumRole>();
                }

                if (user.AddressIdentifier != null)
                {
                    var address = await addressDal.GetAddress(user.AddressIdentifier.Value);

                    if (address != null)
                    {
                        userModel.Address = mapper.Map<AddressModel>(address);

                        var countriees = await countryDal.GetCountries();

                        if (countriees != null)
                        {
                            var country = countriees.First(c => c.Identifier == address.CountryIdentifier);

                            userModel.Address.Country = mapper.Map<CountryModel>(country);
                        }
                    }
                }
            }

            return userModel;
        }
    }
}
