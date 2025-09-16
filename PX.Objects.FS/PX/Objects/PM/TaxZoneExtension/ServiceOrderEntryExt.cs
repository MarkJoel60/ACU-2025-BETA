// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.ServiceOrderEntryExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.FS;
using System;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class ServiceOrderEntryExt : ProjectRevenueTaxZoneExtension<ServiceOrderEntry>
{
  [PXMergeAttributes]
  [PXFormula(typeof (Default<FSServiceOrder.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<FSServiceOrder.taxZoneID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [FSSrvOrdAddress2(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FSServiceOrder.serviceOrderAddressID> e)
  {
  }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  protected override DocumentMapping GetDocumentMapping()
  {
    return new DocumentMapping(typeof (FSServiceOrder))
    {
      ProjectID = typeof (FSServiceOrder.projectID)
    };
  }

  protected override void SetDefaultShipToAddress(PXCache sender, PX.Objects.PM.TaxZoneExtension.Document row)
  {
    SharedRecordAttribute.DefaultRecord<FSServiceOrder.serviceOrderAddressID>(sender, (object) row);
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(
    int? billCustomerID,
    int? billLocationID,
    int? branchID,
    int? projectID,
    Func<int?, int?, int?, int?, string> baseMethod)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, projectID);
    return pmProject != null && !string.IsNullOrEmpty(pmProject.RevenueTaxZoneID) ? pmProject.RevenueTaxZoneID : baseMethod(billCustomerID, billLocationID, branchID, projectID);
  }
}
