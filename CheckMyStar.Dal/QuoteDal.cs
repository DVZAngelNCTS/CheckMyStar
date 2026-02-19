using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class QuoteDal(ICheckMyStarDbContext dbContext) : IQuoteDal
    {
        public async Task<QuoteResult> GetQuote(int quoteIdentifier, CancellationToken ct)
        {
            QuoteResult quoteResult = new QuoteResult();

            try
            {
                var quote = await (from q in dbContext.Quotes.AsNoTracking()
                                  where q.Identifier == quoteIdentifier
                                  select q).FirstOrDefaultAsync(ct);

                quoteResult.IsSuccess = true;
                quoteResult.Quote = quote;
            }
            catch (Exception ex)
            {
                quoteResult.IsSuccess = false;
                quoteResult.Message = ex.Message;
            }

            return quoteResult;
        }
    }
}
