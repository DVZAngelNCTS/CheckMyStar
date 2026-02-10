using System.Collections.Generic;
using System.Threading.Tasks;
using CheckMyStar.Dal.Models;

namespace CheckMyStar.Dal.Abstractions
{
    public interface ICriteresRepository
    {
        Task<IEnumerable<StarCriteriaRawRow>> GetStarCriteriaRawAsync();
        Task<IEnumerable<StarCriteriaDetailRawRow>> GetStarCriteriaDetailsRawAsync();
    }
}
