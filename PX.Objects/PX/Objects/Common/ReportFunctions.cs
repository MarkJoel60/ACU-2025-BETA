// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ReportFunctions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Maintenance;
using PX.Objects.CN.JointChecks.AP.Services;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.IN.PhysicalInventory;
using System;

#nullable disable
namespace PX.Objects.Common;

public class ReportFunctions
{
  private readonly Lazy<JointCheckPrintService> jointCheckPrintService = new Lazy<JointCheckPrintService>((Func<JointCheckPrintService>) (() => new JointCheckPrintService()));

  public bool FeatureInstalled(object feature) => PXAccess.FeatureInstalled((string) feature);

  public object GetOrganizationIDByCD(object organizationCD)
  {
    return (object) PXAccess.GetOrganizationID((string) organizationCD);
  }

  public object GetOrganizationCDByID(object organizationID)
  {
    return (object) PXAccess.GetOrganizationCD((int?) organizationID);
  }

  public object GetBranchIDByCD(object branchCD)
  {
    return (object) PXAccess.GetBranchID((string) branchCD);
  }

  public object GetBranchCDByID(object branchID) => (object) PXAccess.GetBranchCD((int?) branchID);

  public object GetParentOrganizationID(object branchID)
  {
    return (object) PXAccess.GetParentOrganizationID((int?) branchID);
  }

  public object GetOrganizationFinPeriodIDForMaster(object organizationID, object masterFinPeriodID)
  {
    return (object) ((PXGraph) null).GetService<IFinPeriodRepository>().FindFinPeriodIDByMasterPeriodID((int?) organizationID, (string) masterFinPeriodID, true);
  }

  private PXAccess.Organization GetOrganizationByBAccountCD(string bAccountCD)
  {
    BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.acctCD, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(PXGraph.CreateInstance<PXGraph>(), new object[1]
    {
      (object) bAccountCD
    }));
    return (PXAccess.Organization) PXAccess.GetOrganizationByBAccountID((int?) baccountR?.BAccountID) ?? PXAccess.GetParentOrganization(PXAccess.GetBranchByBAccountID((int?) baccountR?.BAccountID)?.BranchID);
  }

  public object GetOrganizationCDByBAccountCD(object bAccountCD)
  {
    PXAccess.Organization organizationByBaccountCd = this.GetOrganizationByBAccountCD((string) bAccountCD);
    return organizationByBaccountCd == null ? (object) null : (object) organizationByBaccountCd.OrganizationCD;
  }

  public object GetOrganizationIDByBAccountCD(object bAccountCD)
  {
    return (object) (int?) this.GetOrganizationByBAccountCD((string) bAccountCD)?.OrganizationID;
  }

  public object GetFullItemClassDescription(object itemClassCD)
  {
    return !(itemClassCD is string itemClassCD1) ? (object) null : (object) DimensionTree<ItemClassTree, ItemClassTree.INItemClass, INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>.Instance.GetFullItemClassDescription(itemClassCD1);
  }

  public bool IsInventoryLocationLocked(int? siteID, int? locationID, int? inventoryID)
  {
    return siteID.HasValue && locationID.HasValue && inventoryID.HasValue && new PILocksInspector(siteID.Value).IsInventoryLocationLocked(inventoryID, locationID, (string) null);
  }

  public bool ShouldShowJointPayees(string documentType, string referenceNumber)
  {
    return this.jointCheckPrintService.Value.DoJointPayeePaymentsWithPositiveAmountExist(documentType, referenceNumber);
  }

  public string GetJointPayeesSingleLine(string documentType, string referenceNumber)
  {
    return this.jointCheckPrintService.Value.GetJointPayeesSingleLine(documentType, referenceNumber);
  }

  public string GetJointPayeesMultiline(string documentType, string referenceNumber)
  {
    return this.jointCheckPrintService.Value.GetJointPayeesMultiline(documentType, referenceNumber);
  }
}
