using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICountryDal
    {
        Task<List<Country>> GetCountries();
    }
}
