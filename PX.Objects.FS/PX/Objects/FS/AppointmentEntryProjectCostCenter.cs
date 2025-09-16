// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AppointmentEntryProjectCostCenter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class AppointmentEntryProjectCostCenter : 
  ProjectCostCenterBase<AppointmentEntry>,
  ICostCenterSupport<FSAppointmentDet>
{
  public int SortOrder => 200;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  public virtual IEnumerable<Type> GetFieldsDependOn()
  {
    yield return typeof (FSAppointmentDet.siteID);
    yield return typeof (FSAppointmentDet.projectID);
    yield return typeof (FSAppointmentDet.projectTaskID);
  }

  public bool IsSpecificCostCenter(FSAppointmentDet line)
  {
    return this.IsSpecificCostCenter(line.SiteID, line.ProjectID, line.ProjectTaskID);
  }

  public virtual int GetCostCenterID(FSAppointmentDet tran)
  {
    return this.FindOrCreateCostCenter(tran.SiteID, tran.ProjectID, tran.ProjectTaskID).CostCenterID.Value;
  }

  public virtual INCostCenter GetCostCenter(FSAppointmentDet tran)
  {
    return this.FindOrCreateCostCenter(tran.SiteID, tran.ProjectID, tran.ProjectTaskID);
  }
}
