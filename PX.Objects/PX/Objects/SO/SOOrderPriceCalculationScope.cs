// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderPriceCalculationScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderPriceCalculationScope : 
  PriceCalculationScope<ARSalesPrice, ARSalesPrice.recordID>
{
  public override bool IsPriceListExist()
  {
    return this.IsUpdatedOnly<SOLine.orderQty>() ? PXAccess.FeatureInstalled<FeaturesSet.supportBreakQty>() : base.IsPriceListExist();
  }

  public virtual bool Any()
  {
    UpdateIfFieldsChangedScope.Changes slot = PXContext.GetSlot<UpdateIfFieldsChangedScope.Changes>();
    return slot?.SourceOfChange != null && slot.SourceOfChange.Any<Type>();
  }
}
