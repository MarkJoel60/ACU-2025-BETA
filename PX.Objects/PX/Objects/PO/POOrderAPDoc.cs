// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderAPDoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select5<APTranSigned, InnerJoin<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APTranSigned.tranType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APTranSigned.refNbr>>>>, Where<APTranSigned.tranType, NotEqual<APDocType.prepayment>>, Aggregate<GroupBy<APTranSigned.pOOrderType, GroupBy<APTranSigned.pONbr, GroupBy<PX.Objects.AP.APInvoice.docType, GroupBy<PX.Objects.AP.APInvoice.refNbr, Sum<APTranSigned.signedBaseQty, Sum<APTranSigned.signedCuryTranAmt, Sum<APTranSigned.signedCuryRetainageAmt, Sum<APTranSigned.pOPPVAmt>>>>>>>>>>), Persistent = false)]
[PXCacheName("Purchase Order to Accounts Payable Document Link")]
[Serializable]
public class POOrderAPDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CuryID;

  /// <summary>Type of the document.</summary>
  /// <value>
  /// Possible values are: "INV" - Invoice, "ACR" - Credit Adjustment, "ADR" - Debit Adjustment,
  /// "CHK" - Payment, "VCK" - Voided Payment, "PPM" - Prepayment, "REF" - Refund,
  /// "QCK" - Cash Purchase, "VQC" - Voided Cash Purchase.
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AP.APInvoice.docType))]
  [APDocType.List]
  [PXUIField]
  public virtual string DocType { get; set; }

  /// <summary>Reference number of the document.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.AP.APInvoice.refNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AP.APInvoice.refNbr, Where<PX.Objects.AP.APInvoice.docType, Equal<Optional<POOrderAPDoc.docType>>>>), Filterable = true)]
  public virtual string RefNbr { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDBDate(BqlField = typeof (PX.Objects.AP.APInvoice.docDate))]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  /// <summary>
  /// Status of the document. The field is calculated based on the values of status flag. It can't be changed directly.
  /// The fields tht determine status of a document are: <see cref="!:Hold" />, <see cref="!:Released" />, <see cref="!:Voided" />,
  /// <see cref="!:Scheduled" />, <see cref="!:Prebooked" />, <see cref="!:Printed" />, <see cref="!:Approved" />, <see cref="!:Rejected" />.
  /// </summary>
  /// <value>
  /// Possible values are:
  /// <c>"H"</c> - Hold, <c>"B"</c> - Balanced, <c>"V"</c> - Voided, <c>"S"</c> - Scheduled,
  /// <c>"N"</c> - Open, <c>"C"</c> - Closed, <c>"P"</c> - Printed, <c>"K"</c> - Prebooked,
  /// <c>"E"</c> - Pending Approval, <c>"R"</c> - Rejected, <c>"Z"</c> - Reserved.
  /// Defaults to Hold.
  /// </value>
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.AP.APInvoice.status))]
  [PXUIField]
  [APDocStatus.List]
  public virtual string Status { get; set; }

  [PXDBQuantity(BqlField = typeof (APTranSigned.signedBaseQty))]
  [PXUIField(DisplayName = "Billed Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBBaseCury(BqlField = typeof (APTranSigned.signedCuryTranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalTranAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (APTranSigned.signedCuryRetainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalRetainageAmt { get; set; }

  /// <summary>
  /// Billed Amount of the item or service associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXBaseCury]
  [PXFormula(typeof (Add<POOrderAPDoc.totalTranAmt, POOrderAPDoc.totalRetainageAmt>))]
  [PXUIField(DisplayName = "Billed Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalAmt { get; set; }

  /// <summary>
  /// Purchase price variance amount associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  /// <seealso cref="P:PX.Objects.PO.POReceiptLine.BillPPVAmt" />
  [PXDBBaseCury(BqlField = typeof (APTranSigned.pOPPVAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "PPV Amt")]
  public virtual Decimal? TotalPPVAmt { get; set; }

  /// <summary>
  /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.AP.APInvoice.curyID))]
  [PXUIField]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// The type of the corresponding <see cref="T:PX.Objects.PO.POOrder">PO Order</see>.
  /// Together with <see cref="P:PX.Objects.PO.POOrderAPDoc.PONbr" /> and <see cref="!:POLineNbr" /> links APTrans to the PO Orders and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POOrder.OrderType">POOrder.OrderType</see> field.
  /// See its description for the list of allowed values.
  /// </value>
  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (APTranSigned.pOOrderType))]
  [PX.Objects.PO.POOrderType.List]
  [PXUIField(DisplayName = "PO Type", Enabled = false, IsReadOnly = true)]
  public virtual string POOrderType { get; set; }

  /// <summary>
  /// The reference number of the corresponding <see cref="T:PX.Objects.PO.POOrder">PO Order</see>.
  /// Together with <see cref="P:PX.Objects.PO.POOrderAPDoc.POOrderType" /> and <see cref="!:POLineNbr" /> links APTrans to the PO Orders and their lines.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.PO.POOrder.OrderNbr">POOrder.OrderNbr</see> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APTranSigned.pONbr))]
  [PXUIField(DisplayName = "PO Number", Enabled = false, IsReadOnly = true)]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Optional<POOrderAPDoc.pOOrderType>>>>))]
  public virtual string PONbr { get; set; }

  [PXString]
  public virtual string StatusText { get; set; }

  public class PK : 
    PrimaryKeyOf<POOrderAPDoc>.By<POOrderAPDoc.docType, POOrderAPDoc.refNbr, POOrderAPDoc.pOOrderType, POOrderAPDoc.pONbr>
  {
    public static POOrderAPDoc Find(
      PXGraph graph,
      string docType,
      string refNbr,
      string pOOrderType,
      string pONbr,
      PKFindOptions options = 0)
    {
      return (POOrderAPDoc) PrimaryKeyOf<POOrderAPDoc>.By<POOrderAPDoc.docType, POOrderAPDoc.refNbr, POOrderAPDoc.pOOrderType, POOrderAPDoc.pONbr>.FindBy(graph, (object) docType, (object) refNbr, (object) pOOrderType, (object) pONbr, options);
    }
  }

  public static class FK
  {
    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<POOrderAPDoc>.By<POOrderAPDoc.docType, POOrderAPDoc.refNbr>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POOrderAPDoc>.By<POOrderAPDoc.pOOrderType, POOrderAPDoc.pONbr>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POOrderAPDoc>.By<POOrderAPDoc.curyID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderAPDoc.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderAPDoc.refNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POOrderAPDoc.docDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderAPDoc.status>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrderAPDoc.totalQty>
  {
  }

  public abstract class totalTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrderAPDoc.totalTranAmt>
  {
  }

  public abstract class totalRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderAPDoc.totalRetainageAmt>
  {
  }

  public abstract class totalAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrderAPDoc.totalAmt>
  {
  }

  public abstract class totalPPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POOrderAPDoc.totalPPVAmt>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderAPDoc.curyID>
  {
  }

  public abstract class pOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderAPDoc.pOOrderType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderAPDoc.pONbr>
  {
  }

  public abstract class statusText : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderAPDoc.statusText>
  {
  }
}
