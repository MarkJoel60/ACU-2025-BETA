// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.GeoZoneMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class GeoZoneMaint : PXGraph<GeoZoneMaint, FSGeoZone>
{
  [PXImport(typeof (FSGeoZone))]
  public PXSelect<FSGeoZone> GeoZoneRecords;
  [PXImport(typeof (FSGeoZone))]
  public PXSelect<FSGeoZoneEmp, Where<FSGeoZoneEmp.geoZoneID, Equal<Current<FSGeoZone.geoZoneID>>>> GeoZoneEmpRecords;
  [PXImport(typeof (FSGeoZone))]
  public PXSelect<FSGeoZonePostalCode, Where<FSGeoZonePostalCode.geoZoneID, Equal<Current<FSGeoZone.geoZoneID>>>> GeoZonePostalCodeRecords;

  [PXMergeAttributes]
  [PXSelector(typeof (FSGeoZone.geoZoneCD))]
  protected virtual void _(Events.CacheAttached<FSGeoZone.geoZoneCD> e)
  {
  }

  public virtual void EnableDisableGeoZonePostalCode(
    PXCache cache,
    FSGeoZonePostalCode fsGeoZonePostalCodeRow)
  {
    PXUIFieldAttribute.SetEnabled<FSGeoZonePostalCode.postalCode>(cache, (object) fsGeoZonePostalCodeRow, string.IsNullOrEmpty(fsGeoZonePostalCodeRow.PostalCode));
  }

  public virtual void EnableDisableGeoZoneEmployee(PXCache cache, FSGeoZoneEmp fsGeoZoneEmpRow)
  {
    PXUIFieldAttribute.SetEnabled<FSGeoZoneEmp.employeeID>(cache, (object) fsGeoZoneEmpRow, !fsGeoZoneEmpRow.EmployeeID.HasValue);
  }

  protected virtual void _(Events.RowSelecting<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSGeoZoneEmp> e)
  {
    if (e.Row == null)
      return;
    this.EnableDisableGeoZoneEmployee(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSGeoZoneEmp>>) e).Cache, e.Row);
  }

  protected virtual void _(Events.RowInserting<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowPersisted<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(Events.RowSelecting<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowSelected<FSGeoZonePostalCode> e)
  {
    if (e.Row == null)
      return;
    this.EnableDisableGeoZonePostalCode(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<FSGeoZonePostalCode>>) e).Cache, e.Row);
  }

  protected virtual void _(Events.RowInserting<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowInserted<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowUpdating<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowUpdated<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowDeleting<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowDeleted<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowPersisting<FSGeoZonePostalCode> e)
  {
  }

  protected virtual void _(Events.RowPersisted<FSGeoZonePostalCode> e)
  {
  }
}
