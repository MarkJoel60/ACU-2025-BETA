// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_EmployeeMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class SM_EmployeeMaint : PXGraphExtension<EmployeeMaint>
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXSelectJoin<FSEmployeeSkill, LeftJoin<FSSkill, On<FSSkill.skillID, Equal<FSEmployeeSkill.skillID>>>, Where<FSEmployeeSkill.employeeID, Equal<Current<EPEmployee.bAccountID>>>> EmployeeSkills;
  public PXSelectJoin<FSGeoZoneEmp, InnerJoin<FSGeoZone, On<FSGeoZone.geoZoneID, Equal<FSGeoZoneEmp.geoZoneID>>>, Where<FSGeoZoneEmp.employeeID, Equal<Current<EPEmployee.bAccountID>>>> EmployeeGeoZones;
  [PXViewDetailsButton(typeof (EPEmployee))]
  public PXSelectJoin<FSLicense, LeftJoin<FSLicenseType, On<FSLicenseType.licenseTypeID, Equal<FSLicense.licenseTypeID>>>, Where<FSLicense.employeeID, Equal<Current<EPEmployee.bAccountID>>>> EmployeeLicenses;
  public PXSelectJoin<FSEmployeeSkill, InnerJoin<FSSkill, On<FSSkill.skillID, Equal<FSEmployeeSkill.skillID>, And<FSSkill.isDriverSkill, Equal<True>>>>, Where<FSEmployeeSkill.employeeID, Equal<Current<EPEmployee.bAccountID>>>> EmployeeDriverSkills;
  public PXAction<EPEmployee> EmployeeSchedule;
  public PXAction<EPEmployee> OpenLicenseDocument;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXButton]
  [PXUIField]
  public virtual void employeeSchedule()
  {
    if (((PXSelectBase<EPEmployee>) this.Base.Employee).Current.BAccountID.HasValue && !string.IsNullOrEmpty(((PXSelectBase<EPEmployee>) this.Base.Employee).Current.AcctName))
      throw new PXRedirectToGIWithParametersRequiredException(new Guid("ae872579-713f-4b93-95ad-89d8dc51a7e6"), new Dictionary<string, string>()
      {
        ["StaffMember"] = ((PXSelectBase<EPEmployee>) this.Base.Employee).Current.AcctCD
      });
    throw new PXException("Please select an Employee.");
  }

  [PXButton]
  [PXUIField]
  public virtual void openLicenseDocument()
  {
    FSLicense current = ((PXSelectBase<FSLicense>) this.EmployeeLicenses).Current;
    LicenseMaint instance = PXGraph.CreateInstance<LicenseMaint>();
    ((PXSelectBase<FSLicense>) instance.LicenseRecords).Current = PXResultset<FSLicense>.op_Implicit(((PXSelectBase<FSLicense>) instance.LicenseRecords).Search<FSLicense.refNbr>((object) ((PXSelectBase<FSLicense>) this.EmployeeLicenses).Current.RefNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Skill")]
  protected virtual void _(PX.Data.Events.CacheAttached<FSEmployeeSkill.skillID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<FSGeoZoneEmp.employeeID>>>>))]
  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  public virtual void FSGeoZoneEmp_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<FSLicense.employeeID>>>>))]
  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  public virtual void FSLicense_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Service Area")]
  [PXSelector(typeof (FSGeoZone.geoZoneID), SubstituteKey = typeof (FSGeoZone.geoZoneCD))]
  public virtual void FSGeoZoneEmp_GeoZoneID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "License Type")]
  [PXSelector(typeof (Search<FSLicenseType.licenseTypeID>), SubstituteKey = typeof (FSLicenseType.licenseTypeCD), DescriptionField = typeof (FSLicense.descr))]
  public virtual void FSLicense_LicenseTypeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [AutoNumber(typeof (Search<FSSetup.licenseNumberingID>), typeof (AccessInfo.businessDate))]
  public virtual void FSLicense_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "License ID", Enabled = false)]
  public virtual void FSLicense_LicenseID_CacheAttached(PXCache sender)
  {
  }

  public virtual void EnableDisableGrids(bool enableGrid)
  {
    ((PXSelectBase) this.EmployeeSkills).Cache.AllowInsert = enableGrid;
    ((PXSelectBase) this.EmployeeSkills).Cache.AllowUpdate = enableGrid;
    ((PXSelectBase) this.EmployeeSkills).Cache.AllowDelete = enableGrid;
    ((PXSelectBase) this.EmployeeGeoZones).Cache.AllowInsert = enableGrid;
    ((PXSelectBase) this.EmployeeGeoZones).Cache.AllowUpdate = enableGrid;
    ((PXSelectBase) this.EmployeeGeoZones).Cache.AllowDelete = enableGrid;
    ((PXSelectBase) this.EmployeeLicenses).Cache.AllowInsert = enableGrid;
    ((PXSelectBase) this.EmployeeLicenses).Cache.AllowUpdate = enableGrid;
    ((PXSelectBase) this.EmployeeLicenses).Cache.AllowDelete = enableGrid;
  }

  public virtual void EnableDisableLicenseFields(
    PXCache cache,
    FSLicense fsLicenseRow,
    bool enabled)
  {
    PXUIFieldAttribute.SetEnabled<FSLicense.licenseTypeID>(cache, (object) fsLicenseRow, !enabled);
    PXUIFieldAttribute.SetEnabled<FSLicense.descr>(cache, (object) fsLicenseRow, enabled);
    PXUIFieldAttribute.SetEnabled<FSLicense.issueDate>(cache, (object) fsLicenseRow, enabled);
    PXCache pxCache = cache;
    FSLicense fsLicense = fsLicenseRow;
    int num;
    if (!fsLicenseRow.NeverExpires.HasValue)
    {
      num = 1;
    }
    else
    {
      bool? neverExpires = fsLicenseRow.NeverExpires;
      num = (neverExpires.HasValue ? new bool?(!neverExpires.GetValueOrDefault()) : new bool?()).Value ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<FSLicense.expirationDate>(pxCache, (object) fsLicense, num != 0);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSEmployeeSkill> e)
  {
    if (e.Row == null)
      return;
    FSEmployeeSkill row = e.Row;
    PXUIFieldAttribute.SetEnabled<FSEmployeeSkill.skillID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSEmployeeSkill>>) e).Cache, (object) row, string.IsNullOrEmpty(row.SkillID.ToString()));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSEmployeeSkill> e)
  {
    if (e.Row == null)
      return;
    FSEmployeeSkill row = e.Row;
    if (PXResultset<FSEmployeeSkill>.op_Implicit(PXSelectBase<FSEmployeeSkill, PXSelect<FSEmployeeSkill, Where<FSEmployeeSkill.skillID, Equal<Required<FSEmployeeSkill.skillID>>, And<FSEmployeeSkill.employeeID, Equal<Current<EPEmployee.bAccountID>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) row.SkillID
    })) == null)
      return;
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSEmployeeSkill>>) e).Cache.RaiseExceptionHandling<FSEmployeeSkill.skillID>((object) e.Row, (object) row.SkillID, (Exception) new PXException("This ID is already in use."));
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSEmployeeSkill> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSLicense, FSLicense.licenseTypeID> e)
  {
    FSLicense row = e.Row;
    if (row == null || !row.LicenseTypeID.HasValue)
      return;
    FSLicenseType fsLicenseType = FSLicenseType.PK.Find(new PXGraph(), row.LicenseTypeID);
    if (fsLicenseType == null)
      return;
    row.Descr = fsLicenseType.Descr;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSLicense, FSLicense.neverExpires> e)
  {
    FSLicense row = e.Row;
    if (row == null || !row.NeverExpires.GetValueOrDefault())
      return;
    row.ExpirationDate = new DateTime?();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSLicense> e)
  {
    if (e.Row == null)
      return;
    FSLicense row = e.Row;
    this.EnableDisableLicenseFields(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLicense>>) e).Cache, row, row.LicenseTypeID.HasValue);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSLicense>>) e).Cache;
    FSLicense fsLicense = row;
    bool? neverExpires = row.NeverExpires;
    int num;
    if (neverExpires.HasValue)
    {
      neverExpires = row.NeverExpires;
      if (neverExpires.GetValueOrDefault())
      {
        num = 2;
        goto label_5;
      }
    }
    num = 1;
label_5:
    PXDefaultAttribute.SetPersistingCheck<FSLicense.expirationDate>(cache, (object) fsLicense, (PXPersistingCheck) num);
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSLicense> e)
  {
    if (e.Row == null)
      return;
    FSLicense row = e.Row;
    if (string.IsNullOrEmpty(PXResultset<FSSetup>.op_Implicit(((PXSelectBase<FSSetup>) this.SetupRecord).Select(Array.Empty<object>())).LicenseNumberingID))
    {
      string displayName = DACHelper.GetDisplayName(typeof (FSSetup));
      ((PXSelectBase) this.EmployeeLicenses).Cache.RaiseExceptionHandling<FSLicense.refNbr>((object) row, (object) row.RefNbr, (Exception) new PXSetPropertyException("The license numbering sequence has not been specified. Specify it in the License Numbering Sequence box on the {0} form.", (PXErrorLevel) 5, new object[1]
      {
        (object) displayName
      }));
      throw new PXException("The license numbering sequence has not been specified. Specify it in the License Numbering Sequence box on the {0} form.", new object[1]
      {
        (object) displayName
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSLicense> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSGeoZoneEmp> e)
  {
    if (e.Row == null)
      return;
    FSGeoZoneEmp row = e.Row;
    PXUIFieldAttribute.SetEnabled<FSGeoZoneEmp.geoZoneID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSGeoZoneEmp>>) e).Cache, (object) row, string.IsNullOrEmpty(row.GeoZoneID.ToString()));
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSGeoZoneEmp> e)
  {
    if (e.Row == null)
      return;
    FSGeoZoneEmp row = e.Row;
    if (PXResultset<FSGeoZoneEmp>.op_Implicit(PXSelectBase<FSGeoZoneEmp, PXSelect<FSGeoZoneEmp, Where<FSGeoZoneEmp.geoZoneID, Equal<Required<FSGeoZoneEmp.geoZoneID>>, And<FSGeoZoneEmp.employeeID, Equal<Current<EPEmployee.bAccountID>>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[1]
    {
      (object) row.GeoZoneID
    })) == null)
      return;
    ((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<FSGeoZoneEmp>>) e).Cache.RaiseExceptionHandling<FSGeoZoneEmp.geoZoneID>((object) e.Row, (object) row.GeoZoneID, (Exception) new PXException("This ID is already in use."));
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSGeoZoneEmp> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPEmployee> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.EmployeeSchedule).SetEnabled(PXCache<EPEmployee>.GetExtension<FSxEPEmployee>(e.Row).SDEnabled.GetValueOrDefault());
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPEmployee> e)
  {
    if (e.Row == null)
      return;
    this.EnableDisableGrids(PXCache<EPEmployee>.GetExtension<FSxEPEmployee>(e.Row).SDEnabled.GetValueOrDefault());
  }

  protected virtual void _(PX.Data.Events.RowInserting<EPEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<EPEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<EPEmployee> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPEmployee> e)
  {
    if (e.Row == null)
      return;
    PXCache<EPEmployee>.GetExtension<FSxEPEmployee>(e.Row).IsDriver = new bool?(((PXSelectBase<FSEmployeeSkill>) this.EmployeeDriverSkills).Select(Array.Empty<object>()).Count > 0);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<EPEmployee> e)
  {
  }
}
