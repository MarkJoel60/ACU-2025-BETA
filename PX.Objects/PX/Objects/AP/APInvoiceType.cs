// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APInvoiceType : APDocType
{
  public new static readonly string[] Values = new string[5]
  {
    "INV",
    "ACR",
    "ADR",
    "PPM",
    "PPI"
  };
  public new static readonly string[] Labels = new string[5]
  {
    "Bill",
    "Credit Adj.",
    "Debit Adj.",
    "Prepayment",
    "Prepmt. Invoice"
  };

  public static string DrCr(string DocType)
  {
    if (DocType != null && DocType.Length == 3)
    {
      switch (DocType[0])
      {
        case 'A':
          switch (DocType)
          {
            case "ACR":
              break;
            case "ADR":
              goto label_9;
            default:
              goto label_10;
          }
          break;
        case 'I':
          if (DocType == "INV")
            break;
          goto label_10;
        case 'P':
          if (DocType == "PPM" || DocType == "PPI")
            break;
          goto label_10;
        case 'Q':
          if (DocType == "QCK")
            break;
          goto label_10;
        case 'R':
          if (DocType == "RQC")
            goto label_9;
          goto label_10;
        case 'V':
          if (DocType == "VQC")
            goto label_9;
          goto label_10;
        default:
          goto label_10;
      }
      return "D";
label_9:
      return "C";
    }
label_10:
    return (string) null;
  }

  /// <summary>
  /// Specialized selector for APInvoice RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// APInvoice.refNbr,APInvoice.docDate, APInvoice.finPeriodID,<br />
  /// APInvoice.vendorID, APInvoice.vendorID_Vendor_acctName,<br />
  /// APInvoice.vendorLocationID, APInvoice.curyID, APInvoice.curyOrigDocAmt,<br />
  /// APInvoice.curyDocBal,APInvoice.status, APInvoice.dueDate, APInvoice.invoiceNbr<br />
  /// </summary>
  public class RefNbrAttribute(System.Type SearchType) : PXSelectorAttribute(SearchType, typeof (APRegister.refNbr), typeof (APRegister.docDate), typeof (APRegister.finPeriodID), typeof (APRegister.vendorID), typeof (APRegister.vendorID_Vendor_acctName), typeof (APRegister.vendorLocationID), typeof (APInvoice.invoiceNbr), typeof (APRegister.curyID), typeof (APRegister.curyOrigDocAmt), typeof (APRegister.curyDocBal), typeof (APRegister.status), typeof (APInvoice.dueDate), typeof (APRegister.docDesc))
  {
  }

  public class AdjdRefNbrAttribute(System.Type SearchType) : PXSelectorAttribute(SearchType, typeof (APRegister.branchID), typeof (APRegister.refNbr), typeof (APRegister.docDate), typeof (APRegister.finPeriodID), typeof (APRegister.vendorLocationID), typeof (APRegister.curyID), typeof (APRegister.curyOrigDocAmt), typeof (APRegister.curyDocBal), typeof (APRegister.status), typeof (APAdjust.APInvoice.dueDate), typeof (APAdjust.APInvoice.invoiceNbr), typeof (APRegister.docDesc))
  {
    protected override bool IsReadDeletedSupported => false;
  }

  public class AdjdLineNbrAttribute : PXSelectorAttribute
  {
    public AdjdLineNbrAttribute()
      : base(typeof (Search2<APTran.lineNbr, InnerJoin<APInvoice, On<APInvoice.docType, Equal<APTran.tranType>, And<APInvoice.refNbr, Equal<APTran.refNbr>>>>, Where<APTran.tranType, Equal<Optional<APAdjust.adjdDocType>>, And<APTran.refNbr, Equal<Optional<APAdjust.adjdRefNbr>>, And<APInvoice.paymentsByLinesAllowed, Equal<True>, And<Where<APTran.curyTranBal, NotEqual<decimal0>, Or<APInvoice.retainageApply, Equal<True>, And<APInvoice.retainageUnreleasedAmt, Equal<decimal0>, And<APInvoice.retainageReleased, Equal<decimal0>>>>>>>>>>), typeof (APTran.lineNbr), typeof (APTran.inventoryID), typeof (APTran.tranDesc), typeof (APTran.projectID), typeof (APTran.taskID), typeof (APTran.costCodeID), typeof (APTran.accountID), typeof (APTran.curyTranBal))
    {
    }

    public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (object.Equals(e.NewValue, (object) 0))
        e.Cancel = true;
      else
        base.FieldVerifying(sender, e);
    }
  }

  /// <summary>
  /// Specialized for APInvoices version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the AP Invoice. <br />
  /// References APInvoice.docType and APInvoice.docDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in AP Setup and APInvoice types:<br />
  /// namely APSetup.invoiceNumberingID, APSetup.adjustmentNumberingID,<br />
  /// APSetup.adjustmentNumberingID, APSetup.invoiceNumberingID <br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    private static string[] _DocTypes
    {
      get
      {
        return new string[5]
        {
          "INV",
          "ACR",
          "ADR",
          "PPM",
          "PPI"
        };
      }
    }

    private static System.Type[] _SetupFields
    {
      get
      {
        return new System.Type[5]
        {
          typeof (APSetup.invoiceNumberingID),
          typeof (APSetup.creditAdjNumberingID),
          typeof (APSetup.debitAdjNumberingID),
          typeof (APSetup.invoiceNumberingID),
          typeof (APSetup.prepaymentInvoiceNumberingID)
        };
      }
    }

    public static System.Type GetNumberingIDField(string docType)
    {
      foreach (Tuple<string, System.Type> tuple in EnumerableExtensions.Zip<string, System.Type>((IEnumerable<string>) APInvoiceType.NumberingAttribute._DocTypes, (IEnumerable<System.Type>) APInvoiceType.NumberingAttribute._SetupFields))
      {
        if (tuple.Item1 == docType)
          return tuple.Item2;
      }
      return (System.Type) null;
    }

    public NumberingAttribute()
      : base(typeof (APInvoice.docType), typeof (APInvoice.docDate), APInvoiceType.NumberingAttribute._DocTypes, APInvoiceType.NumberingAttribute._SetupFields)
    {
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(APInvoiceType.Values, APInvoiceType.Labels)
    {
    }
  }

  public class TaxInvoiceListAttribute : PXStringListAttribute
  {
    public TaxInvoiceListAttribute()
      : base(new string[4]{ "INV", "ACR", "ADR", "PPI" }, new string[4]
      {
        "Bill",
        "Credit Adj.",
        "Debit Adj.",
        "Prepmt. Invoice"
      })
    {
    }
  }

  public class AdjdListAttribute : PXStringListAttribute
  {
    public AdjdListAttribute()
      : base(new string[2]{ "INV", "ACR" }, new string[2]
      {
        "Bill",
        "Credit Adj."
      })
    {
    }
  }

  /// <summary>
  /// String list with DocType, suitable for the APInvoice document.<br />
  /// Used in the DocType selector of APInvoices. <br />
  /// </summary>
  public class InvoiceListAttribute : PXStringListAttribute
  {
    public InvoiceListAttribute()
      : base(new string[3]{ "INV", "ACR", "PPM" }, new string[3]
      {
        "Bill",
        "Credit Adj.",
        "Prepayment"
      })
    {
    }
  }
}
