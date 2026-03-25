using AutoMapper;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class QuoteBus(IUserContextService userContext, IActivityBus activityBus, IQuoteDal quoteDal, IQuoteLineDal quoteLineDal, IUserDal userDal, IAddressDal addressDal, ISocietyDal societyDal, IMapper mapper) : IQuoteBus
    {
        public async Task<QuoteResponse> GetIdentifier(CancellationToken ct)
        {
            var quoteResult = await quoteDal.GetNextIdentifier(ct);

            var response = new QuoteResponse
            {
                IsSuccess = quoteResult.IsSuccess,
                Message = quoteResult.Message,
                Quote = quoteResult.Quote != null ? mapper.Map<QuoteModel>(quoteResult.Quote) : null
            };

            return response;
        }

        public async Task<QuoteResponse> GetQuote(int quoteIdentifier, CancellationToken ct)
        {
            var quoteResult = await quoteDal.GetQuote(quoteIdentifier, ct);

            if (quoteResult.IsSuccess && quoteResult.Quote != null)
            {
                var quoteModel = await LoadQuote(quoteResult.Quote, ct);

                return new QuoteResponse
                {
                    IsSuccess = true,
                    Message = string.IsNullOrWhiteSpace(quoteResult.Message) ? "Devis récupéré avec succès" : quoteResult.Message,
                    Quote = quoteModel
                };
            }

            return new QuoteResponse
            {
                IsSuccess = false,
                Message = quoteResult.IsSuccess ? "Devis non trouvé" : quoteResult.Message,
                Quote = null
            };
        }

        public async Task<QuotesResponse> GetQuotes(CancellationToken ct)
        {
            QuotesResponse quotesResponse = new QuotesResponse();

            var quotes = await quoteDal.GetQuotes(ct);

            if (quotes.IsSuccess && quotes.Quotes != null)
            {
                foreach (Quote quote in quotes.Quotes)
                {
                    var quoteModel = await LoadQuote(quote, ct);
                    quotesResponse.Quotes.Add(quoteModel);
                }

                quotesResponse.IsSuccess = true;
                quotesResponse.Message = quotes.Message;
            }
            else
            {
                quotesResponse.IsSuccess = false;
                quotesResponse.Message = quotes.Message;
            }

            return quotesResponse;
        }

        public async Task<QuotesResponse> GetQuotesByInspector(int inspectorIdentifier, CancellationToken ct)
        {
            QuotesResponse quotesResponse = new QuotesResponse();

            var quotes = await quoteDal.GetQuotesByInspector(inspectorIdentifier, ct);

            if (quotes.IsSuccess && quotes.Quotes != null)
            {
                foreach (Quote quote in quotes.Quotes)
                {
                    var quoteModel = await LoadQuote(quote, ct);
                    quotesResponse.Quotes.Add(quoteModel);
                }

                quotesResponse.IsSuccess = true;
                quotesResponse.Message = quotes.Message;
            }
            else
            {
                quotesResponse.IsSuccess = false;
                quotesResponse.Message = quotes.Message;
            }

            return quotesResponse;
        }

        public async Task<QuoteResponse> AddQuote(QuoteModel quoteModel, int currentUser, CancellationToken ct)
        {
            QuoteResponse response = new QuoteResponse();

            var dateTime = DateTime.Now;

            if (quoteModel.Identifier == 0)
            {
                var identifierResult = await quoteDal.GetNextIdentifier(ct);
                if (identifierResult.IsSuccess && identifierResult.Quote != null)
                {
                    quoteModel.Identifier = identifierResult.Quote.Identifier;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"Erreur lors de la récupération de l'identifiant : {identifierResult.Message}";
                    return response;
                }
            }

            quoteModel.CreatedDate = dateTime;
            quoteModel.UpdatedDate = null;

            var quote = mapper.Map<Quote>(quoteModel);

            var quoteLines = new List<QuoteLine>();
            if (quoteModel.QuoteLines != null && quoteModel.QuoteLines.Any())
            {
                int nextLineIdentifier = 1;
                var lineIdentifierResult = await quoteLineDal.GetNextIdentifier(ct);
                if (lineIdentifierResult.IsSuccess && lineIdentifierResult.QuoteLine != null)
                {
                    nextLineIdentifier = lineIdentifierResult.QuoteLine.Identifier;
                }

                foreach (var lineModel in quoteModel.QuoteLines)
                {
                    var quoteLine = mapper.Map<QuoteLine>(lineModel);
                    quoteLine.Identifier = nextLineIdentifier++;
                    quoteLine.QuoteIdentifier = quoteModel.Identifier;
                    quoteLine.CreatedDate = dateTime;
                    quoteLine.UpdatedDate = null;
                    quoteLines.Add(quoteLine);
                }
            }

            var quoteResult = await quoteDal.AddQuote(quote, ct);

            if (quoteResult.IsSuccess && quoteResult.Quote != null)
            {
                if (quoteLines.Any())
                {
                    var linesResult = await quoteLineDal.AddQuoteLines(quoteLines, ct);
                    if (!linesResult.IsSuccess)
                    {
                        response.IsSuccess = false;
                        response.Message = $"Devis créé mais erreur lors de l'ajout des lignes : {linesResult.Message}";
                        await activityBus.AddActivity(response.Message, dateTime, currentUser, false, ct);
                        return response;
                    }
                }

                response.IsSuccess = true;
                response.Message = quoteResult.Message;
                response.Quote = await LoadQuote(quoteResult.Quote, ct);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = quoteResult.Message;
            }

            await activityBus.AddActivity(quoteResult.Message, dateTime, currentUser, quoteResult.IsSuccess, ct);

            return response;
        }

        public async Task<BaseResponse> UpdateQuote(QuoteModel quoteModel, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var quote = await quoteDal.GetQuote(quoteModel.Identifier, ct);

            if (quote.IsSuccess && quote.Quote != null)
            {
                if (!quote.Quote.IsEditable)
                {
                    result.IsSuccess = false;
                    result.Message = "Ce devis n'est pas modifiable";
                    return result;
                }

                var dateTime = DateTime.Now;

                quoteModel.CreatedDate = quote.Quote.CreatedDate;
                quoteModel.UpdatedDate = dateTime;

                var quoteEntity = mapper.Map<Quote>(quoteModel);

                var quoteResult = await quoteDal.UpdateQuote(quoteEntity, ct);

                if (quoteResult.IsSuccess)
                {
                    await quoteLineDal.DeleteQuoteLinesByQuote(quoteModel.Identifier, ct);

                    if (quoteModel.QuoteLines != null && quoteModel.QuoteLines.Any())
                    {
                        int nextLineIdentifier = 1;
                        var lineIdentifierResult = await quoteLineDal.GetNextIdentifier(ct);
                        if (lineIdentifierResult.IsSuccess && lineIdentifierResult.QuoteLine != null)
                        {
                            nextLineIdentifier = lineIdentifierResult.QuoteLine.Identifier;
                        }

                        var quoteLines = new List<QuoteLine>();
                        foreach (var lineModel in quoteModel.QuoteLines)
                        {
                            var quoteLine = mapper.Map<QuoteLine>(lineModel);
                            quoteLine.Identifier = nextLineIdentifier++;
                            quoteLine.QuoteIdentifier = quoteModel.Identifier;
                            quoteLine.CreatedDate = dateTime;
                            quoteLine.UpdatedDate = null;
                            quoteLines.Add(quoteLine);
                        }

                        var linesResult = await quoteLineDal.AddQuoteLines(quoteLines, ct);
                        if (!linesResult.IsSuccess)
                        {
                            result.IsSuccess = false;
                            result.Message = $"Devis modifié mais erreur lors de l'ajout des lignes : {linesResult.Message}";
                            await activityBus.AddActivity(result.Message, dateTime, currentUser, false, ct);
                            return result;
                        }
                    }
                }

                result.IsSuccess = quoteResult.IsSuccess;
                result.Message = quoteResult.Message;

                await activityBus.AddActivity(quoteResult.Message, dateTime, currentUser, quoteResult.IsSuccess, ct);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = quote.Quote == null ? "Devis non trouvé" : quote.Message;
            }

            return result;
        }

        public async Task<BaseResponse> DeleteQuote(int identifier, int currentUser, CancellationToken ct)
        {
            BaseResponse result = new BaseResponse();

            var quote = await quoteDal.GetQuote(identifier, ct);

            if (quote.IsSuccess && quote.Quote != null)
            {
                var dateTime = DateTime.Now;

                await quoteLineDal.DeleteQuoteLinesByQuote(identifier, ct);

                var quoteResult = await quoteDal.DeleteQuote(quote.Quote, ct);

                result.IsSuccess = quoteResult.IsSuccess;
                result.Message = quoteResult.Message;

                await activityBus.AddActivity(quoteResult.Message, dateTime, currentUser, quoteResult.IsSuccess, ct);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = quote.Quote == null ? "Devis non trouvé" : quote.Message;
            }

            return result;
        }

        private async Task<QuoteModel> LoadQuote(Quote quote, CancellationToken ct)
        {
            var quoteModel = mapper.Map<QuoteModel>(quote);

            if (quote.ClientUserIdentifier != null)
            {
                var userResponse = await userDal.GetUser(quote.ClientUserIdentifier.Value, ct);
                if (userResponse.IsSuccess && userResponse.User != null)
                {
                    quoteModel.ClientUser = mapper.Map<UserModel>(userResponse.User);
                }
            }

            if (quote.ClientAddressIdentifier != null)
            {
                var addressResponse = await addressDal.GetAddress(quote.ClientAddressIdentifier.Value, ct);
                if (addressResponse.IsSuccess && addressResponse.Address != null)
                {
                    quoteModel.ClientAddress = mapper.Map<AddressModel>(addressResponse.Address);
                }
            }

            if (quote.InspectorIdentifier != null)
            {
                var inspectorResponse = await userDal.GetUser(quote.InspectorIdentifier.Value, ct);
                if (inspectorResponse.IsSuccess && inspectorResponse.User != null)
                {
                    quoteModel.Inspector = mapper.Map<UserModel>(inspectorResponse.User);
                }
            }

            if (quote.CompanySocietyIdentifier != null)
            {
                var societyResponse = await societyDal.GetSociety(quote.CompanySocietyIdentifier.Value, ct);
                if (societyResponse.IsSuccess && societyResponse.Society != null)
                {
                    quoteModel.CompanySociety = mapper.Map<SocietyModel>(societyResponse.Society);
                }
            }

            if (quote.CompanyAddressIdentifier != null)
            {
                var addressResponse = await addressDal.GetAddress(quote.CompanyAddressIdentifier.Value, ct);
                if (addressResponse.IsSuccess && addressResponse.Address != null)
                {
                    quoteModel.CompanyAddress = mapper.Map<AddressModel>(addressResponse.Address);
                }
            }

            var quoteLinesResult = await quoteLineDal.GetQuoteLinesByQuote(quote.Identifier, ct);
            if (quoteLinesResult.IsSuccess && quoteLinesResult.QuoteLines != null)
            {
                quoteModel.QuoteLines = mapper.Map<List<QuoteLineModel>>(quoteLinesResult.QuoteLines);
            }

            return quoteModel;
        }
    }
}
