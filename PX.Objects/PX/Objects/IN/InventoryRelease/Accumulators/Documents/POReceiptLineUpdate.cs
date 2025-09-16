// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Documents.POReceiptLineUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.Documents;

[PXHidden]
[POReceiptLineUpdate.Accumulator(BqlTable = typeof (PX.Objects.PO.POReceiptLine))]
public class POReceiptLineUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? INReleased { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? UpdateTranCostFinal { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranCostFinal { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXDefault]
  public virtual string ReasonCode { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp(ForbidChangesOfPersistedRecords = true)]
  public virtual byte[] tstamp { get; set; }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineUpdate.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineUpdate.receiptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineUpdate.lineNbr>
  {
  }

  public abstract class iNReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineUpdate.iNReleased>
  {
  }

  public abstract class updateTranCostFinal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLineUpdate.updateTranCostFinal>
  {
  }

  public abstract class tranCostFinal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineUpdate.tranCostFinal>
  {
  }

  public abstract class reasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineUpdate.reasonCode>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineUpdate.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptLineUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POReceiptLineUpdate.Tstamp>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute() => this.SingleRecord = true;

    protected virtual bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.UpdateOnly = true;
      columns.Update<POReceiptLineUpdate.iNReleased>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POReceiptLineUpdate.reasonCode>((PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<POReceiptLineUpdate.tranCostFinal>(((POReceiptLineUpdate) row).UpdateTranCostFinal.GetValueOrDefault() ? (PXDataFieldAssign.AssignBehavior) 0 : (PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<POReceiptLineUpdate.lastModifiedByID>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POReceiptLineUpdate.lastModifiedDateTime>((PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POReceiptLineUpdate.lastModifiedByScreenID>((PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
