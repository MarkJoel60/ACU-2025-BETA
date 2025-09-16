// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PO.Descriptor.Attributes.PurchaseOrderTypeRestrictorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.PO.DAC;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PO.Descriptor.Attributes;

public class PurchaseOrderTypeRestrictorAttribute : PXRestrictorAttribute
{
  public PurchaseOrderTypeRestrictorAttribute()
    : base(typeof (Where<POOrder.orderType, Equal<Current<PurchaseOrderTypeFilter.type1>>, Or<POOrder.orderType, Equal<Current<PurchaseOrderTypeFilter.type2>>, Or<POOrder.orderType, Equal<Current<PurchaseOrderTypeFilter.type3>>, Or<POOrder.orderType, Equal<Current<PurchaseOrderTypeFilter.type4>>, Or<POOrder.orderType, Equal<Current<PurchaseOrderTypeFilter.type5>>>>>>>), "Only Purchase Orders are allowed.", Array.Empty<Type>())
  {
  }
}
