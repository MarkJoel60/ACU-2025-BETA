// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderItemAvailabilityRelatedItemsExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.RelatedItems;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(null)]
public abstract class SOOrderItemAvailabilityRelatedItemsExtension : 
  PXGraphExtension<SOOrderItemAvailabilityExtension, SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.relatedItems>();

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.GetCheckErrors(PX.Objects.IN.ILSMaster,PX.Objects.IN.IStatus)" />
  /// </summary>
  [PXOverride]
  public virtual IEnumerable<PXExceptionInfo> GetCheckErrors(
    ILSMaster row,
    IStatus availability,
    Func<ILSMaster, IStatus, IEnumerable<PXExceptionInfo>> base_GetCheckErrors)
  {
    if (row is PX.Objects.SO.SOLine soLine && availability is PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter && !this.IsAvailableQty(row, availability))
    {
      SubstitutableSOLine extension = PXCacheEx.GetExtension<SubstitutableSOLine>((IBqlTable) soLine);
      if (extension != null && extension.SuggestRelatedItems.GetValueOrDefault())
      {
        int? relatedItemsRelation = extension.RelatedItemsRelation;
        int num = 0;
        if (relatedItemsRelation.GetValueOrDefault() > num & relatedItemsRelation.HasValue)
        {
          PXCache<PX.Objects.SO.SOLine> lineCache = this.Base1.LineCache;
          RelatedItemsAttribute relatedItemsAttribute = lineCache.GetAttributesOfType<RelatedItemsAttribute>((object) soLine, "RelatedItems").FirstOrDefault<RelatedItemsAttribute>();
          if (relatedItemsAttribute != null)
          {
            PXExceptionInfo pxExceptionInfo = relatedItemsAttribute.QtyMessage(((PXCache) lineCache).GetStateExt<PX.Objects.SO.SOLine.inventoryID>((object) soLine), ((PXCache) lineCache).GetStateExt<PX.Objects.SO.SOLine.subItemID>((object) soLine), ((PXCache) lineCache).GetStateExt<PX.Objects.SO.SOLine.siteID>((object) soLine), (InventoryRelation.RelationType) extension.RelatedItemsRelation.Value);
            if (pxExceptionInfo != null)
              return (IEnumerable<PXExceptionInfo>) new PXExceptionInfo[1]
              {
                new PXExceptionInfo((PXErrorLevel) 2, pxExceptionInfo.MessageFormat, pxExceptionInfo.MessageArguments)
              };
          }
        }
      }
    }
    return base_GetCheckErrors(row, availability);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderItemAvailabilityExtension.RaiseQtyExceptionHandling(PX.Objects.SO.SOLine,PX.Objects.Common.Exceptions.PXExceptionInfo,System.Nullable{System.Decimal})" />
  /// </summary>
  [PXOverride]
  public virtual void RaiseQtyExceptionHandling(
    PX.Objects.SO.SOLine line,
    PXExceptionInfo ei,
    Decimal? newValue,
    Action<PX.Objects.SO.SOLine, PXExceptionInfo, Decimal?> base_RaiseQtyExceptionHandling)
  {
    if (ei.MessageArguments.Length != 0)
      ((PXCache) this.Base1.LineCache).RaiseExceptionHandling<PX.Objects.SO.SOLine.orderQty>((object) line, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, ei.MessageArguments));
    else
      base_RaiseQtyExceptionHandling(line, ei, newValue);
  }

  [PXProtectedAccess(typeof (SOOrderItemAvailabilityExtension))]
  protected abstract bool IsAvailableQty(ILSMaster row, IStatus availability);
}
