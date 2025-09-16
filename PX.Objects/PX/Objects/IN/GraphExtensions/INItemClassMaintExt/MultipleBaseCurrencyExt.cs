// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INItemClassMaintExt.MultipleBaseCurrencyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INItemClassMaintExt;

public class MultipleBaseCurrencyExt : PXGraphExtension<INItemClassMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [RestrictorWithParameters(typeof (Where<INSite.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>>), "The {0} branch specified for the {1} warehouse has other base currency than the {2} branch that is currently selected.", new Type[] {typeof (INSite.branchID), typeof (INSite.siteCD), typeof (Current<AccessInfo.branchID>)})]
  protected virtual void _(
    Events.CacheAttached<INItemClassCurySettings.dfltSiteID> e)
  {
  }

  [PXMergeAttributes]
  [RestrictorWithParameters(typeof (Where<INSite.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>>), "The {0} branch specified for the {1} warehouse has other base currency than the {2} branch that is currently selected.", new Type[] {typeof (INSite.branchID), typeof (INSite.siteCD), typeof (Current<AccessInfo.branchID>)})]
  protected virtual void _(
    Events.CacheAttached<INItemClassRep.replenishmentSourceSiteID> e)
  {
  }
}
