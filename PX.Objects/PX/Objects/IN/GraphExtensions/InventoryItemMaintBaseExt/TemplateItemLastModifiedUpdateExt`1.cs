// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt.TemplateItemLastModifiedUpdateExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.InventoryItemMaintBaseExt;

public abstract class TemplateItemLastModifiedUpdateExt<TGraph> : PXGraphExtension<TGraph> where TGraph : InventoryItemMaintBase
{
  public PXSelect<PX.Objects.IN.Matrix.DAC.Accumulators.TemplateItemLastModifiedUpdate> TemplateItemLastModifiedUpdate;

  protected virtual void _(Events.RowInserted<InventoryItem> eventArgs)
  {
    this.InsertAccumulatorRecord(eventArgs.Row);
  }

  protected virtual void _(Events.RowUpdated<InventoryItem> eventArgs)
  {
    this.InsertAccumulatorRecord(eventArgs.OldRow);
  }

  protected virtual void _(Events.RowDeleted<InventoryItem> eventArgs)
  {
    this.InsertAccumulatorRecord(eventArgs.Row);
  }

  protected virtual void InsertAccumulatorRecord(InventoryItem row)
  {
    if (row == null || !row.TemplateItemID.HasValue)
      return;
    ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Accumulators.TemplateItemLastModifiedUpdate>) this.TemplateItemLastModifiedUpdate).Insert(new PX.Objects.IN.Matrix.DAC.Accumulators.TemplateItemLastModifiedUpdate()
    {
      InventoryID = row.TemplateItemID
    });
  }
}
