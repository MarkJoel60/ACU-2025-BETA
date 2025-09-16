// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DraftScheduleMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.DR;

public class DraftScheduleMaintMultipleBaseCurrencies : PXGraphExtension<DraftScheduleMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXUIFieldAttribute.SetVisible<DRSchedule.baseCuryID>(((PXSelectBase) this.Base.Schedule).Cache, (object) null, !PXAccess.FeatureInstalled<FeaturesSet.aSC606>());
    PXUIFieldAttribute.SetVisible<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>(((PXSelectBase) this.Base.Schedule).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.aSC606>());
  }

  protected virtual void _(Events.FieldUpdated<DRSchedule.bAccountID> e)
  {
    if (e.Row == null)
      return;
    DRSchedule row = (DRSchedule) e.Row;
    BAccount baccount = (BAccount) PXSelectorAttribute.Select<DRSchedule.bAccountID>(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<DRSchedule.bAccountID>>) e).Cache, (object) row);
    string baseCuryId = VisibilityRestriction.IsNotEmpty((int?) baccount?.COrgBAccountID) || VisibilityRestriction.IsNotEmpty((int?) baccount?.VOrgBAccountID) ? baccount.BaseCuryID : (string) null;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<DRSchedule.bAccountID>>) e).Cache.RaiseExceptionHandling<DRSchedule.baseCuryID>((object) row, (object) null, (Exception) null);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<DRSchedule.bAccountID>>) e).Cache.SetValuePending<DRSchedule.baseCuryID>((object) row, PXCache.NotSetValue);
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<DRSchedule.bAccountID>>) e).Cache.SetValue<DRSchedule.baseCuryID>((object) row, (object) (baseCuryId ?? ((PXGraph) this.Base).Accessinfo.BaseCuryID));
  }

  protected virtual void _(Events.FieldUpdated<DRSchedule.baseCuryID> e)
  {
    DRSchedule row = (DRSchedule) e.Row;
    PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.CurrencyInfo).Select(Array.Empty<object>()));
    ((PXSelectBase) this.Base.CurrencyInfo).Cache.Clear();
    PX.Objects.CM.CurrencyInfo currencyInfo2 = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.Base.CurrencyInfo).Cache.CreateInstance();
    if (row.RefNbr == null)
    {
      currencyInfo2.CuryID = (string) e.NewValue;
      currencyInfo2.BaseCuryID = (string) e.NewValue;
    }
    else
      currencyInfo2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo1);
    currencyInfo2.CuryInfoID = new long?();
    currencyInfo2.IsReadOnly = new bool?(false);
    PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.CurrencyInfo).Insert(currencyInfo2));
    row.CuryInfoID = copy.CuryInfoID;
    row.CuryID = copy.CuryID;
  }

  protected virtual void _(Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo.baseCuryID> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo.baseCuryID>, object, object>) e).NewValue = (object) (((PXSelectBase<DRSchedule>) this.Base.Schedule)?.Current?.BaseCuryID ?? ((PXGraph) this.Base).Accessinfo.BaseCuryID);
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<PX.Objects.CM.CurrencyInfo.baseCuryID>>) e).Cancel = true;
  }

  protected virtual void _(Events.RowSelected<DRSchedule> e)
  {
    if (e.Row == null)
      return;
    DRSchedule row = e.Row;
    bool flag1 = DraftScheduleMaintMultipleBaseCurrencies.CanBaseCurrencyBeChanged(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache, row) && !((PXSelectBase<DRScheduleDetail>) this.Base.Components).Any<DRScheduleDetail>();
    PXUIFieldAttribute.SetEnabled<DRSchedule.baseCuryID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache, (object) row, flag1);
    if (e.Row.BaseCuryID == null)
    {
      Exception exception = (Exception) new PXSetPropertyException("'{0}' may not be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache)
      });
      ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache.RaiseExceptionHandling<DRSchedule.baseCuryID>((object) e.Row, (object) null, exception);
    }
    else
      ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache.RaiseExceptionHandling<DRSchedule.baseCuryID>((object) e.Row, (object) null, (Exception) null);
    PXFieldState valueExt = ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache.GetValueExt<DRSchedule.baseCuryID>((object) e.Row) as PXFieldState;
    PXSelectJoin<DRScheduleDetail, LeftJoin<DRSchedule, On<DRScheduleDetail.scheduleID, Equal<DRSchedule.scheduleID>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>, And<Where<DRScheduleDetail.isResidual, Equal<False>, Or2<Where<DRScheduleDetail.defCode, IsNotNull>, And<FeatureInstalled<FeaturesSet.aSC606>>>>>>> components1 = this.Base.Components;
    PXSelectJoin<DRScheduleDetail, LeftJoin<DRSchedule, On<DRScheduleDetail.scheduleID, Equal<DRSchedule.scheduleID>>>, Where<DRScheduleDetail.scheduleID, Equal<Current<DRSchedule.scheduleID>>, And<Where<DRScheduleDetail.isResidual, Equal<False>, Or2<Where<DRScheduleDetail.defCode, IsNotNull>, And<FeatureInstalled<FeaturesSet.aSC606>>>>>>> components2 = this.Base.Components;
    bool flag2;
    ((PXSelectBase) this.Base.Components).AllowDelete = flag2 = valueExt != null && valueExt.Error == null;
    int num1;
    bool flag3 = (num1 = flag2 ? 1 : 0) != 0;
    ((PXSelectBase) components2).AllowUpdate = num1 != 0;
    int num2 = flag3 ? 1 : 0;
    ((PXSelectBase) components1).AllowInsert = num2 != 0;
  }

  internal static bool CanBaseCurrencyBeChanged(PXCache cache, DRSchedule schedule)
  {
    if (schedule.RefNbr != null)
      return false;
    BAccount baccount = (BAccount) PXSelectorAttribute.Select<DRSchedule.bAccountID>(cache, (object) schedule);
    bool flag = VisibilityRestriction.IsNotEmpty((int?) baccount?.COrgBAccountID) || VisibilityRestriction.IsNotEmpty((int?) baccount?.VOrgBAccountID);
    return baccount != null && !flag;
  }
}
