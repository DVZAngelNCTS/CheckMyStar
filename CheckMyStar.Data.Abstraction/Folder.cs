using System;
using System.Collections.Generic;

namespace CheckMyStar.Data;

public partial class Folder
{
    public int Identifier { get; set; }

    public int AccommodationTypeIdentifier { get; set; }

    public int AccommodationIdentifier { get; set; }

    public int OwnerUserIdentifier { get; set; }

    public int InspectorUserIdentifier { get; set; }

    public int FolderStatusIdentifier { get; set; }

    public int? QuoteIdentifier { get; set; }

    public int? InvoiceIdentifier { get; set; }

    public int? AppointmentIdentifier { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
