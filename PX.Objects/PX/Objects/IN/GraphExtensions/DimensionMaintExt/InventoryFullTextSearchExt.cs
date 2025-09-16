// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.DimensionMaintExt.InventoryFullTextSearchExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.DimensionMaintExt;

public class InventoryFullTextSearchExt : PXGraphExtension<DimensionMaint>
{
  private const string InventorySegmentedKeyID = "INVENTORY";

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>();

  protected virtual void _(Events.RowSelected<Dimension> e)
  {
    if (e == null || !string.Equals(e.Row.DimensionID, "INVENTORY", StringComparison.OrdinalIgnoreCase))
      return;
    if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Detail).Cache.Inserted) || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Detail).Cache.Deleted) || GraphHelper.RowCast<Segment>(((PXSelectBase) this.Base.Detail).Cache.Updated).Any<Segment>((Func<Segment, bool>) (x => !((PXSelectBase) this.Base.Detail).Cache.ObjectsEqual<Segment.length, Segment.editMask, Segment.caseConvert, Segment.separator>((object) x, ((PXSelectBase) this.Base.Detail).Cache.GetOriginal((object) x)))))
    {
      if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<Dimension.dimensionID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Dimension>>) e).Cache, (object) e.Row)))
        return;
      PXUIFieldAttribute.SetWarning<Dimension.dimensionID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Dimension>>) e).Cache, (object) e.Row, "Rebuild the search index for the InventoryItem entity on the Rebuild Full-Text Entity Index (SM209500) form.");
    }
    else
    {
      if (!string.Equals(PXUIFieldAttribute.GetError<Dimension.dimensionID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Dimension>>) e).Cache, (object) e.Row), PXLocalizer.Localize("Rebuild the search index for the InventoryItem entity on the Rebuild Full-Text Entity Index (SM209500) form."), StringComparison.OrdinalIgnoreCase))
        return;
      PXUIFieldAttribute.SetWarning<Dimension.dimensionID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<Dimension>>) e).Cache, (object) e.Row, (string) null);
    }
  }
}
