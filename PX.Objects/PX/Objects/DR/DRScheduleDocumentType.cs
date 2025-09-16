// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRScheduleDocumentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.DR;

public static class DRScheduleDocumentType
{
  public const 
  #nullable disable
  string Invoice = "INV";
  public const string Bill = "BIL";

  public static string ExtractModule(string documentType)
  {
    if (documentType != null && documentType.Length == 3)
    {
      switch (documentType[0])
      {
        case 'A':
          if (documentType == "ACR" || documentType == "ADR")
            goto label_11;
          goto label_12;
        case 'B':
          if (documentType == "BIL")
            goto label_11;
          goto label_12;
        case 'C':
          switch (documentType)
          {
            case "CRM":
              break;
            default:
              goto label_12;
          }
          break;
        case 'D':
          if (documentType == "DRM")
            break;
          goto label_12;
        case 'I':
          if (documentType == "INV")
            break;
          goto label_12;
        case 'Q':
          if (documentType == "QCK")
            goto label_12;
          goto label_12;
        case 'R':
          if (documentType == "RCS")
            goto label_12;
          goto label_12;
        case 'V':
          if (documentType == "VQC")
            goto label_12;
          goto label_12;
        default:
          goto label_12;
      }
      return "AR";
label_11:
      return "AP";
    }
label_12:
    throw new PXException("The specified document type is not supported.");
  }

  public static string ExtractDocType(string documentType)
  {
    switch (documentType)
    {
      case "INV":
        return "INV";
      case "BIL":
        return "INV";
      default:
        return documentType;
    }
  }

  public static string BuildDocumentType(string module, string docType)
  {
    if (!(docType == "INV"))
      return docType;
    return module == "AR" ? "INV" : "BIL";
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[11]
      {
        "INV",
        "CRM",
        "DRM",
        "CSL",
        "RCS",
        "BIL",
        "ACR",
        "ADR",
        "QCK",
        "VQC",
        "RQC"
      }, new string[11]
      {
        "Invoice",
        "Credit Memo",
        "Debit Memo",
        "Cash Sale",
        "Cash Return",
        "Bill",
        "Credit Adj.",
        "Debit Adj.",
        "Cash Purchase",
        "Voided Cash Purchase",
        "Cash Return"
      })
    {
    }
  }

  public class invoiceAR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DRScheduleDocumentType.invoiceAR>
  {
    public invoiceAR()
      : base("INV")
    {
    }
  }

  public class invoiceAP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DRScheduleDocumentType.invoiceAP>
  {
    public invoiceAP()
      : base("BIL")
    {
    }
  }
}
