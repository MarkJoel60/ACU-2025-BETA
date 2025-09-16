// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceType : ARDocType
{
  public new static readonly string[] Values = new string[6]
  {
    "INV",
    "DRM",
    "CRM",
    "FCH",
    "SMC",
    "PPI"
  };
  public new static readonly string[] Labels = new string[6]
  {
    "Invoice",
    "Debit Memo",
    "Credit Memo",
    "Overdue Charge",
    "Credit WO",
    "Prepmt. Invoice"
  };

  public static string DrCr(string DocType)
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
              goto label_10;
            default:
              goto label_11;
          }
          break;
        case 'D':
          if (DocType == "DRM")
            break;
          goto label_11;
        case 'F':
          if (DocType == "FCH")
            break;
          goto label_11;
        case 'I':
          if (DocType == "INV")
            break;
          goto label_11;
        case 'P':
          if (DocType == "PPI")
            break;
          goto label_11;
        case 'R':
          if (DocType == "RCS")
            goto label_10;
          goto label_11;
        case 'S':
          if (DocType == "SMC")
            break;
          goto label_11;
        default:
          goto label_11;
      }
      return "C";
label_10:
      return "D";
    }
label_11:
    return (string) null;
  }

  /// <summary>
  /// Specialized selector for ARInvoice RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// ARInvoice.refNbr,ARInvoice.docDate, ARInvoice.finPeriodID,<br />
  /// ARInvoice.customerID, ARInvoice.customerID_Customer_acctName,<br />
  /// ARInvoice.customerLocationID, ARInvoice.curyID, ARInvoice.curyOrigDocAmt,<br />
  /// ARInvoice.curyDocBal,ARInvoice.status, ARInvoice.dueDate, ARInvoice.invoiceNbr<br />
  /// </summary>
  /// <summary>Ctor</summary>
  /// <param name="SearchType">Must be IBqlSearch, returning ARInvoice.refNbr</param>
  public class RefNbrAttribute(Type SearchType) : PXSelectorAttribute(SearchType, new Type[12]
  {
    typeof (ARRegister.refNbr),
    typeof (ARInvoice.invoiceNbr),
    typeof (ARRegister.docDate),
    typeof (ARRegister.finPeriodID),
    typeof (ARRegister.customerID),
    typeof (ARRegister.customerID_Customer_acctName),
    typeof (ARRegister.customerLocationID),
    typeof (ARRegister.curyID),
    typeof (ARRegister.curyOrigDocAmt),
    typeof (ARRegister.curyDocBal),
    typeof (ARRegister.status),
    typeof (ARRegister.dueDate)
  })
  {
  }

  /// <summary>
  /// Specialized selector for ARInvoice RefNbr.<br />
  /// By default, defines the following set of columns for the selector:<br />
  /// ARInvoice.refNbr,ARInvoice.docDate, ARInvoice.finPeriodID,<br />
  /// ARInvoice.customerID, ARInvoice.customerID_Customer_acctName,<br />
  /// ARInvoice.customerLocationID, ARInvoice.curyID, ARInvoice.curyOrigDocAmt,<br />
  /// ARInvoice.curyDocBal,ARInvoice.status, ARInvoice.dueDate, ARInvoice.invoiceNbr<br />
  /// </summary>
  public class AdjdRefNbrAttribute(Type searchType, params Type[] fieldList) : PXSelectorAttribute(searchType, fieldList)
  {
    /// <summary>Ctor</summary>
    /// <param name="SearchType">Must be IBqlSearch, returning ARInvoice.refNbr</param>
    public AdjdRefNbrAttribute(Type SearchType)
      : this(SearchType, typeof (ARRegister.branchID), typeof (ARRegister.refNbr), typeof (ARRegister.docDate), typeof (ARRegister.finPeriodID), typeof (ARRegister.customerID), typeof (ARRegister.customerLocationID), typeof (ARRegister.curyID), typeof (ARRegister.curyOrigDocAmt), typeof (ARRegister.curyDocBal), typeof (ARRegister.status), typeof (ARRegister.dueDate), typeof (ARAdjust.ARInvoice.invoiceNbr), typeof (ARRegister.docDesc))
    {
    }

    protected virtual bool IsReadDeletedSupported => false;
  }

  public class AdjdLineNbrAttribute : PXSelectorAttribute
  {
    public const int emptyLineNbrID = 0;

    public AdjdLineNbrAttribute()
      : base(typeof (Search2<ARTran.lineNbr, InnerJoin<ARInvoice, On<ARInvoice.docType, Equal<ARTran.tranType>, And<ARInvoice.refNbr, Equal<ARTran.refNbr>>>>, Where<ARTran.tranType, Equal<Optional<ARAdjust.adjdDocType>>, And<ARTran.refNbr, Equal<Optional<ARAdjust.adjdRefNbr>>>>>), new Type[8]
      {
        typeof (ARTran.sortOrder),
        typeof (ARTran.inventoryID),
        typeof (ARTran.tranDesc),
        typeof (ARTran.projectID),
        typeof (ARTran.taskID),
        typeof (ARTran.costCodeID),
        typeof (ARTran.accountID),
        typeof (ARTran.curyTranBal)
      })
    {
      this.SubstituteKey = typeof (ARTran.sortOrder);
      this._UnconditionalSelect = (BqlCommand) new Search<ARTran.lineNbr, Where<ARTran.tranType, Equal<Current<ARAdjust.adjdDocType>>, And<ARTran.refNbr, Equal<Current<ARAdjust.adjdRefNbr>>, And<ARTran.lineNbr, Equal<Required<ARTran.lineNbr>>>>>>();
    }

    public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (object.Equals(e.NewValue, (object) 0) || object.Equals(e.NewValue, (object) 0.ToString()))
        ((CancelEventArgs) e).Cancel = true;
      else
        base.FieldVerifying(sender, e);
    }

    public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (object.Equals(e.NewValue, (object) 0) || object.Equals(e.NewValue, (object) 0.ToString()))
        e.NewValue = (object) 0;
      else
        base.SubstituteKeyFieldUpdating(sender, e);
    }

    public virtual void SubstituteKeyFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      if (!object.Equals(e.ReturnValue, (object) 0) && !object.Equals(e.ReturnValue, (object) 0.ToString()))
      {
        base.SubstituteKeyFieldSelecting(sender, e);
      }
      else
      {
        PXSelectorAttribute.SubstituteKeyInfo substituteKeyMask = this.getSubstituteKeyMask(sender);
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, substituteKeyMask.Length, new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(), substituteKeyMask.Mask, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
        if (e.ReturnValue == null || !(e.ReturnValue.GetType() != typeof (string)))
          return;
        e.ReturnValue = (object) e.ReturnValue.ToString();
      }
    }

    public virtual void SubstituteKeyCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
    {
      if (PXDBOperationExt.Option(e.Operation) != 80 /*0x50*/)
        return;
      e.Expr = (SQLExpression) new Column(((PXEventSubscriberAttribute) this).FieldName, (Table) new SimpleTable(((PXEventSubscriberAttribute) this).BqlTable, (string) null), (PXDbType) 100);
    }
  }

  public class AdjdLineNbrRestrictor(Type where, string message, params Type[] pars) : 
    PXRestrictorAttribute(where, message, pars)
  {
    public virtual void Verify(
      PXCache sender,
      PXFieldVerifyingEventArgs e,
      bool IsErrorValueRequired)
    {
      int? newValue = e.NewValue as int?;
      if (!newValue.HasValue)
        return;
      int? nullable = newValue;
      int num = 0;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        return;
      base.Verify(sender, e, IsErrorValueRequired);
    }
  }

  /// <summary>
  /// Specialized for ARInvoices version of the <see cref="T:PX.Objects.CS.AutoNumberAttribute" /><br />
  /// It defines how the new numbers are generated for the AR Invoice. <br />
  /// References ARInvoice.docType and ARInvoice.docDate fields of the document,<br />
  /// and also define a link between  numbering ID's defined in AR Setup and ARInvoice types:<br />
  /// namely ARSetup.invoiceNumberingID - for ARInvoice,
  /// ARSetup.adjustmentNumberingID - for ARDebitMemo and ARCreditMemo<br />
  /// ARSetup.finChargeNumberingID - for FinCharges <br />
  /// ARSetup.paymentNumberingID - for CashSale and CashReturn <br />
  /// </summary>
  public class NumberingAttribute : AutoNumberAttribute
  {
    private static string[] _DocTypes
    {
      get
      {
        return new string[8]
        {
          "INV",
          "DRM",
          "CRM",
          "FCH",
          "SMC",
          "CSL",
          "RCS",
          "PPI"
        };
      }
    }

    private static Type[] _SetupFields
    {
      get
      {
        return new Type[8]
        {
          typeof (ARSetup.invoiceNumberingID),
          typeof (ARSetup.debitAdjNumberingID),
          typeof (ARSetup.creditAdjNumberingID),
          null,
          null,
          typeof (ARSetup.invoiceNumberingID),
          typeof (ARSetup.invoiceNumberingID),
          typeof (ARSetup.prepaymentInvoiceNumberingID)
        };
      }
    }

    public static Type GetNumberingIDField(string docType)
    {
      foreach (Tuple<string, Type> tuple in EnumerableExtensions.Zip<string, Type>((IEnumerable<string>) ARInvoiceType.NumberingAttribute._DocTypes, (IEnumerable<Type>) ARInvoiceType.NumberingAttribute._SetupFields))
      {
        if (tuple.Item1 == docType)
          return tuple.Item2;
      }
      return (Type) null;
    }

    public NumberingAttribute()
      : base(typeof (ARInvoice.docType), typeof (ARInvoice.docDate), ARInvoiceType.NumberingAttribute._DocTypes, ARInvoiceType.NumberingAttribute._SetupFields)
    {
    }

    protected NumberingAttribute(
      Type doctypeField,
      Type dateField,
      string[] doctypeValues,
      Type[] setupFields)
      : base(doctypeField, dateField, doctypeValues, setupFields)
    {
    }
  }

  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(ARInvoiceType.Values, ARInvoiceType.Labels)
    {
    }
  }

  public class AdjdListAttribute : PXStringListAttribute
  {
    public AdjdListAttribute()
      : base(new string[4]{ "INV", "DRM", "FCH", "PPI" }, new string[4]
      {
        "Invoice",
        "Debit Memo",
        "Overdue Charge",
        "Prepmt. Invoice"
      })
    {
    }
  }

  /// <summary>
  /// String list with DocType, valid for the tax adjustments.
  /// </summary>
  public class TaxAdjdListAttribute : PXStringListAttribute
  {
    public TaxAdjdListAttribute()
      : base(new string[4]{ "INV", "DRM", "CRM", "FCH" }, new string[4]
      {
        "Invoice",
        "Debit Memo",
        "Credit Memo",
        "Overdue Charge"
      })
    {
    }
  }

  public class AppListAttribute : PXStringListExtAttribute
  {
    public AppListAttribute()
      : base(new string[4]{ "CRM", "PMT", "PPM", "PPI" }, new string[4]
      {
        "Credit Memo",
        "Payment",
        "Prepayment",
        "Prepmt. Invoice"
      }, ARDocType.Values, ARDocType.Labels)
    {
      this.ExclusiveValues = true;
    }
  }

  public class AdjListAttribute : PXStringListExtAttribute
  {
    public AdjListAttribute()
      : base(new string[5]
      {
        "INV",
        "DRM",
        "FCH",
        "REF",
        "PPI"
      }, new string[5]
      {
        "Invoice",
        "Debit Memo",
        "Overdue Charge",
        "Refund",
        "Prepmt. Invoice"
      }, ARDocType.Values, ARDocType.Labels)
    {
      this.ExclusiveValues = true;
    }
  }
}
