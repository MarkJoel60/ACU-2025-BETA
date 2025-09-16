// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.PO;

[Serializable]
public class POOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;

  [VendorActive]
  [PXDefault(typeof (PX.Objects.AP.APInvoice.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  [PXSelector(typeof (Search5<POOrder.orderNbr, InnerJoin<POLine, On<POLine.orderType, Equal<POOrder.orderType>, And<POLine.orderNbr, Equal<POOrder.orderNbr>>>>, Where<POOrder.orderType, NotIn3<POOrderType.blanket, POOrderType.standardBlanket>, And<POOrder.curyID, Equal<Current<PX.Objects.AP.APInvoice.curyID>>, And<POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<POLine.cancelled, NotEqual<True>, And<POLine.closed, NotEqual<True>, And<Where<Current<PX.Objects.AP.APInvoice.docType>, Equal<APDocType.prepayment>, Or<POLine.pOAccrualType, Equal<POAccrualType.order>>>>>>>>>, Aggregate<GroupBy<POOrder.orderType, GroupBy<POOrder.orderNbr>>>>), new Type[] {typeof (POOrder.orderType), typeof (POOrder.orderNbr), typeof (POOrder.orderDate), typeof (POOrder.vendorID), typeof (POOrder.vendorID_Vendor_acctName), typeof (POOrder.vendorLocationID), typeof (POOrder.curyID), typeof (POOrder.curyOrderTotal)}, Filterable = true)]
  public virtual 
  #nullable disable
  string OrderNbr { get; set; }

  /// <summary>
  /// Get or set CreateBill that mark current document create bill on release.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Show Billed Lines", Enabled = true)]
  public virtual bool? ShowBilledLines { get; set; }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POOrderFilter.vendorID>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderFilter.orderNbr>
  {
  }

  public abstract class showBilledLines : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderFilter.showBilledLines>
  {
  }
}
