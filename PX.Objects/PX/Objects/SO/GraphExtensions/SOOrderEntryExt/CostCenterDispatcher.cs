// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.CostCenterDispatcher
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class CostCenterDispatcher : CostCenterDispatcher<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.costCenterID>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [CostCenterDBDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [CostCenterDBDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLineSplit.costCenterID> e)
  {
  }

  protected override void MoveCostCenterViewCacheToTop()
  {
    ((PXGraph) this.Base).Views.Caches.RemoveAt(((PXGraph) this.Base).Views.Caches.IndexOf(typeof (INCostCenter)));
    ((PXGraph) this.Base).Views.Caches.Insert(((PXGraph) this.Base).Views.Caches.IndexOf(typeof (PX.Objects.SO.SOOrder)) + 1, typeof (INCostCenter));
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.costCenterID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.costCenterID>, PX.Objects.SO.SOLineSplit, object>) e).NewValue = (object) (int?) PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.costCenterID>>) e).Cache, (object) e.Row)?.CostCenterID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.costCenterID>>) e).Cancel = true;
  }
}
