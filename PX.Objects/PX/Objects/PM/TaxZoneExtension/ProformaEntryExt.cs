// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.ProformaEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class ProformaEntryExt : ProjectRevenueTaxZoneExtension<ProformaEntry>
{
  [PXMergeAttributes]
  [PXFormula(typeof (Default<PMProforma.projectID, PMProforma.locationID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProforma.taxZoneID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [PMShippingAddress2(typeof (Select2<PX.Objects.AR.Customer, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<PMProforma.locationID>>>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PMShippingAddress, On<PMShippingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<PMShippingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PMShippingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PMShippingAddress.isDefaultBillAddress, Equal<True>>>>>>>>, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<PMProforma.customerID>>>>), typeof (PMProforma.customerID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProforma.shipAddressID> e)
  {
  }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  protected override DocumentMapping GetDocumentMapping()
  {
    return new DocumentMapping(typeof (PMProforma))
    {
      ProjectID = typeof (PMProforma.projectID)
    };
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(PMProforma row, Func<PMProforma, string> baseMethod)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) row?.ProjectID);
    return pmProject != null && !string.IsNullOrEmpty(pmProject.RevenueTaxZoneID) ? pmProject.RevenueTaxZoneID : baseMethod(row);
  }

  protected override void SetDefaultShipToAddress(PXCache sender, PX.Objects.PM.TaxZoneExtension.Document row)
  {
    SharedRecordAttribute.DefaultRecord<PX.Objects.CR.CROpportunity.shipAddressID>(sender, (object) row);
  }
}
