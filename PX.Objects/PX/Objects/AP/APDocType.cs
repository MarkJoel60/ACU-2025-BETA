// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

public class APDocType : ILabelProvider
{
  public const 
  #nullable disable
  string Invoice = "INV";
  public const string CreditAdj = "ACR";
  public const string DebitAdj = "ADR";
  public const string Check = "CHK";
  public const string VoidCheck = "VCK";
  public const string Prepayment = "PPM";
  public const string Refund = "REF";
  public const string VoidRefund = "VRF";
  public const string QuickCheck = "QCK";
  public const string VoidQuickCheck = "VQC";
  public const string PrepaymentRequest = "PPR";
  public const string CashReturn = "RQC";
  public const string PrepaymentInvoice = "PPI";
  protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INV",
      "Bill"
    },
    {
      "ACR",
      "Credit Adj."
    },
    {
      "ADR",
      "Debit Adj."
    },
    {
      "CHK",
      "Payment"
    },
    {
      "VCK",
      "Voided Payment"
    },
    {
      "PPM",
      nameof (Prepayment)
    },
    {
      "REF",
      nameof (Refund)
    },
    {
      "VRF",
      "Voided Refund"
    },
    {
      "QCK",
      "Cash Purchase"
    },
    {
      "VQC",
      "Voided Cash Purchase"
    },
    {
      "RQC",
      "Cash Return"
    },
    {
      "PPR",
      "Prepayment Req."
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairsUI = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INV",
      "Bill"
    },
    {
      "ACR",
      "Credit Adj."
    },
    {
      "ADR",
      "Debit Adj."
    },
    {
      "PPM",
      nameof (Prepayment)
    },
    {
      "REF",
      nameof (Refund)
    },
    {
      "VRF",
      "Voided Refund"
    },
    {
      "QCK",
      "Cash Purchase"
    },
    {
      "VQC",
      "Voided Cash Purchase"
    },
    {
      "RQC",
      "Cash Return"
    },
    {
      "PPR",
      "Prepayment Req."
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  protected static readonly IEnumerable<ValueLabelPair> _valueDocumentReleasableLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INV",
      "Bill"
    },
    {
      "ACR",
      "Credit Adj."
    },
    {
      "ADR",
      "Debit Adj."
    },
    {
      "VCK",
      "Voided Payment"
    },
    {
      "PPM",
      nameof (Prepayment)
    },
    {
      "REF",
      nameof (Refund)
    },
    {
      "VRF",
      "Voided Refund"
    },
    {
      "VQC",
      "Voided Cash Purchase"
    },
    {
      "RQC",
      "Cash Return"
    },
    {
      "PPR",
      "Prepayment Req."
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  protected static readonly IEnumerable<ValueLabelPair> _valueCheckReleasableLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "CHK",
      "Payment"
    },
    {
      "VCK",
      "Voided Payment"
    },
    {
      "QCK",
      "Cash Purchase"
    },
    {
      "VQC",
      "Voided Cash Purchase"
    },
    {
      "PPM",
      nameof (Prepayment)
    }
  };
  protected static readonly IEnumerable<ValueLabelPair> _valuePrintableLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INV",
      "Bill"
    },
    {
      "ACR",
      "Credit Adj."
    },
    {
      "ADR",
      "Debit Adj."
    },
    {
      "CHK",
      "Payment"
    },
    {
      "VCK",
      "Voided Payment"
    },
    {
      "PPM",
      nameof (Prepayment)
    },
    {
      "PPI",
      "Prepmt. Invoice"
    },
    {
      "REF",
      nameof (Refund)
    },
    {
      "VRF",
      "Voided Refund"
    },
    {
      "QCK",
      "Cash Purchase"
    },
    {
      "VQC",
      "Voided Cash Purchase"
    },
    {
      "RQC",
      "Cash Return"
    }
  };
  public static readonly string[] Values = new string[13]
  {
    "INV",
    "ACR",
    "ADR",
    "CHK",
    "VCK",
    "PPM",
    "REF",
    "VRF",
    "QCK",
    "VQC",
    "RQC",
    "PPR",
    "PPI"
  };
  public static readonly string[] Labels = new string[13]
  {
    "Bill",
    "Credit Adj.",
    "Debit Adj.",
    "Payment",
    "Voided Payment",
    nameof (Prepayment),
    nameof (Refund),
    "Voided Refund",
    "Cash Purchase",
    "Voided Cash Purchase",
    "Cash Return",
    "Prepayment Req.",
    "Prepmt. Invoice"
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs => APDocType._valueLabelPairs;

  public IEnumerable<ValueLabelPair> ValueLabelPairsUI => APDocType._valueLabelPairsUI;

  public static string DocClass(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[1])
      {
        case 'C':
          switch (DocType)
          {
            case "ACR":
            case "QCK":
              break;
            case "VCK":
              goto label_11;
            default:
              goto label_13;
          }
          break;
        case 'D':
          if (DocType == "ADR")
            break;
          goto label_13;
        case 'E':
          if (DocType == "REF")
            goto label_11;
          goto label_13;
        case 'H':
          if (DocType == "CHK")
            goto label_11;
          goto label_13;
        case 'N':
          if (DocType == "INV")
            break;
          goto label_13;
        case 'P':
          switch (DocType)
          {
            case "PPI":
              break;
            case "PPM":
              return "U";
            default:
              goto label_13;
          }
          break;
        case 'Q':
          if (DocType == "VQC" || DocType == "RQC")
            break;
          goto label_13;
        case 'R':
          if (DocType == "VRF")
            goto label_11;
          goto label_13;
        default:
          goto label_13;
      }
      return "N";
label_11:
      return "P";
    }
label_13:
    return (string) null;
  }

  public static bool? Payable(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[1])
      {
        case 'C':
          switch (DocType)
          {
            case "ACR":
              break;
            case "VCK":
            case "QCK":
              goto label_11;
            default:
              goto label_12;
          }
          break;
        case 'D':
          if (DocType == "ADR")
            goto label_11;
          goto label_12;
        case 'E':
          if (DocType == "REF")
            goto label_11;
          goto label_12;
        case 'H':
          if (DocType == "CHK")
            goto label_11;
          goto label_12;
        case 'N':
          if (DocType == "INV")
            break;
          goto label_12;
        case 'P':
          switch (DocType)
          {
            case "PPI":
              break;
            case "PPM":
              goto label_11;
            default:
              goto label_12;
          }
          break;
        case 'Q':
          if (DocType == "VQC" || DocType == "RQC")
            goto label_11;
          goto label_12;
        case 'R':
          if (DocType == "VRF")
            goto label_11;
          goto label_12;
        default:
          goto label_12;
      }
      return new bool?(true);
label_11:
      return new bool?(false);
    }
label_12:
    return new bool?();
  }

  public static short? SortOrder(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[1])
      {
        case 'C':
          switch (DocType)
          {
            case "ACR":
            case "QCK":
              break;
            case "VCK":
              goto label_14;
            default:
              goto label_18;
          }
          break;
        case 'D':
          if (DocType == "ADR")
            return new short?((short) 2);
          goto label_18;
        case 'E':
          if (DocType == "REF")
            return new short?((short) 5);
          goto label_18;
        case 'H':
          if (DocType == "CHK")
            return new short?((short) 3);
          goto label_18;
        case 'N':
          if (DocType == "INV")
            break;
          goto label_18;
        case 'P':
          switch (DocType)
          {
            case "PPM":
              return new short?((short) 1);
            case "PPI":
              return new short?((short) 7);
            default:
              goto label_18;
          }
        case 'Q':
          if (DocType == "VQC" || DocType == "RQC")
            goto label_14;
          goto label_18;
        case 'R':
          if (DocType == "VRF")
            return new short?((short) 6);
          goto label_18;
        default:
          goto label_18;
      }
      return new short?((short) 0);
label_14:
      return new short?((short) 4);
    }
label_18:
    return new short?();
  }

  public static Decimal? SignBalance(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[1])
      {
        case 'C':
          switch (DocType)
          {
            case "ACR":
              break;
            case "VCK":
              goto label_11;
            case "QCK":
              goto label_13;
            default:
              goto label_14;
          }
          break;
        case 'D':
          if (DocType == "ADR")
            goto label_11;
          goto label_14;
        case 'E':
          if (DocType == "REF")
            break;
          goto label_14;
        case 'H':
          if (DocType == "CHK")
            goto label_11;
          goto label_14;
        case 'N':
          if (DocType == "INV")
            break;
          goto label_14;
        case 'P':
          switch (DocType)
          {
            case "PPI":
              break;
            case "PPM":
              return new Decimal?(-1M);
            default:
              goto label_14;
          }
          break;
        case 'Q':
          if (DocType == "VQC" || DocType == "RQC")
            goto label_13;
          goto label_14;
        case 'R':
          if (DocType == "VRF")
            break;
          goto label_14;
        default:
          goto label_14;
      }
      return new Decimal?(1M);
label_11:
      return new Decimal?(-1M);
label_13:
      return new Decimal?(0M);
    }
label_14:
    return new Decimal?();
  }

  public static Decimal? SignAmount(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[1])
      {
        case 'C':
          switch (DocType)
          {
            case "ACR":
            case "QCK":
              break;
            case "VCK":
              goto label_11;
            default:
              goto label_13;
          }
          break;
        case 'D':
          if (DocType == "ADR")
            goto label_11;
          goto label_13;
        case 'E':
          if (DocType == "REF")
            break;
          goto label_13;
        case 'H':
          if (DocType == "CHK")
            goto label_11;
          goto label_13;
        case 'N':
          if (DocType == "INV")
            break;
          goto label_13;
        case 'P':
          switch (DocType)
          {
            case "PPI":
              break;
            case "PPM":
              return new Decimal?(-1M);
            default:
              goto label_13;
          }
          break;
        case 'Q':
          if (DocType == "VQC" || DocType == "RQC")
            goto label_11;
          goto label_13;
        case 'R':
          if (DocType == "VRF")
            break;
          goto label_13;
        default:
          goto label_13;
      }
      return new Decimal?(1M);
label_11:
      return new Decimal?(-1M);
    }
label_13:
    return new Decimal?();
  }

  public static string TaxDrCr(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[1])
      {
        case 'C':
          switch (DocType)
          {
            case "ACR":
            case "QCK":
              break;
            case "VCK":
              goto label_12;
            default:
              goto label_14;
          }
          break;
        case 'D':
          if (DocType == "ADR")
            goto label_11;
          goto label_14;
        case 'E':
          if (DocType == "REF")
            goto label_13;
          goto label_14;
        case 'H':
          if (DocType == "CHK")
            goto label_12;
          goto label_14;
        case 'N':
          if (DocType == "INV")
            break;
          goto label_14;
        case 'P':
          switch (DocType)
          {
            case "PPI":
              break;
            case "PPM":
              goto label_12;
            default:
              goto label_14;
          }
          break;
        case 'Q':
          if (DocType == "VQC" || DocType == "RQC")
            goto label_11;
          goto label_14;
        case 'R':
          if (DocType == "VRF")
            goto label_13;
          goto label_14;
        default:
          goto label_14;
      }
      return "D";
label_11:
      return "C";
label_12:
      return "D";
label_13:
      return "C";
    }
label_14:
    return "D";
  }

  public static bool? HasNegativeAmount(string docType)
  {
    if (docType != null && docType.Length == 3)
    {
      switch (docType[1])
      {
        case 'C':
          switch (docType)
          {
            case "ACR":
            case "QCK":
              break;
            case "VCK":
              goto label_11;
            default:
              goto label_12;
          }
          break;
        case 'D':
          if (docType == "ADR")
            break;
          goto label_12;
        case 'E':
          if (docType == "REF")
            break;
          goto label_12;
        case 'H':
          if (docType == "CHK")
            break;
          goto label_12;
        case 'N':
          if (docType == "INV")
            break;
          goto label_12;
        case 'P':
          if (docType == "PPM" || docType == "PPI")
            break;
          goto label_12;
        case 'Q':
          if (docType == "VQC" || docType == "RQC")
            break;
          goto label_12;
        case 'R':
          if (docType == "VRF")
            goto label_11;
          goto label_12;
        default:
          goto label_12;
      }
      return new bool?(false);
label_11:
      return new bool?(true);
    }
label_12:
    return new bool?();
  }

  /// <summary>
  /// Returns <c>true</c> if the specified document type
  /// has both Invoice and Payment parts related to one
  /// <see cref="!:ARRegister" /> record.
  /// </summary>
  public static bool HasBothInvoiceAndPaymentParts(string docType)
  {
    return docType == "ADR" || docType == "QCK" || docType == "VQC" || docType == "RQC" || docType == "PPM" || docType == "PPI";
  }

  /// <summary>
  /// Query if "Pre-Release" process allowed for the AP document type.
  /// </summary>
  /// <param name="docType">Type of the AP document.</param>
  /// <returns />
  public static bool IsPrebookingAllowedForType(string docType)
  {
    return docType == "INV" || docType == "ACR" || docType == "ADR" || docType == "QCK";
  }

  /// <summary>Get display name of the AP document type.</summary>
  /// <param name="docType">Type of the AP document.</param>
  /// <returns />
  public static string GetDisplayName(string docType)
  {
    return APDocType._valueLabelPairs.FirstOrDefault<ValueLabelPair>((Func<ValueLabelPair, bool>) (a => a.Value == docType)).Label;
  }

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(APDocType._valueLabelPairs)
    {
    }
  }

  public class DocumentReleaseListAttribute : LabelListAttribute
  {
    public DocumentReleaseListAttribute()
      : base(APDocType._valueDocumentReleasableLabelPairs)
    {
    }
  }

  public class ReleaseChecksListAttribute : LabelListAttribute
  {
    public ReleaseChecksListAttribute()
      : base(APDocType._valueCheckReleasableLabelPairs)
    {
    }
  }

  /// <summary>
  /// Defines a Selector of the AP Document types with shorter description.<br />
  /// In the screens displayed as combo-box.<br />
  /// Mostly used in the reports.<br />
  /// </summary>
  public class PrintListAttribute : LabelListAttribute
  {
    public PrintListAttribute()
      : base(APDocType._valuePrintableLabelPairs)
    {
    }
  }

  /// <summary>
  /// Defines a list of the AP Document types, which are used for approval.
  /// </summary>
  public class APApprovalDocTypeListAttribute : LabelListAttribute
  {
    private static readonly IEnumerable<ValueLabelPair> _approvalValueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
    {
      {
        "INV",
        "Bill"
      },
      {
        "PPI",
        "Prepmt. Invoice"
      },
      {
        "ACR",
        "Credit Adj."
      },
      {
        "ADR",
        "Debit Adj."
      },
      {
        "PPR",
        "Prepayment Req."
      },
      {
        "CHK",
        "Payment"
      },
      {
        "QCK",
        "Cash Purchase"
      },
      {
        "PPM",
        "Prepayment"
      }
    };

    public APApprovalDocTypeListAttribute()
      : base(APDocType.APApprovalDocTypeListAttribute._approvalValueLabelPairs)
    {
    }
  }

  public class invoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.invoice>
  {
    public invoice()
      : base("INV")
    {
    }
  }

  public class creditAdj : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.creditAdj>
  {
    public creditAdj()
      : base("ACR")
    {
    }
  }

  public class debitAdj : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.debitAdj>
  {
    public debitAdj()
      : base("ADR")
    {
    }
  }

  public class check : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.check>
  {
    public check()
      : base("CHK")
    {
    }
  }

  public class voidCheck : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.voidCheck>
  {
    public voidCheck()
      : base("VCK")
    {
    }
  }

  public class prepayment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.prepayment>
  {
    public prepayment()
      : base("PPM")
    {
    }
  }

  public class refund : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.refund>
  {
    public refund()
      : base("REF")
    {
    }
  }

  public class voidRefund : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.voidRefund>
  {
    public voidRefund()
      : base("VRF")
    {
    }
  }

  public class quickCheck : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.quickCheck>
  {
    public quickCheck()
      : base("QCK")
    {
    }
  }

  public class voidQuickCheck : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.voidQuickCheck>
  {
    public voidQuickCheck()
      : base("VQC")
    {
    }
  }

  public class prepaymentRequest : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.prepaymentRequest>
  {
    public prepaymentRequest()
      : base("PPR")
    {
    }
  }

  public class cashReturn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.cashReturn>
  {
    public cashReturn()
      : base("RQC")
    {
    }
  }

  public class prepaymentInvoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocType.prepaymentInvoice>
  {
    public prepaymentInvoice()
      : base("PPI")
    {
    }
  }
}
