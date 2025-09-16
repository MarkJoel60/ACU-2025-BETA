// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderCostCenterDispatcher
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.GraphExtensions;

#nullable disable
namespace PX.Objects.FS;

public class ServiceOrderCostCenterDispatcher : 
  CostCenterDispatcher<ServiceOrderEntry, FSSODet, FSSODet.costCenterID>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() || PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [CostCenterDBDefault]
  protected virtual void _(Events.CacheAttached<FSSODet.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [CostCenterDBDefault]
  protected virtual void _(Events.CacheAttached<FSSODetSplit.costCenterID> e)
  {
  }

  protected virtual void _(
    Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.costCenterID> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.costCenterID>, FSSODetSplit, object>) e).NewValue = (object) (int?) PXParentAttribute.SelectParent<FSSODet>(((Events.Event<PXFieldDefaultingEventArgs, Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.costCenterID>>) e).Cache, (object) e.Row)?.CostCenterID;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<FSSODetSplit, FSSODetSplit.costCenterID>>) e).Cancel = true;
  }
}
