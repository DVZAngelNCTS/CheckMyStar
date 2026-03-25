using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class QuoteDal(ICheckMyStarDbContext dbContext) : IQuoteDal
    {
        public async Task<QuoteResult> GetNextIdentifier(CancellationToken ct)
        {
            QuoteResult quoteResult = new QuoteResult();

            try
            {
                var existingIdentifiers = await (from q in dbContext.Quotes.AsNoTracking()
                                                 orderby q.Identifier
                                                 select q.Identifier).ToListAsync(ct);

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

                quoteResult.IsSuccess = true;
                quoteResult.Quote = new Quote 
                { 
                    Identifier = nextIdentifier
                };
                quoteResult.Message = "Identifiant récupéré avec succès";
            }
            catch (Exception ex)
            {
                quoteResult.IsSuccess = false;
                quoteResult.Message = ex.Message;
            }

            return quoteResult;
        }

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

        public async Task<QuotesResult> GetQuotes(CancellationToken ct)
        {
            QuotesResult quotesResult = new QuotesResult();

            try
            {
                var quotes = await (from q in dbContext.Quotes.AsNoTracking()
                                   select q).ToListAsync(ct);

                quotesResult.IsSuccess = true;
                quotesResult.Quotes = quotes;
                quotesResult.Message = "Devis récupérés avec succès";
            }
            catch (Exception ex)
            {
                quotesResult.IsSuccess = false;
                quotesResult.Message = ex.Message;
            }

            return quotesResult;
        }

        public async Task<QuotesResult> GetQuotesByInspector(int inspectorIdentifier, CancellationToken ct)
        {
            QuotesResult quotesResult = new QuotesResult();

            try
            {
                var quotes = await (from q in dbContext.Quotes.AsNoTracking()
                                   where q.InspectorIdentifier == inspectorIdentifier
                                   select q).ToListAsync(ct);

                quotesResult.IsSuccess = true;
                quotesResult.Quotes = quotes;
                quotesResult.Message = "Devis récupérés avec succès";
            }
            catch (Exception ex)
            {
                quotesResult.IsSuccess = false;
                quotesResult.Message = ex.Message;
            }

            return quotesResult;
        }

        public async Task<QuoteResult> AddQuote(Quote quote, CancellationToken ct)
        {
            QuoteResult quoteResult = new QuoteResult();

            try
            {
                await dbContext.AddAsync(quote, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    quoteResult.IsSuccess = true;
                    quoteResult.Quote = quote;
                    quoteResult.Message = $"Devis {quote.Identifier} ajouté avec succès";
                }
                else
                {
                    quoteResult.IsSuccess = false;
                    quoteResult.Message = $"Impossible d'ajouter le devis {quote.Identifier}";
                }
            }
            catch (Exception ex)
            {
                quoteResult.IsSuccess = false;
                quoteResult.Message = $"Impossible d'ajouter le devis {quote.Identifier} : " + ex.Message;
            }

            return quoteResult;
        }

        public async Task<BaseResult> UpdateQuote(Quote quote, CancellationToken ct)
        {
            BaseResult result = new BaseResult();

            try
            {
                quote.UpdatedDate = DateTime.Now;
                await dbContext.UpdateAsync(quote, ct);
                await dbContext.SaveChangesAsync(ct);

                result.IsSuccess = true;
                result.Message = "Devis modifié avec succès";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResult> DeleteQuote(Quote quote, CancellationToken ct)
        {
            BaseResult result = new BaseResult();

            try
            {
                await dbContext.RemoveAsync(quote, ct);
                await dbContext.SaveChangesAsync(ct);

                result.IsSuccess = true;
                result.Message = "Devis supprimé avec succès";
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
