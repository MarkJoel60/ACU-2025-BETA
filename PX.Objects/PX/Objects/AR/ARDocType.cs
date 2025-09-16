// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class ARDocType : ILabelProvider
{
  public const 
  #nullable disable
  string Invoice = "INV";
  public const string DebitMemo = "DRM";
  public const string CreditMemo = "CRM";
  public const string Payment = "PMT";
  public const string VoidPayment = "RPM";
  public const string Prepayment = "PPM";
  public const string Refund = "REF";
  public const string VoidRefund = "VRF";
  public const string FinCharge = "FCH";
  public const string SmallBalanceWO = "SMB";
  public const string SmallCreditWO = "SMC";
  public const string CashSale = "CSL";
  public const string CashReturn = "RCS";
  public const string Undefined = "UND";
  public const string NoUpdate = "UND";
  public const string InvoiceOrCreditMemo = "INC";
  public const string CashSaleOrReturn = "CSR";
  public const string PrepaymentInvoice = "PPI";
  protected static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INV",
      nameof (Invoice)
    },
    {
      "DRM",
      "Debit Memo"
    },
    {
      "CRM",
      "Credit Memo"
    },
    {
      "PMT",
      nameof (Payment)
    },
    {
      "RPM",
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
      "FCH",
      "Overdue Charge"
    },
    {
      "SMB",
      "Balance WO"
    },
    {
      "SMC",
      "Credit WO"
    },
    {
      "CSL",
      "Cash Sale"
    },
    {
      "RCS",
      "Cash Return"
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  protected static readonly IEnumerable<ValueLabelPair> _valuePrintLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INV",
      nameof (Invoice)
    },
    {
      "DRM",
      "Debit Memo"
    },
    {
      "CRM",
      "Credit Memo"
    },
    {
      "PMT",
      nameof (Payment)
    },
    {
      "RPM",
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
      "FCH",
      "Overdue Charge"
    },
    {
      "SMB",
      "Balance WO"
    },
    {
      "SMC",
      "Credit WO"
    },
    {
      "CSL",
      "Cash Sale"
    },
    {
      "RCS",
      "Cash Return"
    },
    {
      "PPI",
      "Prepmt. Invoice"
    }
  };
  private static readonly IEnumerable<ValueLabelPair> _soValueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INV",
      nameof (Invoice)
    },
    {
      "DRM",
      "Debit Memo"
    },
    {
      "CRM",
      "Credit Memo"
    },
    {
      "CSL",
      "Cash Sale"
    },
    {
      "RCS",
      "Cash Return"
    },
    {
      "UND",
      "No Update"
    }
  };
  private static readonly IEnumerable<ValueLabelPair> _mixedOrderValueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "INC",
      "Invoice/Credit Memo"
    },
    {
      "CSR",
      "Cash Sale/Cash Return"
    }
  };
  public static readonly string[] Values = new string[14]
  {
    "INV",
    "DRM",
    "CRM",
    "PMT",
    "RPM",
    "PPM",
    "REF",
    "VRF",
    "FCH",
    "SMB",
    "SMC",
    "CSL",
    "RCS",
    "PPI"
  };
  public static readonly string[] Labels = new string[14]
  {
    nameof (Invoice),
    "Debit Memo",
    "Credit Memo",
    nameof (Payment),
    "Voided Payment",
    nameof (Prepayment),
    nameof (Refund),
    "Voided Refund",
    "Overdue Charge",
    "Balance WO",
    "Credit WO",
    "Cash Sale",
    "Cash Return",
    "Prepmt. Invoice"
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs => ARDocType._valueLabelPairs;

  public static bool? Payable(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[2])
      {
        case 'B':
          if (DocType == "SMB")
            goto label_13;
          goto label_14;
        case 'C':
          if (DocType == "SMC")
            break;
          goto label_14;
        case 'F':
          if (DocType == "REF" || DocType == "VRF")
            goto label_13;
          goto label_14;
        case 'H':
          if (DocType == "FCH")
            break;
          goto label_14;
        case 'I':
          if (DocType == "PPI")
            break;
          goto label_14;
        case 'L':
          if (DocType == "CSL")
            goto label_13;
          goto label_14;
        case 'M':
          switch (DocType)
          {
            case "DRM":
              break;
            case "PPM":
            case "CRM":
            case "RPM":
              goto label_13;
            default:
              goto label_14;
          }
          break;
        case 'S':
          if (DocType == "RCS")
            goto label_13;
          goto label_14;
        case 'T':
          if (DocType == "PMT")
            goto label_13;
          goto label_14;
        case 'V':
          if (DocType == "INV")
            break;
          goto label_14;
        default:
          goto label_14;
      }
      return new bool?(true);
label_13:
      return new bool?(false);
    }
label_14:
    return new bool?();
  }

  public static short? SortOrder(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[2])
      {
        case 'B':
          if (DocType == "SMB")
            goto label_15;
          goto label_19;
        case 'C':
          if (DocType == "SMC")
            break;
          goto label_19;
        case 'F':
          switch (DocType)
          {
            case "REF":
              return new short?((short) 4);
            case "VRF":
              return new short?((short) 5);
            default:
              goto label_19;
          }
        case 'H':
          if (DocType == "FCH")
            break;
          goto label_19;
        case 'I':
          if (DocType == "PPI")
            break;
          goto label_19;
        case 'L':
          if (DocType == "CSL")
            break;
          goto label_19;
        case 'M':
          switch (DocType)
          {
            case "DRM":
              break;
            case "CRM":
              return new short?((short) 1);
            case "PPM":
              return new short?((short) 2);
            case "RPM":
              goto label_18;
            default:
              goto label_19;
          }
          break;
        case 'S':
          if (DocType == "RCS")
            goto label_18;
          goto label_19;
        case 'T':
          if (DocType == "PMT")
            goto label_15;
          goto label_19;
        case 'V':
          if (DocType == "INV")
            break;
          goto label_19;
        default:
          goto label_19;
      }
      return new short?((short) 0);
label_15:
      return new short?((short) 3);
label_18:
      return new short?((short) 6);
    }
label_19:
    return new short?();
  }

  public static Decimal? SignBalance(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[2])
      {
        case 'B':
          if (DocType == "SMB")
            goto label_14;
          goto label_16;
        case 'C':
          if (DocType == "SMC" || DocType == "INC")
            break;
          goto label_16;
        case 'F':
          if (DocType == "REF" || DocType == "VRF")
            break;
          goto label_16;
        case 'H':
          if (DocType == "FCH")
            break;
          goto label_16;
        case 'I':
          if (DocType == "PPI")
            break;
          goto label_16;
        case 'L':
          if (DocType == "CSL")
            goto label_15;
          goto label_16;
        case 'M':
          switch (DocType)
          {
            case "DRM":
              break;
            case "CRM":
            case "PPM":
            case "RPM":
              goto label_14;
            default:
              goto label_16;
          }
          break;
        case 'R':
          if (DocType == "CSR")
            break;
          goto label_16;
        case 'S':
          if (DocType == "RCS")
            goto label_15;
          goto label_16;
        case 'T':
          if (DocType == "PMT")
            goto label_14;
          goto label_16;
        case 'V':
          if (DocType == "INV")
            break;
          goto label_16;
        default:
          goto label_16;
      }
      return new Decimal?(1M);
label_14:
      return new Decimal?(-1M);
label_15:
      return new Decimal?(0M);
    }
label_16:
    return new Decimal?();
  }

  public static Decimal? SignAmount(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[2])
      {
        case 'B':
          if (DocType == "SMB")
            goto label_13;
          goto label_14;
        case 'C':
          if (DocType == "SMC")
            break;
          goto label_14;
        case 'F':
          if (DocType == "REF" || DocType == "VRF")
            break;
          goto label_14;
        case 'H':
          if (DocType == "FCH")
            break;
          goto label_14;
        case 'I':
          if (DocType == "PPI")
            break;
          goto label_14;
        case 'L':
          if (DocType == "CSL")
            break;
          goto label_14;
        case 'M':
          switch (DocType)
          {
            case "DRM":
              break;
            case "CRM":
            case "PPM":
            case "RPM":
              goto label_13;
            default:
              goto label_14;
          }
          break;
        case 'S':
          if (DocType == "RCS")
            goto label_13;
          goto label_14;
        case 'T':
          if (DocType == "PMT")
            goto label_13;
          goto label_14;
        case 'V':
          if (DocType == "INV")
            break;
          goto label_14;
        default:
          goto label_14;
      }
      return new Decimal?(1M);
label_13:
      return new Decimal?(-1M);
    }
label_14:
    return new Decimal?();
  }

  public static string TaxDrCr(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[0])
      {
        case 'C':
          switch (DocType)
          {
            case "CSL":
              break;
            case "CRM":
              goto label_9;
            default:
              goto label_10;
          }
          break;
        case 'D':
          if (DocType == "DRM")
            break;
          goto label_10;
        case 'F':
          if (DocType == "FCH")
            break;
          goto label_10;
        case 'I':
          if (DocType == "INV")
            break;
          goto label_10;
        case 'P':
          if (DocType == "PPI")
            break;
          goto label_10;
        case 'R':
          if (DocType == "RCS")
            goto label_9;
          goto label_10;
        default:
          goto label_10;
      }
      return "C";
label_9:
      return "D";
    }
label_10:
    return "C";
  }

  public static string DocClass(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[2])
      {
        case 'B':
          if (DocType == "SMB")
            goto label_14;
          goto label_15;
        case 'C':
          if (DocType == "SMC")
            goto label_14;
          goto label_15;
        case 'F':
          if (DocType == "REF" || DocType == "VRF")
            goto label_13;
          goto label_15;
        case 'H':
          if (DocType == "FCH")
            break;
          goto label_15;
        case 'I':
          if (DocType == "PPI")
            break;
          goto label_15;
        case 'L':
          if (DocType == "CSL")
            break;
          goto label_15;
        case 'M':
          switch (DocType)
          {
            case "DRM":
            case "CRM":
              break;
            case "RPM":
              goto label_13;
            case "PPM":
              goto label_14;
            default:
              goto label_15;
          }
          break;
        case 'S':
          if (DocType == "RCS")
            break;
          goto label_15;
        case 'T':
          if (DocType == "PMT")
            goto label_13;
          goto label_15;
        case 'V':
          if (DocType == "INV")
            break;
          goto label_15;
        default:
          goto label_15;
      }
      return "N";
label_13:
      return "P";
label_14:
      return "U";
    }
label_15:
    return (string) null;
  }

  /// <summary>
  /// Returns <c>true</c> if the specified document type
  /// corresponds to self-voiding documents, i.e. one for which
  /// the void process does not create a separate document, but just
  /// reverses all of its applications in full.
  /// </summary>
  public static bool IsSelfVoiding(string docType)
  {
    if (!ARDocType._valueLabelPairs.Any<ValueLabelPair>((Func<ValueLabelPair, bool>) (pair => pair.Value == docType)))
      throw new PXException("The document type is not supported or implemented.");
    string str = docType;
    return str == "SMB" || str == "SMC";
  }

  public static bool? HasNegativeAmount(string docType)
  {
    if (docType != null && docType.Length == 3)
    {
      switch (docType[2])
      {
        case 'B':
          if (docType == "SMB")
            break;
          goto label_13;
        case 'C':
          if (docType == "SMC")
            break;
          goto label_13;
        case 'F':
          switch (docType)
          {
            case "REF":
              break;
            case "VRF":
              goto label_12;
            default:
              goto label_13;
          }
          break;
        case 'H':
          if (docType == "FCH")
            break;
          goto label_13;
        case 'L':
          if (docType == "CSL")
            break;
          goto label_13;
        case 'M':
          switch (docType)
          {
            case "DRM":
            case "CRM":
              break;
            case "RPM":
              goto label_12;
            default:
              goto label_13;
          }
          break;
        case 'S':
          if (!(docType == "RCS"))
            goto label_13;
          break;
        case 'T':
          if (docType == "PMT")
            break;
          goto label_13;
        case 'V':
          if (docType == "INV")
            break;
          goto label_13;
        default:
          goto label_13;
      }
      return new bool?(false);
label_12:
      return new bool?(true);
    }
label_13:
    return new bool?();
  }

  /// <summary>
  /// Returns <c>true</c> if the specified document type
  /// has both Invoice and Payment parts related to one
  /// <see cref="T:PX.Objects.AR.ARRegister" /> record.
  /// </summary>
  public static bool HasBothInvoiceAndPaymentParts(string docType)
  {
    return docType == "CRM" || docType == "CSL" || docType == "RCS" || docType == "PPI";
  }

  /// <summary>Get display name of the AR document type.</summary>
  /// <param name="docType">Type of the AR document.</param>
  /// <returns />
  public static string GetDisplayName(string docType)
  {
    return ARDocType._valueLabelPairs.FirstOrDefault<ValueLabelPair>((Func<ValueLabelPair, bool>) (a => a.Value == docType)).Label;
  }

  public class CustomListAttribute(IEnumerable<ValueLabelPair> valueLabelPairs) : LabelListAttribute(valueLabelPairs)
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;
  }

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(ARDocType._valueLabelPairs)
    {
    }
  }

  /// <summary>
  /// Defines a Selector of the AR Document types with shorter description.<br />
  /// In the screens displayed as combo-box.<br />
  /// Mostly used in the reports.<br />
  /// </summary>
  public class PrintListAttribute : LabelListAttribute
  {
    public PrintListAttribute()
      : base(ARDocType._valuePrintLabelPairs)
    {
    }
  }

  public class SOListAttribute : ARDocType.CustomListAttribute
  {
    public SOListAttribute()
      : base(ARDocType._soValueLabelPairs)
    {
    }
  }

  public class MixedOrderListAttribute : ARDocType.CustomListAttribute
  {
    public MixedOrderListAttribute()
      : base(ARDocType._mixedOrderValueLabelPairs)
    {
    }
  }

  public class SOFullListAttribute : ARDocType.CustomListAttribute
  {
    public SOFullListAttribute()
      : base(ARDocType._soValueLabelPairs.Concat<ValueLabelPair>(ARDocType._mixedOrderValueLabelPairs))
    {
    }
  }

  /// <summary>
  /// Defines the list of AR document types that can be approved.
  /// </summary>
  public class ARApprovalDocTypeListAttribute : LabelListAttribute
  {
    private static readonly IEnumerable<ValueLabelPair> _approvalValueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
    {
      {
        "INV",
        "Invoice"
      },
      {
        "PPI",
        "Prepmt. Invoice"
      },
      {
        "DRM",
        "Debit Memo"
      },
      {
        "CRM",
        "Credit Memo"
      },
      {
        "REF",
        "Refund"
      },
      {
        "RCS",
        "Cash Return"
      }
    };

    public ARApprovalDocTypeListAttribute()
      : base(ARDocType.ARApprovalDocTypeListAttribute._approvalValueLabelPairs)
    {
    }
  }

  /// <summary>
  /// Defines a list of AR document types that can be used in the SO module.
  /// </summary>
  public class SOEntryListAttribute : ARDocType.CustomListAttribute
  {
    private static readonly string[] _soEntryListValues = new string[5]
    {
      "INV",
      "DRM",
      "CRM",
      "CSL",
      "RCS"
    };

    public SOEntryListAttribute()
      : base(ARDocType._valueLabelPairs.Where<ValueLabelPair>((Func<ValueLabelPair, bool>) (pair => ((IEnumerable<string>) ARDocType.SOEntryListAttribute._soEntryListValues).Contains<string>(pair.Value))))
    {
    }
  }

  public class RetainageInvoiceListAttribute : ARDocType.CustomListAttribute
  {
    private static readonly string[] _invoiceAndMemosListValues = new string[3]
    {
      "INV",
      "DRM",
      "CRM"
    };

    public RetainageInvoiceListAttribute()
      : base(ARDocType._valueLabelPairs.Where<ValueLabelPair>((Func<ValueLabelPair, bool>) (pair => ((IEnumerable<string>) ARDocType.RetainageInvoiceListAttribute._invoiceAndMemosListValues).Contains<string>(pair.Value))))
    {
    }
  }

  /// <summary>
  /// Defines the list of AR document types for Print Invoices.
  /// </summary>
  public class PrintARDocTypeListAttribute : LabelListAttribute
  {
    private static readonly IEnumerable<ValueLabelPair> _printARValueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
    {
      {
        "INV",
        "Invoice"
      },
      {
        "DRM",
        "Debit Memo"
      },
      {
        "CRM",
        "Credit Memo"
      },
      {
        "FCH",
        "Overdue Charge"
      },
      {
        "SMC",
        "Credit WO"
      },
      {
        "CSL",
        "Cash Sale"
      },
      {
        "RCS",
        "Cash Return"
      }
    };

    public PrintARDocTypeListAttribute()
      : base(ARDocType.PrintARDocTypeListAttribute._printARValueLabelPairs)
    {
    }
  }

  public class invoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.invoice>
  {
    public invoice()
      : base("INV")
    {
    }
  }

  public class debitMemo : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.debitMemo>
  {
    public debitMemo()
      : base("DRM")
    {
    }
  }

  public class creditMemo : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.creditMemo>
  {
    public creditMemo()
      : base("CRM")
    {
    }
  }

  public class payment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.payment>
  {
    public payment()
      : base("PMT")
    {
    }
  }

  public class voidPayment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.voidPayment>
  {
    public voidPayment()
      : base("RPM")
    {
    }
  }

  public class prepayment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.prepayment>
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
  ARDocType.refund>
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
  ARDocType.voidRefund>
  {
    public voidRefund()
      : base("VRF")
    {
    }
  }

  public class finCharge : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.finCharge>
  {
    public finCharge()
      : base("FCH")
    {
    }
  }

  public class smallBalanceWO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.smallBalanceWO>
  {
    public smallBalanceWO()
      : base("SMB")
    {
    }
  }

  public class smallCreditWO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.smallCreditWO>
  {
    public smallCreditWO()
      : base("SMC")
    {
    }
  }

  public class undefined : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.undefined>
  {
    public undefined()
      : base("UND")
    {
    }
  }

  public class noUpdate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.noUpdate>
  {
    public noUpdate()
      : base("UND")
    {
    }
  }

  public class cashSale : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.cashSale>
  {
    public cashSale()
      : base("CSL")
    {
    }
  }

  public class cashReturn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.cashReturn>
  {
    public cashReturn()
      : base("RCS")
    {
    }
  }

  public class invoiceOrCreditMemo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARDocType.invoiceOrCreditMemo>
  {
    public invoiceOrCreditMemo()
      : base("INC")
    {
    }
  }

  public class cashSaleOrReturn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.cashSaleOrReturn>
  {
    public cashSaleOrReturn()
      : base("CSR")
    {
    }
  }

  public class prepaymentInvoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocType.prepaymentInvoice>
  {
    public prepaymentInvoice()
      : base("PPI")
    {
    }
  }
}
