using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICivilityDal
    {
        Task<List<Civility>> GetCivilities();
    }
}
