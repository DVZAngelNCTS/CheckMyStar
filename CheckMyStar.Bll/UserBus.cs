using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Dal.Abstractions;

namespace CheckMyStar.Bll
{
    public partial class UserBus(IUserDal userDal, ICivilityDal civilityDal, IRoleDal roleDal, IAddressDal addressDal, ICountryDal countryDal, IMapper mapper) : IUserBus
    {
        public async Task<UserModel?> GetUser(string login, string password)
        {
            UserModel? userModel = null;

            var user = await userDal.GetUser(login, password);

            if (user != null)
            {
                userModel = mapper.Map<UserModel?>(user);

                if (userModel != null)
                {
                    var civilites = await civilityDal.GetCivilities();

                    if (civilites != null)
                    {
                        var civility = civilites.First(c => c.Identifier == user.CivilityIdentifier);

                        userModel.Civility = mapper.Map<CivilityModel>(civility);
                    }

                    var roles = await roleDal.GetRoles();

                    if (roles != null)
                    {
                        var role = roles.First(r => r.Identifier == user.RoleIdentifier);

                        userModel.Role = mapper.Map<RoleModel>(role);
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
            }

            return userModel;
        }
    }
}
