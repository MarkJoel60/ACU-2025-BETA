// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SM.GraphExtensions.SmNotificationMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CN.Subcontracts.SM.Extension;
using PX.Objects.CS;
using PX.SM;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SM.GraphExtensions;

public class SmNotificationMaintExt : PXGraphExtension<SMNotificationMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected IEnumerable entityItems(string parent)
  {
    SmNotificationMaintExt notificationMaintExt = this;
    IEnumerable enumerable = notificationMaintExt.Base.entityItems(parent);
    PXSiteMapNode siteMap = PXSiteMap.Provider.FindSiteMapNodeByScreenID(((PXSelectBase<Notification>) notificationMaintExt.Base.Notifications).Current.ScreenID);
    foreach (CacheEntityItem cacheEntityItem in enumerable)
    {
      if (siteMap.GraphType == typeof (SubcontractEntry).FullName)
        cacheEntityItem.Name = cacheEntityItem.GetSubcontractViewName();
      yield return (object) cacheEntityItem;
    }
  }
}
