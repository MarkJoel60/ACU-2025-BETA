// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CASetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CASetupMaint : PXGraph<CASetupMaint>
{
  public PXSelect<CASetup> CASetupRecord;
  public PXSave<CASetup> Save;
  public PXCancel<CASetup> Cancel;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXSelect<CASetupApproval> Approval;

  public CASetupMaint()
  {
    PX.Objects.GL.GLSetup current = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
  }

  protected virtual void CASetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CASetup row = (CASetup) e.Row;
    bool flag = row != null && row.InvoiceFilterByDate.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<CASetup.daysBeforeInvoiceDiscountDate>(sender, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<CASetup.daysBeforeInvoiceDueDate>(sender, (object) null, flag);
    PXUIFieldAttribute.SetEnabled<CASetup.daysAfterInvoiceDueDate>(sender, (object) null, flag);
    PXUIFieldAttribute.SetRequired<CASetup.daysBeforeInvoiceDiscountDate>(sender, flag);
    PXUIFieldAttribute.SetRequired<CASetup.daysBeforeInvoiceDueDate>(sender, flag);
    PXUIFieldAttribute.SetRequired<CASetup.daysAfterInvoiceDueDate>(sender, flag);
  }

  protected virtual void CASetup_InvoiceFilterByDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CASetup row = (CASetup) e.Row;
    if (row == null || row.InvoiceFilterByDate.GetValueOrDefault())
      return;
    row.DaysBeforeInvoiceDiscountDate = new int?(0);
    row.DaysBeforeInvoiceDueDate = new int?(0);
    row.DaysAfterInvoiceDueDate = new int?(0);
  }

  protected virtual void CASetup_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CASetup newRow = (CASetup) e.NewRow;
    if (e.NewRow == null || newRow == null)
      return;
    int? nullable = newRow.TransitAcctId;
    if (!nullable.HasValue)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CASetup.transitAcctId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newRow.TransitAcctId
    }));
    if (cashAccount == null)
      return;
    nullable = cashAccount.SubID;
    int num = newRow.TransitSubID.Value;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    Sub sub = PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<CASetup.transitSubID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newRow.TransitSubID
    }));
    sender.RaiseExceptionHandling<CASetup.transitSubID>((object) newRow, (object) sub.SubCD, (Exception) new PXSetPropertyException("Wrong sub account for this account"));
  }

  protected virtual void _(PX.Data.Events.RowPersisted<CASetup> e)
  {
    if (e.TranStatus != null)
      return;
    PXUpdate<Set<CashAccount.receiptTranDaysBefore, Required<CashAccount.receiptTranDaysBefore>, Set<CashAccount.receiptTranDaysAfter, Required<CashAccount.receiptTranDaysAfter>, Set<CashAccount.disbursementTranDaysBefore, Required<CashAccount.disbursementTranDaysBefore>, Set<CashAccount.disbursementTranDaysAfter, Required<CashAccount.disbursementTranDaysAfter>, Set<CashAccount.allowMatchingCreditMemo, Required<CashAccount.allowMatchingCreditMemo>, Set<CashAccount.allowMatchingDebitAdjustment, Required<CashAccount.allowMatchingDebitAdjustment>, Set<CashAccount.refNbrCompareWeight, Required<CashAccount.refNbrCompareWeight>, Set<CashAccount.dateCompareWeight, Required<CashAccount.dateCompareWeight>, Set<CashAccount.payeeCompareWeight, Required<CashAccount.payeeCompareWeight>, Set<CashAccount.dateMeanOffset, Required<CashAccount.dateMeanOffset>, Set<CashAccount.dateSigma, Required<CashAccount.dateSigma>, Set<CashAccount.skipVoided, Required<CashAccount.skipVoided>, Set<CashAccount.curyDiffThreshold, Required<CashAccount.curyDiffThreshold>, Set<CashAccount.amountWeight, Required<CashAccount.amountWeight>, Set<CashAccount.emptyRefNbrMatching, Required<CashAccount.emptyRefNbrMatching>, Set<CashAccount.matchThreshold, Required<CashAccount.matchThreshold>, Set<CashAccount.relativeMatchThreshold, Required<CashAccount.relativeMatchThreshold>, Set<CashAccount.invoiceFilterByCashAccount, Required<CashAccount.invoiceFilterByCashAccount>, Set<CashAccount.invoiceFilterByDate, Required<CashAccount.invoiceFilterByDate>, Set<CashAccount.daysBeforeInvoiceDiscountDate, Required<CashAccount.daysBeforeInvoiceDiscountDate>, Set<CashAccount.daysBeforeInvoiceDueDate, Required<CashAccount.daysBeforeInvoiceDueDate>, Set<CashAccount.daysAfterInvoiceDueDate, Required<CashAccount.daysAfterInvoiceDueDate>, Set<CashAccount.invoiceRefNbrCompareWeight, Required<CashAccount.invoiceRefNbrCompareWeight>, Set<CashAccount.invoiceDateCompareWeight, Required<CashAccount.invoiceDateCompareWeight>, Set<CashAccount.invoicePayeeCompareWeight, Required<CashAccount.invoicePayeeCompareWeight>, Set<CashAccount.averagePaymentDelay, Required<CashAccount.averagePaymentDelay>, Set<CashAccount.invoiceDateSigma, Required<CashAccount.invoiceDateSigma>>>>>>>>>>>>>>>>>>>>>>>>>>>>, CashAccount, Where<CashAccount.matchSettingsPerAccount, NotEqual<True>>>.Update((PXGraph) this, new object[27]
    {
      (object) e.Row.ReceiptTranDaysBefore,
      (object) e.Row.ReceiptTranDaysAfter,
      (object) e.Row.DisbursementTranDaysBefore,
      (object) e.Row.DisbursementTranDaysAfter,
      (object) e.Row.AllowMatchingCreditMemo,
      (object) e.Row.AllowMatchingDebitAdjustment,
      (object) e.Row.RefNbrCompareWeight,
      (object) e.Row.DateCompareWeight,
      (object) e.Row.PayeeCompareWeight,
      (object) e.Row.DateMeanOffset,
      (object) e.Row.DateSigma,
      (object) e.Row.SkipVoided,
      (object) e.Row.CuryDiffThreshold,
      (object) e.Row.AmountWeight,
      (object) e.Row.EmptyRefNbrMatching,
      (object) e.Row.MatchThreshold,
      (object) e.Row.RelativeMatchThreshold,
      (object) e.Row.InvoiceFilterByCashAccount,
      (object) e.Row.InvoiceFilterByDate,
      (object) e.Row.DaysBeforeInvoiceDiscountDate,
      (object) e.Row.DaysBeforeInvoiceDueDate,
      (object) e.Row.DaysAfterInvoiceDueDate,
      (object) e.Row.InvoiceRefNbrCompareWeight,
      (object) e.Row.InvoiceDateCompareWeight,
      (object) e.Row.InvoicePayeeCompareWeight,
      (object) e.Row.AveragePaymentDelay,
      (object) e.Row.InvoiceDateSigma
    });
  }

  protected virtual IEnumerable<string> GetEntityTypeScreens()
  {
    return (IEnumerable<string>) new string[2]
    {
      "CA302000",
      "CA304000"
    };
  }

  private CASetupMaint.Definition Definitions
  {
    get
    {
      CASetupMaint.Definition definitions = PXContext.GetSlot<CASetupMaint.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<CASetupMaint.Definition>(PXDatabase.GetSlot<CASetupMaint.Definition, CASetupMaint>(typeof (CASetupMaint.Definition).FullName, this, new Type[1]
        {
          typeof (SiteMap)
        }));
      return definitions;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CASetupApproval.graphType> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CASetupApproval.graphType>, object, object>) e).NewValue = (object) this.Definitions.SiteMapNodes.OrderBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.Title)).FirstOrDefault<PXSiteMapNode>()?.GraphType;
  }

  protected virtual void _(PX.Data.Events.FieldSelecting<CASetupApproval.graphType> e)
  {
    CASetupApproval row = (CASetupApproval) e.Row;
    IOrderedEnumerable<PXSiteMapNode> source = this.Definitions.SiteMapNodes.OrderBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.Title));
    string[] array1 = source.Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.GraphType)).ToArray<string>();
    string[] array2 = source.Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.Title)).ToArray<string>();
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<CASetupApproval.graphType>>) e).ReturnState = (object) PXStringState.CreateInstance(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<CASetupApproval.graphType>>) e).ReturnState, new int?(10), new bool?(true), "graphType", new bool?(false), new int?(-1), (string) null, ((IEnumerable<string>) array1).ToArray<string>(), ((IEnumerable<string>) array2).ToArray<string>(), new bool?(true), ((IEnumerable<string>) array1).First<string>(), (string[]) null);
  }

  private class Definition : IPrefetchable<CASetupMaint>, IPXCompanyDependent
  {
    public List<PXSiteMapNode> SiteMapNodes = new List<PXSiteMapNode>();

    public void Prefetch(CASetupMaint graph)
    {
      this.SiteMapNodes = PXSiteMapProviderExtensions.GetNodes(PXSiteMap.Provider).Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (x => graph.GetEntityTypeScreens().Contains<string>(x.ScreenID))).GroupBy<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (x => x.ScreenID)).Select<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>((Func<IGrouping<string, PXSiteMapNode>, PXSiteMapNode>) (x => x.First<PXSiteMapNode>())).ToList<PXSiteMapNode>();
    }
  }
}
