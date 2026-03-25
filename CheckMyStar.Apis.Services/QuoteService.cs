using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;

using PdfSharp.Fonts;

namespace CheckMyStar.Apis.Services
{
    public class QuoteService(IQuoteBusForService quoteBusForService) : IQuoteService
    {
        static QuoteService()
        {
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
        }

        public async Task<QuoteResponse> GetNextIdentifier(CancellationToken ct)
        {
            var quote = await quoteBusForService.GetNextIdentifier(ct);

            return quote;
        }

        public async Task<QuotePdfResponse> GenerateQuotePdf(int quoteIdentifier, CancellationToken ct)
        {
            var quoteResponse = await quoteBusForService.GetQuote(quoteIdentifier, ct);

            if (!quoteResponse.IsSuccess || quoteResponse.Quote == null)
            {
                return new QuotePdfResponse
                {
                    IsSuccess = false,
                    Message = quoteResponse.Message,
                    Content = null,
                    FileName = string.Empty
                };
            }

            var quoteYear = quoteResponse.Quote.CreatedDate == default ? DateTime.Now.Year : quoteResponse.Quote.CreatedDate.Year;
            var fileName = $"Devis-{quoteYear}-{quoteResponse.Quote.Identifier:0000}.pdf";
            var content = BuildQuotePdf(quoteResponse.Quote);

            return new QuotePdfResponse
            {
                IsSuccess = true,
                Message = "PDF généré avec succès",
                Content = content,
                FileName = fileName
            };
        }

        public async Task<QuotesResponse> GetQuotes(CancellationToken ct)
        {
            var quotes = await quoteBusForService.GetQuotes(ct);

            return quotes;
        }

        public async Task<QuotesResponse> GetQuotes(QuoteGetRequest request, CancellationToken ct)
        {
            var quotes = await quoteBusForService.GetQuotes(request, ct);

            return quotes;
        }

        public async Task<QuoteResponse> AddQuote(QuoteSaveRequest request, CancellationToken ct)
        {
            var quote = await quoteBusForService.AddQuote(request, ct);

            return quote;
        }

        public async Task<BaseResponse> UpdateQuote(QuoteSaveRequest request, CancellationToken ct)
        {
            var result = await quoteBusForService.UpdateQuote(request, ct);

            return result;
        }

        public async Task<BaseResponse> DeleteQuote(QuoteDeleteRequest request, CancellationToken ct)
        {
            var result = await quoteBusForService.DeleteQuote(request, ct);

            return result;
        }

        private static byte[] BuildQuotePdf(QuoteModel quote)
        {
            var document = new Document();
            document.Styles["Normal"].Font.Name = "Arial";
            document.Styles["Normal"].Font.Size = 10;
            var section = document.AddSection();
            section.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1.5);

            var issuerName = string.IsNullOrWhiteSpace(quote.CompanySociety?.Name) ? "Entreprise" : quote.CompanySociety.Name;
            var recipientName = GetRecipientName(quote);
            var quoteDate = quote.CreatedDate == default ? DateTime.Now : quote.CreatedDate;
            var vatAmount = quote.TotalAmountTTC - quote.TotalAmountHT;
            if (vatAmount < 0)
            {
                vatAmount = quote.QuoteLines.Sum(line => (line.Quantity * line.UnitPriceHT) * (line.VATRate / 100m));
            }

            var issuerAddressText = FormatAddress(quote.CompanyAddress ?? quote.CompanySociety?.Address);
            var recipientAddressText = FormatAddress(quote.ClientAddress ?? quote.ClientUser?.Address);

            var quoteTitle = section.AddParagraph("DEVIS");
            quoteTitle.Format.Font.Size = 22;
            quoteTitle.Format.Font.Bold = true;
            quoteTitle.Format.SpaceAfter = Unit.FromCentimeter(0.15);

            var quoteNumber = section.AddParagraph($"N° {quoteDate:yyyy}-{quote.Identifier:0000}");
            quoteNumber.Format.Font.Size = 12;
            quoteNumber.Format.SpaceAfter = Unit.FromCentimeter(0.3);

            var separator = section.AddParagraph();
            separator.Format.Borders.Bottom.Width = 1;
            separator.Format.Borders.Bottom.Color = Colors.Black;
            separator.Format.SpaceAfter = Unit.FromCentimeter(0.3);

            var datesTable = section.AddTable();
            datesTable.Borders.Visible = false;
            datesTable.AddColumn(Unit.FromCentimeter(6));
            datesTable.AddColumn(Unit.FromCentimeter(5));
            datesTable.AddColumn(Unit.FromCentimeter(5));

            var datesRow = datesTable.AddRow();
            datesRow.Cells[0].AddParagraph($"Date du devis : {quoteDate:dd/MM/yyyy}");
            datesRow.Cells[1].AddParagraph($"Validité : {(quote.ValidityDate.HasValue ? quote.ValidityDate.Value.ToString("dd/MM/yyyy") : "-")}");
            datesRow.Cells[2].AddParagraph($"Exécution : {(quote.ExecutionDate.HasValue ? quote.ExecutionDate.Value.ToString("dd/MM/yyyy") : "-")}");
            datesRow.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            datesRow.Cells[2].Format.Alignment = ParagraphAlignment.Right;

            section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.35);

            var partyTable = section.AddTable();
            partyTable.Borders.Visible = false;
            partyTable.AddColumn(Unit.FromCentimeter(8.2));
            partyTable.AddColumn(Unit.FromCentimeter(0.6));
            partyTable.AddColumn(Unit.FromCentimeter(8.2));

            var partyRow = partyTable.AddRow();
            var issuerCell = partyRow.Cells[0];
            var recipientCell = partyRow.Cells[2];

            issuerCell.Borders.Width = 0.5;
            recipientCell.Borders.Width = 0.5;

            var issuerHeader = issuerCell.AddParagraph("Entreprise facturante");
            issuerHeader.Format.Font.Bold = true;
            issuerHeader.Format.Shading.Color = Colors.LightGray;
            issuerHeader.Format.SpaceAfter = Unit.FromCentimeter(0.2);

            var issuerBlock = issuerCell.AddParagraph(issuerName);
            issuerBlock.Format.Font.Bold = true;
            issuerBlock.Format.SpaceAfter = Unit.FromCentimeter(0.1);
            issuerCell.AddParagraph(issuerAddressText);

            var companyEmail = string.IsNullOrWhiteSpace(quote.CompanyEmail) ? quote.CompanySociety?.Email : quote.CompanyEmail;
            var companyPhone = string.IsNullOrWhiteSpace(quote.CompanyPhone) ? quote.CompanySociety?.Phone : quote.CompanyPhone;
            var companySiret = string.IsNullOrWhiteSpace(quote.CompanySiretCode) ? quote.CompanySociety?.SiretCode : quote.CompanySiretCode;
            var companyVat = string.IsNullOrWhiteSpace(quote.CompanyVatNumber) ? quote.CompanySociety?.VatNumber : quote.CompanyVatNumber;
            var companyLegal = string.IsNullOrWhiteSpace(quote.CompanyLegalInformation) ? quote.CompanySociety?.LegalInformation : quote.CompanyLegalInformation;

            if (!string.IsNullOrWhiteSpace(companyEmail) || !string.IsNullOrWhiteSpace(companyPhone))
            {
                var contactInfo = issuerCell.AddParagraph();
                contactInfo.Format.SpaceBefore = Unit.FromCentimeter(0.1);
                contactInfo.AddText($"Email : {companyEmail ?? "-"}");
                contactInfo.AddLineBreak();
                contactInfo.AddText($"Tél : {companyPhone ?? "-"}");
            }

            if (!string.IsNullOrWhiteSpace(companySiret) || !string.IsNullOrWhiteSpace(companyVat))
            {
                var legalIdentifiers = issuerCell.AddParagraph();
                legalIdentifiers.Format.SpaceBefore = Unit.FromCentimeter(0.1);
                legalIdentifiers.AddText($"SIRET : {companySiret ?? "-"}");
                legalIdentifiers.AddLineBreak();
                legalIdentifiers.AddText($"TVA : {companyVat ?? "-"}");
            }

            if (!string.IsNullOrWhiteSpace(companyLegal))
            {
                var legalInfo = issuerCell.AddParagraph(companyLegal);
                legalInfo.Format.SpaceBefore = Unit.FromCentimeter(0.1);
                legalInfo.Format.Font.Size = 9;
            }

            var recipientHeader = recipientCell.AddParagraph("Destinataire");
            recipientHeader.Format.Font.Bold = true;
            recipientHeader.Format.Shading.Color = Colors.LightGray;
            recipientHeader.Format.SpaceAfter = Unit.FromCentimeter(0.2);

            var recipientNameParagraph = recipientCell.AddParagraph(recipientName);
            recipientNameParagraph.Format.Font.Bold = true;
            recipientNameParagraph.Format.SpaceAfter = Unit.FromCentimeter(0.1);
            recipientCell.AddParagraph(recipientAddressText);

            section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.35);

            var intro = section.AddParagraph("Nous vous remercions pour votre confiance. Veuillez trouver ci-dessous le détail de notre proposition.");
            intro.Format.SpaceAfter = Unit.FromCentimeter(0.35);

            section.AddParagraph("Détail des prestations").Format.Font.Bold = true;

            var table = section.AddTable();
            table.Borders.Width = 0.5;
            table.Rows.LeftIndent = 0;
            table.Format.SpaceBefore = Unit.FromCentimeter(0.15);

            table.AddColumn(Unit.FromCentimeter(9.5));
            table.AddColumn(Unit.FromCentimeter(1.8));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(2.8));
            table.AddColumn(Unit.FromCentimeter(2.9));

            var header = table.AddRow();
            header.Shading.Color = Colors.LightGray;
            header.Cells[0].AddParagraph("Description");
            header.Cells[1].AddParagraph("Qté");
            header.Cells[2].AddParagraph("Unité");
            header.Cells[3].AddParagraph("PU HT");
            header.Cells[4].AddParagraph("Total HT");
            header.Format.Font.Bold = true;
            header.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            header.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            header.Cells[4].Format.Alignment = ParagraphAlignment.Right;

            foreach (var line in quote.QuoteLines)
            {
                var row = table.AddRow();
                row.Cells[0].AddParagraph(string.IsNullOrWhiteSpace(line.Description) ? "-" : line.Description);
                row.Cells[1].AddParagraph(line.Quantity.ToString("0.##"));
                row.Cells[2].AddParagraph(line.Unit);
                row.Cells[3].AddParagraph($"{line.UnitPriceHT:0.00} €");
                row.Cells[4].AddParagraph($"{(line.Quantity * line.UnitPriceHT):0.00} €");
                row.Cells[1].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            }

            section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.4);

            var summaryTable = section.AddTable();
            summaryTable.Borders.Visible = false;
            summaryTable.AddColumn(Unit.FromCentimeter(11.5));
            summaryTable.AddColumn(Unit.FromCentimeter(4));
            summaryTable.AddColumn(Unit.FromCentimeter(2.5));

            AddSummaryRow(summaryTable, "Total HT", quote.TotalAmountHT);
            AddSummaryRow(summaryTable, "TVA", vatAmount);
            AddSummaryRow(summaryTable, "Total TTC", quote.TotalAmountTTC, true);

            section.AddParagraph().Format.SpaceAfter = Unit.FromCentimeter(0.4);
            var legalNote = section.AddParagraph("Ce devis est établi sous réserve de validation et conformément aux conditions générales applicables.");
            legalNote.Format.Font.Size = 9;
            legalNote.Format.Font.Italic = true;

            var renderer = new PdfDocumentRenderer(unicode: true)
            {
                Document = document
            };
            renderer.RenderDocument();

            using var stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);

            return stream.ToArray();
        }

        private static void AddSummaryRow(Table summaryTable, string label, decimal amount, bool isBold = false)
        {
            var row = summaryTable.AddRow();
            row.Cells[1].AddParagraph(label);
            row.Cells[2].AddParagraph($"{amount:0.00} €");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[1].Borders.Top.Width = 0.25;
            row.Cells[2].Borders.Top.Width = 0.25;

            if (isBold)
            {
                row.Format.Font.Bold = true;
                row.Cells[1].Borders.Top.Width = 0.75;
                row.Cells[2].Borders.Top.Width = 0.75;
            }
        }

        private static string GetRecipientName(QuoteModel quote)
        {
            var fullName = $"{quote.ClientUser?.FirstName} {quote.ClientUser?.LastName}".Trim();

            return string.IsNullOrWhiteSpace(fullName) ? "Client" : fullName;
        }

        private static string FormatAddress(AddressModel? address)
        {
            if (address == null)
            {
                return "Adresse non renseignée";
            }

            var line1 = $"{address.Number} {address.AddressLine}".Trim();
            var line2 = $"{address.ZipCode} {address.City}".Trim();
            var line3 = $"{address.Region} {address.Country?.Name}".Trim();

            var lines = new[] { line1, line2, line3 }.Where(line => !string.IsNullOrWhiteSpace(line));
            var formatted = string.Join("\n", lines);

            return string.IsNullOrWhiteSpace(formatted) ? "Adresse non renseignée" : formatted;
        }

    }
}
