// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.FeaturesMaintMaterialManagementExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM.Project.Overview;

public class FeaturesMaintMaterialManagementExt : PXGraphExtension<FeaturesMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  public virtual void _(Events.RowSelected<FeaturesSet> e)
  {
    if (e.Row == null)
      return;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FeaturesSet>>) e).Cache.RaiseExceptionHandling<FeaturesSet.projectOverview>((object) e.Row, (object) (bool?) e.Row?.ProjectOverview, (Exception) new PXSetPropertyException<FeaturesSet.projectOverview>("This feature is available only in the Modern UI.", (PXErrorLevel) 2));
  }
}
