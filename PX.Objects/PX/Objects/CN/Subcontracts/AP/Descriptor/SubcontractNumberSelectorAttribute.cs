// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.AP.Descriptor.SubcontractNumberSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.AP.Descriptor;

public sealed class SubcontractNumberSelectorAttribute : PXSelectorAttribute
{
  private static readonly Type[] Fields = new Type[7]
  {
    typeof (PX.Objects.PO.POOrder.orderNbr),
    typeof (PX.Objects.PO.POOrder.orderDate),
    typeof (PX.Objects.PO.POOrder.vendorID),
    typeof (PX.Objects.PO.POOrder.vendorID_Vendor_acctName),
    typeof (PX.Objects.PO.POOrder.vendorLocationID),
    typeof (PX.Objects.PO.POOrder.curyID),
    typeof (PX.Objects.PO.POOrder.curyOrderTotal)
  };

  public SubcontractNumberSelectorAttribute()
    : base(typeof (Search5<PX.Objects.PO.POOrder.orderNbr, InnerJoin<PX.Objects.PO.POLine, On<PX.Objects.PO.POLine.orderType, Equal<PX.Objects.PO.POOrder.orderType>, And<PX.Objects.PO.POLine.orderNbr, Equal<PX.Objects.PO.POOrder.orderNbr>, And<PX.Objects.PO.POLine.pOAccrualType, Equal<POAccrualType.order>, And<PX.Objects.PO.POLine.cancelled, NotEqual<True>, And<PX.Objects.PO.POLine.closed, NotEqual<True>>>>>>, LeftJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.pOOrderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.AP.APTran.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.AP.APTran.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>, And<PX.Objects.AP.APTran.receiptNbr, IsNull, And<PX.Objects.AP.APTran.receiptLineNbr, IsNull, And<PX.Objects.AP.APTran.released, Equal<False>>>>>>>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularSubcontract>, And<PX.Objects.PO.POOrder.curyID, Equal<Current<PX.Objects.AP.APInvoice.curyID>>, And<PX.Objects.PO.POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<Where<PX.Objects.AP.APTran.refNbr, IsNull, Or<PX.Objects.AP.APTran.refNbr, Equal<Current<PX.Objects.AP.APInvoice.refNbr>>>>>>>>, Aggregate<GroupBy<PX.Objects.PO.POOrder.orderType, GroupBy<PX.Objects.PO.POOrder.orderNbr>>>>), SubcontractNumberSelectorAttribute.Fields)
  {
    this.Filterable = true;
  }
}
