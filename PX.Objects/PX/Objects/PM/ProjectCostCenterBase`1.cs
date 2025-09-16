// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectCostCenterBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ProjectCostCenterBase<T> : PXGraphExtension<T> where T : PXGraph
{
  public string GetCostLayerType(INTran tran) => this.GetCostLayerType(tran.ProjectID);

  public string GetInventorySource(INTran tran)
  {
    return this.GetInventorySource(tran.ProjectID, tran.TaskID, tran.SiteID, tran.LocationID);
  }

  public virtual string GetDestinationInventorySource(INTran tran)
  {
    return this.GetInventorySource(tran.ToProjectID, tran.ToTaskID, tran.ToSiteID, tran.ToLocationID);
  }

  protected virtual string GetInventorySource(
    int? projectID,
    int? taskID,
    int? siteID,
    int? locationID)
  {
    return this.GetAccountingMode(projectID) == "L" ? (locationID.HasValue && !this.IsProjectLocation(locationID) ? "F" : "P") : (!this.IsSpecificCostCenter(siteID, projectID, taskID) ? "F" : "P");
  }

  protected virtual bool IsProjectLocation(int? locationId)
  {
    INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, locationId);
    return inLocation != null && inLocation.ProjectID.HasValue;
  }

  protected virtual string BuildCostCenterCD(int? siteID, int? projectID, int? taskID)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, projectID);
    PMTask pmTask = PMTask.PK.Find((PXGraph) this.Base, projectID, taskID);
    return pmProject != null && pmTask != null ? $"{pmProject.ContractCD.Trim()}/{pmTask.TaskCD.Trim()}" : (string) null;
  }

  protected virtual INCostCenter FindOrCreateCostCenter(int? siteID, int? projectID, int? taskID)
  {
    int num = projectID ?? ProjectDefaultAttribute.NonProject().GetValueOrDefault();
    INCostCenter createCostCenter1 = new INCostCenter()
    {
      CostCenterID = new int?(0)
    };
    if (ProjectDefaultAttribute.IsNonProject(new int?(num)))
      return createCostCenter1;
    INCostCenter createCostCenter2 = PXResultset<INCostCenter>.op_Implicit(((PXSelectBase<INCostCenter>) new PXSelect<INCostCenter, Where<INCostCenter.siteID, Equal<Required<INCostCenter.siteID>>, And<INCostCenter.projectID, Equal<Required<INCostCenter.projectID>>, And<INCostCenter.taskID, Equal<Required<INCostCenter.taskID>>>>>>((PXGraph) this.Base)).Select(new object[3]
    {
      (object) siteID,
      (object) num,
      (object) taskID
    }));
    if (createCostCenter2 != null)
      return createCostCenter2;
    return this.GetAccountingMode(projectID) == "L" ? createCostCenter1 : this.InsertNewCostSite(siteID, new int?(num), taskID);
  }

  protected virtual INCostCenter InsertNewCostSite(int? siteID, int? projectID, int? taskID)
  {
    INCostCenter inCostCenter = new INCostCenter()
    {
      CostLayerType = this.GetCostLayerType(projectID),
      SiteID = siteID,
      ProjectID = projectID,
      TaskID = taskID,
      CostCenterCD = this.BuildCostCenterCD(siteID, projectID, taskID)
    };
    return GraphHelper.Caches<INCostCenter>((PXGraph) this.Base).Insert(inCostCenter) ?? throw new InvalidOperationException("Failed to insert new INCostCenter");
  }

  protected string GetCostLayerType(int? projectID)
  {
    return !(this.GetAccountingMode(projectID) == "P") ? "N" : "P";
  }

  protected string GetAccountingMode(int? projectID)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, projectID);
    return pmProject != null && !pmProject.NonProject.GetValueOrDefault() ? pmProject.AccountingMode : "V";
  }

  protected virtual bool IsSpecificCostCenter(int? siteID, int? projectID, int? taskID)
  {
    return siteID.HasValue && taskID.HasValue && !ProjectDefaultAttribute.IsNonProject(projectID) && this.GetAccountingMode(projectID) != "L";
  }
}
