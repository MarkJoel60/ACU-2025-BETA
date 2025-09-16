// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARReleaseProcessExt.AffectedSOOrdersWithCreditLimitExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARReleaseProcessExt;

public class AffectedSOOrdersWithCreditLimitExt : 
  AffectedSOOrdersWithCreditLimitExtBase<ARReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public override void Persist(Action basePersist)
  {
    IEnumerable<PX.Objects.SO.SOOrder> affectedEntities1 = this.GetAffectedEntities();
    IEnumerable<PX.Objects.SO.SOOrder> affectedEntities2 = this.GetLatelyAffectedEntities();
    base.Persist(basePersist);
    if (affectedEntities2 == null && !affectedEntities1.Any<PX.Objects.SO.SOOrder>())
      return;
    this.ClearCaches((PXGraph) this.Base, ((PXGraph) this.Base).FindImplementation<AffectedSOOrdersWithCreditLimitExt>().ProcessAffectedEntities(affectedEntities2 == null ? affectedEntities1 : affectedEntities2.Union<PX.Objects.SO.SOOrder>(affectedEntities1, PXCacheEx.GetComparer<PX.Objects.SO.SOOrder>(GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)))));
  }
}
