// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DraftScheduleMaintASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.DR;

public class DraftScheduleMaintASC606 : PXGraphExtension<DraftScheduleMaint>
{
  public PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<DRSchedule.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<DRSchedule.refNbr>>>>> OriginalDocument;
  public PXAction<DRSchedule> recalculate;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.aSC606>();

  [PXMergeAttributes]
  [DRDocumentSelectorASC606(typeof (DRSchedule.module), typeof (DRSchedule.docType), typeof (DRSchedule.bAccountID))]
  protected virtual void DRSchedule_RefNbr_CacheAttached(PXCache sencer)
  {
  }

  [PXOverride]
  public virtual Decimal GetCuryTotalAmt(
    PXCache sender,
    DRScheduleDetail scheduleDetail,
    Decimal remainingAmount,
    DraftScheduleMaintASC606.GetCuryTotalAmtDelegate baseMethod)
  {
    return remainingAmount;
  }

  protected virtual void DRSchedule_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e,
    PXRowPersisting baseDelegate)
  {
    DRSchedule row = (DRSchedule) e.Row;
    if (e.Operation == 3)
      return;
    switch (row?.Module)
    {
      case "AP":
        baseDelegate.Invoke(sender, e);
        break;
      case "AR":
        this.VerifySchedule(row);
        row.IsRecalculated = new bool?(false);
        break;
      default:
        throw new NotImplementedException();
    }
    if (!(row.Module == "AR") || row.RefNbr == null || !row.IsCustom.GetValueOrDefault())
      return;
    PX.Objects.AR.ARRegister arRegister = ((PXSelectBase<PX.Objects.AR.ARRegister>) this.OriginalDocument).SelectSingle(new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    });
    arRegister.DRSchedCntr = arRegister.LineCntr;
    ((PXSelectBase<PX.Objects.AR.ARRegister>) this.OriginalDocument).Update(arRegister);
    ((PXGraph) this.Base).Clear((PXClearOption) 4);
  }

  private void VerifySchedule(DRSchedule schedule)
  {
    if (schedule.RefNbr == null || schedule.NetTranPrice.GetValueOrDefault() == 0M)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.GetCurrencyInfo<DRSchedule.curyInfoID>(((PXSelectBase) this.Base.Schedule).Cache, (object) schedule);
    bool? nullable1 = schedule.IsCustom;
    Decimal? nullable2;
    Decimal? nullable3;
    if (nullable1.GetValueOrDefault())
    {
      nullable2 = schedule.NetTranPrice;
      nullable3 = schedule.ComponentsTotal;
      if (nullable2.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        throw new PXException("You cannot link the deferral schedule to the document because its Total Component amount ({0} {2}) is greater than the Net Transaction Price ({1} {2}) of the document.", new object[3]
        {
          (object) schedule.ComponentsTotal,
          (object) schedule.NetTranPrice,
          (object) currencyInfo.BaseCuryID
        });
    }
    nullable3 = schedule.NetTranPrice;
    nullable2 = schedule.ComponentsTotal;
    if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
      return;
    nullable1 = schedule.IsOverridden;
    if (nullable1.GetValueOrDefault())
      throw new PXException("After the modification of the schedule linked to the invoice, the Component Total amount of the schedule ({0} {2}) must be equal to the Net Transaction Price ({1} {2}) of the invoice.", new object[3]
      {
        (object) schedule.ComponentsTotal,
        (object) schedule.NetTranPrice,
        (object) currencyInfo.BaseCuryID
      });
  }

  [PXUIField(DisplayName = "Recalculate", Visible = true, Enabled = false)]
  [PXButton]
  public virtual IEnumerable Recalculate(PXAdapter adapter)
  {
    DRSchedule current = ((PXSelectBase<DRSchedule>) this.Base.DocumentProperties).Current;
    if (current.IsOverridden.GetValueOrDefault() && ((PXSelectBase) this.Base.DocumentProperties).View.Ask((object) ((PXSelectBase<DRSchedule>) this.Base.DocumentProperties).Current, "Confirmation", "On recalculating the schedule, manual changes will be discarded. Do you want to continue?", (MessageButtons) 4, (MessageIcon) 2) != 6)
      return adapter.Get();
    current.IsOverridden = new bool?(false);
    current.IsRecalculated = new bool?(false);
    ((PXSelectBase<DRSchedule>) this.Base.DocumentProperties).Update(current);
    SingleScheduleCreator.RecalculateSchedule(this.Base);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<DRSchedule> e)
  {
    if (e.Row == null)
      return;
    DRSchedule row = e.Row;
    this.SetVisibileAndEnable(e);
    if (!(row.Module == "AR"))
      return;
    this.CalcTotals();
  }

  private void SetVisibileAndEnable(PX.Data.Events.RowSelected<DRSchedule> e)
  {
    bool flag1 = e.Row.Module == "AR";
    bool? nullable1 = e.Row.IsDraft;
    int num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    nullable1 = e.Row.IsCustom;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    bool flag2 = num1 == 0 & valueOrDefault;
    bool? nullable2;
    int num2;
    if (e.Row.Module == "AR" && e.Row.IsDraft.GetValueOrDefault())
    {
      nullable2 = e.Row.IsCustom;
      bool flag3 = false;
      if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
      {
        num2 = e.Row.RefNbr != null ? 1 : 0;
        goto label_4;
      }
    }
    num2 = 0;
label_4:
    bool flag4 = num2 != 0;
    bool flag5 = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    ((PXAction) this.recalculate).SetEnabled(flag4);
    PXUIFieldAttribute.SetVisible<DRSchedule.isOverridden>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<DRSchedule.isOverridden>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, flag4);
    PXUIFieldAttribute.SetVisible<DRSchedule.curyNetTranPrice>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, flag1 && e.Row.RefNbr != null);
    PXUIFieldAttribute.SetVisible<DRSchedule.componentsTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.defTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.lineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.origLineAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.termStartDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1);
    PXUIFieldAttribute.SetVisible<DRSchedule.termEndDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1);
    PXUIFieldAttribute.SetVisibility<DRScheduleDetail.taskID>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag1 ? (PXUIVisibility) 7 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<DRScheduleDetail.termStartDate>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag1 ? (PXUIVisibility) 7 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<DRScheduleDetail.termEndDate>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag1 ? (PXUIVisibility) 7 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<DRScheduleDetail.taskID>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<DRScheduleDetail.termStartDate>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<DRScheduleDetail.termEndDate>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<DRScheduleDetail.discountPercent>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag5);
    PXUIFieldAttribute.SetEnabled<DRSchedule.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<DRSchedule.termStartDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<DRSchedule.termEndDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1 & flag2);
    PXUIFieldAttribute.SetEnabled<DRSchedule.lineNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, !flag1 & flag2 && e.Row.RefNbr != null);
    if (!flag1)
      return;
    nullable2 = e.Row.IsDraft;
    int num3;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = e.Row.IsOverridden;
      if (!nullable2.GetValueOrDefault())
      {
        nullable2 = e.Row.IsCustom;
        num3 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      else
        num3 = 1;
    }
    else
      num3 = 0;
    bool flag6 = num3 != 0;
    nullable2 = e.Row.IsDraft;
    int num4;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = e.Row.IsCustom;
      num4 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    bool flag7 = num4 != 0;
    ((PXSelectBase) this.Base.Components).Cache.AllowInsert = flag6;
    ((PXSelectBase) this.Base.Components).Cache.AllowUpdate = flag6;
    ((PXSelectBase) this.Base.Components).Cache.AllowDelete = flag6;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Base.Components).Cache, (string) null, flag6);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.branchID>(((PXSelectBase) this.Base.Components).Cache, (object) null, flag7);
    PXUIFieldAttribute.SetReadOnly<DRScheduleDetail.branchID>(((PXSelectBase) this.Base.Components).Cache, (object) null, !flag7);
  }

  private void CalcTotals()
  {
    ((PXSelectBase<DRSchedule>) this.Base.Schedule).Current.ComponentsTotal = new Decimal?(0M);
    ((PXSelectBase<DRSchedule>) this.Base.Schedule).Current.DefTotal = new Decimal?(0M);
    foreach (PXResult<DRScheduleDetail, DRSchedule> pxResult in ((PXSelectBase<DRScheduleDetail>) this.Base.Components).Select(Array.Empty<object>()))
    {
      DRScheduleDetail drScheduleDetail = PXResult<DRScheduleDetail, DRSchedule>.op_Implicit(pxResult);
      DRSchedule current1 = ((PXSelectBase<DRSchedule>) this.Base.Schedule).Current;
      Decimal? nullable = current1.ComponentsTotal;
      Decimal num1 = PXCurrencyAttribute.BaseRound((PXGraph) this.Base, drScheduleDetail.TotalAmt);
      current1.ComponentsTotal = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num1) : new Decimal?();
      DRSchedule current2 = ((PXSelectBase<DRSchedule>) this.Base.Schedule).Current;
      nullable = current2.DefTotal;
      Decimal num2 = PXCurrencyAttribute.BaseRound((PXGraph) this.Base, drScheduleDetail.DefAmt);
      current2.DefTotal = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num2) : new Decimal?();
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<DRScheduleDetail> e)
  {
    if (e.Row == null || e.Row.Module != "AR")
      return;
    bool flag = ((PXSelectBase) this.Base.Transactions).View.SelectSingleBound(new object[1]
    {
      (object) e.Row
    }, Array.Empty<object>()) != null;
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.componentID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRScheduleDetail>>) e).Cache, (object) e.Row, !flag);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.defCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRScheduleDetail>>) e).Cache, (object) e.Row, !flag);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRScheduleDetail>>) e).Cache, (object) e.Row, !flag);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.termStartDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRScheduleDetail>>) e).Cache, (object) e.Row, !flag);
    PXUIFieldAttribute.SetEnabled<DRScheduleDetail.termEndDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DRScheduleDetail>>) e).Cache, (object) e.Row, !flag);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<DRSchedule> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<DRSchedule>>) e).Cache.ObjectsEqual<DRSchedule.refNbr>((object) e.Row, (object) e.OldRow) || !(e.Row.Module == "AR") || e.OldRow.RefNbr == null)
      return;
    this.SetDRCounterOnOrigDoc(e.OldRow);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<DRSchedule> e)
  {
    if (!(e.Row.Module == "AR") || e.Row.RefNbr == null)
      return;
    this.SetDRCounterOnOrigDoc(e.Row);
  }

  private void SetDRCounterOnOrigDoc(DRSchedule row)
  {
    PX.Objects.AR.ARRegister arRegister = PXResultset<PX.Objects.AR.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<DRSchedule.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<DRSchedule.refNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    }));
    arRegister.DRSchedCntr = new int?(PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<ARTran.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>, And<ARTran.deferredCode, IsNotNull, And<Where<ARTran.lineType, IsNull, Or<ARTran.lineType, NotEqual<SOLineType.discount>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    }).Count);
    ((PXSelectBase<PX.Objects.AR.ARRegister>) this.OriginalDocument).Update(arRegister);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden> e)
  {
    if (e.Row == null)
      return;
    bool? isOverridden;
    if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>>) e).ExternalCall || ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>>) e).Cache.Graph.IsImport)
    {
      isOverridden = e.Row.IsOverridden;
      bool flag = false;
      if (isOverridden.GetValueOrDefault() == flag & isOverridden.HasValue && (bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>, DRSchedule, object>) e).OldValue && e.Row.DocType != null && e.Row.RefNbr != null)
        SingleScheduleCreator.RecalculateSchedule(this.Base);
    }
    if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>>) e).ExternalCall || ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>>) e).Cache.Graph.IsImport)
    {
      isOverridden = e.Row.IsOverridden;
      if (isOverridden.GetValueOrDefault() && !(bool) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>, DRSchedule, object>) e).OldValue && e.Row.DocType != null && e.Row.RefNbr != null)
      {
        DRSchedule copy = (DRSchedule) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>>) e).Cache.CreateCopy((object) e.Row);
        copy.IsRecalculated = new bool?(false);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DRSchedule, DRSchedule.isOverridden>>) e).Cache.Update((object) copy);
      }
    }
    ((PXSelectBase) this.Base.ReallocationPool).View.RequestRefresh();
  }

  protected virtual void DRScheduleDetail_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e,
    PXRowInserting baseDelegate)
  {
    if (((PXSelectBase<DRSchedule>) this.Base.Schedule).Current?.Module == "AP")
      baseDelegate.Invoke(sender, e);
    if (!(e.Row is DRScheduleDetail row) || ((PXSelectBase<DRSchedule>) this.Base.Schedule).Current == null)
      return;
    row.ComponentID = new int?(row.ComponentID.GetValueOrDefault());
    row.ScheduleID = ((PXSelectBase<DRSchedule>) this.Base.Schedule).Current.ScheduleID;
  }

  public virtual IEnumerable associated([PXDBString] string scheduleNbr)
  {
    IEnumerable enumerable = (IEnumerable) new List<DraftScheduleMaint.DRScheduleEx>();
    if (scheduleNbr == null)
      return enumerable;
    DRSchedule drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelect<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (drSchedule == null)
      return enumerable;
    if (drSchedule.Module == "AR")
    {
      if (drSchedule.DocType == "CRM")
      {
        PXResultset<DraftScheduleMaint.DRScheduleEx> pxResultset = PXSelectBase<DraftScheduleMaint.DRScheduleEx, PXSelectJoin<DraftScheduleMaint.DRScheduleEx, InnerJoin<ARTran, On<DRSchedule.scheduleID, Equal<ARTran.defScheduleID>>>, Where<ARTran.tranType, Equal<Current<DRSchedule.docType>>, And<ARTran.refNbr, Equal<Current<DRSchedule.refNbr>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>());
        if (pxResultset != null && pxResultset.Count > 0)
          return (IEnumerable) pxResultset;
      }
      else if (drSchedule.DocType == "INV" || drSchedule.DocType == "DRM")
      {
        List<DraftScheduleMaint.DRScheduleEx> drScheduleExList = new List<DraftScheduleMaint.DRScheduleEx>();
        foreach (PXResult<DraftScheduleMaint.DRScheduleEx> pxResult in PXSelectBase<DraftScheduleMaint.DRScheduleEx, PXSelectJoin<DraftScheduleMaint.DRScheduleEx, InnerJoin<ARTran, On<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<ARTran.tranType>, And<DRSchedule.refNbr, Equal<ARTran.refNbr>>>>>, Where<ARTran.defScheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
        {
          DraftScheduleMaint.DRScheduleEx drScheduleEx = PXResult<DraftScheduleMaint.DRScheduleEx>.op_Implicit(pxResult);
          drScheduleExList.Add(drScheduleEx);
        }
        return (IEnumerable) drScheduleExList;
      }
    }
    else if (drSchedule.Module == "AP")
      return this.Base.associated(scheduleNbr);
    return enumerable;
  }

  public delegate Decimal GetCuryTotalAmtDelegate(
    PXCache sender,
    DRScheduleDetail scheduleDetail,
    Decimal remainingAmount);
}
