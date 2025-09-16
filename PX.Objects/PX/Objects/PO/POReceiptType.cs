// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PO;

public static class POReceiptType
{
  public const 
  #nullable disable
  string All = "AL";
  public const string TransferReceipt = "RX";
  public const string POReceipt = "RT";
  public const string POReturn = "RN";

  public static string GetINTranType(string aReceiptType)
  {
    string empty = string.Empty;
    switch (aReceiptType)
    {
      case "RT":
        return "RCP";
      case "RX":
        return "TRX";
      case "RN":
        return "III";
      default:
        return empty;
    }
  }

  /// <summary>Default Ctor</summary>
  /// <param name="SearchType"> Must be IBqlSearch type, pointing to POReceipt.refNbr</param>
  public class RefNbrAttribute(Type SearchType) : PXSelectorAttribute(SearchType, new Type[8]
  {
    typeof (PX.Objects.PO.POReceipt.receiptNbr),
    typeof (PX.Objects.PO.POReceipt.invoiceNbr),
    typeof (PX.Objects.PO.POReceipt.receiptDate),
    typeof (PX.Objects.PO.POReceipt.vendorID),
    typeof (PX.Objects.PO.POReceipt.vendorID_Vendor_acctName),
    typeof (PX.Objects.PO.POReceipt.vendorLocationID),
    typeof (PX.Objects.PO.POReceipt.status),
    typeof (PX.Objects.PO.POReceipt.curyID)
  })
  {
  }

  /// <summary>
  /// Specialized version of the AutoNumber attribute for POReceipts<br />
  /// It defines how the new numbers are generated for the PO Receipt. <br />
  /// References POReceipt.receiptDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in PO Setup:<br />
  /// namely POSetup.receiptNumberingID for any receipt types<br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    public NumberingAttribute()
      : base(typeof (POSetup.receiptNumberingID), typeof (PX.Objects.PO.POReceipt.receiptDate))
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    private static (string, string)[] ValuesToLabels = new (string, string)[3]
    {
      ("RT", "Receipt"),
      ("RN", "Return"),
      ("RX", "Transfer Receipt")
    };

    public ListAttribute()
      : this(false)
    {
    }

    protected ListAttribute(bool all)
    {
      (string, string)[] valueTupleArray;
      if (!all)
        valueTupleArray = POReceiptType.ListAttribute.ValuesToLabels;
      else
        valueTupleArray = ((IEnumerable<(string, string)>) new (string, string)[1]
        {
          ("AL", "All")
        }).Concat<(string, string)>((IEnumerable<(string, string)>) POReceiptType.ListAttribute.ValuesToLabels).ToArray<(string, string)>();
      // ISSUE: explicit constructor call
      base.\u002Ector(valueTupleArray);
    }

    internal bool TryGetValue(string label, out string value)
    {
      int index = Array.IndexOf<string>(this._AllowedLabels, label);
      if (index >= 0)
      {
        value = this._AllowedValues[index];
        return true;
      }
      value = (string) null;
      return false;
    }

    public class WithAll : POReceiptType.ListAttribute
    {
      public WithAll()
        : base(true)
      {
      }
    }
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptType.all>
  {
    public all()
      : base("AL")
    {
    }
  }

  public class poreceipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptType.poreceipt>
  {
    public poreceipt()
      : base("RT")
    {
    }
  }

  public class poreturn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptType.poreturn>
  {
    public poreturn()
      : base("RN")
    {
    }
  }

  public class transferreceipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptType.transferreceipt>
  {
    public transferreceipt()
      : base("RX")
    {
    }
  }
}
