// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INTranType
{
  public const 
  #nullable disable
  string Assembly = "ASY";
  public const string Disassembly = "DSY";
  public const string Receipt = "RCP";
  public const string Issue = "III";
  public const string Return = "RET";
  public const string Adjustment = "ADJ";
  public const string Transfer = "TRX";
  public const string Invoice = "INV";
  public const string DebitMemo = "DRM";
  public const string CreditMemo = "CRM";
  public const string StandardCostAdjustment = "ASC";
  public const string NegativeCostAdjustment = "NSC";
  public const string ReceiptCostAdjustment = "RCA";
  public const string NoUpdate = "UND";

  public static string DocType(string TranType)
  {
    if (TranType != null && TranType.Length == 3)
    {
      switch (TranType[2])
      {
        case 'I':
          if (TranType == "III")
            break;
          goto label_11;
        case 'M':
          if (TranType == "DRM" || TranType == "CRM")
            break;
          goto label_11;
        case 'P':
          if (TranType == "RCP")
            return "R";
          goto label_11;
        case 'T':
          if (TranType == "RET")
            break;
          goto label_11;
        case 'V':
          if (TranType == "INV")
            break;
          goto label_11;
        case 'X':
          if (TranType == "TRX")
            return "T";
          goto label_11;
        default:
          goto label_11;
      }
      return "I";
    }
label_11:
    return "0";
  }

  public static short? InvtMult(string TranType)
  {
    if (TranType != null && TranType.Length == 3)
    {
      switch (TranType[2])
      {
        case 'A':
          if (TranType == "RCA")
            goto label_15;
          goto label_16;
        case 'C':
          if (TranType == "ASC" || TranType == "NSC")
            goto label_14;
          goto label_16;
        case 'D':
          if (TranType == "UND")
            goto label_15;
          goto label_16;
        case 'I':
          if (TranType == "III")
            break;
          goto label_16;
        case 'J':
          if (TranType == "ADJ")
            goto label_14;
          goto label_16;
        case 'M':
          switch (TranType)
          {
            case "DRM":
              break;
            case "CRM":
              goto label_14;
            default:
              goto label_16;
          }
          break;
        case 'P':
          if (TranType == "RCP")
            goto label_14;
          goto label_16;
        case 'T':
          if (TranType == "RET")
            goto label_14;
          goto label_16;
        case 'V':
          if (TranType == "INV")
            break;
          goto label_16;
        case 'X':
          if (TranType == "TRX")
            break;
          goto label_16;
        case 'Y':
          if (TranType == "ASY" || TranType == "DSY")
            break;
          goto label_16;
        default:
          goto label_16;
      }
      return new short?((short) -1);
label_14:
      return new short?((short) 1);
label_15:
      return new short?((short) 0);
    }
label_16:
    return new short?();
  }

  public static short? SalesMult(string TranType)
  {
    switch (TranType)
    {
      case "INV":
      case "DRM":
        return new short?((short) 1);
      case "CRM":
        return new short?((short) -1);
      default:
        return new short?();
    }
  }

  public static string TranTypeFromInvoiceType(string docType, Decimal? qtySign)
  {
    Decimal? nullable = qtySign;
    Decimal num = 0M;
    bool flag = nullable.GetValueOrDefault() < num & nullable.HasValue;
    switch (docType)
    {
      case "INV":
      case "CSL":
        return flag ? "CRM" : "INV";
      case "DRM":
        return flag ? "CRM" : "DRM";
      case "CRM":
      case "RCS":
        return flag ? "INV" : "CRM";
      default:
        return (string) null;
    }
  }

  public static short? InvtMultFromInvoiceType(string docType)
  {
    switch (docType)
    {
      case "INV":
      case "DRM":
      case "CSL":
        return new short?((short) -1);
      case "CRM":
      case "RCS":
        return new short?((short) 1);
      default:
        return new short?();
    }
  }

  public static string GetOppositeTranType(string docType)
  {
    string oppositeTranType;
    switch (docType)
    {
      case "III":
        oppositeTranType = "RET";
        break;
      case "INV":
      case "DRM":
        oppositeTranType = "CRM";
        break;
      default:
        oppositeTranType = (string) null;
        break;
    }
    return oppositeTranType;
  }

  public class CustomListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public CustomListAttribute(string[] AllowedValues, string[] AllowedLabels)
      : base(AllowedValues, AllowedLabels)
    {
    }

    public CustomListAttribute(Tuple<string, string>[] valuesToLabels)
      : base(valuesToLabels)
    {
    }
  }

  public class ListAttribute : INTranType.CustomListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[13]
      {
        PXStringListAttribute.Pair("RCP", "Receipt"),
        PXStringListAttribute.Pair("III", "Issue"),
        PXStringListAttribute.Pair("RET", "Return"),
        PXStringListAttribute.Pair("ADJ", "Adjustment"),
        PXStringListAttribute.Pair("TRX", "Transfer"),
        PXStringListAttribute.Pair("INV", "Invoice"),
        PXStringListAttribute.Pair("DRM", "Debit Memo"),
        PXStringListAttribute.Pair("CRM", "Credit Memo"),
        PXStringListAttribute.Pair("ASY", "Assembly"),
        PXStringListAttribute.Pair("DSY", "Disassembly"),
        PXStringListAttribute.Pair("ASC", "Standard Cost Adjustment"),
        PXStringListAttribute.Pair("NSC", "Negative Cost Adjustment"),
        PXStringListAttribute.Pair("RCA", "Receipt Cost Adjustment")
      })
    {
    }
  }

  public class IssueListAttribute : INTranType.CustomListAttribute
  {
    public IssueListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("III", "Issue"),
        PXStringListAttribute.Pair("RCP", "Receipt"),
        PXStringListAttribute.Pair("RET", "Return"),
        PXStringListAttribute.Pair("INV", "Invoice"),
        PXStringListAttribute.Pair("DRM", "Debit Memo"),
        PXStringListAttribute.Pair("CRM", "Credit Memo"),
        PXStringListAttribute.Pair("ADJ", "Adjustment")
      })
    {
    }
  }

  public class SOListAttribute : INTranType.CustomListAttribute
  {
    public SOListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("III", "Issue"),
        PXStringListAttribute.Pair("RET", "Return"),
        PXStringListAttribute.Pair("TRX", "Transfer"),
        PXStringListAttribute.Pair("INV", "Invoice"),
        PXStringListAttribute.Pair("DRM", "Debit Memo"),
        PXStringListAttribute.Pair("CRM", "Credit Memo"),
        PXStringListAttribute.Pair("UND", "No Update")
      })
    {
    }
  }

  public class SONonARListAttribute : INTranType.CustomListAttribute
  {
    public SONonARListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("III", "Issue"),
        PXStringListAttribute.Pair("RET", "Return"),
        PXStringListAttribute.Pair("TRX", "Transfer"),
        PXStringListAttribute.Pair("UND", "No Update")
      })
    {
    }
  }

  public class adjustment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.adjustment>
  {
    public adjustment()
      : base("ADJ")
    {
    }
  }

  public class receipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.receipt>
  {
    public receipt()
      : base("RCP")
    {
    }
  }

  public class issue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.issue>
  {
    public issue()
      : base("III")
    {
    }
  }

  public class transfer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.transfer>
  {
    public transfer()
      : base("TRX")
    {
    }
  }

  public class return_ : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.return_>
  {
    public return_()
      : base("RET")
    {
    }
  }

  public class invoice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.invoice>
  {
    public invoice()
      : base("INV")
    {
    }
  }

  public class creditMemo : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.creditMemo>
  {
    public creditMemo()
      : base("CRM")
    {
    }
  }

  public class debitMemo : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.debitMemo>
  {
    public debitMemo()
      : base("DRM")
    {
    }
  }

  public class assembly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.assembly>
  {
    public assembly()
      : base("ASY")
    {
    }
  }

  public class disassembly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.disassembly>
  {
    public disassembly()
      : base("DSY")
    {
    }
  }

  public class standardCostAdjustment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INTranType.standardCostAdjustment>
  {
    public standardCostAdjustment()
      : base("ASC")
    {
    }
  }

  public class negativeCostAdjustment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INTranType.negativeCostAdjustment>
  {
    public negativeCostAdjustment()
      : base("NSC")
    {
    }
  }

  public class receiptCostAdjustment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INTranType.receiptCostAdjustment>
  {
    public receiptCostAdjustment()
      : base("RCA")
    {
    }
  }

  public class noUpdate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INTranType.noUpdate>
  {
    public noUpdate()
      : base("UND")
    {
    }
  }
}
