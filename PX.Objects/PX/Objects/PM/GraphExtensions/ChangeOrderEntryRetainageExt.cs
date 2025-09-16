// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.ChangeOrderEntryRetainageExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class ChangeOrderEntryRetainageExt : PXGraphExtension<ChangeOrderEntry>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.changeOrder>() && PXAccess.FeatureInstalled<FeaturesSet.retainage>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMChangeOrderLine> e)
  {
    if (e.Row == null || !(e.Row.LineType != "L"))
      return;
    Decimal defaultRetainagePct = this.GetDefaultRetainagePct(e.Row);
    Decimal? retainagePct1 = e.Row.RetainagePct;
    if (!retainagePct1.HasValue)
      return;
    retainagePct1 = e.Row.RetainagePct;
    if (!(retainagePct1.GetValueOrDefault() != defaultRetainagePct))
      return;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMChangeOrderLine>>) e).Cache;
    PMChangeOrderLine row = e.Row;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> retainagePct2 = (ValueType) e.Row.RetainagePct;
    object[] objArray = new object[2];
    retainagePct1 = e.Row.RetainagePct;
    objArray[0] = (object) retainagePct1.Value.ToString("f6");
    objArray[1] = (object) defaultRetainagePct.ToString("f6");
    PXSetPropertyException propertyException = new PXSetPropertyException("The retainage percent in the line ({0}) differs from the retainage percent specified in the commitment document ({1}). On release of the change order, the retainage percent and retainage amount in the related commitment line will be changed to the values specified in the change order line.", (PXErrorLevel) 2, objArray);
    cache.RaiseExceptionHandling<PMChangeOrderLine.retainagePct>((object) row, (object) retainagePct2, (Exception) propertyException);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.vendorID> e)
  {
    Decimal defaultRetainagePct = this.GetDefaultRetainagePct(e.Row);
    if (e.Row.RetainagePct.HasValue && !(defaultRetainagePct != e.Row.RetainagePct.GetValueOrDefault()))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.vendorID>>) e).Cache.SetValueExt<PMChangeOrderLine.retainagePct>((object) e.Row, (object) defaultRetainagePct);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOOrderNbr> e)
  {
    Decimal defaultRetainagePct = this.GetDefaultRetainagePct(e.Row);
    if (e.Row.RetainagePct.HasValue && !(defaultRetainagePct != e.Row.RetainagePct.GetValueOrDefault()))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOOrderNbr>>) e).Cache.SetValueExt<PMChangeOrderLine.retainagePct>((object) e.Row, (object) defaultRetainagePct);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOLineNbr> e)
  {
    Decimal defaultRetainagePct = this.GetDefaultRetainagePct(e.Row);
    if (e.Row.RetainagePct.HasValue && !(defaultRetainagePct != e.Row.RetainagePct.GetValueOrDefault()))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.pOLineNbr>>) e).Cache.SetValueExt<PMChangeOrderLine.retainagePct>((object) e.Row, (object) defaultRetainagePct);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.amount> e)
  {
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.amount>>) e).Cache.RaiseFieldDefaulting<PMChangeOrderLine.retainageAmt>((object) e.Row, ref obj);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.amount>>) e).Cache.SetValueExt<PMChangeOrderLine.retainageAmt>((object) e.Row, obj);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.retainagePct> e)
  {
    if (e.Row == null)
      return;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.retainagePct>>) e).Cache.RaiseFieldDefaulting<PMChangeOrderLine.retainageAmt>((object) e.Row, ref obj);
    Decimal? nullable1 = obj as Decimal?;
    Decimal? retainageAmt = e.Row.RetainageAmt;
    Decimal? nullable2 = nullable1;
    if (retainageAmt.GetValueOrDefault() == nullable2.GetValueOrDefault() & retainageAmt.HasValue == nullable2.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.retainagePct>>) e).Cache.SetValueExt<PMChangeOrderLine.retainageAmt>((object) e.Row, (object) nullable1);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.retainageAmt> e)
  {
    if (e.Row == null)
      return;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.retainageAmt>>) e).Cache.RaiseFieldDefaulting<PMChangeOrderLine.retainagePct>((object) e.Row, ref obj);
    Decimal? nullable1 = obj as Decimal?;
    Decimal? retainagePct = e.Row.RetainagePct;
    Decimal? nullable2 = nullable1;
    if (!(retainagePct.GetValueOrDefault() == nullable2.GetValueOrDefault() & retainagePct.HasValue == nullable2.HasValue))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.retainageAmt>>) e).Cache.SetValueExt<PMChangeOrderLine.retainagePct>((object) e.Row, (object) nullable1);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMChangeOrderLine, PMChangeOrderLine.retainageAmt>>) e).Cache.SetValueExt<PMChangeOrderLine.retainageAmtInProjectCury>((object) e.Row, (object) this.Base.GetAmountInProjectCurrency(e.Row.CuryID, e.Row.RetainageAmt));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.retainagePct> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.retainagePct>, PMChangeOrderLine, object>) e).NewValue = (object) (!e.Row.RetainageAmt.HasValue || !(e.Row.Amount.GetValueOrDefault() != 0M) ? 0M : Math.Abs(Math.Round(e.Row.RetainageAmt.Value / e.Row.Amount.GetValueOrDefault() * 100M, 6)));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.retainageAmt> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMChangeOrderLine, PMChangeOrderLine.retainageAmt>, PMChangeOrderLine, object>) e).NewValue = (object) ChangeOrderEntryRetainageExt.CalculateRetainage(e.Row.Amount, e.Row.RetainagePct);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.retainageAmt> e)
  {
    if (e.Row == null)
      return;
    Decimal? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.retainageAmt>, PMChangeOrderLine, object>) e).NewValue as Decimal?;
    if (!newValue.HasValue)
      return;
    Decimal? amount = e.Row.Amount;
    if (!amount.HasValue)
      return;
    Decimal num1 = newValue.Value;
    amount = e.Row.Amount;
    Decimal num2 = amount.Value;
    if (!(num1 * num2 < 0M))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMChangeOrderLine, PMChangeOrderLine.retainageAmt>, PMChangeOrderLine, object>) e).NewValue = e.OldValue;
  }

  [PXOverride]
  public virtual PMChangeOrderLine CreateChangeOrderLine(
    POLinePM poLine,
    Func<POLinePM, PMChangeOrderLine> baseMethod)
  {
    PMChangeOrderLine line = baseMethod(poLine);
    line.RetainagePct = new Decimal?(this.GetDefaultRetainagePct(line));
    return line;
  }

  [PXOverride]
  public virtual PX.Objects.PO.POLine ModifyExistsingLineInOrder(
    POOrderEntry target,
    PMChangeOrderLine line,
    Func<POOrderEntry, PMChangeOrderLine, PX.Objects.PO.POLine> baseMethod)
  {
    PX.Objects.PO.POLine poLine = baseMethod(target, line);
    if (line.RetainagePct.HasValue)
    {
      poLine.RetainagePct = line.RetainagePct;
      poLine.RetainageAmt = new Decimal?(ChangeOrderEntryRetainageExt.CalculateRetainage(poLine.CuryLineAmt, line.RetainagePct));
      poLine = ((PXSelectBase<PX.Objects.PO.POLine>) target.Transactions).Update(poLine);
    }
    return poLine;
  }

  private static Decimal CalculateRetainage(Decimal? amount, Decimal? retainagePct)
  {
    return amount.GetValueOrDefault() * retainagePct.GetValueOrDefault() / 100M;
  }

  private Decimal GetDefaultRetainagePct(PMChangeOrderLine line)
  {
    if (line == null)
      return 0M;
    bool? retainageApply;
    if (!string.IsNullOrWhiteSpace(line.POOrderNbr))
    {
      PX.Objects.PO.POLine poLine = PX.Objects.PO.POLine.PK.Find((PXGraph) this.Base, line.POOrderType, line.POOrderNbr, line.POLineNbr);
      if (poLine != null)
        return poLine.RetainagePct.GetValueOrDefault();
      PX.Objects.PO.POOrder poOrder = PX.Objects.PO.POOrder.PK.Find((PXGraph) this.Base, line.POOrderType, line.POOrderNbr);
      if (poOrder != null)
      {
        retainageApply = poOrder.RetainageApply;
        if (retainageApply.GetValueOrDefault())
          return poOrder.DefRetainagePct.GetValueOrDefault();
      }
    }
    PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this.Base, line.VendorID);
    if (vendor != null)
    {
      retainageApply = vendor.RetainageApply;
      if (retainageApply.GetValueOrDefault())
        return vendor.RetainagePct.GetValueOrDefault();
    }
    return 0M;
  }
}
