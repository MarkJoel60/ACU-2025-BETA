// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.APTranReceiptUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>
/// This DAC is used for update of Unreceived Qty during PO Receipt Release process
/// </summary>
[PXProjection(typeof (Select<PX.Objects.AP.APTran>), Persistent = true)]
[PXHidden]
[Serializable]
public class APTranReceiptUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [APDocType.List]
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AP.APTran.tranType))]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.AP.APTran.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.AP.APTran.lineNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AP.APTran.released))]
  public virtual bool? Released { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.AP.APTran.pOAccrualType))]
  [PX.Objects.PO.POAccrualType.List]
  public virtual string POAccrualType { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.AP.APTran.pOOrderType))]
  [PX.Objects.PO.POOrderType.List]
  public virtual string POOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AP.APTran.pONbr))]
  public virtual string PONbr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.AP.APTran.pOLineNbr))]
  public virtual int? POLineNbr { get; set; }

  [APTranInventoryItem(BqlField = typeof (PX.Objects.AP.APTran.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [INUnit(typeof (APTranReceiptUpdate.inventoryID), BqlField = typeof (PX.Objects.AP.APTran.uOM))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (APTranReceiptUpdate.uOM), typeof (APTranReceiptUpdate.baseUnreceivedQty), HandleEmptyKey = true, BqlField = typeof (PX.Objects.AP.APTran.unreceivedQty))]
  [PXDefault]
  public virtual Decimal? UnreceivedQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.AP.APTran.baseUnreceivedQty))]
  [PXDefault]
  public virtual Decimal? BaseUnreceivedQty { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (PX.Objects.AP.APTran.finPeriodID))]
  public virtual string FinPeriodID { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.AP.APTran.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.AP.APTran.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.AP.APTran.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp(BqlField = typeof (PX.Objects.AP.APTran.Tstamp))]
  public virtual byte[] tstamp { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranReceiptUpdate.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranReceiptUpdate.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranReceiptUpdate.lineNbr>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTranReceiptUpdate.released>
  {
  }

  public abstract class pOAccrualType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranReceiptUpdate.pOAccrualType>
  {
  }

  public abstract class pOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranReceiptUpdate.pOOrderType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranReceiptUpdate.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranReceiptUpdate.pOLineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTranReceiptUpdate.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTranReceiptUpdate.uOM>
  {
  }

  public abstract class unreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranReceiptUpdate.unreceivedQty>
  {
  }

  public abstract class baseUnreceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTranReceiptUpdate.baseUnreceivedQty>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranReceiptUpdate.finPeriodID>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APTranReceiptUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APTranReceiptUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    APTranReceiptUpdate.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APTranReceiptUpdate.Tstamp>
  {
  }
}
