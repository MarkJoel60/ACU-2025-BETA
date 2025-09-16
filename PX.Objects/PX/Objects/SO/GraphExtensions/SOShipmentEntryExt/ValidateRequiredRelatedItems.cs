// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ValidateRequiredRelatedItems
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.RelatedItems;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class ValidateRequiredRelatedItems : 
  ValidateRequiredRelatedItems<SOShipmentEntry, SOOrder, SOLine>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.relatedItems>();

  public override void ThrowError()
  {
    if (this.IsMassProcessing)
      throw new PXException("The shipment cannot be created because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab on the Sales Orders (SO301000) form.");
    throw new PXException("The shipment cannot be created because it contains items that require substitution. Select substitute items by using the buttons in the Related Items column of the Details tab.");
  }
}
