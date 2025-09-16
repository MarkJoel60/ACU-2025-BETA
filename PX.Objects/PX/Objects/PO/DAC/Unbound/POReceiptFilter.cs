// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DAC.Unbound.POReceiptFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PO.DAC.Unbound;

[Serializable]
public class POReceiptFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXSelector(typeof (Search5<POReceiptLineS.pONbr, InnerJoin<PX.Objects.PO.POReceipt, On<POReceiptLineS.FK.Receipt>, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderNbr, Equal<POReceiptLineS.pONbr>>, LeftJoin<POOrderReceiptLink, On<POOrderReceiptLink.receiptType, Equal<POReceiptLineS.receiptType>, And<POOrderReceiptLink.receiptNbr, Equal<POReceiptLineS.receiptNbr>, And<POOrderReceiptLink.pOType, Equal<POReceiptLineS.pOType>, And<POOrderReceiptLink.pONbr, Equal<POReceiptLineS.pONbr>>>>>, LeftJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.receiptType, Equal<POReceiptLineS.receiptType>, And<PX.Objects.AP.APTran.receiptNbr, Equal<POReceiptLineS.receiptNbr>, And<PX.Objects.AP.APTran.receiptLineNbr, Equal<POReceiptLineS.lineNbr>, And<PX.Objects.AP.APTran.released, Equal<False>>>>>>>>>, Where2<Where<PX.Objects.PO.POReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>, And<PX.Objects.PO.POReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.vendorLocationID>>, And2<Not<FeatureInstalled<FeaturesSet.vendorRelations>>, Or2<FeatureInstalled<FeaturesSet.vendorRelations>, And<PX.Objects.PO.POReceipt.vendorID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorID>>, And<PX.Objects.PO.POReceipt.vendorLocationID, Equal<Current<PX.Objects.AP.APInvoice.suppliedByVendorLocationID>>, And<POOrderReceiptLink.payToVendorID, Equal<Current<PX.Objects.AP.APInvoice.vendorID>>>>>>>>>, And<PX.Objects.PO.POReceipt.hold, Equal<False>, And<PX.Objects.PO.POReceipt.released, Equal<True>, And<PX.Objects.AP.APTran.refNbr, IsNull, And<POReceiptLineS.unbilledQty, Greater<decimal0>, And<Where<POReceiptLineS.receiptType, Equal<POReceiptType.poreceipt>, And<Optional<PX.Objects.AP.APInvoice.docType>, Equal<APDocType.invoice>, Or<POReceiptLineS.receiptType, Equal<POReceiptType.poreturn>, And<Optional<PX.Objects.AP.APInvoice.docType>, Equal<APDocType.debitAdj>>>>>>>>>>>, Aggregate<GroupBy<POReceiptLineS.pONbr>>>), new Type[] {typeof (POReceiptLineS.pONbr), typeof (PX.Objects.PO.POOrder.orderDate), typeof (PX.Objects.PO.POOrder.vendorID), typeof (PX.Objects.PO.POOrder.vendorID_Vendor_acctName), typeof (PX.Objects.PO.POOrder.vendorLocationID), typeof (PX.Objects.PO.POOrder.curyID), typeof (PX.Objects.PO.POOrder.curyOrderTotal)}, Filterable = true)]
  public virtual 
  #nullable disable
  string OrderNbr { get; set; }

  /// <exclude />
  [PXDBInt]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public int? ReceiptLineNbr { get; set; }

  /// <exclude />
  [PXDBString]
  [PXUIField(DisplayName = "Receipt Nbr.", Visible = false, Enabled = false)]
  public string ReceiptNbr { get; set; }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptFilter.vendorID>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptFilter.orderNbr>
  {
  }

  public abstract class receiptLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptFilter.receiptLineNbr>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptFilter.receiptNbr>
  {
  }
}
