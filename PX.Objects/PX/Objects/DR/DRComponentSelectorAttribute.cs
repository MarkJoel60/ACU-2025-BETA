// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRComponentSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.DR;

public class DRComponentSelectorAttribute : PXSelectorAttribute
{
  private const string EmptyComponentCD = "<NONE>";

  public DRComponentSelectorAttribute()
    : base(typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>>>>), new Type[2]
    {
      typeof (InventoryItem.inventoryCD),
      typeof (InventoryItem.descr)
    })
  {
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (object.Equals((object) 0.ToString(), e.NewValue) || e.NewValue is 0)
      return;
    base.FieldVerifying(sender, e);
  }

  public virtual void SubstituteKeyFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is "<NONE>")
      e.NewValue = (object) 0;
    else
      base.SubstituteKeyFieldUpdating(sender, e);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (object.Equals((object) 0.ToString(), e.ReturnValue))
      e.ReturnValue = (object) "<NONE>";
    else
      base.FieldSelecting(sender, e);
  }
}
