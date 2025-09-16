// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.VehicleMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;

#nullable disable
namespace PX.Objects.FS;

public class VehicleMaint : PXGraph<VehicleMaint, EPEquipment>
{
  public PXSelectJoin<EPEquipment, LeftJoin<FSVehicle, On<FSVehicle.sourceID, Equal<EPEquipment.equipmentID>, And<FSVehicle.sourceType, Equal<ListField_SourceType_Equipment.Vehicle>>>>, Where<FSVehicle.SMequipmentID, IsNotNull>> EPEquipmentRecords;
  public PXSelect<FSVehicle, Where<FSVehicle.sourceID, Equal<Current<EPEquipment.equipmentID>>, And<FSVehicle.sourceType, Equal<ListField_SourceType_Equipment.Vehicle>>>> VehicleSelected;
  [PXViewName("Answers")]
  public CRAttributeList<FSVehicle> Answers;
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;

  public VehicleMaint()
  {
    if (((PXSelectBase<FSSetup>) this.SetupRecord).Current == null || ((PXSelectBase<FSSetup>) this.SetupRecord).Current.EquipmentNumberingID == null)
      throw new PXSetupNotEnteredException("The equipment numbering sequence has not been specified. Specify it in the Equipment Numbering Sequence box on the {0} form.", typeof (FSEquipmentSetup), new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSEquipmentSetup))
      });
  }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search2<EPEquipment.equipmentCD, InnerJoin<FSVehicle, On<FSVehicle.sourceID, Equal<EPEquipment.equipmentID>, And<FSVehicle.sourceType, Equal<ListField_SourceType_Equipment.Vehicle>>>>>), new System.Type[] {typeof (EPEquipment.equipmentCD), typeof (FSEquipment.status), typeof (FSEquipment.descr), typeof (FSEquipment.registrationNbr), typeof (FSVehicle.manufacturerModelID), typeof (FSVehicle.manufacturerID), typeof (FSEquipment.manufacturingYear), typeof (FSEquipment.color)})]
  [AutoNumber(typeof (Search<FSSetup.equipmentNumberingID>), typeof (AccessInfo.businessDate))]
  [PXUIField]
  protected virtual void EPEquipment_EquipmentCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<AccessInfo.branchID>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<AccessInfo.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  protected virtual void EPEquipment_BranchLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(typeof (Search<FSEquipment.descr, Where<FSEquipment.sourceID, Equal<Current<EPEquipment.equipmentID>>, And<FSEquipment.sourceType, Equal<ListField_SourceType_Equipment.Vehicle>>>>))]
  protected virtual void EPEquipment_VehicleDescr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSEquipment.refNbr, Where<FSEquipment.isVehicle, Equal<True>>>), new System.Type[] {typeof (FSEquipment.refNbr), typeof (FSEquipment.status), typeof (FSEquipment.descr), typeof (FSEquipment.registrationNbr), typeof (FSEquipment.manufacturerModelID), typeof (FSEquipment.manufacturerID), typeof (FSEquipment.manufacturingYear), typeof (FSEquipment.color)}, DescriptionField = typeof (FSEquipment.descr))]
  protected virtual void FSVehicle_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (EPEquipment.equipmentID))]
  [PXDBChildIdentity(typeof (EPEquipment.equipmentID))]
  protected virtual void FSVehicle_SourceID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("VEH")]
  [PXUIField(DisplayName = "Source Type", Enabled = false)]
  [ListField_SourceType_Equipment.ListAtrribute]
  public virtual void FSVehicle_SourceType_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Is Vehicle", Enabled = false)]
  protected virtual void FSVehicle_IsVehicle_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Vehicle Type ID")]
  [PXDefault]
  [PXSelector(typeof (FSVehicleType.vehicleTypeID), SubstituteKey = typeof (FSVehicleType.vehicleTypeCD))]
  public virtual void FSVehicle_VehicleTypeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Target Equipment")]
  protected virtual void FSVehicle_RequireMaintenance_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FSVehicle, FSEquipment.vehicleTypeID> e)
  {
    if (e.Row == null)
      return;
    FSVehicle row = e.Row;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FSVehicle, FSEquipment.vehicleTypeID>>) e).Cache.SetDefaultExt<FSVehicle.vehicleTypeCD>((object) row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSVehicle> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<FSVehicle> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<FSVehicle> e)
  {
    if (e.Row == null || ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current == null)
      return;
    e.Row.RefNbr = ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current.EquipmentCD;
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSVehicle> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FSVehicle> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FSVehicle> e)
  {
    if (e.Row == null)
      return;
    FSVehicle row = e.Row;
    if (!EquipmentHelper.UpdateEPEquipmentWithFSEquipment(((PXSelectBase) this.EPEquipmentRecords).Cache, ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FSVehicle>>) e).Cache, (FSEquipment) row))
      return;
    ((PXSelectBase) this.EPEquipmentRecords).Cache.Update((object) ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<FSVehicle> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FSVehicle> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FSVehicle> e)
  {
    if (e.Row == null || ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current == null)
      return;
    FSVehicle row = e.Row;
    if (e.Operation != 2)
      return;
    row.RefNbr = ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current.EquipmentCD;
    row.SourceRefNbr = ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current.EquipmentCD;
    row.SourceID = ((PXSelectBase<EPEquipment>) this.EPEquipmentRecords).Current.EquipmentID;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<FSVehicle> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPEquipment, EPEquipment.fixedAssetID> e)
  {
    if (e.Row == null)
      return;
    EPEquipment row = e.Row;
    if (!row.FixedAssetID.HasValue)
      return;
    EquipmentHelper.SetDefaultValuesFromFixedAsset(((PXSelectBase) this.VehicleSelected).Cache, (FSEquipment) ((PXSelectBase<FSVehicle>) this.VehicleSelected).Current, row.FixedAssetID);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserting<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPEquipment> e)
  {
    if (e.Row == null)
      return;
    EPEquipment row = e.Row;
    if (((PXSelectBase<FSVehicle>) this.VehicleSelected).Current != null)
      return;
    ((PXSelectBase<FSVehicle>) this.VehicleSelected).Insert(new FSVehicle()).SourceID = row.EquipmentID;
    ((PXSelectBase) this.VehicleSelected).Cache.IsDirty = false;
  }

  protected virtual void _(PX.Data.Events.RowUpdating<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleting<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowDeleted<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisted<EPEquipment> e)
  {
    if (e.Row == null || ((PXSelectBase<FSVehicle>) this.VehicleSelected).Current == null)
      return;
    int? equipmentId = e.Row.EquipmentID;
    int? sourceId = ((PXSelectBase<FSVehicle>) this.VehicleSelected).Current.SourceID;
    if (!(equipmentId.GetValueOrDefault() == sourceId.GetValueOrDefault() & equipmentId.HasValue == sourceId.HasValue) || e.Operation != 3)
      return;
    if (e.TranStatus != null)
      return;
    try
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXSelectBase<FSVehicle>) this.VehicleSelected).Delete(((PXSelectBase<FSVehicle>) this.VehicleSelected).Current);
        ((PXSelectBase) this.VehicleSelected).Cache.Persist((PXDBOperation) 3);
        transactionScope.Complete();
      }
      ((PXSelectBase) this.VehicleSelected).Cache.Persisted(false);
    }
    catch
    {
      ((PXSelectBase) this.VehicleSelected).Cache.Persisted(true);
      throw;
    }
  }
}
