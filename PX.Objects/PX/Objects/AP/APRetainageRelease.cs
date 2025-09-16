// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APRetainageRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APRetainageRelease : PXGraph<APRetainageRelease>
{
  public PXFilter<APRetainageFilter> Filter;
  public PXCancel<APRetainageFilter> Cancel;
  public PXSelect<APInvoice> APInvoiceDummy;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<APInvoiceExt, APRetainageFilter> DocumentList;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXAction<APRetainageFilter> viewDocument;

  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (this.DocumentList.Current != null)
      PXRedirectHelper.TryRedirect(this.DocumentList.Cache, (object) this.DocumentList.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return adapter.Get();
  }

  protected virtual IEnumerable documentList()
  {
    APRetainageRelease graph = this;
    FbqlSelect<SelectFromBase<APInvoiceExt, TypeArrayOf<IFbqlJoin>.Empty>, APInvoiceExt>.View view = new FbqlSelect<SelectFromBase<APInvoiceExt, TypeArrayOf<IFbqlJoin>.Empty>, APInvoiceExt>.View((PXGraph) graph);
    if (graph.APSetup.Current.MigrationMode.GetValueOrDefault())
      view.WhereAnd<PX.Data.Where<BqlOperand<APInvoice.isMigratedRecord, IBqlBool>.IsEqual<True>>>();
    if (graph.Filter.Current.OrgBAccountID.HasValue)
      view.WhereAnd<Where<APInvoice.branchID, InsideBranchesOf<Current<APRetainageFilter.orgBAccountID>>>>();
    foreach (PXResult<APInvoiceExt> pxResult in view.Select())
    {
      APInvoiceExt apInvoiceExt = (APInvoiceExt) pxResult;
      if ((APRetainageInvoice) PXSelectBase<APRetainageInvoice, PXSelectJoin<APRetainageInvoice, LeftJoin<APTran, On<APRetainageInvoice.paymentsByLinesAllowed, Equal<True>, And<APTran.tranType, Equal<APRetainageInvoice.docType>, And<APTran.refNbr, Equal<APRetainageInvoice.refNbr>, And<APTran.origLineNbr, Equal<Required<APTran.origLineNbr>>>>>>>, Where<APRetainageInvoice.isRetainageDocument, Equal<True>, And<APRetainageInvoice.origDocType, Equal<Required<APInvoice.docType>>, And<APRetainageInvoice.origRefNbr, Equal<Required<APInvoice.refNbr>>, And<APRetainageInvoice.released, NotEqual<True>, PX.Data.And<Where<APRetainageInvoice.paymentsByLinesAllowed, NotEqual<True>, Or<APTran.lineNbr, PX.Data.IsNotNull>>>>>>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object) apInvoiceExt.APTranLineNbr, (object) apInvoiceExt.DocType, (object) apInvoiceExt.RefNbr) == null)
        yield return (object) apInvoiceExt;
    }
  }

  public APRetainageRelease()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    PXUIFieldAttribute.SetVisible<APRetainageFilter.projectID>(this.Filter.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APInvoiceExt.displayProjectID>(this.DocumentList.Cache, (object) null, true);
    OpenPeriodAttribute.SetValidatePeriod<APRetainageFilter.finPeriodID>(this.Filter.Cache, (object) null, this.IsContractBasedAPI || this.IsImport || this.IsExport || this.UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  protected virtual void APRetainageFilter_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is APRetainageFilter row) || PXSelectorAttribute.Select<APRetainageFilter.refNbr>(sender, (object) row, e.NewValue) != null)
      return;
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void APRetainageFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    APRetainageFilter filter = e.Row as APRetainageFilter;
    if (filter == null)
      return;
    PX.Objects.AP.APSetup current1 = this.APSetup.Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.RetainageBillsAutoRelease;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    int num2;
    if (num1 != 0)
    {
      PX.Objects.AP.APSetup current2 = this.APSetup.Current;
      if (current2 == null)
      {
        num2 = 1;
      }
      else
      {
        nullable = current2.MigrationMode;
        num2 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num2 = 0;
    bool isAutoRelease = num2 != 0;
    this.DocumentList.SetProcessDelegate((PXProcessingBase<APInvoiceExt>.ProcessListDelegate) (list =>
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      APInvoiceEntryRetainage extension = instance.GetExtension<APInvoiceEntryRetainage>();
      RetainageOptions retainageOptions = new RetainageOptions()
      {
        DocDate = filter.DocDate,
        MasterFinPeriodID = FinPeriodIDAttribute.CalcMasterPeriodID<APRetainageFilter.finPeriodID>(instance.Caches[typeof (APRetainageFilter)], (object) filter)
      };
      List<APInvoiceExt> list1 = list;
      RetainageOptions retainageOpts = retainageOptions;
      int num3 = isAutoRelease ? 1 : 0;
      extension.ReleaseRetainageProc(list1, retainageOpts, num3 != 0);
    }));
  }

  protected virtual void APInvoiceExt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APInvoiceExt row))
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APInvoice.selected>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<APInvoiceExt.retainageReleasePct>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<APInvoiceExt.curyRetainageReleasedAmt>(sender, (object) row, true);
    if (!(row.Selected ?? true))
      return;
    if (PXUIFieldAttribute.GetErrors(sender, (object) row, PXErrorLevel.Error).Count <= 0)
      return;
    row.Selected = new bool?(false);
    this.DocumentList.Cache.SetStatus((object) row, PXEntryStatus.Updated);
    sender.RaiseExceptionHandling<APInvoice.selected>((object) row, (object) null, (Exception) new PXSetPropertyException("{0} record raised at least one error. Please review the errors.", PXErrorLevel.RowError));
    PXUIFieldAttribute.SetEnabled<APInvoice.selected>(sender, (object) row, false);
  }

  public override bool IsDirty => false;

  public class SingleCurrency : SingleCurrencyGraph<APRetainageRelease, APInvoiceExt>
  {
    public static bool IsActive() => true;
  }
}
