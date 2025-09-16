// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.InventoryLookupUnassignedLocationFix
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.WMS;

public class InventoryLookupUnassignedLocationFix : PXGraphExtension<InventoryItemLookup.Host>
{
  protected virtual void _(
    Events.FieldSelecting<InventorySummaryEnquiryResult.locationID> e,
    PXFieldSelecting baseMethod)
  {
    object returnValue = ((Events.FieldSelectingBase<Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).ReturnValue;
    string str = returnValue == null ? PXMessages.LocalizeNoPrefix("<UNASSIGNED>") : (!(returnValue is -1) ? INLocation.PK.Find((PXGraph) this.Base, ((Events.FieldSelectingBase<Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).ReturnValue as int?).With<INLocation, string>((Func<INLocation, string>) (loc => !((PXGraph) this.Base).IsMobile ? loc.LocationCD : loc.Descr ?? loc.LocationCD)) : PXMessages.LocalizeNoPrefix("Total:"));
    if (str == null)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).ReturnState = (object) PXFieldState.CreateInstance((object) str, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, "locationID", (string) null, GetLocationDisplayName(), (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    ((Events.FieldSelectingBase<Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).Cancel = true;

    string GetLocationDisplayName()
    {
      string locationDisplayName = PXUIFieldAttribute.GetDisplayName<InventorySummaryEnquiryResult.locationID>(((Events.Event<PXFieldSelectingEventArgs, Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).Cache);
      if (locationDisplayName != null)
        locationDisplayName = PXMessages.LocalizeNoPrefix(locationDisplayName);
      return locationDisplayName;
    }
  }

  [PXDBInt]
  [PXUIField]
  protected virtual void _(
    Events.CacheAttached<InventorySummaryEnquiryResult.locationID> e)
  {
  }
}
