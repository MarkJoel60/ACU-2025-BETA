// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Mobile.EP.GraphExtensions.TimeCardMaintExt.TimeCardMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Mobile.EP.Descriptor.Attributes;
using PX.Objects.CS;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.CN.Mobile.EP.GraphExtensions.TimeCardMaintExt;

public class TimeCardMaintExt : PXGraphExtension<TimeCardMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [MobileProjectCostCode(typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectTaskID), "E", typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.projectID), typeof (TimeCardMaint.EPTimeCardSummaryWithInfo.labourItemID), true)]
  protected virtual void _(
    Events.CacheAttached<TimeCardMaint.EPTimeCardSummaryWithInfo.costCodeID> e)
  {
  }

  [MobileProjectCostCode(typeof (TimeCardMaint.EPTimecardDetail.projectTaskID), "E", typeof (TimeCardMaint.EPTimecardDetail.projectID), typeof (TimeCardMaint.EPTimecardDetail.labourItemID), true)]
  protected virtual void _(
    Events.CacheAttached<TimeCardMaint.EPTimecardDetail.costCodeID> e)
  {
  }

  [MobileProjectCostCode(typeof (EPTimeCardItem.taskID), "E", typeof (EPTimeCardItem.projectID), typeof (EPTimeCardItem.inventoryID), true)]
  protected virtual void _(Events.CacheAttached<EPTimeCardItem.costCodeID> e)
  {
  }
}
