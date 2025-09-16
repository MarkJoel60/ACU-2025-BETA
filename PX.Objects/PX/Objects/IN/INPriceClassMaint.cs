// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPriceClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.IN;

public class INPriceClassMaint : PXGraph<INPriceClassMaint>
{
  public PXSelect<INPriceClass> Records;
  public PXSavePerRow<INPriceClass> Save;
  public PXCancel<INPriceClass> Cancel;

  protected virtual void INPriceClass_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is INPriceClass))
      return;
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (DiscountInventoryPriceClass.inventoryPriceClassID), (Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (InventoryItem.priceClassID), (Type) null, (string) null);
  }
}
