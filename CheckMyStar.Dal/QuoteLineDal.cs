using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class QuoteLineDal(ICheckMyStarDbContext dbContext) : IQuoteLineDal
    {
        public async Task<QuoteLineResult> GetNextIdentifier(CancellationToken ct)
        {
            QuoteLineResult quoteLineResult = new QuoteLineResult();

            try
            {
                var existingIdentifiers = await (from ql in dbContext.QuoteLines.AsNoTracking()
                                                 orderby ql.Identifier
                                                 select ql.Identifier).ToListAsync(ct);

                int nextIdentifier;

                if (existingIdentifiers.Count == 0)
                {
                    nextIdentifier = 1;
                }
                else
                {
                    nextIdentifier = 1;

                    foreach (var id in existingIdentifiers)
                    {
                        if (id == nextIdentifier)
                        {
                            nextIdentifier++;
                        }
                        else if (id > nextIdentifier)
                        {
                            break;
                        }
                    }
                }

                quoteLineResult.IsSuccess = true;
                quoteLineResult.QuoteLine = new QuoteLine { Identifier = nextIdentifier };
                quoteLineResult.Message = "Identifiant récupéré avec succès";
            }
            catch (Exception ex)
            {
                quoteLineResult.IsSuccess = false;
                quoteLineResult.Message = ex.Message;
            }

            return quoteLineResult;
        }

        public async Task<QuoteLinesResult> GetQuoteLinesByQuote(int quoteIdentifier, CancellationToken ct)
        {
            QuoteLinesResult quoteLinesResult = new QuoteLinesResult();

            try
            {
                var quoteLines = await (from ql in dbContext.QuoteLines.AsNoTracking()
                                       where ql.QuoteIdentifier == quoteIdentifier
                                       select ql).ToListAsync(ct);

                quoteLinesResult.IsSuccess = true;
                quoteLinesResult.QuoteLines = quoteLines;
                quoteLinesResult.Message = "Lignes de devis récupérées avec succès";
            }
            catch (Exception ex)
            {
                quoteLinesResult.IsSuccess = false;
                quoteLinesResult.Message = ex.Message;
            }

            return quoteLinesResult;
        }

        public async Task<BaseResult> AddQuoteLines(List<QuoteLine> quoteLines, CancellationToken ct)
        {
            BaseResult result = new BaseResult();

            try
            {
                await dbContext.AddRangeAsync(quoteLines, ct);
                await dbContext.SaveChangesAsync(ct);

                result.IsSuccess = true;
                result.Message = "Lignes de devis ajoutées avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResult> DeleteQuoteLinesByQuote(int quoteIdentifier, CancellationToken ct)
        {
            BaseResult result = new BaseResult();

            try
            {
                var quoteLines = await (from ql in dbContext.QuoteLines
                                       where ql.QuoteIdentifier == quoteIdentifier
                                       select ql).ToListAsync(ct);

                if (quoteLines.Any())
                {
                    await dbContext.RemoveRangeAsync(quoteLines, ct);
                    await dbContext.SaveChangesAsync(ct);
                }

                result.IsSuccess = true;
                result.Message = "Lignes de devis supprimées avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
