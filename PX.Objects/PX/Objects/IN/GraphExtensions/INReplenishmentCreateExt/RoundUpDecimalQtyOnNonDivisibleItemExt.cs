// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INReplenishmentCreateExt.RoundUpDecimalQtyOnNonDivisibleItemExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INReplenishmentCreateExt;

/// <exclude />
public class RoundUpDecimalQtyOnNonDivisibleItemExt : PXGraphExtension<INReplenishmentCreate>
{
  public virtual void Initialize() => ((PXGraphExtension) this).Initialize();

  [PXOverride]
  public virtual Decimal? RecalcQty(
    INReplenishmentItem rec,
    Func<INReplenishmentItem, Decimal?> baseMethod)
  {
    rec.QtyProcess = baseMethod(rec);
    if (!rec.QtyProcessRounded.GetValueOrDefault())
    {
      bool? decimalBaseUnit = rec.DecimalBaseUnit;
      bool flag = false;
      if (decimalBaseUnit.GetValueOrDefault() == flag & decimalBaseUnit.HasValue)
      {
        Decimal? qtyProcess = rec.QtyProcess;
        Decimal num1 = (Decimal) 1;
        Decimal? nullable1 = qtyProcess.HasValue ? new Decimal?(qtyProcess.GetValueOrDefault() % num1) : new Decimal?();
        Decimal num2 = 0M;
        if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
        {
          INReplenishmentItem replenishmentItem = rec;
          nullable1 = rec.QtyProcess;
          Decimal? nullable2 = new Decimal?(Math.Ceiling(nullable1.Value));
          replenishmentItem.QtyProcess = nullable2;
          rec.QtyProcessRounded = new bool?(true);
        }
      }
    }
    return rec.QtyProcess;
  }

  [PXOverride]
  public virtual bool ManageQtyProcessRoundedWarning(
    INReplenishmentItem rec,
    Func<INReplenishmentItem, bool> baseMethod)
  {
    if (rec.QtyProcessRounded.GetValueOrDefault())
    {
      ((PXSelectBase) this.Base.Records).Cache.RaiseExceptionHandling<INReplenishmentItem.qtyProcess>((object) rec, (object) rec.QtyProcess, (Exception) new PXSetPropertyException<INReplenishmentItem.qtyProcess>("The value in the Qty. To Process column has been rounded to an integer number because the base UOM of the {0} item is not divisible.", (PXErrorLevel) 2, new object[1]
      {
        (object) rec.InventoryCD
      }));
      return true;
    }
    if (PXUIFieldAttribute.GetErrorWithLevel<INReplenishmentItem.qtyProcess>(((PXSelectBase) this.Base.Records).Cache, (object) rec).Item2 == 2)
      ((PXSelectBase) this.Base.Records).Cache.RaiseExceptionHandling<INReplenishmentItem.qtyProcess>((object) rec, (object) rec.QtyProcess, (Exception) null);
    return false;
  }

  public virtual void _(
    Events.FieldUpdating<INReplenishmentItem.qtyProcess> e)
  {
    INReplenishmentItem row = (INReplenishmentItem) e.Row;
    if (row == null || ((Events.FieldUpdatingBase<Events.FieldUpdating<INReplenishmentItem.qtyProcess>>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((Events.FieldUpdatingBase<Events.FieldUpdating<INReplenishmentItem.qtyProcess>>) e).NewValue;
    bool? decimalBaseUnit = row.DecimalBaseUnit;
    bool flag = false;
    if (decimalBaseUnit.GetValueOrDefault() == flag & decimalBaseUnit.HasValue && newValue % 1M != 0M)
    {
      ((Events.FieldUpdatingBase<Events.FieldUpdating<INReplenishmentItem.qtyProcess>>) e).NewValue = (object) Math.Ceiling(newValue);
      row.QtyProcessRounded = new bool?(true);
    }
    else
      row.QtyProcessRounded = new bool?(false);
  }
}
