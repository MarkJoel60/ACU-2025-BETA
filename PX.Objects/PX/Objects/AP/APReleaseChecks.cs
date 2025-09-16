// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APReleaseChecks
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APReleaseChecks : PXGraph<APReleaseChecks>
{
  public PXFilter<ReleaseChecksFilter> Filter;
  public PXCancel<ReleaseChecksFilter> Cancel;
  public PXAction<ReleaseChecksFilter> ViewDocument;
  public ToggleCurrency<ReleaseChecksFilter> CurrencyView;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<APPayment, ReleaseChecksFilter, Where<boolTrue, Equal<boolTrue>>, PX.Data.OrderBy<Desc<APPayment.selected>>> APPaymentList;
  [Obsolete("This item is not used anymore")]
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyinfo;
  [Obsolete("This item is not used anymore")]
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXAction<ReleaseChecksFilter> Release;
  public PXAction<ReleaseChecksFilter> Reprint;
  public PXAction<ReleaseChecksFilter> VoidReprint;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Optional<ReleaseChecksFilter.payAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Optional<ReleaseChecksFilter.payTypeID>>>>> cashaccountdetail;
  private bool cleared;

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (this.APPaymentList.Current != null)
      PXRedirectHelper.TryRedirect(this.APPaymentList.Cache, (object) this.APPaymentList.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return adapter.Get();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  public IEnumerable release(PXAdapter a) => a.Get();

  [PXProcessButton]
  [PXUIField(DisplayName = "Reprint", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public IEnumerable reprint(PXAdapter a) => a.Get();

  [PXProcessButton]
  [PXUIField(DisplayName = "Reprint with New Number", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  public IEnumerable voidReprint(PXAdapter a) => a.Get();

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [APDocType.ReleaseChecksList]
  protected virtual void APPayment_DocType_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<PX.Objects.CS.FeaturesSet.branch>>.Or<FeatureInstalled<PX.Objects.CS.FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APPayment.branchID> e)
  {
  }

  [Obsolete("The method is obsolete after AC-80359 fix")]
  public virtual void SetPreloaded(APPrintChecks graph)
  {
    ReleaseChecksFilter copy = PXCache<ReleaseChecksFilter>.CreateCopy(this.Filter.Current);
    copy.PayAccountID = graph.Filter.Current.PayAccountID;
    copy.PayTypeID = graph.Filter.Current.PayTypeID;
    copy.CuryID = graph.Filter.Current.CuryID;
    this.Filter.Cache.Update((object) copy);
    foreach (APPayment apPayment in graph.APPaymentList.Cache.Updated)
    {
      apPayment.Passed = new bool?(true);
      this.APPaymentList.Cache.Update((object) apPayment);
      this.APPaymentList.Cache.SetStatus((object) apPayment, PXEntryStatus.Updated);
    }
    this.APPaymentList.Cache.IsDirty = false;
    this.TimeStamp = graph.TimeStamp;
  }

  public APReleaseChecks()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    PXUIFieldAttribute.SetEnabled(this.APPaymentList.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<APPayment.selected>(this.APPaymentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APPayment.extRefNbr>(this.APPaymentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<APPayment.printCheck>(this.APPaymentList.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisibility<APPayment.printCheck>(this.APPaymentList.Cache, (object) null, PXUIVisibility.Invisible);
  }

  public override void Clear()
  {
    this.cleared = true;
    base.Clear();
  }

  protected virtual IEnumerable appaymentlist()
  {
    if (this.cleared)
    {
      foreach (APRegister apRegister in this.APPaymentList.Cache.Updated)
        apRegister.Passed = new bool?(false);
    }
    return (IEnumerable) PXSelectBase<APPayment, PXSelectJoin<APPayment, InnerJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APPayment.vendorID>>>, Where2<Where<APPayment.released, NotEqual<True>, And<APPayment.hold, NotEqual<True>, And<APPayment.printed, Equal<True>, And<APPayment.docType, NotEqual<APDocType.refund>, And<APPayment.docType, NotEqual<APDocType.voidRefund>, And<APPayment.docType, NotEqual<APDocType.cashReturn>, And<APPayment.cashAccountID, Equal<Current<ReleaseChecksFilter.payAccountID>>, And<APPayment.paymentMethodID, Equal<Current<ReleaseChecksFilter.payTypeID>>, PX.Data.And<Match<Vendor, Current<AccessInfo.userName>>>>>>>>>>>, PX.Data.And<Where<APRegister.dontApprove, Equal<True>, Or<APRegister.approved, Equal<True>>>>>>.Config>.Select((PXGraph) this).RowCast<APPayment>();
  }

  public static void ReleasePayments(List<APPayment> list, string Action)
  {
    APReleaseChecks instance1 = PXGraph.CreateInstance<APReleaseChecks>();
    APPaymentEntry instance2 = PXGraph.CreateInstance<APPaymentEntry>();
    bool flag1 = false;
    bool flag2 = false;
    List<APRegister> list1 = new List<APRegister>(list.Count);
    foreach (APPayment apPayment in list)
    {
      if (apPayment.Passed.GetValueOrDefault())
        instance1.TimeStamp = instance2.TimeStamp = apPayment.tstamp;
      PXProcessing<APPayment>.SetCurrentItem((object) apPayment);
      if (Action == "R")
      {
        try
        {
          instance2.Document.Current = (APPayment) instance2.Document.Search<APPayment.refNbr>((object) apPayment.RefNbr, (object) apPayment.DocType);
          if (instance2.Document.Current?.ExtRefNbr != apPayment.ExtRefNbr)
          {
            APPayment copy = PXCache<APPayment>.CreateCopy(instance2.Document.Current);
            copy.ExtRefNbr = apPayment.ExtRefNbr;
            instance2.Document.Update(copy);
          }
          if (PaymentRefAttribute.PaymentRefMustBeUnique(instance2.paymenttype.Current) && string.IsNullOrEmpty(apPayment.ExtRefNbr))
            throw new PXException("'{0}' cannot be empty.", new object[1]
            {
              (object) PXUIFieldAttribute.GetDisplayName<APPayment.extRefNbr>(instance2.GetPrimaryCache())
            });
          apPayment.IsReleaseCheckProcess = new bool?(true);
          bool? nullable = apPayment.Prebooked;
          if (!nullable.GetValueOrDefault())
            APPrintChecks.AssignNumbersWithNoAdditionalProcessing(instance2, instance2.Document.Current);
          instance2.Save.Press();
          nullable = apPayment.Passed;
          if (nullable.GetValueOrDefault())
            instance2.Document.Current.Passed = new bool?(true);
          list1.Add((APRegister) instance2.Document.Current);
          flag2 = true;
        }
        catch (PXException ex)
        {
          PXProcessing<APPayment>.SetError((Exception) ex);
          list1.Add((APRegister) null);
          flag1 = true;
        }
      }
      if (Action == "D" || Action == "V")
      {
        try
        {
          apPayment.IsPrintingProcess = new bool?(true);
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            CABatch caBatch = (CABatch) PXSelectBase<CABatch, PXSelectJoin<CABatch, InnerJoin<CABatchDetail, On<CABatch.batchNbr, Equal<CABatchDetail.batchNbr>>>, Where<CABatchDetail.origModule, Equal<Required<APRegister.origModule>>, And<CABatchDetail.origDocType, Equal<Required<APPayment.docType>>, And<CABatchDetail.origRefNbr, Equal<Required<APPayment.refNbr>>>>>>.Config>.Select((PXGraph) instance1, (object) apPayment.OrigModule, (object) apPayment.DocType, (object) apPayment.RefNbr);
            if (caBatch != null)
            {
              CABatchEntry instance3 = PXGraph.CreateInstance<CABatchEntry>();
              instance3.Document.Current = caBatch;
              int count = instance3.Details.Select().Count;
              CABatchDetail caBatchDetail = instance3.Details.Locate(new CABatchDetail()
              {
                BatchNbr = instance3.Document.Current.BatchNbr,
                OrigModule = apPayment.OrigModule,
                OrigDocType = apPayment.DocType,
                OrigRefNbr = apPayment.RefNbr,
                OrigLineNbr = new int?(0)
              });
              if (caBatchDetail != null)
              {
                if (count == 1)
                  instance3.Document.Delete(instance3.Document.Current);
                else
                  instance3.Details.Delete(caBatchDetail);
              }
              instance3.Save.Press();
            }
            else
            {
              PXCache cache = instance1.APPaymentList.Cache;
              PXEntityEventBase<APPayment>.Container<APPayment.Events>.Select((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.CancelPrintCheck)).FireOn((PXGraph) instance1, apPayment);
              cache.PersistUpdated((object) apPayment);
              cache.Persisted(false);
            }
            if (((PX.Objects.CA.PaymentMethod) instance2.paymenttype.Select((object) apPayment.PaymentMethodID)).APPrintChecks.GetValueOrDefault() && Action == "D")
            {
              APPayment doc = apPayment;
              ParameterExpression parameterExpression1;
              // ISSUE: method reference
              // ISSUE: method reference
              HashSet<string> checkNbrs = new HashSet<string>((IEnumerable<string>) instance2.Adjustments_Raw.Select((object) doc.DocType, (object) doc.RefNbr).Select<PXResult<APAdjust>, string>(Expression.Lambda<Func<PXResult<APAdjust>, string>>((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (APAdjust.get_StubNbr))), parameterExpression1)));
              ParameterExpression parameterExpression2;
              // ISSUE: method reference
              IEnumerable<CashAccountCheck> items = (IEnumerable<CashAccountCheck>) PXSelectBase<CashAccountCheck, PXViewOf<CashAccountCheck>.BasedOn<SelectFromBase<CashAccountCheck, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select((PXGraph) instance1).Select<PXResult<CashAccountCheck>, CashAccountCheck>(Expression.Lambda<Func<PXResult<CashAccountCheck>, CashAccountCheck>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression2)).Where<CashAccountCheck>((Expression<Func<CashAccountCheck, bool>>) (_ => _.CashAccountID == doc.CashAccountID && _.PaymentMethodID == doc.PaymentMethodID && EnumerableExtensions.IsIn<string>(_.CheckNbr, checkNbrs)));
              instance1.Caches<CashAccountCheck>().DeleteAll((IEnumerable<object>) items);
              PaymentMethodAccount row = instance1.cashaccountdetail.SelectSingle((object) apPayment.CashAccountID, (object) apPayment.PaymentMethodID);
              PXSelectBase<CashAccountCheck, PXSelectReadonly<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Optional<PaymentMethodAccount.cashAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Optional<PaymentMethodAccount.paymentMethodID>>>>, PX.Data.OrderBy<Desc<CashAccountCheck.Tstamp>>>.Config>.Clear((PXGraph) instance1);
              CashAccountCheck cashAccountCheck = (CashAccountCheck) PXSelectBase<CashAccountCheck, PXSelectReadonly<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Optional<PaymentMethodAccount.cashAccountID>>, And<CashAccountCheck.paymentMethodID, Equal<Optional<PaymentMethodAccount.paymentMethodID>>>>, PX.Data.OrderBy<Desc<CashAccountCheck.Tstamp>>>.Config>.SelectSingleBound((PXGraph) instance1, new object[1]
              {
                (object) row
              });
              row.APLastRefNbr = cashAccountCheck?.CheckNbr;
              instance1.cashaccountdetail.Cache.PersistUpdated((object) row);
              instance1.cashaccountdetail.Cache.Persisted(false);
              instance1.Caches<CashAccountCheck>().Persist(PXDBOperation.Delete);
            }
            if (string.IsNullOrEmpty(apPayment.ExtRefNbr))
              instance1.APPaymentList.Cache.SetDefaultExt<APPayment.extRefNbr>((object) apPayment);
            transactionScope.Complete();
            PXProcessing<APPayment>.SetProcessed();
          }
        }
        catch (PXException ex)
        {
          PXProcessing<APPayment>.SetError((Exception) ex);
        }
        list1.Add((APRegister) null);
      }
    }
    if (flag2)
      APDocumentRelease.ReleaseDoc(list1, true);
    if (flag1)
      throw new PXOperationCompletedWithErrorException("One or more documents could not be released.");
  }

  protected virtual void ReleaseChecksFilter_PayAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.Filter.Cache.SetDefaultExt<ReleaseChecksFilter.curyID>(e.Row);
    this.APPaymentList.Cache.Clear();
  }

  protected virtual void ReleaseChecksFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ReleaseChecksFilter oldRow = (ReleaseChecksFilter) e.OldRow;
    if (oldRow.PayAccountID.HasValue && oldRow.PayTypeID != null)
      return;
    ((ReleaseChecksFilter) e.Row).Action = "R";
    if (this.UnattendedMode)
      return;
    sender.SetDefaultExt<ReleaseChecksFilter.branchID>(e.Row);
  }

  protected virtual void ReleaseChecksFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetVisible<ReleaseChecksFilter.curyID>(sender, (object) null, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    ReleaseChecksFilter filter = e.Row as ReleaseChecksFilter;
    if (filter == null)
      return;
    PX.Objects.CA.PaymentMethod paymentMethod = (PX.Objects.CA.PaymentMethod) PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<ReleaseChecksFilter.payTypeID>>>>.Config>.Select((PXGraph) this, (object) filter.PayTypeID);
    this.Reprint.SetEnabled(paymentMethod != null && paymentMethod.PrintOrExport.GetValueOrDefault());
    this.VoidReprint.SetEnabled(paymentMethod != null && paymentMethod.PrintOrExport.GetValueOrDefault());
    List<Tuple<string, string>> list1 = new List<Tuple<string, PXAction>>()
    {
      new Tuple<string, PXAction>("R", (PXAction) this.Release),
      new Tuple<string, PXAction>("D", (PXAction) this.Reprint),
      new Tuple<string, PXAction>("V", (PXAction) this.VoidReprint)
    }.Select(t => new
    {
      ShortCut = t.Item1,
      State = t.Item2.GetState((object) filter) as PXButtonState
    }).Where(t =>
    {
      PXButtonState state = t.State;
      return state != null && state.Enabled;
    }).Select(t => new Tuple<string, string>(t.ShortCut, t.State.DisplayName)).ToList<Tuple<string, string>>();
    string[] array = list1.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>();
    PXStringListAttribute.SetLocalizable<ReleaseChecksFilter.action>(this.Filter.Cache, (object) null, false);
    PXStringListAttribute.SetList<ReleaseChecksFilter.action>(this.Filter.Cache, (object) null, array, list1.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>());
    PXUIFieldAttribute.SetEnabled<ReleaseChecksFilter.action>(this.Filter.Cache, (object) filter, list1.Count > 1);
    if (list1.Count > 0)
    {
      if (filter.Action == null || !((IEnumerable<string>) array).Contains<string>(filter.Action))
        filter.Action = array[0];
    }
    else
      filter.Action = (string) null;
    string action = filter.Action;
    this.APPaymentList.SetProcessEnabled(action != null);
    this.APPaymentList.SetProcessAllEnabled(action != null);
    this.APPaymentList.SetProcessDelegate((PXProcessingBase<APPayment>.ProcessListDelegate) (list => APReleaseChecks.ReleasePayments(list, action)));
  }

  protected virtual void ReleaseChecksFilter_PayTypeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ReleaseChecksFilter.payAccountID>(e.Row);
  }

  protected virtual void APPayment_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is APPayment newRow) || !newRow.Selected.GetValueOrDefault())
      return;
    PXFieldState stateExt = (PXFieldState) sender.GetStateExt<APPayment.extRefNbr>((object) newRow);
    if (stateExt == null || string.IsNullOrEmpty(stateExt.Error))
      return;
    newRow.Selected = new bool?(false);
  }

  protected virtual void APPayment_PrintCheck_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (((APRegister) e.Row).Printed.GetValueOrDefault())
      return;
    ((APRegister) e.Row).Selected = new bool?(true);
  }
}
