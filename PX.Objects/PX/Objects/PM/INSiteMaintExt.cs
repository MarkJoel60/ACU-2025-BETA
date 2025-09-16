// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.INSiteMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class INSiteMaintExt : PXGraphExtension<INSiteMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual void Persist(Action baseHandler)
  {
    foreach (INLocation inLocation in GraphHelper.RowCast<INLocation>((IEnumerable) ((PXSelectBase<INLocation>) this.Base.location).Select(Array.Empty<object>())).Where<INLocation>((Func<INLocation, bool>) (a => a.ProjectID.HasValue && a.Active.GetValueOrDefault() && !ProjectDefaultAttribute.IsNonProject(a.ProjectID))))
    {
      INLocation location = inLocation;
      if (((PXSelectBase) this.Base.location).Cache.GetStatus((object) location) != null)
      {
        IEnumerable<INLocation> inLocations = GraphHelper.RowCast<INLocation>((IEnumerable) ((PXSelectBase<INLocation>) this.Base.location).Select(Array.Empty<object>())).Where<INLocation>((Func<INLocation, bool>) (a =>
        {
          int? projectId1 = a.ProjectID;
          int? projectId2 = location.ProjectID;
          return projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue && a.Active.GetValueOrDefault();
        }));
        if (((IQueryable<PXResult<INLocation>>) PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INLocation.projectID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLocation.active, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) location.SiteID,
          (object) location.ProjectID
        })).Any<PXResult<INLocation>>())
          inLocations = (IEnumerable<INLocation>) inLocations.OrderBy<INLocation, DateTime?>((Func<INLocation, DateTime?>) (a => a.CreatedDateTime));
        if (inLocations.Any<INLocation>())
        {
          INLocation originalLocation = inLocations.First<INLocation>();
          if (location != originalLocation)
            this.ShowErrorWhenDuplicatedInSameWarehouse(location, inLocations, originalLocation);
        }
      }
    }
    baseHandler();
  }

  private void ShowErrorWhenDuplicatedInSameWarehouse(
    INLocation location,
    IEnumerable<INLocation> duplicatedInSameWarehoue,
    INLocation originalLocation)
  {
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, originalLocation.SiteID);
    INLocation inLocation = GraphHelper.RowCast<INLocation>((IEnumerable) duplicatedInSameWarehoue).Where<INLocation>((Func<INLocation, bool>) (a =>
    {
      int? taskId1 = a.TaskID;
      int? taskId2 = location.TaskID;
      return taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue;
    })).First<INLocation>();
    int num;
    if (location != inLocation)
    {
      int? taskId3 = location.TaskID;
      int? taskId4 = inLocation.TaskID;
      if (taskId3.GetValueOrDefault() == taskId4.GetValueOrDefault() & taskId3.HasValue == taskId4.HasValue)
      {
        num = inLocation.TaskID.HasValue ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    bool flag1 = num != 0;
    bool flag2 = false;
    IEnumerable<INLocation> source = duplicatedInSameWarehoue.Where<INLocation>((Func<INLocation, bool>) (a => !a.TaskID.HasValue));
    if (source.Any<INLocation>())
      flag2 = source.First<INLocation>() != location;
    int? taskId;
    if (!originalLocation.TaskID.HasValue)
    {
      taskId = location.TaskID;
      if (!taskId.HasValue)
      {
        PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, originalLocation.ProjectID);
        ((PXSelectBase) this.Base.location).Cache.RaiseExceptionHandling<INLocation.projectID>((object) location, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException("The {0} project is already assigned to the {1} warehouse location of the {2} warehouse.", new object[3]
        {
          (object) pmProject.ContractCD,
          (object) originalLocation.LocationCD,
          (object) inSite.SiteCD
        }));
        return;
      }
    }
    taskId = location.TaskID;
    if (!taskId.HasValue & flag2)
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, originalLocation.ProjectID);
      ((PXSelectBase) this.Base.location).Cache.RaiseExceptionHandling<INLocation.projectID>((object) location, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException("The {0} project is already assigned to the {1} warehouse location of the {2} warehouse. To be able to use the same project with multiple warehouse locations, assign different project tasks to each of these locations.", new object[3]
      {
        (object) pmProject.ContractCD,
        (object) originalLocation.LocationCD,
        (object) inSite.SiteCD
      }));
    }
    else
    {
      if (!flag1)
        return;
      PMTask pmTask = PMTask.PK.Find((PXGraph) this.Base, inLocation.ProjectID, inLocation.TaskID);
      ((PXSelectBase) this.Base.location).Cache.RaiseExceptionHandling<INLocation.taskID>((object) location, (object) pmTask.TaskCD, (Exception) new PXSetPropertyException("The {0} project task is already assigned to the {1} warehouse location of the {2} warehouse.", new object[3]
      {
        (object) pmTask.TaskCD,
        (object) inLocation.LocationCD,
        (object) ((PXSelectBase<PX.Objects.IN.INSite>) this.Base.site).Current.SiteCD
      }));
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<INLocation, INLocation.projectID> e)
  {
    INLocation row = e.Row;
    if ((row != null ? (row.ProjectID.HasValue ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INLocation, INLocation.projectID>>) e).Cache.SetValueExt<INLocation.isCosted>((object) e.Row, (object) true);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INLocation, INLocation.projectID> e)
  {
    if (e.Row == null)
      return;
    if (PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.projectID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.released, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) e.Row.ProjectID,
      (object) e.Row.LocationID
    })) != null)
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) e.Row.ProjectID ?? ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.projectID>, INLocation, object>) e).NewValue
      }));
      if (pmProject != null)
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.projectID>, INLocation, object>) e).NewValue = (object) pmProject.ContractCD;
      throw new PXSetPropertyException("Project cannot be changed. Atleast one Unrelased PO Receipt exists for the given Project.");
    }
    if (PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.projectID, Equal<P.AsInt>>>>, And<BqlOperand<SOShipLine.released, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<SOShipLine.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) e.Row.ProjectID,
      (object) e.Row.LocationID
    })) != null)
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) e.Row.ProjectID ?? ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.projectID>, INLocation, object>) e).NewValue
      }));
      if (pmProject != null)
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.projectID>, INLocation, object>) e).NewValue = (object) pmProject.ContractCD;
      throw new PXSetPropertyException("Project cannot be changed. Atleast one Unrelased SO Shipment exists for the given Project.");
    }
    if (PXResultset<INLocationStatusByCostCenter>.op_Implicit(PXSelectBase<INLocationStatusByCostCenter, PXViewOf<INLocationStatusByCostCenter>.BasedOn<SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocationStatusByCostCenter.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INLocationStatusByCostCenter.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLocationStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) e.Row.SiteID,
      (object) e.Row.LocationID
    })) != null)
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) e.Row.ProjectID ?? ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.projectID>, INLocation, object>) e).NewValue
      }));
      if (pmProject != null)
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.projectID>, INLocation, object>) e).NewValue = (object) pmProject.ContractCD;
      throw new PXSetPropertyException("Project cannot be changed. Available Quantity on this location is not zero.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INLocation, INLocation.taskID> e)
  {
    if (e.Row == null)
      return;
    if (PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.taskID, Equal<P.AsInt>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.released, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) e.Row.TaskID,
      (object) e.Row.LocationID
    })) != null)
    {
      PMTask dirty = PMTask.PK.FindDirty((PXGraph) this.Base, e.Row.ProjectID, e.Row.TaskID ?? (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.taskID>, INLocation, object>) e).NewValue);
      if (dirty != null)
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.taskID>, INLocation, object>) e).NewValue = (object) dirty.TaskCD;
      throw new PXSetPropertyException("Project Task cannot be changed. Atleast one Unrelased PO Receipt exists for the given Project Task.");
    }
    if (PXResultset<SOShipLine>.op_Implicit(PXSelectBase<SOShipLine, PXViewOf<SOShipLine>.BasedOn<SelectFromBase<SOShipLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.taskID, Equal<P.AsInt>>>>, And<BqlOperand<SOShipLine.released, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<SOShipLine.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) e.Row.TaskID,
      (object) e.Row.LocationID
    })) != null)
    {
      PMTask dirty = PMTask.PK.FindDirty((PXGraph) this.Base, e.Row.ProjectID, e.Row.TaskID ?? (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.taskID>, INLocation, object>) e).NewValue);
      if (dirty != null)
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.taskID>, INLocation, object>) e).NewValue = (object) dirty.TaskCD;
      throw new PXSetPropertyException("Project Task cannot be changed. Atleast one Unrelased SO Shipment exists for the given Project Task.");
    }
    if (PXResultset<INLocationStatusByCostCenter>.op_Implicit(PXSelectBase<INLocationStatusByCostCenter, PXViewOf<INLocationStatusByCostCenter>.BasedOn<SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocationStatusByCostCenter.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INLocationStatusByCostCenter.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLocationStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[2]
    {
      (object) e.Row.SiteID,
      (object) e.Row.LocationID
    })) != null)
    {
      PMTask dirty = PMTask.PK.FindDirty((PXGraph) this.Base, e.Row.ProjectID, e.Row.TaskID ?? (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.taskID>, INLocation, object>) e).NewValue);
      if (dirty != null)
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INLocation, INLocation.taskID>, INLocation, object>) e).NewValue = (object) dirty.TaskCD;
      throw new PXSetPropertyException("Project Task cannot be changed. Available Quantity on this location is not zero.");
    }
  }
}
