// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.QuoteMaintExt
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

public class QuoteMaintExt : ProjectRevenueTaxZoneExtension<QuoteMaint>
{
  [PXMergeAttributes]
  [PXFormula(typeof (Default<PX.Objects.CR.CRQuote.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.CRQuote.taxZoneID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt(BqlField = typeof (CROpportunityRevision.shipAddressID))]
  [CRShippingAddress2(typeof (Select<PX.Objects.CR.Address, Where<True, Equal<False>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.CRQuote.shipAddressID> e)
  {
  }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  protected override DocumentMapping GetDocumentMapping()
  {
    return new DocumentMapping(typeof (PX.Objects.CR.CRQuote))
    {
      ProjectID = typeof (PX.Objects.CR.CRQuote.projectID)
    };
  }

  protected override void SetDefaultShipToAddress(PXCache sender, PX.Objects.PM.TaxZoneExtension.Document row)
  {
    SharedRecordAttribute.DefaultRecord<PX.Objects.CR.CRQuote.shipAddressID>(sender, (object) row);
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(PX.Objects.CR.CRQuote row, Func<PX.Objects.CR.CRQuote, string> baseMethod)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) row?.ProjectID);
    return pmProject != null && !string.IsNullOrEmpty(pmProject.RevenueTaxZoneID) ? pmProject.RevenueTaxZoneID : baseMethod(row);
  }
}
