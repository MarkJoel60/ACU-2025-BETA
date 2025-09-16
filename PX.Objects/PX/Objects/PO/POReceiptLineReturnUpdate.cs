// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineReturnUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXHidden]
[POReceiptLineReturnUpdate.Accumulator(BqlTable = typeof (POReceiptLine))]
[Serializable]
public class POReceiptLineReturnUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  public virtual 
  #nullable disable
  string ReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  public virtual int? LineNbr { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReturnedQty { get; set; }

  [PXDecimal(6)]
  public virtual Decimal? BaseOrigQty { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.receiptType>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.receiptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturnUpdate.lineNbr>
  {
  }

  public abstract class baseReturnedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.baseReturnedQty>
  {
  }

  public abstract class baseOrigQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.baseOrigQty>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    POReceiptLineReturnUpdate.Tstamp>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute() => this.SingleRecord = true;

    protected virtual bool PrepareInsert(
      PXCache sender,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(sender, row, columns))
        return false;
      POReceiptLineReturnUpdate lineReturnUpdate = (POReceiptLineReturnUpdate) row;
      columns.Update<POReceiptLineReturnUpdate.baseReturnedQty>((object) lineReturnUpdate.BaseReturnedQty, (PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<POReceiptLineReturnUpdate.lastModifiedByID>((object) lineReturnUpdate.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POReceiptLineReturnUpdate.lastModifiedDateTime>((object) lineReturnUpdate.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POReceiptLineReturnUpdate.lastModifiedByScreenID>((object) lineReturnUpdate.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
      if (lineReturnUpdate.BaseOrigQty.HasValue)
        columns.AppendException("The specified Returned Qty. exceeds the Received Qty. specified in the original PO receipt.", new PXAccumulatorRestriction[1]
        {
          (PXAccumulatorRestriction) new PXAccumulatorRestriction<POReceiptLineReturnUpdate.baseReturnedQty>((PXComp) 5, (object) lineReturnUpdate.BaseOrigQty)
        });
      return true;
    }
  }
}
