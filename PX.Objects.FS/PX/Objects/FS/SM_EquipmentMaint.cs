// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SM_EquipmentMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.FS;

public class SM_EquipmentMaint : PXGraphExtension<EquipmentMaint>
{
  public PXAction<EPEquipment> extendToSMEquipment;
  public PXAction<EPEquipment> viewInSMEquipment;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
  }

  private FSEquipment GetRelatedFSEquipmentRow(PXGraph graph)
  {
    return PXResultset<FSEquipment>.op_Implicit(PXSelectBase<FSEquipment, PXSelect<FSEquipment, Where<FSEquipment.sourceID, Equal<Current<EPEquipment.equipmentID>>, And<Where<FSEquipment.sourceType, Equal<ListField_SourceType_Equipment.EP_Equipment>, Or<FSEquipment.sourceType, Equal<ListField_SourceType_Equipment.Vehicle>>>>>>.Config>.Select(graph, Array.Empty<object>()));
  }

  [PXUIField]
  [PXButton]
  public virtual void ExtendToSMEquipment()
  {
    SMEquipmentMaint instance = PXGraph.CreateInstance<SMEquipmentMaint>();
    ((PXSelectBase<FSEquipment>) instance.EquipmentRecords).Current = ((PXSelectBase<FSEquipment>) instance.EquipmentRecords).Insert(new FSEquipment()
    {
      SourceID = ((PXSelectBase<EPEquipment>) this.Base.Equipment).Current.EquipmentID,
      SourceRefNbr = ((PXSelectBase<EPEquipment>) this.Base.Equipment).Current.EquipmentCD,
      SourceType = "EPE",
      RequireMaintenance = new bool?(false),
      ResourceEquipment = new bool?(true)
    });
    EquipmentHelper.UpdateFSEquipmentWithEPEquipment(((PXSelectBase) instance.EquipmentRecords).Cache, ((PXSelectBase<FSEquipment>) instance.EquipmentRecords).Current, ((PXSelectBase) this.Base.Equipment).Cache, ((PXSelectBase<EPEquipment>) this.Base.Equipment).Current);
    EquipmentHelper.SetDefaultValuesFromFixedAsset(((PXSelectBase) instance.EquipmentRecords).Cache, ((PXSelectBase<FSEquipment>) instance.EquipmentRecords).Current, ((PXSelectBase<EPEquipment>) this.Base.Equipment).Current.FixedAssetID);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewInSMEquipment()
  {
    FSEquipment relatedFsEquipmentRow = this.GetRelatedFSEquipmentRow((PXGraph) this.Base);
    if (relatedFsEquipmentRow == null)
      return;
    if (relatedFsEquipmentRow.SourceType == "VEH")
    {
      VehicleMaint instance = PXGraph.CreateInstance<VehicleMaint>();
      ((PXSelectBase<EPEquipment>) instance.EPEquipmentRecords).Current = PXResultset<EPEquipment>.op_Implicit(((PXSelectBase<EPEquipment>) instance.EPEquipmentRecords).Search<EPEquipment.equipmentCD>((object) relatedFsEquipmentRow.SourceRefNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    SMEquipmentMaint instance1 = PXGraph.CreateInstance<SMEquipmentMaint>();
    ((PXSelectBase<FSEquipment>) instance1.EquipmentRecords).Current = PXResultset<FSEquipment>.op_Implicit(((PXSelectBase<FSEquipment>) instance1.EquipmentRecords).Search<FSEquipment.refNbr>((object) relatedFsEquipmentRow.RefNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException1 = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
    ((PXBaseRedirectException) requiredException1).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException1;
  }

  protected virtual void _(PX.Data.Events.RowSelecting<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPEquipment> e)
  {
    if (e.Row == null)
      return;
    EPEquipment row = e.Row;
    FSEquipment relatedFsEquipmentRow = this.GetRelatedFSEquipmentRow(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPEquipment>>) e).Cache.Graph);
    ((PXAction) this.extendToSMEquipment).SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPEquipment>>) e).Cache.GetStatus((object) row) != 2 && relatedFsEquipmentRow == null);
    ((PXAction) this.viewInSMEquipment).SetEnabled(relatedFsEquipmentRow != null);
  }

  protected virtual void _(PX.Data.Events.RowInserting<EPEquipment> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<EPEquipment> e)
  {
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
    if (e.Row == null || e.TranStatus != null)
      return;
    EPEquipment row = e.Row;
    FSEquipment relatedFsEquipmentRow = this.GetRelatedFSEquipmentRow(((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPEquipment>>) e).Cache.Graph);
    if (relatedFsEquipmentRow == null)
      return;
    PXCache<FSEquipment> cacheFSEquipment = new PXCache<FSEquipment>((PXGraph) this.Base);
    ((PXCache) cacheFSEquipment).Graph.SelectTimeStamp();
    if (!EquipmentHelper.UpdateFSEquipmentWithEPEquipment((PXCache) cacheFSEquipment, relatedFsEquipmentRow, ((PX.Data.Events.Event<PXRowPersistedEventArgs, PX.Data.Events.RowPersisted<EPEquipment>>) e).Cache, row))
      return;
    cacheFSEquipment.Update(relatedFsEquipmentRow);
    ((PXCache) cacheFSEquipment).Persist((PXDBOperation) 1);
  }
}
