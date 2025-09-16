// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.InterbranchTranValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class InterbranchTranValidation : 
  InterbranchSiteRestrictionExtension<SOOrderEntry, PX.Objects.SO.SOOrder.branchID, PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>
{
  protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder.branchID> e)
  {
    PX.Objects.SO.SOOrder row = (PX.Objects.SO.SOOrder) e.Row;
    if (row?.Behavior == "QT")
      return;
    if (row != null && row.IsTransferOrder.GetValueOrDefault() && !this.IsDestinationSiteValid(row, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder.branchID>, object, object>) e).NewValue))
      this.RaiseBranchFieldWarning((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder.branchID>, object, object>) e).NewValue, "Destination warehouse belongs to another branch.");
    else
      base._(e);
  }

  protected override void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine> e)
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.CurrentDocument).Current?.Behavior == "QT" || e.Row.LineType == "MI")
      return;
    base._(e);
  }

  protected override IEnumerable<PX.Objects.SO.SOLine> GetDetails()
  {
    return GraphHelper.RowCast<PX.Objects.SO.SOLine>((IEnumerable) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>()));
  }

  protected virtual bool IsDestinationSiteValid(PX.Objects.SO.SOOrder row, int? newBranchId)
  {
    if (!newBranchId.HasValue || PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
      return true;
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, row.DestinationSiteID);
    return inSite == null || PXAccess.IsSameParentOrganization(newBranchId, inSite.BranchID);
  }

  protected virtual void SOOrder_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (PXDBOperationExt.Command(e.Operation) == 3)
      return;
    PX.Objects.SO.SOOrder row = (PX.Objects.SO.SOOrder) e.Row;
    if ((row != null ? (!row.IsTransferOrder.GetValueOrDefault() ? 1 : 0) : 1) != 0 || !row.BranchID.HasValue || PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
      return;
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, row.DestinationSiteID);
    if (inSite == null || PXAccess.IsSameParentOrganization(row.BranchID, inSite.BranchID))
      return;
    cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.destinationSiteID>(e.Row, (object) inSite.SiteCD, (Exception) new PXSetPropertyException("Inter-Branch Transactions feature is disabled."));
  }
}
