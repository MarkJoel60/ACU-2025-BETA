// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.InterbranchSiteRestrictionExtension`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Extensions;

public abstract class InterbranchSiteRestrictionExtension<TGraph, THeaderBranchField, TDetail, TDetailSiteField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where THeaderBranchField : class, IBqlField
  where TDetail : class, IBqlTable, new()
  where TDetailSiteField : class, IBqlField
{
  protected PXCache HeaderCache
  {
    get => this.Base.Caches[BqlCommand.GetItemType(typeof (THeaderBranchField))];
  }

  protected PXCache DetailsCache
  {
    get => this.Base.Caches[BqlCommand.GetItemType(typeof (TDetailSiteField))];
  }

  protected abstract IEnumerable<TDetail> GetDetails();

  protected virtual void _(PX.Data.Events.FieldVerifying<THeaderBranchField> e)
  {
    int? newValue = (int?) e.NewValue;
    if (!newValue.HasValue || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.interBranch>())
      return;
    bool flag = false;
    foreach (TDetail detail in this.GetDetails())
    {
      INSite inSite = (INSite) PXSelectorAttribute.Select(this.DetailsCache, (object) detail, typeof (TDetailSiteField).Name);
      if (inSite != null && !PXAccess.IsSameParentOrganization(newValue, inSite.BranchID))
      {
        flag = true;
        this.DetailsCache.MarkUpdated((object) detail);
      }
    }
    if (!flag)
      return;
    this.RaiseBranchFieldWarning((int?) e.NewValue, "One of the warehouses on the Document Details tab belongs to another branch.");
  }

  protected void RaiseBranchFieldWarning(int? newBranch, string message)
  {
    this.HeaderCache.RaiseExceptionHandling<THeaderBranchField>(this.HeaderCache.Current, (object) newBranch, (Exception) new PXSetPropertyException(message, PXErrorLevel.Warning));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<TDetail> e)
  {
    if (e.Operation.Command() == PXDBOperation.Delete)
      return;
    int? branchA = (int?) this.HeaderCache.GetValue<THeaderBranchField>(this.HeaderCache.Current);
    if (!branchA.HasValue || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.interBranch>())
      return;
    INSite inSite = (INSite) PXSelectorAttribute.Select(this.DetailsCache, (object) e.Row, typeof (TDetailSiteField).Name);
    if (inSite == null || PXAccess.IsSameParentOrganization(branchA, inSite.BranchID))
      return;
    this.DetailsCache.RaiseExceptionHandling<INTran.siteID>((object) e.Row, (object) inSite.SiteCD, (Exception) new PXSetPropertyException("Inter-Branch Transactions feature is disabled."));
  }
}
