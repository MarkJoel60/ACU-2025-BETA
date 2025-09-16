// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Accrual Detail")]
[Serializable]
public class POAccrualDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? DocumentNoteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? POAccrualRefNoteID { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? POAccrualLineNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PX.Objects.PO.POAccrualType.List]
  public virtual 
  #nullable disable
  string POAccrualType { get; set; }

  [PX.Objects.AP.APDocType.List]
  [PXDBString(3, IsFixed = true)]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string APRefNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string POReceiptNbr { get; set; }

  [Vendor]
  [PXDefault]
  public virtual int? VendorID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Posted { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsDropShip { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsReversed { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsReversing { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? DocDate { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault]
  public virtual string FinPeriodID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  public virtual string TranDesc { get; set; }

  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AccruedQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseAccruedQty { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AccruedCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PPVAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAccruedCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAdjAmt { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (BqlFunction<Add<POAccrualDetail.accruedCost, POAccrualDetail.pPVAmt>, IBqlDecimal>.Add<POAccrualDetail.taxAccruedCost>), typeof (Decimal))]
  public virtual Decimal? AccruedCostTotal { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string PPVAdjRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PPVAdjPosted { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string TaxAdjRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? TaxAdjPosted { get; set; }

  /// <summary>
  /// Equals True when POAccrualDetail belongs to the correction receipt line without correction INTran
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? UseOrigINDoc { get; set; }

  /// <summary>
  /// DocType of the IN Document of the original receipt line
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [INDocType.List]
  public virtual string OrigINDocType { get; set; }

  /// <summary>
  /// RefNbr of the IN Document of the original receipt line
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  public virtual string OrigINDocRefNbr { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <summary>
  /// The financial period, when parent document was reversed.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string ReversedFinPeriodID { get; set; }

  /// <summary>
  /// The financial period, when parent document was reversing the original document.
  /// </summary>
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string ReversingFinPeriodID { get; set; }

  public class PK : 
    PrimaryKeyOf<POAccrualDetail>.By<POAccrualDetail.documentNoteID, POAccrualDetail.lineNbr>
  {
    public static POAccrualDetail Find(
      PXGraph graph,
      Guid? documentNoteID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POAccrualDetail) PrimaryKeyOf<POAccrualDetail>.By<POAccrualDetail.documentNoteID, POAccrualDetail.lineNbr>.FindBy(graph, (object) documentNoteID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class AccrualStatus : 
      PrimaryKeyOf<POAccrualStatus>.By<POAccrualStatus.refNoteID, POAccrualStatus.lineNbr, POAccrualStatus.type>.ForeignKeyOf<POAccrualDetail>.By<POAccrualDetail.pOAccrualRefNoteID, POAccrualDetail.pOAccrualLineNbr, POAccrualDetail.pOAccrualType>
    {
    }

    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<POAccrualDetail>.By<POAccrualDetail.aPDocType, POAccrualDetail.aPRefNbr>
    {
    }

    public class APTran : 
      PrimaryKeyOf<PX.Objects.AP.APTran>.By<PX.Objects.AP.APTran.tranType, PX.Objects.AP.APTran.refNbr, PX.Objects.AP.APTran.lineNbr>.ForeignKeyOf<POAccrualDetail>.By<POAccrualDetail.aPDocType, POAccrualDetail.aPRefNbr, POAccrualDetail.lineNbr>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POAccrualDetail>.By<POAccrualDetail.pOReceiptType, POAccrualDetail.pOReceiptNbr>
    {
    }

    public class ReceiptLine : 
      PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.ForeignKeyOf<POAccrualDetail>.By<POAccrualDetail.pOReceiptType, POAccrualDetail.pOReceiptNbr, POAccrualDetail.lineNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POReceiptLine>.By<POAccrualDetail.branchID>
    {
    }
  }

  public abstract class documentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualDetail.documentNoteID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualDetail.lineNbr>
  {
  }

  public abstract class pOAccrualRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualDetail.pOAccrualRefNoteID>
  {
  }

  public abstract class pOAccrualLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualDetail.pOAccrualLineNbr>
  {
  }

  public abstract class pOAccrualType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.pOAccrualType>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualDetail.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualDetail.aPRefNbr>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.pOReceiptNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualDetail.vendorID>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetail.posted>
  {
  }

  public abstract class isDropShip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetail.isDropShip>
  {
  }

  public abstract class isReversed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetail.isReversed>
  {
  }

  public abstract class isReversing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetail.isReversing>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualDetail.branchID>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POAccrualDetail.docDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualDetail.finPeriodID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualDetail.tranDesc>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualDetail.uOM>
  {
  }

  public abstract class accruedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualDetail.accruedQty>
  {
  }

  public abstract class baseAccruedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualDetail.baseAccruedQty>
  {
  }

  public abstract class accruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualDetail.accruedCost>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualDetail.pPVAmt>
  {
  }

  public abstract class taxAccruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualDetail.taxAccruedCost>
  {
  }

  public abstract class taxAdjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualDetail.taxAdjAmt>
  {
  }

  public abstract class accruedCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualDetail.accruedCostTotal>
  {
  }

  public abstract class pPVAdjRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.pPVAdjRefNbr>
  {
  }

  public abstract class pPVAdjPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetail.pPVAdjPosted>
  {
  }

  public abstract class taxAdjRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.taxAdjRefNbr>
  {
  }

  public abstract class taxAdjPosted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetail.taxAdjPosted>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAccrualDetail.UseOrigINDoc" />
  public abstract class useOrigINDoc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetail.useOrigINDoc>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAccrualDetail.OrigINDocType" />
  public abstract class origINDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.origINDocType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAccrualDetail.OrigINDocRefNbr" />
  public abstract class origINDocRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.origINDocRefNbr>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualDetail.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAccrualDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualDetail.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POAccrualDetail.Tstamp>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAccrualDetail.ReversedFinPeriodID" />
  public abstract class reversedFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.reversedFinPeriodID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POAccrualDetail.ReversingFinPeriodID" />
  public abstract class reversingFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetail.reversingFinPeriodID>
  {
  }
}
