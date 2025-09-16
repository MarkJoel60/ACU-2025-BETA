// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.SOOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class SOOrderEntryExt : ProjectRevenueTaxZoneExtension<SOOrderEntry>
{
  [PXMergeAttributes]
  [PXFormula(typeof (Default<SOOrder.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOOrder.taxZoneID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBInt]
  [SOShippingAddress2(typeof (Select2<PX.Objects.CR.Address, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Standalone.Location.defAddressID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<SOOrder.customerLocationID>>>>>>, LeftJoin<SOShippingAddress, On<SOShippingAddress.customerID, Equal<PX.Objects.CR.Address.bAccountID>, And<SOShippingAddress.customerAddressID, Equal<PX.Objects.CR.Address.addressID>, And<SOShippingAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<SOShippingAddress.isDefaultAddress, Equal<True>>>>>>>, Where<True, Equal<True>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOOrder.shipAddressID> e)
  {
  }

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && new ProjectSettingsManager().CalculateProjectSpecificTaxes;
  }

  protected override DocumentMapping GetDocumentMapping()
  {
    return new DocumentMapping(typeof (SOOrder))
    {
      ProjectID = typeof (SOOrder.projectID)
    };
  }

  protected override void SetDefaultShipToAddress(PXCache sender, PX.Objects.PM.TaxZoneExtension.Document row)
  {
    SharedRecordAttribute.DefaultRecord<SOOrder.shipAddressID>(sender, (object) row);
  }

  [PXOverride]
  public virtual string GetDefaultTaxZone(SOOrder row, Func<SOOrder, string> baseMethod)
  {
    if (row != null && row.OverrideTaxZone.GetValueOrDefault())
      return row.TaxZoneID;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, (int?) row?.ProjectID);
    return pmProject != null && !string.IsNullOrEmpty(pmProject.RevenueTaxZoneID) ? pmProject.RevenueTaxZoneID : baseMethod(row);
  }
}
