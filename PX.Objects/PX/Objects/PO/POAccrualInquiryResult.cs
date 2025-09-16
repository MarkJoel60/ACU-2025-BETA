// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualInquiryResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("Purchase Accrual Balance Result")]
[PXProjection(typeof (SelectFromBase<POAccrualDetail, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POAccrualStatus>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.posted, Equal<True>>>>, And<BqlOperand<POAccrualDetail.branchID, IBqlInt>.Is<Inside<BqlField<POAccrualInquiryFilter.orgBAccountID, IBqlInt>.FromCurrent>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CurrentValue<POAccrualInquiryFilter.vendorID>, IsNull>>>>.Or<BqlOperand<POAccrualDetail.vendorID, IBqlInt>.IsEqual<BqlField<POAccrualInquiryFilter.vendorID, IBqlInt>.FromCurrent.Value>>>>, And<BqlOperand<POAccrualDetail.finPeriodID, IBqlString>.IsLessEqual<BqlField<POAccrualInquiryFilter.finPeriodID, IBqlString>.FromCurrent.Value>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualStatus.closedFinPeriodID, IsNull>>>>.Or<BqlOperand<POAccrualStatus.closedFinPeriodID, IBqlString>.IsGreater<BqlField<POAccrualInquiryFilter.finPeriodID, IBqlString>.FromCurrent.Value>>>>, And<BqlOperand<POAccrualStatus.acctID, IBqlInt>.IsEqual<BqlField<POAccrualInquiryFilter.acctID, IBqlInt>.FromCurrent.Value>>>>.And<POAccrualDetail.FK.AccrualStatus>>>, FbqlJoins.Left<PX.Objects.GL.Sub>.On<POAccrualStatus.FK.Subaccount>>, FbqlJoins.Left<POReceiptLineAccrual>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLineAccrual.pOReceiptType, Equal<POAccrualDetail.pOReceiptType>>>>, And<BqlOperand<POReceiptLineAccrual.pOReceiptNbr, IBqlString>.IsEqual<POAccrualDetail.pOReceiptNbr>>>>.And<BqlOperand<POReceiptLineAccrual.pOReceiptLineNbr, IBqlInt>.IsEqual<POAccrualDetail.lineNbr>>>>, FbqlJoins.Left<APTranAccrual>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APTranAccrual.aPDocType, Equal<POAccrualDetail.aPDocType>>>>, And<BqlOperand<APTranAccrual.aPRefNbr, IBqlString>.IsEqual<POAccrualDetail.aPRefNbr>>>>.And<BqlOperand<APTranAccrual.aPLineNbr, IBqlInt>.IsEqual<POAccrualDetail.lineNbr>>>>, FbqlJoins.Left<INTranDocInfo>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.useOrigINDoc, Equal<False>>>>, And<BqlOperand<INTranDocInfo.docType, IBqlString>.IsNotIn<PX.Objects.IN.INDocType.adjustment, PX.Objects.IN.INDocType.transfer>>>, And<BqlOperand<INTranDocInfo.pOReceiptType, IBqlString>.IsEqual<POAccrualDetail.pOReceiptType>>>, And<BqlOperand<INTranDocInfo.pOReceiptNbr, IBqlString>.IsEqual<POAccrualDetail.pOReceiptNbr>>>, And<BqlOperand<INTranDocInfo.pOReceiptLineNbr, IBqlInt>.IsEqual<POAccrualDetail.lineNbr>>>>.And<BqlOperand<INTranDocInfo.pOReceiptType, IBqlString>.IsEqual<POAccrualDetail.pOReceiptType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CurrentValue<POAccrualInquiryFilter.subCD>, IsNull>>>>.Or<BqlOperand<PX.Objects.GL.Sub.subCD, IBqlString>.IsLike<BqlField<POAccrualInquiryFilter.subCDWildcard, IBqlString>.FromCurrent.Value>>>>>.And<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.aPRefNbr, IsNotNull>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.taxAdjPosted, NotEqual<True>>>>, Or<BqlOperand<POAccrualDetail.pPVAdjPosted, IBqlBool>.IsNotEqual<True>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.reversedFinPeriodID, Greater<BqlField<POAccrualInquiryFilter.finPeriodID, IBqlString>.FromCurrent.Value>>>>>.Or<BqlOperand<POAccrualDetail.reversedFinPeriodID, IBqlString>.IsNull>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.reversingFinPeriodID, Greater<BqlField<POAccrualInquiryFilter.finPeriodID, IBqlString>.FromCurrent.Value>>>>>.Or<BqlOperand<POAccrualDetail.reversingFinPeriodID, IBqlString>.IsNull>>>>>.And<BqlOperand<POAccrualDetail.accruedCostTotal, IBqlDecimal>.IsNotEqual<Use<IsNull<BqlFunction<Add<APTranAccrual.accruedCost, APTranAccrual.pPVAmt>, IBqlDecimal>.Add<APTranAccrual.taxAccruedCost>, decimal0>>.AsDecimal>>>>>>.Or<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.pOReceiptNbr, IsNotNull>>>, And<BqlOperand<POAccrualDetail.accruedCostTotal, IBqlDecimal>.IsNotEqual<Use<IsNull<BqlFunction<Add<POReceiptLineAccrual.accruedCost, POReceiptLineAccrual.pPVAmt>, IBqlDecimal>.Add<POReceiptLineAccrual.taxAccruedCost>, decimal0>>.AsDecimal>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualDetail.reversedFinPeriodID, Greater<BqlField<POAccrualInquiryFilter.finPeriodID, IBqlString>.FromCurrent.Value>>>>>.Or<BqlOperand<POAccrualDetail.reversedFinPeriodID, IBqlString>.IsNull>>>>>>), Persistent = false)]
public class POAccrualInquiryResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [POAccrualInquiryResult.documentNoteID.Note]
  public virtual Guid? DocumentNoteID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (POAccrualDetail.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POAccrualStatus.orderType))]
  [PXUIField(DisplayName = "PO Type")]
  [POOrderType.List]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POAccrualStatus.orderNbr))]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Current<POAccrualInquiryResult.orderType>>>>))]
  [PXUIField(DisplayName = "PO Ref. Nbr.")]
  public virtual string OrderNbr { get; set; }

  /// <summary>
  /// The unreceived quantity of goods in line of AP Bill, selected in the Document Nbr. column. Empty if the document is not AP Bill.
  /// </summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<Sub<POAccrualDetail.baseAccruedQty, Use<IsNull<APTranAccrual.baseAccruedQty, decimal0>>.AsDecimal>, IBqlDecimal>.When<BqlOperand<POAccrualDetail.aPRefNbr, IBqlString>.IsNotNull>.ElseNull), typeof (Decimal))]
  [PXUIField(DisplayName = "Qty. Not Received")]
  public virtual Decimal? NotReceivedQty { get; set; }

  /// <summary>
  /// Base order quantity in related PO line, if any, else empty.
  /// </summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<POAccrualStatus.baseOrigQty, IBqlDecimal>.When<BqlOperand<POAccrualStatus.orderLineNbr, IBqlInt>.IsNotNull>.ElseNull), typeof (Decimal))]
  [PXUIField(DisplayName = "Order Qty.")]
  public virtual Decimal? OrderQty { get; set; }

  /// <summary>
  /// Quantity of goods in the purchase receipt or return selected in the Document Nbr. column for which no related AP bill has been prepared yet. Empty, if the document is not PO Receipt/Return.
  /// </summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<Sub<POAccrualDetail.baseAccruedQty, Use<IsNull<POReceiptLineAccrual.baseAccruedQty, decimal0>>.AsDecimal>, IBqlDecimal>.When<BqlOperand<POAccrualDetail.pOReceiptNbr, IBqlString>.IsNotNull>.ElseNull), typeof (Decimal))]
  [PXUIField(DisplayName = "Unbilled Qty.")]
  public virtual Decimal? UnbilledQty { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POAccrualDetail.pOReceiptType))]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POAccrualDetail.pOReceiptNbr))]
  public virtual string POReceiptNbr { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (POAccrualDetail.aPDocType))]
  [PX.Objects.AP.APDocType.List]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POAccrualDetail.aPRefNbr))]
  public virtual string APRefNbr { get; set; }

  [PXDBBool(BqlField = typeof (POAccrualDetail.isReversed))]
  public virtual bool? IsReversed { get; set; }

  [PXDBBool(BqlField = typeof (POAccrualDetail.isReversing))]
  public virtual bool? IsReversing { get; set; }

  [PXString(2, IsFixed = true)]
  [POAccrualInquiryResult.documentType.List]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<POAccrualInquiryResult.aPDocType, IBqlString>.IsEqual<PX.Objects.AP.APDocType.invoice>>, POAccrualInquiryResult.documentType.apBill, Case<Where<BqlOperand<POAccrualInquiryResult.aPDocType, IBqlString>.IsEqual<PX.Objects.AP.APDocType.debitAdj>>, POAccrualInquiryResult.documentType.debitAdj, Case<Where<BqlOperand<POAccrualInquiryResult.pOReceiptType, IBqlString>.IsEqual<PX.Objects.PO.POReceiptType.poreceipt>>, POAccrualInquiryResult.documentType.poReceipt, Case<Where<BqlOperand<POAccrualInquiryResult.pOReceiptType, IBqlString>.IsEqual<PX.Objects.PO.POReceiptType.poreturn>>, POAccrualInquiryResult.documentType.poReturn>>>>, Null>))]
  [PXUIField(DisplayName = "Document Type")]
  public virtual string DocumentType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Document Number")]
  [PXFormula(typeof (BqlOperand<POAccrualInquiryResult.aPRefNbr, IBqlString>.When<BqlOperand<POAccrualInquiryResult.aPRefNbr, IBqlString>.IsNotNull>.Else<POAccrualInquiryResult.pOReceiptNbr>))]
  public virtual string DocumentNbr { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (POAccrualDetail.finPeriodID))]
  [PXUIField(DisplayName = "Post Period")]
  public virtual string FinPeriodID { get; set; }

  [PXDBDate(BqlField = typeof (POAccrualDetail.docDate))]
  [PXUIField(DisplayName = "Document Date")]
  public virtual DateTime? DocDate { get; set; }

  [Vendor(DescriptionField = typeof (PX.Objects.AP.Vendor.acctCD), BqlField = typeof (POAccrualDetail.vendorID))]
  public virtual int? VendorID { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<POAccrualInquiryResult.vendorID, PX.Objects.AP.Vendor.acctName>))]
  [PXUIField(DisplayName = "Vendor Name")]
  public virtual string VendorName { get; set; }

  [PXString(1, IsFixed = true)]
  [PXDBCalced(typeof (IsNull<INTranDocInfo.docType, POAccrualDetail.origINDocType>), typeof (string))]
  [PX.Objects.IN.INDocType.List]
  [PXUIField]
  public virtual string INDocType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXDBCalced(typeof (IsNull<INTranDocInfo.refNbr, POAccrualDetail.origINDocRefNbr>), typeof (string))]
  [PXUIField(DisplayName = "IN Document Ref. Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POAccrualInquiryResult.iNDocType>>>>))]
  public virtual string INRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POAccrualDetail.pPVAdjRefNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<PX.Objects.IN.INDocType.adjustment>>>))]
  public virtual string PPVAdjRefNbr { get; set; }

  [PXDBBool(BqlField = typeof (POAccrualDetail.pPVAdjPosted))]
  public virtual bool? PPVAdjPosted { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POAccrualDetail.taxAdjRefNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<PX.Objects.IN.INDocType.adjustment>>>))]
  public virtual string TaxAdjRefNbr { get; set; }

  [PXDBBool(BqlField = typeof (POAccrualDetail.taxAdjPosted))]
  public virtual bool? TaxAdjPosted { get; set; }

  [Branch(null, null, true, true, true, BqlField = typeof (POAccrualDetail.branchID), Required = false)]
  public virtual int? BranchID { get; set; }

  [Site(BqlField = typeof (POAccrualStatus.siteID))]
  public virtual int? SiteID { get; set; }

  [AnyInventory(BqlField = typeof (POAccrualStatus.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POAccrualDetail.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [Account(BqlField = typeof (POAccrualStatus.acctID))]
  public virtual int? AcctID { get; set; }

  [SubAccount(BqlField = typeof (POAccrualStatus.subID))]
  public virtual int? SubID { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualDetail.accruedCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AccruedCost { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualDetail.pPVAmt))]
  public virtual Decimal? PPVAmt { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (BqlFunction<Add<POAccrualDetail.accruedCost, POAccrualDetail.pPVAmt>, IBqlDecimal>.Add<POAccrualDetail.taxAccruedCost>), typeof (Decimal))]
  public virtual Decimal? AccruedCostTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POAccrualDetail.taxAdjAmt))]
  public virtual Decimal? TaxAdjAmt { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (IsNull<POReceiptLineAccrual.accruedCost, decimal0>), typeof (Decimal))]
  public virtual Decimal? AccruedByReceiptsCost { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (IsNull<POReceiptLineAccrual.pPVAmt, decimal0>), typeof (Decimal))]
  public virtual Decimal? AccruedByReceiptsPPVAmt { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (IsNull<BqlFunction<Add<POReceiptLineAccrual.accruedCost, POReceiptLineAccrual.pPVAmt>, IBqlDecimal>.Add<POReceiptLineAccrual.taxAccruedCost>, decimal0>), typeof (Decimal))]
  public virtual Decimal? AccruedByReceiptsTotal { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (BqlFunction<Add<APTranAccrual.accruedCost, APTranAccrual.pPVAmt>, IBqlDecimal>.Add<APTranAccrual.taxAccruedCost>), typeof (Decimal))]
  public virtual Decimal? AccruedByBillsTotal { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (BqlOperand<Sub<Sub<POAccrualInquiryResult.accruedCost, POAccrualInquiryResult.accruedByReceiptsCost>, POAccrualInquiryResult.accruedByReceiptsPPVAmt>, IBqlDecimal>.When<BqlOperand<POAccrualInquiryResult.pOReceiptNbr, IBqlString>.IsNotNull>.Else<decimal0>))]
  [PXUIField(DisplayName = "Unbilled Amount")]
  public virtual Decimal? UnbilledAmt { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<POAccrualInquiryResult.aPRefNbr, IBqlString>.IsNotNull>, BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<POAccrualInquiryResult.pPVAdjPosted, IBqlBool>.IsEqual<True>>, decimal0>>, BqlOperand<POAccrualInquiryResult.pPVAmt, IBqlDecimal>.Multiply<decimal_1>>, IBqlDecimal>.Add<BqlOperand<decimal0, IBqlDecimal>.When<BqlOperand<POAccrualInquiryResult.taxAdjPosted, IBqlBool>.IsEqual<True>>.Else<POAccrualInquiryResult.taxAdjAmt>>>, decimal0>))]
  [PXUIField(DisplayName = "IN Adjustment Amount Not Released")]
  public virtual Decimal? NotAdjustedAmt { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (BqlOperand<Sub<POAccrualInquiryResult.accruedCostTotal, Use<IsNull<POAccrualInquiryResult.accruedByBillsTotal, POAccrualInquiryResult.taxAdjAmt>>.AsDecimal>, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualInquiryResult.aPRefNbr, IsNotNull>>>>.And<BqlOperand<POAccrualInquiryResult.orderType, IBqlString>.IsNotEqual<POOrderType.dropShip>>>.Else<decimal0>))]
  [PXUIField(DisplayName = "Not Received Amount")]
  public virtual Decimal? NotReceivedAmt { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (BqlOperand<Sub<POAccrualInquiryResult.accruedCostTotal, Use<IsNull<POAccrualInquiryResult.accruedByBillsTotal, POAccrualInquiryResult.taxAdjAmt>>.AsDecimal>, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POAccrualInquiryResult.aPRefNbr, IsNotNull>>>>.And<BqlOperand<POAccrualInquiryResult.orderType, IBqlString>.IsEqual<POOrderType.dropShip>>>.Else<decimal0>))]
  [PXUIField(DisplayName = "Drop-Ship Amount Not Invoiced")]
  public virtual Decimal? NotInvoicedAmt { get; set; }

  [PXBaseCury]
  [PXFormula(typeof (BqlFunctionMirror<Add<Add<POAccrualInquiryResult.notAdjustedAmt, POAccrualInquiryResult.notReceivedAmt>, POAccrualInquiryResult.notInvoicedAmt>, IBqlDecimal>.Subtract<POAccrualInquiryResult.unbilledAmt>))]
  [PXUIField(DisplayName = "PO Accrued Amount")]
  public virtual Decimal? AccrualAmt { get; set; }

  public abstract class documentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualInquiryResult.documentNoteID>
  {
    public class NoteAttribute : ProjectionNoteAttribute
    {
      public NoteAttribute()
        : base(typeof (POReceipt))
      {
        ((PXDBFieldAttribute) this).IsKey = true;
        ((PXDBFieldAttribute) this).BqlField = typeof (POAccrualDetail.documentNoteID);
      }

      protected override string GetEntityType(PXCache cache, Guid? noteId)
      {
        int num;
        if (!(cache.Current is POAccrualInquiryResult current))
        {
          num = !noteId.HasValue ? 1 : 0;
        }
        else
        {
          Guid? documentNoteId = current.DocumentNoteID;
          Guid? nullable = noteId;
          num = documentNoteId.HasValue == nullable.HasValue ? (documentNoteId.HasValue ? (documentNoteId.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0;
        }
        return num != 0 && current != null && current.APDocType != null ? typeof (PX.Objects.AP.APInvoice).FullName : base.GetEntityType(cache, noteId);
      }
    }
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryResult.lineNbr>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualInquiryResult.orderNbr>
  {
  }

  public abstract class notReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.notReceivedQty>
  {
  }

  public abstract class orderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.orderQty>
  {
  }

  public abstract class unbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.unbilledQty>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.pOReceiptNbr>
  {
  }

  public abstract class aPDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualInquiryResult.aPRefNbr>
  {
  }

  public abstract class isReversed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualInquiryResult.isReversed>
  {
  }

  public abstract class isReversing : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POAccrualInquiryResult.isReversing>
  {
  }

  public abstract class documentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.documentType>
  {
    public const string Bill = "BL";
    public const string DebitAdj = "DA";
    public const string Receipt = "PR";
    public const string Return = "RT";

    public class apBill : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      POAccrualInquiryResult.documentType.apBill>
    {
      public apBill()
        : base("BL")
      {
      }
    }

    public class debitAdj : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      POAccrualInquiryResult.documentType.debitAdj>
    {
      public debitAdj()
        : base("DA")
      {
      }
    }

    public class poReceipt : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      POAccrualInquiryResult.documentType.poReceipt>
    {
      public poReceipt()
        : base("PR")
      {
      }
    }

    public class poReturn : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      POAccrualInquiryResult.documentType.poReturn>
    {
      public poReturn()
        : base("RT")
      {
      }
    }

    [PXLocalizable]
    public static class Messages
    {
      public const string Bill = "AP Bill";
      public const string DebitAdj = "Debit Adj.";
      public const string Receipt = "PO Receipt";
      public const string Return = "PO Return";
    }

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new (string, string)[4]
        {
          ("BL", "AP Bill"),
          ("DA", "Debit Adj."),
          ("PR", "PO Receipt"),
          ("RT", "PO Return")
        })
      {
      }
    }
  }

  public abstract class documentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.documentNbr>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.finPeriodID>
  {
  }

  public abstract class docDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualInquiryResult.docDate>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryResult.vendorID>
  {
  }

  public abstract class vendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.vendorName>
  {
  }

  public abstract class iNDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.iNDocType>
  {
  }

  public abstract class iNRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualInquiryResult.iNRefNbr>
  {
  }

  public abstract class pPVAdjRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.iNRefNbr>
  {
  }

  public abstract class pPVAdjPosted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POAccrualInquiryResult.pPVAdjPosted>
  {
  }

  public abstract class taxAdjRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualInquiryResult.taxAdjRefNbr>
  {
  }

  public abstract class taxAdjPosted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POAccrualInquiryResult.taxAdjPosted>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryResult.branchID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryResult.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryResult.inventoryID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualInquiryResult.tranDesc>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryResult.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualInquiryResult.subID>
  {
  }

  public abstract class accruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.accruedCost>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualInquiryResult.pPVAmt>
  {
  }

  public abstract class accruedCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.accruedCostTotal>
  {
  }

  public abstract class taxAdjAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.taxAdjAmt>
  {
  }

  public abstract class accruedByReceiptsCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.accruedByReceiptsCost>
  {
  }

  public abstract class accruedByReceiptsPPVAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.accruedByReceiptsPPVAmt>
  {
  }

  public abstract class accruedByReceiptsTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.accruedByReceiptsTotal>
  {
  }

  public abstract class accruedByBillsTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.accruedByBillsTotal>
  {
  }

  public abstract class unbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.unbilledAmt>
  {
  }

  public abstract class notAdjustedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.notAdjustedAmt>
  {
  }

  public abstract class notReceivedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.notReceivedAmt>
  {
  }

  public abstract class notInvoicedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.notInvoicedAmt>
  {
  }

  public abstract class accrualAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualInquiryResult.accrualAmt>
  {
  }
}
