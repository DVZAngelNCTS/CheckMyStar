using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Quote
{
    public int Identifier { get; set; }

    public int? ClientUserIdentifier { get; set; }

    public int? ClientAddressIdentifier { get; set; }

    public int? InspectorIdentifier { get; set; }

    public int? CompanySocietyIdentifier { get; set; }

    public int? CompanyAddressIdentifier { get; set; }

    public string? CompanyLogoPath { get; set; }

    public string? CompanyEmail { get; set; }

    public string? CompanyPhone { get; set; }

    public string? CompanySiretCode { get; set; }

    public string? CompanyVatNumber { get; set; }

    public string? CompanyLegalInformation { get; set; }

    public decimal TotalAmountHT { get; set; }

    public decimal TotalAmountTTC { get; set; }

    public DateTime? ValidityDate { get; set; }

    public DateTime? ExecutionDate { get; set; }

    public int? QuoteStatusIdentifier { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool IsEditable { get; set; }

    public ICollection<QuoteLine> QuoteLines { get; set; } = [];
}
