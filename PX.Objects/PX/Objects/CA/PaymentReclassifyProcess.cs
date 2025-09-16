// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentReclassifyProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class PaymentReclassifyProcess : 
  PXGraph<
  #nullable disable
  PaymentReclassifyProcess>,
  IAddARTransaction,
  IAddAPTransaction
{
  public PXCancel<PaymentReclassifyProcess.Filter> Cancel;
  public PXFilter<PaymentReclassifyProcess.Filter> filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<CASplitExt, PaymentReclassifyProcess.Filter> Adjustments;
  public PXAction<PaymentReclassifyProcess.Filter> viewResultDocument;
  public PXSetup<APSetup> apSetup;
  public PXSetup<ARSetup> arSetup;

  public PaymentReclassifyProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PaymentReclassifyProcess.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new PaymentReclassifyProcess.\u003C\u003Ec__DisplayClass8_0();
    ((PXProcessingBase<CASplitExt>) this.Adjustments).SetSelected<CASplitExt.selected>();
    ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = true;
    PXUIFieldAttribute.SetEnabled<CASplitExt.origModule>(((PXSelectBase) this.Adjustments).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<CASplitExt.referenceID>(((PXSelectBase) this.Adjustments).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<CASplitExt.locationID>(((PXSelectBase) this.Adjustments).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<CASplitExt.paymentMethodID>(((PXSelectBase) this.Adjustments).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<CASplitExt.pMInstanceID>(((PXSelectBase) this.Adjustments).Cache, (object) null, true);
    if (((PXSelectBase<ARSetup>) this.arSetup).Current.RequireExtRef.GetValueOrDefault() && ((PXSelectBase<APSetup>) this.apSetup).Current.RequireVendorRef.GetValueOrDefault())
      PXDefaultAttribute.SetPersistingCheck<CASplitExt.extRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) null, (PXPersistingCheck) 1);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass80.branchID = ((PXSelectBase) this.filter).Cache.InternalCurrent is PaymentReclassifyProcess.Filter internalCurrent ? internalCurrent.BranchID : new int?();
    // ISSUE: method pointer
    ((PXProcessingBase<CASplitExt>) this.Adjustments).SetProcessDelegate(new PXProcessingBase<CASplitExt>.ProcessItemDelegate((object) cDisplayClass80, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual IEnumerable ViewResultDocument(PXAdapter adapter)
  {
    CASplitExt current = ((PXSelectBase<CASplitExt>) this.Adjustments).Current;
    if (current != null && current.TranID.HasValue)
    {
      CATran caTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelectJoin<CATran, InnerJoin<PaymentReclassifyProcess.CATranRef, On<PaymentReclassifyProcess.CATranRef.tranID, Equal<CATran.refTranID>, And<PaymentReclassifyProcess.CATranRef.cashAccountID, Equal<CATran.refTranAccountID>>>>, Where<PaymentReclassifyProcess.CATranRef.tranID, Equal<Required<CATran.tranID>>, And<CATran.refSplitLineNbr, Equal<Required<CATran.refSplitLineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) current.TranID,
        (object) current.LineNbr
      }));
      if (caTran != null)
      {
        if (caTran.OrigModule == "AR")
        {
          ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
          ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<ARRegister.refNbr>((object) caTran.OrigRefNbr, new object[1]
          {
            (object) caTran.OrigTranType
          }));
          if (((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current != null)
            throw new PXRedirectRequiredException((PXGraph) instance, "");
        }
        else if (caTran.OrigModule == "AP")
        {
          APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
          ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Search<PX.Objects.AP.APRegister.refNbr>((object) caTran.OrigRefNbr, new object[1]
          {
            (object) caTran.OrigTranType
          }));
          if (((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current != null)
            throw new PXRedirectRequiredException((PXGraph) instance, "");
        }
      }
    }
    return adapter.Get();
  }

  public virtual bool IsDirty => false;

  protected virtual IEnumerable adjustments()
  {
    PaymentReclassifyProcess reclassifyProcess = this;
    PaymentReclassifyProcess.Filter current = ((PXSelectBase<PaymentReclassifyProcess.Filter>) reclassifyProcess.filter).Current;
    List<CASplitExt> caSplitExtList = new List<CASplitExt>();
    if (current != null)
    {
      PXSelectBase<CASplit> pxSelectBase = (PXSelectBase<CASplit>) new PXSelectJoin<CASplit, InnerJoin<CAAdj, On<CASplit.adjTranType, Equal<CAAdj.adjTranType>, And<CASplit.adjRefNbr, Equal<CAAdj.adjRefNbr>>>, InnerJoin<CATran, On<CATran.origModule, Equal<BatchModule.moduleCA>, And<CATran.origTranType, Equal<CASplit.adjTranType>, And<CATran.origRefNbr, Equal<CASplit.adjRefNbr>>>>, InnerJoin<CashAccount, On<CashAccount.branchID, Equal<CASplit.branchID>, And<CashAccount.accountID, Equal<CASplit.accountID>, And<CashAccount.subID, Equal<CASplit.subID>>>>, LeftJoin<PaymentReclassifyProcess.CATranRef, On<PaymentReclassifyProcess.CATranRef.refTranAccountID, Equal<CATran.cashAccountID>, And<PaymentReclassifyProcess.CATranRef.refTranID, Equal<CATran.tranID>, And<PaymentReclassifyProcess.CATranRef.refSplitLineNbr, Equal<CASplit.lineNbr>>>>, LeftJoin<PX.Objects.AR.ARPayment, On<PaymentReclassifyProcess.CATranRef.origTranType, Equal<PX.Objects.AR.ARPayment.docType>, And<PaymentReclassifyProcess.CATranRef.origRefNbr, Equal<PX.Objects.AR.ARPayment.refNbr>, And<PaymentReclassifyProcess.CATranRef.origModule, Equal<BatchModule.moduleAR>>>>, LeftJoin<PX.Objects.AP.APPayment, On<PaymentReclassifyProcess.CATranRef.origTranType, Equal<PX.Objects.AP.APPayment.docType>, And<PaymentReclassifyProcess.CATranRef.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>, And<PaymentReclassifyProcess.CATranRef.origModule, Equal<BatchModule.moduleAP>>>>>>>>>>, Where<CAAdj.entryTypeID, Equal<Current<PaymentReclassifyProcess.Filter.entryTypeID>>>>((PXGraph) reclassifyProcess);
      int? nullable = current.CashAccountID;
      if (nullable.HasValue)
        pxSelectBase.WhereAnd<Where<CAAdj.cashAccountID, Equal<Current<PaymentReclassifyProcess.Filter.cashAccountID>>>>();
      else if (!string.IsNullOrEmpty(current.CuryID))
        pxSelectBase.WhereAnd<Where<CAAdj.curyID, Equal<Current<PaymentReclassifyProcess.Filter.curyID>>>>();
      if (current.EndDate.HasValue)
        pxSelectBase.WhereAnd<Where<CAAdj.tranDate, LessEqual<Current<PaymentReclassifyProcess.Filter.endDate>>>>();
      if (current.StartDate.HasValue)
        pxSelectBase.WhereAnd<Where<CAAdj.tranDate, GreaterEqual<Current<PaymentReclassifyProcess.Filter.startDate>>>>();
      if (!current.IncludeUnreleased.Value)
        pxSelectBase.WhereAnd<Where<CAAdj.released, Equal<boolTrue>>>();
      if (!current.ShowReclassified.Value)
        pxSelectBase.WhereAnd<Where<PaymentReclassifyProcess.CATranRef.tranID, IsNull>>();
      else
        pxSelectBase.WhereAnd<Where<PaymentReclassifyProcess.CATranRef.tranID, IsNotNull>>();
      if (PXAccess.FeatureInstalled<FeaturesSet.branch>())
      {
        nullable = current.CashAccountID;
        if (!nullable.HasValue)
          pxSelectBase.WhereAnd<Where<CashAccount.baseCuryID, EqualBaseCuryID<Current2<PaymentReclassifyProcess.Filter.branchID>>, And<Current2<PaymentReclassifyProcess.Filter.branchID>, IsNotNull>>>();
      }
      int count = 0;
      foreach (PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment> pxResult in pxSelectBase.Select(Array.Empty<object>()))
      {
        CASplitExt caSplitExt1 = new CASplitExt();
        CASplit aSrc1 = PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult);
        CAAdj aAdj = PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult);
        CashAccount aSrc2 = PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult);
        caSplitExt1.CopyFrom(aSrc1);
        caSplitExt1.CopyFrom(aAdj);
        caSplitExt1.TranDescAdj = aAdj.TranDesc;
        caSplitExt1.TranDescSplit = aSrc1.TranDesc;
        caSplitExt1.TranDesc = current.CopyDescriptionfromDetails.GetValueOrDefault() ? aSrc1.TranDesc : aAdj.TranDesc;
        caSplitExt1.CopyFrom(PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult));
        caSplitExt1.CopyFrom(PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult));
        caSplitExt1.CopyFrom(aSrc2);
        if (current.ShowReclassified.GetValueOrDefault())
        {
          if (caSplitExt1.ChildOrigModule == "AR")
            caSplitExt1.CopyFrom(PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult));
          else
            caSplitExt1.CopyFrom(PXResult<CASplit, CAAdj, CATran, CashAccount, PaymentReclassifyProcess.CATranRef, PX.Objects.AR.ARPayment, PX.Objects.AP.APPayment>.op_Implicit(pxResult));
        }
        ++count;
        CASplitExt caSplitExt2 = (CASplitExt) null;
        foreach (CASplitExt caSplitExt3 in ((PXSelectBase) reclassifyProcess.Adjustments).Cache.Inserted)
        {
          if (caSplitExt3.AdjRefNbr == caSplitExt1.AdjRefNbr && caSplitExt3.AdjTranType == caSplitExt1.AdjTranType)
          {
            nullable = caSplitExt3.LineNbr;
            int? lineNbr = caSplitExt1.LineNbr;
            if (nullable.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable.HasValue == lineNbr.HasValue)
              caSplitExt2 = caSplitExt3;
          }
        }
        if (caSplitExt2 == null)
        {
          if (current.ShowReclassified.GetValueOrDefault())
          {
            ((PXSelectBase) reclassifyProcess.Adjustments).Cache.SetStatus((object) caSplitExt1, (PXEntryStatus) 5);
            yield return (object) caSplitExt1;
          }
          else
            yield return (object) ((PXSelectBase<CASplitExt>) reclassifyProcess.Adjustments).Insert(caSplitExt1);
        }
        else
          yield return (object) caSplitExt2;
      }
    }
  }

  protected virtual void CASplitExt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CASplitExt row = (CASplitExt) e.Row;
    if (row == null)
      return;
    bool hasValue = row.ChildTranID.HasValue;
    bool flag1 = row.OrigModule == "AP";
    bool flag2 = row.OrigModule == "AR";
    PXUIFieldAttribute.SetEnabled<CASplitExt.paymentMethodID>(sender, e.Row, !hasValue && flag1 | flag2);
    bool flag3 = false;
    bool? nullable1;
    if (!hasValue & flag2 && !string.IsNullOrEmpty(row.PaymentMethodID))
    {
      nullable1 = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PaymentMethodID
      })).IsAccountNumberRequired;
      flag3 = nullable1.GetValueOrDefault();
    }
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.AccountID,
      (object) row.SubID
    }));
    if (!hasValue)
    {
      PaymentMethodAccount paymentMethodAccount = PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelectReadonly2<PaymentMethodAccount, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethod.isActive, Equal<True>>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<Where2<Where<PaymentMethodAccount.useForAP, Equal<Required<PaymentMethodAccount.useForAP>>, And<PaymentMethodAccount.useForAP, Equal<True>>>, Or<Where<PaymentMethodAccount.useForAR, Equal<Required<PaymentMethodAccount.useForAR>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) cashAccount.CashAccountID,
        (object) flag1,
        (object) flag2
      }));
      int? nullable2;
      if (paymentMethodAccount != null)
      {
        nullable2 = paymentMethodAccount.CashAccountID;
        if (nullable2.HasValue)
        {
          sender.RaiseExceptionHandling<CASplitExt.paymentMethodID>(e.Row, (object) null, (Exception) null);
          sender.RaiseExceptionHandling<CASplitExt.accountID>(e.Row, (object) null, (Exception) null);
          goto label_8;
        }
      }
      string accountCd = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.AccountID
      })).AccountCD;
      sender.RaiseExceptionHandling<CASplitExt.paymentMethodID>(e.Row, (object) null, (Exception) new PXSetPropertyException("There is no active Payment Method which may be used with account '{0}' to create documents for Module '{1}'. Please, check the configuration for the Cash Account '{0}'.", (PXErrorLevel) 2, new object[2]
      {
        (object) cashAccount.CashAccountCD,
        (object) row.OrigModule
      }));
      sender.RaiseExceptionHandling<CASplitExt.accountID>(e.Row, (object) accountCd, (Exception) new PXSetPropertyException("There is no active Payment Method which may be used with account '{0}' to create documents for Module '{1}'. Please, check the configuration for the Cash Account '{0}'.", (PXErrorLevel) 2, new object[2]
      {
        (object) accountCd,
        (object) row.OrigModule
      }));
label_8:
      APSetup apSetup = PXResultset<APSetup>.op_Implicit(PXSelectBase<APSetup, PXSelect<APSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (string.IsNullOrEmpty(row.ExtRefNbr))
      {
        nullable1 = row.Selected;
        if (nullable1.GetValueOrDefault())
        {
          if (flag2)
          {
            nullable1 = arSetup.RequireExtRef;
            if (nullable1.GetValueOrDefault())
              goto label_14;
          }
          if (flag1)
          {
            nullable1 = apSetup.RequireVendorRef;
            if (!nullable1.GetValueOrDefault())
              goto label_15;
          }
          else
            goto label_15;
label_14:
          sender.RaiseExceptionHandling<CASplitExt.extRefNbr>((object) row, (object) row.ExtRefNbr, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
          {
            (object) typeof (CASplitExt.extRefNbr).Name
          }));
          goto label_16;
        }
      }
label_15:
      sender.RaiseExceptionHandling<CASplitExt.extRefNbr>((object) row, (object) null, (Exception) null);
label_16:
      nullable2 = row.ReferenceID;
      if (!nullable2.HasValue)
      {
        nullable1 = row.Selected;
        if (nullable1.GetValueOrDefault())
        {
          sender.RaiseExceptionHandling<CASplitExt.referenceID>((object) row, (object) row.ReferenceID, (Exception) new PXException("'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.referenceID>(sender)
          }));
          goto label_20;
        }
      }
      sender.RaiseExceptionHandling<CASplitExt.referenceID>((object) row, (object) null, (Exception) null);
    }
label_20:
    PXUIFieldAttribute.SetEnabled<CASplitExt.pMInstanceID>(sender, e.Row, flag3);
  }

  protected virtual void CASplitExt_OrigModule_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CASplitExt row = (CASplitExt) e.Row;
    if (row == null)
      return;
    if (row.DrCr == "C")
    {
      e.NewValue = (object) "AP";
    }
    else
    {
      if (!(row.DrCr == "D"))
        return;
      e.NewValue = (object) "AR";
    }
  }

  protected virtual void CASplitExt_OrigModule_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CASplitExt row = (CASplitExt) e.Row;
    bool? showReclassified = ((PXSelectBase<PaymentReclassifyProcess.Filter>) this.filter).Current.ShowReclassified;
    bool flag = false;
    if (!(showReclassified.GetValueOrDefault() == flag & showReclassified.HasValue))
      return;
    sender.SetDefaultExt<CASplitExt.referenceID>(e.Row);
    sender.SetDefaultExt<CASplitExt.locationID>(e.Row);
    sender.SetDefaultExt<CASplitExt.paymentMethodID>((object) row);
    sender.SetDefaultExt<CASplitExt.pMInstanceID>(e.Row);
  }

  protected virtual void CASplitExt_ReferenceID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (((PXSelectBase<PaymentReclassifyProcess.Filter>) this.filter).Current == null || !((PXSelectBase<PaymentReclassifyProcess.Filter>) this.filter).Current.ShowReclassified.GetValueOrDefault())
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CASplitExt_ReferenceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CASplitExt row = (CASplitExt) e.Row;
    sender.SetDefaultExt<CASplitExt.locationID>(e.Row);
    sender.SetDefaultExt<CASplitExt.paymentMethodID>(e.Row);
    sender.SetDefaultExt<CASplitExt.pMInstanceID>(e.Row);
  }

  protected virtual void CASplitExt_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<CASplitExt.pMInstanceID>(e.Row);
  }

  protected virtual void CASplitExt_PMInstanceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CASplitExt row = (CASplitExt) e.Row;
  }

  protected virtual void Filter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PaymentReclassifyProcess.Filter row = (PaymentReclassifyProcess.Filter) e.Row;
    if (row == null)
      return;
    bool flag = row.ShowReclassified.Value;
    PXCache cache = ((PXSelectBase) this.Adjustments).Cache;
    PXUIFieldAttribute.SetEnabled<CASplitExt.referenceID>(cache, (object) null, !flag);
    PXUIFieldAttribute.SetEnabled<CASplitExt.extRefNbr>(cache, (object) null, !flag);
    PXUIFieldAttribute.SetEnabled<CASplitExt.locationID>(cache, (object) null, !flag);
    PXUIFieldAttribute.SetEnabled<CASplitExt.paymentMethodID>(cache, (object) null, !flag);
    PXUIFieldAttribute.SetEnabled<CASplitExt.pMInstanceID>(cache, (object) null, !flag);
    PXUIFieldAttribute.SetEnabled<CASplitExt.origModule>(cache, (object) null, !flag);
    PXUIFieldAttribute.SetEnabled<CASplitExt.selected>(cache, (object) null, !flag);
    PXUIFieldAttribute.SetVisible<CASplitExt.childOrigModule>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CASplitExt.childOrigTranType>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<CASplitExt.childOrigRefNbr>(cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<PaymentReclassifyProcess.Filter.curyID>(sender, (object) row, row.CashAccountID.HasValue);
  }

  protected virtual void Filter_ShowReclassified_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PaymentReclassifyProcess.Filter row = (PaymentReclassifyProcess.Filter) e.Row;
    if (row.ShowReclassified.GetValueOrDefault())
    {
      int num = 7;
      DateTime dateTime = row.EndDate.HasValue ? row.EndDate.Value.AddDays((double) -num) : DateTime.Today.AddDays((double) -num);
      sender.SetValueExt<PaymentReclassifyProcess.Filter.startDate>((object) row, (object) dateTime);
    }
    else
      sender.SetValueExt<PaymentReclassifyProcess.Filter.startDate>((object) row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID> e)
  {
    EnumerableExtensions.ForEach<CASplitExt>(GraphHelper.RowCast<CASplitExt>((IEnumerable) ((PXSelectBase<CASplitExt>) this.Adjustments).Select(Array.Empty<object>())), (Action<CASplitExt>) (adjustment => adjustment.Selected = new bool?(false)));
    PaymentReclassifyProcess.Filter row = e.Row;
    if (row == null || !PXAccess.FeatureInstalled<FeaturesSet.branch>())
      return;
    PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<PaymentReclassifyProcess.Filter.branchID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID>>) e).Cache, (object) row) as PX.Objects.GL.Branch;
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID>>) e).Cache.GetValueExt<PaymentReclassifyProcess.Filter.cashAccountID>((object) e.Row) is PXFieldState valueExt) || !(PXSelectorAttribute.Select<PaymentReclassifyProcess.Filter.cashAccountID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID>>) e).Cache, (object) row, valueExt.Value) is CashAccount cashAccount) || !(branch?.BaseCuryID != cashAccount.BaseCuryID) && !cashAccount.RestrictVisibilityWithBranch.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID>>) e).Cache.SetValue<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID>>) e).Cache.SetValueExt<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID>>) e).Cache.SetValuePending<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PaymentReclassifyProcess.Filter, PaymentReclassifyProcess.Filter.branchID>>) e).Cache.RaiseExceptionHandling<PaymentReclassifyProcess.Filter.cashAccountID>((object) row, (object) null, (Exception) null);
  }

  protected virtual void Filter_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PaymentReclassifyProcess.Filter row = (PaymentReclassifyProcess.Filter) e.Row;
    int? cashAccountId = row.CashAccountID;
    int? oldValue = (int?) e.OldValue;
    if (cashAccountId.GetValueOrDefault() == oldValue.GetValueOrDefault() & cashAccountId.HasValue == oldValue.HasValue)
      return;
    sender.SetDefaultExt<PaymentReclassifyProcess.Filter.curyID>((object) row);
  }

  protected virtual void Filter_EntryTypeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PaymentReclassifyProcess.Filter row = (PaymentReclassifyProcess.Filter) e.Row;
    if (!(row.EntryTypeID != (string) e.OldValue))
      return;
    sender.SetDefaultExt<PaymentReclassifyProcess.Filter.cashAccountID>((object) row);
  }

  protected virtual void Filter_CopyDescriptionfromDetails_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<CASplitExt> pxResult in ((PXSelectBase<CASplitExt>) this.Adjustments).Select(Array.Empty<object>()))
    {
      CASplitExt caSplitExt = PXResult<CASplitExt>.op_Implicit(pxResult);
      bool? descriptionfromDetails = (e.Row as PaymentReclassifyProcess.Filter).CopyDescriptionfromDetails;
      caSplitExt.TranDesc = !descriptionfromDetails.GetValueOrDefault() ? caSplitExt.TranDescAdj : caSplitExt.TranDescSplit;
      ((PXSelectBase<CASplitExt>) this.Adjustments).Update(caSplitExt);
    }
  }

  protected static void ReclassifyPaymentProc(PaymentReclassifyProcess graph, CASplitExt aRow)
  {
    graph.VerifyExistingReclassificationEntry(graph, aRow);
    if (aRow.OrigModule == "AR")
      PaymentReclassifyProcess.AddARTransaction(graph, (ICADocSource) aRow, (PX.Objects.CM.CurrencyInfo) null);
    if (!(aRow.OrigModule == "AP"))
      return;
    PaymentReclassifyProcess.AddAPTransaction((IAddAPTransaction) graph, (ICADocSource) aRow, (PX.Objects.CM.CurrencyInfo) null);
  }

  protected virtual void VerifyExistingReclassificationEntry(
    PaymentReclassifyProcess graph,
    CASplitExt aRow)
  {
    if (((IQueryable<PXResult<PaymentReclassifyProcess.CATranRef>>) PXSelectBase<PaymentReclassifyProcess.CATranRef, PXViewOf<PaymentReclassifyProcess.CATranRef>.BasedOn<SelectFromBase<PaymentReclassifyProcess.CATranRef, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PaymentReclassifyProcess.CATranRef.refTranID, IBqlLong>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) aRow.TranID
    })).Any<PXResult<PaymentReclassifyProcess.CATranRef>>())
      throw new PXException("The {0} transaction has already been processed.", new object[1]
      {
        (object) aRow.AdjRefNbr
      });
  }

  public static CATran AddAPTransaction(
    IAddAPTransaction graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo)
  {
    PaymentReclassifyProcess.CheckAPTransaction(parameters);
    return PaymentReclassifyProcess.AddAPTransaction(graph, parameters, aCuryInfo, (IList<ICADocAdjust>) null, true);
  }

  public static void CheckAPTransaction(ICADocSource parameters)
  {
    if (!(parameters.OrigModule == "AP"))
      return;
    PXCache<CASplitExt> pxCache = GraphHelper.Caches<CASplitExt>((PXGraph) PXGraph.CreateInstance<PaymentReclassifyProcess>());
    if (!parameters.BAccountID.HasValue)
      throw new PXRowPersistingException(typeof (CASplitExt.referenceID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.referenceID>((PXCache) pxCache)
      });
    if (!parameters.LocationID.HasValue)
      throw new PXRowPersistingException(typeof (CASplitExt.locationID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.locationID>((PXCache) pxCache)
      });
    if (string.IsNullOrEmpty(parameters.PaymentMethodID))
      throw new PXRowPersistingException(typeof (CASplitExt.paymentMethodID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.paymentMethodID>((PXCache) pxCache)
      });
    if (PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.Select((PXGraph) PXGraph.CreateInstance<APPaymentEntry>(), new object[1]
    {
      (object) parameters.BAccountID
    })) == null)
      throw new PXRowPersistingException(typeof (CASplitExt.referenceID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.referenceID>((PXCache) pxCache)
      });
  }

  public static CATran AddAPTransaction(
    IAddAPTransaction graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IList<ICADocAdjust> aAdjustments,
    bool aOnHold)
  {
    if (parameters.OrigModule != "AP")
      return (CATran) null;
    APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
    ((PXSelectBase) instance.Document).View.Answer = (WebDialogResult) 7;
    PX.Objects.AP.APPayment doc = graph.InitializeAPPayment(instance, parameters, aCuryInfo, aAdjustments, aOnHold);
    graph.InitializeCurrencyInfo(instance, parameters, aCuryInfo, doc);
    if (aAdjustments != null)
    {
      foreach (ICADocAdjust aAdjustment in (IEnumerable<ICADocAdjust>) aAdjustments)
        graph.InitializeAPAdjustment(instance, aAdjustment);
    }
    ((PXAction) instance.Save).Press();
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Current<PX.Objects.AP.APPayment.cATranID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
  }

  public static CATran AddARTransaction(
    PaymentReclassifyProcess graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo)
  {
    PaymentReclassifyProcess.CheckARTransaction(parameters);
    return PaymentReclassifyProcess.AddARTransaction((IAddARTransaction) graph, parameters, aCuryInfo, (IEnumerable<ICADocAdjust>) null, true);
  }

  public static void CheckARTransaction(ICADocSource parameters)
  {
    if (!(parameters.OrigModule == "AR"))
      return;
    PXCache<CASplitExt> pxCache = GraphHelper.Caches<CASplitExt>((PXGraph) PXGraph.CreateInstance<PaymentReclassifyProcess>());
    if (!parameters.CashAccountID.HasValue)
      throw new PXRowPersistingException(typeof (CASplitExt.cashAccountID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.cashAccountID>((PXCache) pxCache)
      });
    if (!parameters.BAccountID.HasValue)
      throw new PXRowPersistingException(typeof (CASplitExt.referenceID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.referenceID>((PXCache) pxCache)
      });
    if (!parameters.LocationID.HasValue)
      throw new PXRowPersistingException(typeof (CASplitExt.locationID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.locationID>((PXCache) pxCache)
      });
    if (string.IsNullOrEmpty(parameters.PaymentMethodID))
      throw new PXRowPersistingException(typeof (CASplitExt.paymentMethodID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.paymentMethodID>((PXCache) pxCache)
      });
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    if (!parameters.PMInstanceID.HasValue)
    {
      if (PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) parameters.PaymentMethodID
      })).IsAccountNumberRequired.GetValueOrDefault())
        throw new PXRowPersistingException(typeof (CASplitExt.pMInstanceID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.pMInstanceID>((PXCache) pxCache)
        });
    }
    if (PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) parameters.BAccountID
    })) == null)
      throw new PXRowPersistingException(typeof (CASplitExt.referenceID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CASplitExt.referenceID>((PXCache) pxCache)
      });
  }

  public static CATran AddARTransaction(
    IAddARTransaction graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IEnumerable<ICADocAdjust> aAdjustments,
    bool aOnHold)
  {
    List<ARAdjust> aAdjustments1 = new List<ARAdjust>();
    if (aAdjustments != null)
    {
      foreach (ICADocAdjust aAdjustment in aAdjustments)
      {
        ARAdjust arAdjust = new ARAdjust();
        arAdjust.AdjdDocType = aAdjustment.AdjdDocType;
        arAdjust.AdjdRefNbr = aAdjustment.AdjdRefNbr;
        arAdjust.CuryAdjgDiscAmt = aAdjustment.CuryAdjgDiscAmt;
        arAdjust.CuryAdjgWOAmt = aAdjustment.CuryAdjgWhTaxAmt;
        arAdjust.AdjdCuryRate = aAdjustment.AdjdCuryRate;
        if (aAdjustment.CuryAdjgAmount.HasValue)
          arAdjust.CuryAdjgAmt = aAdjustment.CuryAdjgAmount;
        arAdjust.CuryAdjgDiscAmt = aAdjustment.CuryAdjgDiscAmt;
        arAdjust.CuryAdjgWOAmt = aAdjustment.CuryAdjgWhTaxAmt;
        aAdjustments1.Add(arAdjust);
      }
    }
    return PaymentReclassifyProcess.AddARTransaction(graph, parameters, aCuryInfo, (IEnumerable<ARAdjust>) aAdjustments1, aOnHold);
  }

  public static CATran AddARTransaction(
    IAddARTransaction graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IEnumerable<ARAdjust> aAdjustments,
    bool aOnHold)
  {
    if (parameters.OrigModule != "AR")
      return (CATran) null;
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    PX.Objects.AR.ARPayment doc = graph.InitializeARPayment(instance, parameters, aCuryInfo, aOnHold);
    graph.InitializeCurrencyInfo(instance, parameters, aCuryInfo, doc);
    Decimal curyAppliedAmt = 0M;
    if (aAdjustments != null)
    {
      foreach (ARAdjust aAdjustment in aAdjustments)
        curyAppliedAmt = graph.InitializeARAdjustment(instance, aAdjustment, curyAppliedAmt);
    }
    ((PXAction) instance.Save).Press();
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Current<PX.Objects.AR.ARPayment.cATranID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()));
  }

  public virtual PX.Objects.AR.ARPayment InitializeARPayment(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    bool aOnHold)
  {
    return AddARTransactionHelper.InitializeARPayment(graph, parameters, aCuryInfo, aOnHold);
  }

  public virtual void InitializeCurrencyInfo(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AR.ARPayment doc)
  {
    AddARTransactionHelper.InitializeCurrencyInfo(graph, parameters, aCuryInfo, doc);
  }

  public virtual Decimal InitializeARAdjustment(
    ARPaymentEntry graph,
    ARAdjust adjustment,
    Decimal curyAppliedAmt)
  {
    return AddARTransactionHelper.InitializeARAdjustment(graph, adjustment, curyAppliedAmt);
  }

  public virtual PX.Objects.AP.APPayment InitializeAPPayment(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IList<ICADocAdjust> aAdjustments,
    bool aOnHold)
  {
    return AddAPTransactionHelper.InitializeAPPayment(graph, parameters, aCuryInfo, aAdjustments, aOnHold);
  }

  public virtual void InitializeCurrencyInfo(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AP.APPayment doc)
  {
    AddAPTransactionHelper.InitializeCurrencyInfo(graph, parameters, aCuryInfo, doc);
  }

  public virtual APAdjust InitializeAPAdjustment(APPaymentEntry graph, ICADocAdjust adjustment)
  {
    return AddAPTransactionHelper.InitializeAPAdjustment(graph, adjustment);
  }

  [Serializable]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _EntryTypeID;
    protected string _CuryID;
    protected DateTime? _StartDate;
    protected DateTime? _EndDate;
    protected bool? _ShowSummary;
    protected bool? _IncludeUnreleased;
    protected bool? _ShowReclassified;

    [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.branch>>))]
    [Branch(null, null, true, true, true)]
    public virtual int? BranchID { get; set; }

    [PXDBString(10, IsUnicode = true)]
    [PXDefault(typeof (Search<CASetup.unknownPaymentEntryTypeID>))]
    [PXSelector(typeof (Search<CAEntryType.entryTypeId, Where<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.useToReclassifyPayments, Equal<True>>>>), DescriptionField = typeof (CAEntryType.descr))]
    [PXUIField]
    public virtual string EntryTypeID
    {
      get => this._EntryTypeID;
      set => this._EntryTypeID = value;
    }

    [CashAccount(typeof (PaymentReclassifyProcess.Filter.branchID), typeof (Search2<CashAccount.cashAccountID, InnerJoin<CashAccountETDetail, On<CashAccount.cashAccountID, Equal<CashAccountETDetail.cashAccountID>>>, Where<CashAccountETDetail.entryTypeID, Equal<Current<PaymentReclassifyProcess.Filter.entryTypeID>>, And<Where<CashAccount.branchID, Equal<Current<PaymentReclassifyProcess.Filter.branchID>>, Or<CashAccount.restrictVisibilityWithBranch, Equal<False>, Or<Not<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>>>>>>))]
    public virtual int? CashAccountID { get; set; }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField(DisplayName = "Currency", Enabled = false)]
    [PXDefault(typeof (Search<CashAccount.curyID, Where<CashAccount.cashAccountID, Equal<Current<PaymentReclassifyProcess.Filter.cashAccountID>>>>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBDate]
    [PXUIField]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Summary", Visible = false)]
    public virtual bool? ShowSummary
    {
      get => this._ShowSummary;
      set => this._ShowSummary = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Unreleased", Visible = false)]
    public virtual bool? IncludeUnreleased
    {
      get => this._IncludeUnreleased;
      set => this._IncludeUnreleased = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Reclassified", Visible = true)]
    public virtual bool? ShowReclassified
    {
      get => this._ShowReclassified;
      set => this._ShowReclassified = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Copy Description from Details", Visible = true)]
    public virtual bool? CopyDescriptionfromDetails { get; set; }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.branchID>
    {
    }

    public abstract class entryTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.entryTypeID>
    {
    }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.cashAccountID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.curyID>
    {
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.startDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.endDate>
    {
    }

    public abstract class showSummary : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.showSummary>
    {
    }

    public abstract class includeUnreleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.includeUnreleased>
    {
    }

    public abstract class showReclassified : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.showReclassified>
    {
    }

    public abstract class copyDescriptionfromDetails : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PaymentReclassifyProcess.Filter.copyDescriptionfromDetails>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class CATranRef : CATran
  {
    public new abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.cashAccountID>
    {
    }

    public new abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.tranID>
    {
    }

    public new abstract class refTranAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.refTranAccountID>
    {
    }

    public new abstract class refTranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.refTranID>
    {
    }

    public new abstract class refSplitLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.refSplitLineNbr>
    {
    }

    public new abstract class origModule : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.origModule>
    {
    }

    public new abstract class origTranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.origTranType>
    {
    }

    public new abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PaymentReclassifyProcess.CATranRef.origRefNbr>
    {
    }
  }
}
