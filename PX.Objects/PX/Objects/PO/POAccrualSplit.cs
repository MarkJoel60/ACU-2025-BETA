// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Accrual Allocation")]
[Serializable]
public class POAccrualSplit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBInt]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [POAccrualType.List]
  public virtual 
  #nullable disable
  string Type { get; set; }

  [PX.Objects.AP.APDocType.List]
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string APDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string APRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? APLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  public virtual string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string POReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? POReceiptLineNbr { get; set; }

  [PXDBString(6, IsUnicode = true)]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBDecimal(6)]
  [PXDefault]
  public virtual Decimal? AccruedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseAccruedQty { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AccruedCost { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PPVAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsReversed { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAccruedCost { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDefault]
  public virtual string FinPeriodID { get; set; }

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

  public class PK : 
    PrimaryKeyOf<POAccrualSplit>.By<POAccrualSplit.aPDocType, POAccrualSplit.aPRefNbr, POAccrualSplit.aPLineNbr, POAccrualSplit.pOReceiptType, POAccrualSplit.pOReceiptNbr, POAccrualSplit.pOReceiptLineNbr>
  {
    public static POAccrualSplit Find(
      PXGraph graph,
      string aPDocType,
      string aPRefNbr,
      int? aPLineNbr,
      string pOReceiptType,
      string pOReceiptNbr,
      int? pOReceiptLineNbr,
      PKFindOptions options = 0)
    {
      return (POAccrualSplit) PrimaryKeyOf<POAccrualSplit>.By<POAccrualSplit.aPDocType, POAccrualSplit.aPRefNbr, POAccrualSplit.aPLineNbr, POAccrualSplit.pOReceiptType, POAccrualSplit.pOReceiptNbr, POAccrualSplit.pOReceiptLineNbr>.FindBy(graph, (object) aPDocType, (object) aPRefNbr, (object) aPLineNbr, (object) pOReceiptType, (object) pOReceiptNbr, (object) pOReceiptLineNbr, options);
    }
  }

  public static class FK
  {
    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POAccrualSplit>.By<POAccrualSplit.pOReceiptType, POAccrualSplit.pOReceiptNbr>
    {
    }

    public class ReceiptLine : 
      PrimaryKeyOf<POReceiptLine>.By<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>.ForeignKeyOf<POAccrualSplit>.By<POAccrualSplit.pOReceiptType, POAccrualSplit.pOReceiptNbr, POAccrualSplit.pOReceiptLineNbr>
    {
    }

    public class AccrualStatus : 
      PrimaryKeyOf<POAccrualStatus>.By<POAccrualStatus.refNoteID, POAccrualStatus.lineNbr, POAccrualStatus.type>.ForeignKeyOf<POAccrualSplit>.By<POAccrualSplit.refNoteID, POAccrualSplit.lineNbr, POAccrualSplit.type>
    {
    }

    public class APInvoice : 
      PrimaryKeyOf<PX.Objects.AP.APInvoice>.By<PX.Objects.AP.APInvoice.docType, PX.Objects.AP.APInvoice.refNbr>.ForeignKeyOf<POAccrualSplit>.By<POAccrualSplit.aPDocType, POAccrualSplit.aPRefNbr>
    {
    }

    public class APTran : 
      PrimaryKeyOf<PX.Objects.AP.APTran>.By<PX.Objects.AP.APTran.tranType, PX.Objects.AP.APTran.refNbr, PX.Objects.AP.APTran.lineNbr>.ForeignKeyOf<POAccrualSplit>.By<POAccrualSplit.aPDocType, POAccrualSplit.aPRefNbr, POAccrualSplit.aPLineNbr>
    {
    }
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAccrualSplit.refNoteID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualSplit.lineNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualSplit.type>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualSplit.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualSplit.aPRefNbr>
  {
  }

  public abstract class aPLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualSplit.aPLineNbr>
  {
  }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualSplit.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualSplit.pOReceiptNbr>
  {
  }

  public abstract class pOReceiptLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualSplit.pOReceiptLineNbr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualSplit.uOM>
  {
  }

  public abstract class accruedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualSplit.accruedQty>
  {
  }

  public abstract class baseAccruedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualSplit.baseAccruedQty>
  {
  }

  public abstract class accruedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualSplit.accruedCost>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POAccrualSplit.pPVAmt>
  {
  }

  public abstract class isReversed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualSplit.isReversed>
  {
  }

  public abstract class taxAccruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualSplit.taxAccruedCost>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POAccrualSplit.finPeriodID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualSplit.createdDateTime>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POAccrualSplit.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualSplit.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualSplit.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualSplit.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualSplit.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POAccrualSplit.Tstamp>
  {
  }
}
