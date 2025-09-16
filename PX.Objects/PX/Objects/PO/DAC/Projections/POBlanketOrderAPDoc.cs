// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DAC.Projections.POBlanketOrderAPDoc
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
namespace PX.Objects.PO.DAC.Projections;

/// <exclude />
[PXProjection(typeof (Select5<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.APTranSigned, On<PX.Objects.PO.APTranSigned.pOOrderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.APTranSigned.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.PO.APTranSigned.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>, InnerJoin<PX.Objects.AP.APRegister, On<PX.Objects.AP.APRegister.docType, Equal<PX.Objects.PO.APTranSigned.tranType>, And<PX.Objects.AP.APRegister.refNbr, Equal<PX.Objects.PO.APTranSigned.refNbr>>>>>, Where<PX.Objects.PO.APTranSigned.tranType, NotEqual<APDocType.prepayment>>, Aggregate<GroupBy<PX.Objects.PO.POLine.pOType, GroupBy<PX.Objects.PO.POLine.pONbr, GroupBy<PX.Objects.AP.APRegister.docType, GroupBy<PX.Objects.AP.APRegister.refNbr, Sum<PX.Objects.PO.APTranSigned.signedBaseQty, Sum<PX.Objects.PO.APTranSigned.signedCuryTranAmt, Sum<PX.Objects.PO.APTranSigned.signedCuryRetainageAmt, Sum<PX.Objects.PO.APTranSigned.pOPPVAmt>>>>>>>>>>), Persistent = false)]
[PXCacheName("Purchase Order to Accounts Payable Document Link")]
[Serializable]
public class POBlanketOrderAPDoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Type of the document.</summary>
  /// <value>
  /// Possible values are: "INV" - Invoice, "ACR" - Credit Adjustment, "ADR" - Debit Adjustment,
  /// "CHK" - Payment, "VCK" - Voided Payment, "PPM" - Prepayment, "REF" - Refund,
  /// "QCK" - Cash Purchase, "VQC" - Voided Cash Purchase.
  /// </value>
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AP.APRegister.docType))]
  [APDocType.List]
  [PXUIField]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  /// <summary>Reference number of the document.</summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.AP.APRegister.refNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.AP.APRegister.refNbr, Where<PX.Objects.AP.APRegister.docType, Equal<Optional<POBlanketOrderAPDoc.docType>>>>), Filterable = true)]
  public virtual string RefNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.pOType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "PO Type", Enabled = false, IsReadOnly = true)]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.pONbr))]
  [PXUIField(DisplayName = "PO Number", Enabled = false, IsReadOnly = true)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Optional<POBlanketOrderAPDoc.pOType>>>>))]
  public virtual string PONbr { get; set; }

  /// <summary>Date of the document.</summary>
  [PXDBDate(BqlField = typeof (PX.Objects.AP.APRegister.docDate))]
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
  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.AP.APRegister.status))]
  [PXUIField]
  [APDocStatus.List]
  public virtual string Status { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.APTranSigned.signedBaseQty))]
  [PXUIField(DisplayName = "Billed Qty.", Enabled = false)]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.PO.APTranSigned.signedCuryTranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalTranAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.PO.APTranSigned.signedCuryRetainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalRetainageAmt { get; set; }

  /// <summary>
  /// Billed Amount of the item or service associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXBaseCury]
  [PXFormula(typeof (Add<POBlanketOrderAPDoc.totalTranAmt, POBlanketOrderAPDoc.totalRetainageAmt>))]
  [PXUIField(DisplayName = "Billed Amt.")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalAmt { get; set; }

  /// <summary>
  /// Purchase price variance amount associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  /// <seealso cref="P:PX.Objects.PO.POReceiptLine.BillPPVAmt" />
  [PXDBBaseCury(BqlField = typeof (PX.Objects.PO.APTranSigned.pOPPVAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "PPV Amt")]
  public virtual Decimal? TotalPPVAmt { get; set; }

  /// <summary>
  /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.AP.APRegister.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXString]
  public virtual string StatusText { get; set; }

  public class PK : 
    PrimaryKeyOf<POOrderAPDoc>.By<POBlanketOrderAPDoc.docType, POBlanketOrderAPDoc.refNbr, POBlanketOrderAPDoc.pOType, POBlanketOrderAPDoc.pONbr>
  {
    public static POOrderAPDoc Find(
      PXGraph graph,
      string docType,
      string refNbr,
      string pOType,
      string pONbr,
      PKFindOptions options = 0)
    {
      return (POOrderAPDoc) PrimaryKeyOf<POOrderAPDoc>.By<POBlanketOrderAPDoc.docType, POBlanketOrderAPDoc.refNbr, POBlanketOrderAPDoc.pOType, POBlanketOrderAPDoc.pONbr>.FindBy(graph, (object) docType, (object) refNbr, (object) pOType, (object) pONbr, options);
    }
  }

  public static class FK
  {
    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<POOrderAPDoc>.By<POBlanketOrderAPDoc.docType, POBlanketOrderAPDoc.refNbr>
    {
    }

    public class Order : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<POOrderAPDoc>.By<POBlanketOrderAPDoc.pOType, POBlanketOrderAPDoc.pONbr>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POOrderAPDoc>.By<POBlanketOrderAPDoc.curyID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderAPDoc.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderAPDoc.refNbr>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderAPDoc.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderAPDoc.pONbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POBlanketOrderAPDoc.docDate>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderAPDoc.status>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POBlanketOrderAPDoc.totalQty>
  {
  }

  public abstract class totalTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POBlanketOrderAPDoc.totalTranAmt>
  {
  }

  public abstract class totalRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POBlanketOrderAPDoc.totalRetainageAmt>
  {
  }

  public abstract class totalAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POBlanketOrderAPDoc.totalAmt>
  {
  }

  public abstract class totalPPVAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POBlanketOrderAPDoc.totalPPVAmt>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POBlanketOrderAPDoc.curyID>
  {
  }

  public abstract class statusText : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POBlanketOrderAPDoc.statusText>
  {
  }
}
