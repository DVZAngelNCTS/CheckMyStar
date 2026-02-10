using System.Collections.Generic;
using System.Threading.Tasks;
using CheckMyStar.Bll.Models;

namespace CheckMyStar.Bll.Criteres
{
    public interface ICriteresService
    {
        Task<IEnumerable<StarCriteriaDto>> GetStarCriteriaAsync();
        Task<IEnumerable<StarCriteriaDetailDto>> GetStarCriteriaDetailsAsync();
    }
}
