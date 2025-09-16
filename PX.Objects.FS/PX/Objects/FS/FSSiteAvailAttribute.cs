// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSiteAvailAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXDBInt]
[PXUIField]
public class FSSiteAvailAttribute : SiteAvailAttribute
{
  public FSSiteAvailAttribute(Type InventoryType, Type SubItemType)
    : base(InventoryType, SubItemType, typeof (PX.Objects.IN.CostCenter.freeStock))
  {
  }

  public FSSiteAvailAttribute(Type InventoryType, Type SubItemType, Type CostCenterType)
    : base(InventoryType, SubItemType, CostCenterType)
  {
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    FSSrvOrdType fsSrvOrdType = (FSSrvOrdType) null;
    FSBranchLocation fsBranchLocation = (FSBranchLocation) null;
    if (sender.Graph is ServiceOrderEntry)
    {
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) ((ServiceOrderEntry) sender.Graph).ServiceOrderTypeSelected).Current;
      fsBranchLocation = ((PXSelectBase<FSBranchLocation>) ((ServiceOrderEntry) sender.Graph).CurrentBranchLocation).Current;
    }
    else if (sender.Graph is AppointmentEntry)
    {
      fsSrvOrdType = ((PXSelectBase<FSSrvOrdType>) ((AppointmentEntry) sender.Graph).ServiceOrderTypeSelected).Current;
      fsBranchLocation = ((PXSelectBase<FSBranchLocation>) ((AppointmentEntry) sender.Graph).CurrentBranchLocation).Current;
    }
    if (fsSrvOrdType?.PostTo == "AR")
    {
      e.NewValue = (object) null;
    }
    else
    {
      if (!(fsSrvOrdType?.PostTo != "AR") || e.NewValue != null)
        return;
      object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
      if (obj == null)
        e.NewValue = fsBranchLocation == null || !fsBranchLocation.DfltSiteID.HasValue ? e.NewValue : (object) fsBranchLocation.DfltSiteID;
      else
        e.NewValue = obj;
    }
  }
}
