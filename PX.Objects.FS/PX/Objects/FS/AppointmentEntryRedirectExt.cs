// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentEntryRedirectExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Extensions;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public class AppointmentEntryRedirectExt : 
  PXGraphExtension<RedirectExtension<AppointmentEntry>, AppointmentEntry>
{
  public PXAction<VendorR> viewPOVendor;
  public PXAction<VendorR> viewPOVendorLocation;
  public PXAction<VendorR> viewEmployee;

  private RedirectExtension<AppointmentEntry> BaseRedirect { get; set; }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ViewPOVendor(PXAdapter adapter)
  {
    this.BaseRedirect = ((PXGraph) ((PXGraphExtension<AppointmentEntry>) this).Base).GetExtension<RedirectExtension<AppointmentEntry>>();
    return this.BaseRedirect.ViewCustomerVendorEmployee<FSAppointmentDet.poVendorID>(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ViewPOVendorLocation(PXAdapter adapter)
  {
    this.BaseRedirect = ((PXGraph) ((PXGraphExtension<AppointmentEntry>) this).Base).GetExtension<RedirectExtension<AppointmentEntry>>();
    return this.BaseRedirect.ViewVendorLocation<FSAppointmentDet.poVendorLocationID, FSAppointmentDet.poVendorID>(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ViewEmployee(PXAdapter adapter)
  {
    this.BaseRedirect = ((PXGraph) ((PXGraphExtension<AppointmentEntry>) this).Base).GetExtension<RedirectExtension<AppointmentEntry>>();
    return this.BaseRedirect.ViewCustomerVendorEmployee<FSAppointmentEmployee.employeeID>(adapter);
  }
}
