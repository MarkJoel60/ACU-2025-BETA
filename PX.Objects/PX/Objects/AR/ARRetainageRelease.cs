// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRetainageRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARRetainageRelease : PXGraph<ARRetainageRelease>
{
  public PXFilter<ARRetainageFilter> Filter;
  public PXCancel<ARRetainageFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ARInvoiceExt, ARRetainageFilter> DocumentList;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXAction<ARRetainageFilter> viewDocument;

  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARInvoiceExt>) this.DocumentList).Current != null)
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.DocumentList).Cache, (object) ((PXSelectBase<ARInvoiceExt>) this.DocumentList).Current, "Document", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  protected virtual IEnumerable documentList()
  {
    ARRetainageRelease graph = this;
    FbqlSelect<SelectFromBase<ARInvoiceExt, TypeArrayOf<IFbqlJoin>.Empty>, ARInvoiceExt>.View view = new FbqlSelect<SelectFromBase<ARInvoiceExt, TypeArrayOf<IFbqlJoin>.Empty>, ARInvoiceExt>.View((PXGraph) graph);
    if (((PXSelectBase<PX.Objects.AR.ARSetup>) graph.ARSetup).Current.MigrationMode.GetValueOrDefault())
      ((PXSelectBase<ARInvoiceExt>) view).WhereAnd<Where<BqlOperand<ARInvoice.isMigratedRecord, IBqlBool>.IsEqual<True>>>();
    int? nullable = ((PXSelectBase<ARRetainageFilter>) graph.Filter).Current.OrgBAccountID;
    if (nullable.HasValue)
      ((PXSelectBase<ARInvoiceExt>) view).WhereAnd<Where<ARInvoice.branchID, InsideBranchesOf<Current<ARRetainageFilter.orgBAccountID>>>>();
    ((PXSelectBase<ARInvoiceExt>) view).WhereAnd<Where<NotExists<Select2<ARRetainageInvoice, InnerJoin<ARTran, On<ARTran.refNbr, Equal<ARRetainageInvoice.refNbr>, And<ARTran.tranType, Equal<ARRetainageInvoice.docType>>>>, Where<ARRetainageInvoice.isRetainageDocument, Equal<True>, And<ARTran.origDocType, Equal<ARInvoiceExt.docType>, And<ARTran.origRefNbr, Equal<ARInvoiceExt.refNbr>, And<ARRetainageInvoice.released, NotEqual<True>, And<Where<ARTran.origLineNbr, Equal<ARInvoiceExt.aRTranLineNbr>, Or<ARTran.origLineNbr, Equal<int0>>>>>>>>>>>>();
    foreach (PXResult<ARInvoiceExt> pxResult in ((PXSelectBase<ARInvoiceExt>) view).SelectWithViewContext(Array.Empty<object>()))
    {
      ARInvoiceExt doc = PXResult<ARInvoiceExt>.op_Implicit(pxResult);
      ARRetainageFilter current = ((PXSelectBase<ARRetainageFilter>) graph.Filter).Current;
      bool flag = true;
      if (!doc.PaymentsByLinesAllowed.GetValueOrDefault())
      {
        nullable = current.ProjectTaskID;
        if (!nullable.HasValue)
        {
          nullable = current.AccountGroupID;
          if (!nullable.HasValue)
          {
            nullable = current.CostCodeID;
            if (!nullable.HasValue)
            {
              nullable = current.InventoryID;
              if (!nullable.HasValue)
                goto label_12;
            }
          }
        }
        flag = graph.SearchProjectTransaction(doc) != null;
      }
label_12:
      if (flag)
        yield return (object) doc;
    }
    PXView.StartRow = 0;
  }

  private ARTran SearchProjectTransaction(ARInvoiceExt doc)
  {
    ARRetainageFilter current = ((PXSelectBase<ARRetainageFilter>) this.Filter).Current;
    List<object> objectList = new List<object>();
    PXSelectJoin<ARTran, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<ARTran.accountID>>>, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>> pxSelectJoin = new PXSelectJoin<ARTran, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<ARTran.accountID>>>, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>((PXGraph) this);
    objectList.Add((object) doc.DocType);
    objectList.Add((object) doc.RefNbr);
    if (current.ProjectTaskID.HasValue)
    {
      ((PXSelectBase<ARTran>) pxSelectJoin).WhereAnd<Where<ARTran.taskID, Equal<Required<ARTran.taskID>>>>();
      objectList.Add((object) current.ProjectTaskID);
    }
    int? nullable = current.AccountGroupID;
    if (nullable.HasValue)
    {
      ((PXSelectBase<ARTran>) pxSelectJoin).WhereAnd<Where<PX.Objects.GL.Account.accountGroupID, Equal<Required<PX.Objects.GL.Account.accountGroupID>>>>();
      objectList.Add((object) current.AccountGroupID);
    }
    nullable = current.CostCodeID;
    if (nullable.HasValue)
    {
      ((PXSelectBase<ARTran>) pxSelectJoin).WhereAnd<Where<ARTran.costCodeID, Equal<Required<ARTran.costCodeID>>>>();
      objectList.Add((object) current.CostCodeID);
    }
    nullable = current.InventoryID;
    if (nullable.HasValue)
    {
      ((PXSelectBase<ARTran>) pxSelectJoin).WhereAnd<Where<ARTran.inventoryID, Equal<Required<ARTran.inventoryID>>>>();
      objectList.Add((object) current.InventoryID);
    }
    return ((PXSelectBase<ARTran>) pxSelectJoin).SelectSingle(objectList.ToArray());
  }

  public ARRetainageRelease()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    OpenPeriodAttribute.SetValidatePeriod<ARRetainageFilter.finPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  protected virtual void ARRetainageFilter_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is ARRetainageFilter row) || PXSelectorAttribute.Select<ARRetainageFilter.refNbr>(sender, (object) row, e.NewValue) != null)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ARRetainageFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARRetainageRelease.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new ARRetainageRelease.\u003C\u003Ec__DisplayClass11_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.filter = e.Row as ARRetainageFilter;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass110.filter == null)
      return;
    bool? nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.RetainageInvoicesAutoRelease;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.MigrationMode;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.isAutoRelease = num != 0;
    // ISSUE: method pointer
    ((PXProcessingBase<ARInvoiceExt>) this.DocumentList).SetProcessDelegate(new PXProcessingBase<ARInvoiceExt>.ProcessListDelegate((object) cDisplayClass110, __methodptr(\u003CARRetainageFilter_RowSelected\u003Eb__0)));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARRetainageFilter> e)
  {
    if (e.Row == null || e.OldRow == null)
      return;
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARRetainageFilter>>) e).Cache.ObjectsEqual<ARRetainageFilter.branchID, ARRetainageFilter.docDate, ARRetainageFilter.finPeriodID, ARRetainageFilter.customerID, ARRetainageFilter.projectID, ARRetainageFilter.refNbr, ARRetainageFilter.showBillsWithOpenBalance>((object) e.Row, (object) e.OldRow) || !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARRetainageFilter>>) e).Cache.ObjectsEqual<ARRetainageFilter.projectTaskID, ARRetainageFilter.accountGroupID, ARRetainageFilter.costCodeID, ARRetainageFilter.inventoryID>((object) e.Row, (object) e.OldRow))
    {
      ((PXSelectBase) this.DocumentList).Cache.Clear();
      ((PXSelectBase) this.DocumentList).View.Clear();
      e.Row.CuryRetainageReleasedAmt = new Decimal?(0M);
    }
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARRetainageFilter>>) e).Cache.ObjectsEqual<ARRetainageFilter.retainageReleasePct>((object) e.Row, (object) e.OldRow) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARRetainageFilter>>) e).Cache.ObjectsEqual<ARRetainageFilter.showBillsWithOpenBalance>((object) e.Row, (object) e.OldRow))
      return;
    Decimal num = 0M;
    foreach (PXResult<ARInvoiceExt> pxResult in ((PXSelectBase<ARInvoiceExt>) this.DocumentList).Select(Array.Empty<object>()))
    {
      ARInvoiceExt arInvoiceExt = PXResult<ARInvoiceExt>.op_Implicit(pxResult);
      ((PXSelectBase) this.DocumentList).Cache.SetValueExt<ARInvoiceExt.retainageReleasePct>((object) arInvoiceExt, (object) e.Row.RetainageReleasePct);
      ((PXSelectBase<ARInvoiceExt>) this.DocumentList).Update(arInvoiceExt);
      num += arInvoiceExt.Selected.GetValueOrDefault() ? arInvoiceExt.CuryRetainageReleasedAmt.GetValueOrDefault() : 0M;
    }
    e.Row.CuryRetainageReleasedAmt = new Decimal?(num);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARRetainageFilter, ARRetainageFilter.customerID> e)
  {
    if (e.NewValue == null || !e.Row.ProjectID.HasValue)
      return;
    object projectId = (object) e.Row.ProjectID;
    try
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARRetainageFilter, ARRetainageFilter.customerID>>) e).Cache.RaiseFieldVerifying<ARRetainageFilter.projectID>((object) e.Row, ref projectId);
    }
    catch (Exception ex)
    {
    }
  }

  protected virtual void ARInvoiceExt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARInvoiceExt row))
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ARInvoice.selected>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<ARInvoiceExt.retainageReleasePct>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<ARInvoiceExt.curyRetainageReleasedAmt>(sender, (object) row, true);
    if (!(row.Selected ?? true))
      return;
    bool? nullable = row.ProformaExists;
    if (nullable.GetValueOrDefault())
    {
      PMProforma pmProforma = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.aRInvoiceDocType, Equal<Current<ARInvoice.docType>>, And<PMProforma.aRInvoiceRefNbr, Equal<Current<ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmProforma != null)
      {
        nullable = pmProforma.Corrected;
        if (nullable.GetValueOrDefault())
          throw new PXSetPropertyException("The system cannot release retainage from the invoice {0} because the related pro forma invoice {1} is under correction. To be able to release the invoice {0}, release the pro forma invoice {1} on the Pro Forma Invoices (PM307000) form first.", new object[2]
          {
            (object) row.RefNbr,
            (object) pmProforma.RefNbr
          });
      }
    }
    if (PXUIFieldAttribute.GetErrors(sender, (object) row, new PXErrorLevel[1]
    {
      (PXErrorLevel) 4
    }).Count <= 0)
      return;
    row.Selected = new bool?(false);
    ((PXSelectBase) this.DocumentList).Cache.SetStatus((object) row, (PXEntryStatus) 1);
    sender.RaiseExceptionHandling<ARInvoice.selected>((object) row, (object) null, (Exception) new PXSetPropertyException("{0} record raised at least one error. Please review the errors.", (PXErrorLevel) 5));
    PXUIFieldAttribute.SetEnabled<ARInvoice.selected>(sender, (object) row, false);
  }

  public virtual bool IsDirty => false;

  public class SingleCurrency : SingleCurrencyGraph<ARRetainageRelease, ARInvoiceExt>
  {
    public static bool IsActive() => true;
  }
}
