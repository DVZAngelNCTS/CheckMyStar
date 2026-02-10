using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Models;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal.Criteres
{
    public class CriteresRepository : ICriteresRepository
    {
        private readonly CheckMyStarDbContext _context;

        public CriteresRepository(CheckMyStarDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<StarCriteriaDetailRawRow>> GetStarCriteriaDetailsRawAsync()
        {
            var query = from s in _context.StarLevels
                        join slc in _context.StarLevelCriteria on s.StarLevelId equals slc.StarLevelId
                        join c in _context.Criteria on slc.CriterionId equals c.CriterionId
                        join ct in _context.CriterionTypes on slc.TypeCode equals ct.TypeCode
                        orderby s.StarLevelId, c.CriterionId
                        select new StarCriteriaDetailRawRow
                        {
                            Rating = Convert.ToInt32(s.StarLevelId),
                            StarLabel = s.Label ?? string.Empty,
                            CriterionId = c.CriterionId,
                            Description = c.Description ?? string.Empty,
                            BasePoints = c.BasePoints,
                            TypeCode = slc.TypeCode ?? string.Empty,
                            TypeLabel = ct.Label ?? string.Empty
                        };

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<StarCriteriaRawRow>> GetStarCriteriaRawAsync()
        {
            var query = from s in _context.StarLevels
                        join slc in _context.StarLevelCriteria on s.StarLevelId equals slc.StarLevelId
                        join ct in _context.CriterionTypes on slc.TypeCode equals ct.TypeCode
                        group new { s, ct } by new { Rating = s.StarLevelId, StarLabel = s.Label, TypeCode = ct.TypeCode, TypeLabel = ct.Label } into g
                        orderby g.Key.Rating, g.Key.TypeCode
                        select new StarCriteriaRawRow
                        {
                            Rating = Convert.ToInt32(g.Key.Rating),
                            StarLabel = g.Key.StarLabel ?? string.Empty,
                            TypeCode = g.Key.TypeCode ?? string.Empty,
                            TypeLabel = g.Key.TypeLabel ?? string.Empty,
                            Count = g.Count()
                        };

            var list = await query.ToListAsync().ConfigureAwait(false);
            return list;
        }
    }
}
