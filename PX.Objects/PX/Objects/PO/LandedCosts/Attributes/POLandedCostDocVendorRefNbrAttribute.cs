// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.Attributes.POLandedCostDocVendorRefNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;

#nullable disable
namespace PX.Objects.PO.LandedCosts.Attributes;

public class POLandedCostDocVendorRefNbrAttribute : BaseVendorRefNbrAttribute
{
  public POLandedCostDocVendorRefNbrAttribute()
    : base(typeof (POLandedCostDoc.vendorID))
  {
  }

  protected override bool IsIgnored(PXCache sender, object row)
  {
    POLandedCostDoc poLandedCostDoc = (POLandedCostDoc) row;
    if (poLandedCostDoc.Released.GetValueOrDefault() || !poLandedCostDoc.CreateBill.GetValueOrDefault() || base.IsIgnored(sender, row))
      return true;
    return string.IsNullOrEmpty(poLandedCostDoc.VendorRefNbr) && poLandedCostDoc.Hold.GetValueOrDefault();
  }

  protected override BaseVendorRefNbrAttribute.EntityKey GetEntityKey(PXCache sender, object row)
  {
    return new BaseVendorRefNbrAttribute.EntityKey()
    {
      _DetailID = this.DETAIL_DUMMY,
      _MasterID = this.GetMasterNoteId(typeof (POLandedCostDoc), typeof (POLandedCostDoc.noteID), row)
    };
  }

  public override Guid? GetSiblingID(PXCache sender, object row)
  {
    return (Guid?) sender.GetValue<POLandedCostDoc.noteID>(row);
  }
}
