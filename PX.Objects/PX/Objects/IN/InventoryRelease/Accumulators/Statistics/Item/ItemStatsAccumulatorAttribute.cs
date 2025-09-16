// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item.ItemStatsAccumulatorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.Statistics.Item;

public class ItemStatsAccumulatorAttribute : PXAccumulatorAttribute
{
  public Type LastCostDateField { get; set; }

  public Type LastCostField { get; set; }

  public Type MinCostField { get; set; }

  public Type MaxCostField { get; set; }

  public Type QtyOnHandField { get; set; }

  public Type TotalCostField { get; set; }

  public Type LastPurchasedDateField { get; set; }

  public ItemStatsAccumulatorAttribute() => this.SingleRecord = true;

  protected virtual bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
  {
    if (!base.PrepareInsert(sender, row, columns) || !sender.IsKeysFilled(row))
      return false;
    DateTime? nullable1 = (DateTime?) sender.GetValue(row, this.LastCostDateField.Name);
    DateTime? nullable2 = INItemStats.MinDate.get();
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
    {
      sender.SetValue(row, this.LastCostField.Name, (object) null);
      columns.Update(this.LastCostField, (object) 0M, (PXDataFieldAssign.AssignBehavior) 4);
      columns.Update(this.LastCostDateField, (PXDataFieldAssign.AssignBehavior) 4);
    }
    else
    {
      columns.Update(this.LastCostField, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update(this.LastCostDateField, (PXDataFieldAssign.AssignBehavior) 0);
    }
    Decimal? nullable3 = (Decimal?) sender.GetValue(row, this.MinCostField.Name);
    PXAccumulatorCollection accumulatorCollection = columns;
    Type minCostField = this.MinCostField;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) nullable3;
    Decimal? nullable4 = nullable3;
    Decimal num1 = 0M;
    int num2 = nullable4.GetValueOrDefault() == num1 & nullable4.HasValue ? 4 : 3;
    accumulatorCollection.Update(minCostField, (object) local, (PXDataFieldAssign.AssignBehavior) num2);
    columns.Update(this.MaxCostField, (PXDataFieldAssign.AssignBehavior) 2);
    columns.Update(this.QtyOnHandField, (PXDataFieldAssign.AssignBehavior) 1);
    columns.Update(this.TotalCostField, (PXDataFieldAssign.AssignBehavior) 1);
    if (this.LastPurchasedDateField != (Type) null)
    {
      DateTime? nullable5 = (DateTime?) sender.GetValue(row, this.LastPurchasedDateField.Name);
      if (nullable5.HasValue)
        columns.Update(this.LastPurchasedDateField, (object) nullable5, (PXDataFieldAssign.AssignBehavior) 2);
    }
    return true;
  }
}
