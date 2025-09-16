// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.POReceiptEntryCostCenterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class POReceiptEntryCostCenterExt : 
  ProjectCostCenterBase<POReceiptEntry>,
  ICostCenterSupport<PX.Objects.PO.POReceiptLine>
{
  public virtual int SortOrder => 200;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  public virtual IEnumerable<Type> GetFieldsDependOn()
  {
    yield return typeof (PX.Objects.PO.POReceiptLine.isSpecialOrder);
    yield return typeof (PX.Objects.PO.POReceiptLine.siteID);
    yield return typeof (PX.Objects.PO.POReceiptLine.projectID);
    yield return typeof (PX.Objects.PO.POReceiptLine.taskID);
  }

  public bool IsSpecificCostCenter(PX.Objects.PO.POReceiptLine line)
  {
    return !line.IsSpecialOrder.GetValueOrDefault() && this.IsSpecificCostCenter(line.SiteID, line.ProjectID, line.TaskID);
  }

  public virtual int GetCostCenterID(PX.Objects.PO.POReceiptLine tran)
  {
    return this.FindOrCreateCostCenter(tran.SiteID, tran.ProjectID, tran.TaskID).CostCenterID.Value;
  }

  public virtual INCostCenter GetCostCenter(PX.Objects.PO.POReceiptLine tran)
  {
    return this.FindOrCreateCostCenter(tran.SiteID, tran.ProjectID, tran.TaskID);
  }
}
