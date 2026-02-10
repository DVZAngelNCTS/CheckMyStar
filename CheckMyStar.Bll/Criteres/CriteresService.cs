using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckMyStar.Bll.Models;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Models;

namespace CheckMyStar.Bll.Criteres
{
    public class CriteresService : ICriteresService
    {
        private readonly ICriteresRepository _repo;

        public CriteresService(ICriteresRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IEnumerable<StarCriteriaDetailDto>> GetStarCriteriaDetailsAsync()
        {
            var raw = await _repo.GetStarCriteriaDetailsRawAsync().ConfigureAwait(false);

            var grouped = raw
                .GroupBy(r => new { r.Rating, r.StarLabel })
                .OrderBy(g => g.Key.Rating)
                .Select(g => new StarCriteriaDetailDto
                {
                    Rating = g.Key.Rating,
                    StarLabel = g.Key.StarLabel,
                    Criteria = g.Select(r => new StarCriterionDto
                    {
                        CriterionId = r.CriterionId,
                        Description = r.Description,
                        BasePoints = r.BasePoints,
                        TypeCode = r.TypeCode,
                        TypeLabel = r.TypeLabel
                    }).OrderBy(c => c.CriterionId).ToList()
                });

            return grouped.ToList();
        }

        public async Task<IEnumerable<StarCriteriaDto>> GetStarCriteriaAsync()
        {
            var raw = await _repo.GetStarCriteriaRawAsync().ConfigureAwait(false);

            var grouped = raw
                .GroupBy(r => new { r.Rating, r.StarLabel })
                .OrderBy(g => g.Key.Rating)
                .Select(g => new StarCriteriaDto
                {
                    Rating = g.Key.Rating,
                    Label = g.Key.StarLabel,
                    Description = $"Critères {g.Key.Rating} étoile(s)",
                    LastUpdate = DateTime.UtcNow,
                    Statuses = g.Select(r => new StarStatusDto
                    {
                        Code = r.TypeCode,
                        Label = r.TypeLabel,
                        Count = r.Count
                    }).OrderBy(s => s.Code).ToList()
                });

            return grouped.ToList();
        }
    }
}
