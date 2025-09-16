// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.FeaturesMaintExt.InventoryFullTextSearchExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Data.Services;
using PX.Data.Update;
using PX.Objects.CS;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.FeaturesMaintExt;

public class InventoryFullTextSearchExt : PXGraphExtension<FeaturesMaint>
{
  protected virtual void _(
    Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch> e)
  {
    if (!e.Row.DistributionModule.GetValueOrDefault() || e.OldValue == ((Events.FieldVerifyingBase<Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch>, FeaturesSet, object>) e).NewValue || !(bool) ((Events.FieldVerifyingBase<Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch>, FeaturesSet, object>) e).NewValue || !((Events.FieldVerifyingBase<Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch>>) e).ExternalCall)
      return;
    bool flag = this.IsFTSAvailable();
    ((Events.FieldVerifyingBase<Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch>, FeaturesSet, object>) e).NewValue = (object) flag;
    if (!flag)
      throw new PXSetPropertyException((IBqlTable) e.Row, "This feature is available only if full-text search is enabled for the MS SQL database.");
    bool newValue;
    this.CheckFTSUsageInOtherTenants(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch>>) e).Cache, e.Row, out newValue);
    ((Events.FieldVerifyingBase<Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch>, FeaturesSet, object>) e).NewValue = (object) newValue;
    if (!newValue)
      return;
    PXUIFieldAttribute.SetWarning<FeaturesSet.inventoryFullTextSearch>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<FeaturesSet, FeaturesSet.inventoryFullTextSearch>>) e).Cache, (object) e.Row, "To use this feature, rebuild the search index for the InventoryItem entity on the Rebuild Full-Text Entity Index (SM209500) form.");
  }

  protected virtual void _(Events.RowSelected<FeaturesSet> e)
  {
    PXUIFieldAttribute.SetVisible<FeaturesSet.inventoryFullTextSearch>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FeaturesSet>>) e).Cache, (object) e.Row, this.IsSqlDialectAllowedForFTS());
    if (e?.Row == null || !e.Row.InventoryFullTextSearch.GetValueOrDefault() || this.IsFTSAvailable())
      return;
    PXUIFieldAttribute.SetWarning<FeaturesSet.inventoryFullTextSearch>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FeaturesSet>>) e).Cache, (object) e.Row, "This feature is available only if full-text search is enabled for the MS SQL database.");
  }

  protected virtual bool IsSqlDialectAllowedForFTS()
  {
    return InventoryFullTextSearchHelper.IsSqlDialectAllowedForFTS();
  }

  protected virtual bool IsFTSAvailable() => InventoryFullTextSearchHelper.IsFTSAvailable();

  private void CheckFTSUsageInOtherTenants(
    PXCache cache,
    FeaturesSet featuresSet,
    out bool newValue)
  {
    newValue = true;
    int currentCompany = PXInstanceHelper.CurrentCompany;
    ICompanyService instance = ServiceLocator.Current.GetInstance<ICompanyService>();
    List<string> withEnabledFeature = instance.GetOtherCompanyNamesWithEnabledFeature(currentCompany, "InventoryFullTextSearch");
    List<string> namesHavingAnyData = instance.GetOtherCompanyNamesHavingAnyData(currentCompany, "InventorySearchIndex");
    if (!withEnabledFeature.Any<string>() && !namesHavingAnyData.Any<string>() || ((PXSelectBase) this.Base.Features).View.Ask($"This feature is already enabled in the following tenants: {string.Join(", ", withEnabledFeature.Concat<string>((IEnumerable<string>) namesHavingAnyData).Distinct<string>())}. For optimal results, you need to increase the maximum number of results in the web.config file. Alternatively, you can disable this feature in other tenants and clear their search indexes on the Rebuild Full-Text Entity Index (SM209500) form. Do you want to enable this feature?", (MessageButtons) 4) == 6)
      return;
    newValue = false;
  }
}
