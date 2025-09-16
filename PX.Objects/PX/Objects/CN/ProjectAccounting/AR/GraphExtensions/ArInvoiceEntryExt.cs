// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.AR.GraphExtensions.ArInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CN.Common.Services;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.AR.GraphExtensions;

public class ArInvoiceEntryExt : PXGraphExtension<ARInvoiceEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.construction>() && !SiteMapExtension.IsInvoicesScreenId();
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), "Task Type is not valid", new Type[] {typeof (PMTask.type)})]
  protected virtual void _(PX.Data.Events.CacheAttached<ARTran.taskID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARTran> args)
  {
    ARTran row = args.Row;
    if (row == null)
      return;
    object taskId = (object) row.TaskID;
    try
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARTran>>) args).Cache.RaiseFieldVerifying<ARTran.taskID>((object) row, ref taskId);
    }
    catch (PXSetPropertyException ex)
    {
      row.TaskID = new int?();
    }
  }

  protected virtual PMProforma GetPMProformaOfCurrentARInvoice()
  {
    return PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.aRInvoiceDocType, Equal<Current<ARInvoice.docType>>, And<PMProforma.aRInvoiceRefNbr, Equal<Current<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  [PXOverride]
  public virtual IEnumerable PayInvoice(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseHandler)
  {
    if (((PXSelectBase<ARInvoice>) this.Base.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.Released;
      if (nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.ProformaExists;
        if (nullable.GetValueOrDefault())
        {
          PMProforma currentArInvoice = this.GetPMProformaOfCurrentARInvoice();
          if (currentArInvoice != null)
          {
            nullable = currentArInvoice.Corrected;
            if (nullable.GetValueOrDefault())
              throw new PXSetPropertyException("The system cannot prepare payment for the invoice {0} because the related pro forma invoice {1} is under correction. To be able to prepare a payment for the invoice {0}, release the pro forma invoice {1} on the Pro Forma Invoices (PM307000) form first.", new object[2]
              {
                (object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current.RefNbr,
                (object) currentArInvoice.RefNbr
              });
          }
        }
      }
    }
    return baseHandler(adapter);
  }

  [PXOverride]
  public virtual IEnumerable ReverseInvoice(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseHandler)
  {
    if (((PXSelectBase<ARInvoice>) this.Base.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.ProformaExists;
      if (nullable.GetValueOrDefault())
      {
        PMProforma currentArInvoice = this.GetPMProformaOfCurrentARInvoice();
        if (currentArInvoice != null)
        {
          nullable = currentArInvoice.Corrected;
          if (nullable.GetValueOrDefault())
          {
            if (currentArInvoice.Status == "C")
              return this.SkipUserApprovalDialogOnReverseInvoice(((PXSelectBase<ARInvoice>) this.Base.Document).Current) || this.Base.AskUserApprovalIfReversingDocumentAlreadyExists(((PXSelectBase<ARInvoice>) this.Base.Document).Current) ? baseHandler(adapter) : adapter.Get();
            throw new PXSetPropertyException("The system cannot reverse the invoice {0} or apply it to a credit memo because the related pro forma invoice {1} is under correction. To be able to reverse the invoice {0} or apply it to a credit memo, release the pro forma invoice {1} on the Pro Forma Invoices (PM307000) form first.", new object[2]
            {
              (object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current.RefNbr,
              (object) currentArInvoice.RefNbr
            });
          }
          if (!this.AskUserApprovalIfProformaExistAndClosed(currentArInvoice, ((PXSelectBase<ARInvoice>) this.Base.Document).Current))
            return adapter.Get();
        }
      }
    }
    return baseHandler(adapter);
  }

  [PXOverride]
  public virtual IEnumerable ReverseInvoiceAndApplyToMemo(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> baseHandler)
  {
    if (((PXSelectBase<ARInvoice>) this.Base.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<ARInvoice>) this.Base.Document).Current.ProformaExists;
      if (nullable.GetValueOrDefault())
      {
        PMProforma currentArInvoice = this.GetPMProformaOfCurrentARInvoice();
        if (currentArInvoice != null)
        {
          nullable = currentArInvoice.Corrected;
          if (nullable.GetValueOrDefault())
            throw new PXSetPropertyException("The system cannot reverse the invoice {0} or apply it to a credit memo because the related pro forma invoice {1} is under correction. To be able to reverse the invoice {0} or apply it to a credit memo, release the pro forma invoice {1} on the Pro Forma Invoices (PM307000) form first.", new object[2]
            {
              (object) ((PXSelectBase<ARInvoice>) this.Base.Document).Current.RefNbr,
              (object) currentArInvoice.RefNbr
            });
          if (!this.AskUserApprovalIfProformaExistAndClosed(currentArInvoice, ((PXSelectBase<ARInvoice>) this.Base.Document).Current))
            return adapter.Get();
        }
      }
    }
    return baseHandler(adapter);
  }

  protected virtual bool AskUserApprovalIfProformaExistAndClosed(
    PMProforma proforma,
    ARInvoice invoice)
  {
    if (proforma?.Status != "C" || !invoice.Released.GetValueOrDefault())
      return true;
    return ((PXSelectBase) this.Base.Document).View.Ask((object) null, "Manual AR Document Reversal", PXMessages.LocalizeFormatNoPrefix("If you reverse the document manually, you will not be able to create another account receivable invoice for the {0} pro forma invoice from which the current document originates. To reverse the accounts receivable invoice along with the pro forma invoice, use the Correct action on the Pro Forma Invoices (PM307000) form instead.\r\nAre you sure you want to proceed with manual reversal?", new object[1]
    {
      (object) proforma.RefNbr
    }), (MessageButtons) 4, (MessageIcon) 3) == 6;
  }

  protected virtual bool SkipUserApprovalDialogOnReverseInvoice(ARInvoice invoice)
  {
    if (!invoice.IsOriginalRetainageDocument())
      return false;
    Decimal? curyRetainageTotal = invoice.CuryRetainageTotal;
    Decimal? retainageUnreleasedAmt = invoice.CuryRetainageUnreleasedAmt;
    if (!(curyRetainageTotal.GetValueOrDefault() == retainageUnreleasedAmt.GetValueOrDefault() & curyRetainageTotal.HasValue == retainageUnreleasedAmt.HasValue))
      return true;
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, invoice.CustomerID);
    return customer != null && customer.PaymentsByLinesAllowed.GetValueOrDefault();
  }
}
