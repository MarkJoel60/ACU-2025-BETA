// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.ValidateRequiredRelatedItemsInSalesOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class ValidateRequiredRelatedItemsInSalesOrder : 
  PXGraphExtension<CreateShipmentSOExtension, ValidateRequiredRelatedItems, SOShipmentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.relatedItems>();

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentSOExtension.ValidateLineBeforeShipment(PX.Objects.SO.SOLine)" />
  /// .
  [PXOverride]
  public virtual bool ValidateLineBeforeShipment(SOLine line, Func<SOLine, bool> baseImpl)
  {
    return ((PXGraphExtension<ValidateRequiredRelatedItems, SOShipmentEntry>) this).Base1.Validate(line) && baseImpl(line);
  }
}
