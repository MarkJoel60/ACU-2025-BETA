// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.OpportunityMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class OpportunityMaintExt : ProjectRevenueTaxZoneExtension<OpportunityMaint>
{
  [PXMergeAttributes]
  [PXFormula(typeof (Default<PX.Objects.CR.CROpportunity.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.CROpportunity.taxZoneID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipAddressID))]
  [CRShippingAddress2(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.CR.CROpportunity.shipAddressID> e)
  {
  }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  protected override DocumentMapping GetDocumentMapping()
  {
    return new DocumentMapping(typeof (PX.Objects.CR.CROpportunity))
    {
      ProjectID = typeof (PX.Objects.CR.CROpportunity.projectID)
    };
  }

  protected override void SetDefaultShipToAddress(PXCache sender, PX.Objects.PM.TaxZoneExtension.Document row)
  {
    if (!row.ProjectID.HasValue)
      return;
    int? projectId = row.ProjectID;
    int num = 0;
    if (projectId.GetValueOrDefault() == num & projectId.HasValue)
      return;
    PMProject pmProject = PMProject.PK.Find(sender.Graph, row.ProjectID);
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault())
      return;
    SharedRecordAttribute.DefaultRecord<PX.Objects.CR.CROpportunity.shipAddressID>(sender, (object) row);
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(PX.Objects.CR.CROpportunity row, Func<PX.Objects.CR.CROpportunity, string> baseMethod)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) row?.ProjectID);
    return pmProject != null && !string.IsNullOrEmpty(pmProject.RevenueTaxZoneID) ? pmProject.RevenueTaxZoneID : baseMethod(row);
  }
}
