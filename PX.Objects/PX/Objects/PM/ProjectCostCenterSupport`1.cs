// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectCostCenterSupport`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProjectCostCenterSupport<T> : 
  ProjectCostCenterBase<T>,
  IINTranCostCenterSupport,
  ICostCenterSupport<INTran>
  where T : INRegisterEntryBase
{
  public bool IsSupported(string inventorySource)
  {
    return inventorySource == "P" || inventorySource == "F";
  }

  public bool IsSupported(INTran tran) => !tran.IsSpecialOrder.GetValueOrDefault();

  public virtual int SortOrder => 200;

  public bool IsShipmentPosting { get; set; }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.inventorySource>, NotEqual<InventorySourceType.freeStock>>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<True>>>), "The Free Stock inventory source cannot be used if a project is selected in the line. Select a different inventory source or specify the non-project code in the line.", new Type[] {}, SuppressVerify = true)]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.inventorySource>, NotEqual<InventorySourceType.projectStock>>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsNotEqual<True>>>), "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.toInventorySource>, NotEqual<InventorySourceType.freeStock>>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<True>>>), "The Free Stock inventory source cannot be used if a project is selected in the line. Select a different inventory source or specify the non-project code in the line.", new Type[] {}, SuppressVerify = true)]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<INTran.toInventorySource>, NotEqual<InventorySourceType.projectStock>>>>>.Or<BqlOperand<PMProject.nonProject, IBqlBool>.IsNotEqual<True>>>), "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.toProjectID> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<INTran, INTran.projectID> e)
  {
    if (!(e.Row?.InventorySource == "P") || this.IsProjectLocation((int?) e.Row?.LocationID))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.projectID>, INTran, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<INTran, INTran.projectID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<INTran, INTran.projectID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue == null || !((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>>) e).ExternalCall)
      return;
    this.ValidateFreeStockForProject((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.projectID>, INTran, object>) e).NewValue, e.Row.InventorySource, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID>, INTran, object>) e).NewValue == null || !((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID>>) e).ExternalCall)
      return;
    this.ValidateFreeStockForProject((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTran, INTran.toProjectID>, INTran, object>) e).NewValue, e.Row.ToInventorySource, e.Row);
  }

  protected virtual void ValidateFreeStockForProject(
    int? projectID,
    string inventorySource,
    INTran row)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) (object) this.Base, projectID);
    if (pmProject != null && !pmProject.NonProject.GetValueOrDefault() && !(inventorySource != "F"))
      throw new PXSetPropertyException((IBqlTable) row, "The Free Stock inventory source cannot be used if a project is selected in the line. Select a different inventory source or specify the non-project code in the line.", (PXErrorLevel) 4)
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID> e)
  {
    if (e.Row == null)
      return;
    INTran inTran = (INTran) PXParentAttribute.SelectParent(((PXSelectBase) this.Base.INTranSplitDataMember).Cache, (object) e.Row, typeof (INTran));
    if (inTran == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID>, INTranSplit, object>) e).NewValue == null)
      return;
    int? locationId = inTran.LocationID;
    int? nullable = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID>, INTranSplit, object>) e).NewValue;
    if (!(locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue) && this.GetAccountingMode(inTran.ProjectID) == "P" && !this.IsShipmentPosting)
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) (object) this.Base, inTran.ProjectID);
      INLocation inLocation = INLocation.PK.Find((PXGraph) (object) this.Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<INTranSplit, INTranSplit.locationID>, INTranSplit, object>) e).NewValue);
      object[] objArray = new object[2]
      {
        (object) pmProject.ContractCD.Trim(),
        null
      };
      nullable = e.Row.LineNbr;
      ref int? local = ref nullable;
      objArray[1] = (object) (local.HasValue ? local.GetValueOrDefault().ToString() : (string) null);
      PXSetPropertyException propertyException = new PXSetPropertyException("The issue transaction cannot be saved because the {0} project selected in the line {1} has the Track by Project Quantity and Cost inventory tracking mode, and different warehouse locations have been selected in the line splits for this line. Select the same warehouse location in all line splits for the line.", objArray);
      if (inLocation != null)
        propertyException.ErrorValue = (object) inLocation.LocationCD;
      throw propertyException;
    }
  }

  public virtual IEnumerable<Type> GetFieldsDependOn()
  {
    yield return typeof (INTran.isSpecialOrder);
    yield return typeof (INTran.siteID);
    yield return typeof (INTran.locationID);
    yield return typeof (INTran.projectID);
    yield return typeof (INTran.taskID);
  }

  public virtual bool IsSpecificCostCenter(INTran tran)
  {
    return !tran.IsSpecialOrder.GetValueOrDefault() && tran.InventorySource != "F" && this.IsSpecificCostCenter(tran.SiteID, tran.ProjectID, tran.TaskID);
  }

  public virtual IEnumerable<Type> GetDestinationFieldsDependOn()
  {
    yield return typeof (INTran.isSpecialOrder);
    yield return typeof (INTran.toSiteID);
    yield return typeof (INTran.toLocationID);
    yield return typeof (INTran.toProjectID);
    yield return typeof (INTran.toTaskID);
  }

  public virtual bool IsDestinationSpecificCostCenter(INTran tran)
  {
    return tran.ToInventorySource != "F" && this.IsSpecificCostCenter(tran.ToSiteID, tran.ToProjectID, tran.ToTaskID);
  }

  public virtual int GetCostCenterID(INTran tran)
  {
    return this.FindOrCreateCostCenter(tran.SiteID, tran.ProjectID, tran.TaskID).CostCenterID.Value;
  }

  public virtual INCostCenter GetCostCenter(INTran tran)
  {
    return this.FindOrCreateCostCenter(tran.SiteID, tran.ProjectID, tran.TaskID);
  }

  public virtual INCostCenter GetDestinationCostCenter(INTran tran)
  {
    return this.FindOrCreateCostCenter(tran.ToSiteID, tran.ToProjectID, tran.ToTaskID);
  }

  public virtual void OnInventorySourceChanged(
    INTran tran,
    string newInventorySource,
    bool isExternalCall)
  {
    ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).SetValueExt<INTran.taskID>((object) tran, (object) null);
    ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).SetValueExt<INTran.costCodeID>((object) tran, (object) null);
    if (!isExternalCall)
      return;
    object projectId = (object) tran.ProjectID;
    try
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseFieldVerifying<INTran.projectID>((object) tran, ref projectId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).SetValueExt<INTran.projectID>((object) tran, (object) null);
    }
  }

  public virtual void OnDestinationInventorySourceChanged(
    INTran tran,
    string newInventorySource,
    bool isExternalCall)
  {
    ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).SetValueExt<INTran.toTaskID>((object) tran, (object) null);
    ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).SetValueExt<INTran.toCostCodeID>((object) tran, (object) null);
    if (!isExternalCall)
      return;
    object toProjectId = (object) tran.ToProjectID;
    try
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseFieldVerifying<INTran.toProjectID>((object) tran, ref toProjectId);
      this.ValidateFreeStockForProject((int?) toProjectId, tran.ToInventorySource, tran);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).SetValueExt<INTran.toProjectID>((object) tran, (object) null);
    }
  }

  public virtual void ValidateForPersisting(INTran tran)
  {
    this.ValidateProjectLayerFields(tran);
    PMProject pmProject = PMProject.PK.Find((PXGraph) (object) this.Base, tran.ProjectID);
    if (pmProject == null)
      return;
    bool valueOrDefault = pmProject.NonProject.GetValueOrDefault();
    if (tran.InventorySource == "F" && !valueOrDefault && pmProject.AccountingMode != "L")
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.projectID>((object) tran, (object) pmProject?.ContractCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "The Free Stock inventory source cannot be used if a project is selected in the line. Select a different inventory source or specify the non-project code in the line.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("projectID", (object) tran.ProjectID, "The Free Stock inventory source cannot be used if a project is selected in the line. Select a different inventory source or specify the non-project code in the line.");
    }
    if (tran.InventorySource == "P" & valueOrDefault)
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.projectID>((object) tran, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("projectID", (object) tran.ProjectID, "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.");
    }
  }

  public virtual void ValidateProjectLayerFields(INTran tran)
  {
    if (tran.CostLayerType == "P" && (!tran.TaskID.HasValue || !tran.SiteID.HasValue))
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) (object) this.Base, tran.ProjectID);
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.projectID>((object) tran, (object) pmProject?.ContractCD, (Exception) new PXSetPropertyException("The Warehouse, Location, Project, and Project Task columns cannot be empty in lines with the cost layer of the Project type.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("projectID", (object) tran.ProjectID, "The Warehouse, Location, Project, and Project Task columns cannot be empty in lines with the cost layer of the Project type.");
    }
  }

  public virtual void ValidateDestinationForPersisting(INTran tran)
  {
    this.ValidateDestinationProjectLayerFields(tran);
    PMProject pmProject = PMProject.PK.Find((PXGraph) (object) this.Base, tran.ToProjectID);
    if (pmProject == null)
      return;
    bool valueOrDefault = pmProject.NonProject.GetValueOrDefault();
    if (tran.ToInventorySource == "F" && !valueOrDefault && pmProject.AccountingMode != "L")
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.toProjectID>((object) tran, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "The Free Stock inventory source cannot be used if a project is selected in the line. Select a different inventory source or specify the non-project code in the line.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("toProjectID", (object) tran.ToProjectID, "The Free Stock inventory source cannot be used if a project is selected in the line. Select a different inventory source or specify the non-project code in the line.");
    }
    if (tran.ToInventorySource == "P" & valueOrDefault)
    {
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.toProjectID>((object) tran, (object) pmProject.ContractCD, (Exception) new PXSetPropertyException((IBqlTable) tran, "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("toProjectID", (object) tran.ToProjectID, "The Project Stock inventory source cannot be used if the non-project code is selected in the line. Select a different inventory source or specify a project for this line.");
    }
  }

  public virtual void ValidateDestinationProjectLayerFields(INTran tran)
  {
    if (tran.ToCostLayerType == "P" && (!tran.ToTaskID.HasValue || !tran.ToSiteID.HasValue))
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) (object) this.Base, tran.ToProjectID);
      ((PXCache) GraphHelper.Caches<INTran>((PXGraph) (object) this.Base)).RaiseExceptionHandling<INTran.toProjectID>((object) tran, (object) pmProject?.ContractCD, (Exception) new PXSetPropertyException("The Destination Warehouse, Location, Project, and Project Task columns cannot be empty in lines with the cost layer of the Project type.", (PXErrorLevel) 4));
      throw new PXRowPersistingException("toProjectID", (object) tran.ToProjectID, "The Destination Warehouse, Location, Project, and Project Task columns cannot be empty in lines with the cost layer of the Project type.");
    }
  }
}
