// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Documents.ARTranUpdate
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
[ARTranUpdate.Accumulator(BqlTable = typeof (PX.Objects.AR.ARTran))]
public class ARTranUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranCost { get; set; }

  [PXDBBool]
  public virtual bool? IsTranCostFinal { get; set; }

  [PXDBBool]
  public virtual bool? InvtReleased { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp(ForbidChangesOfPersistedRecords = true)]
  public virtual byte[] tstamp { get; set; }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranUpdate.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTranUpdate.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTranUpdate.lineNbr>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTranUpdate.tranCost>
  {
  }

  public abstract class isTranCostFinal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranUpdate.isTranCostFinal>
  {
  }

  public abstract class invtReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTranUpdate.invtReleased>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARTranUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTranUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTranUpdate.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARTranUpdate.Tstamp>
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
      columns.Update<ARTranUpdate.tranCost>((PXDataFieldAssign.AssignBehavior) 1);
      if (cache.GetValue<ARTranUpdate.isTranCostFinal>(row) != null)
        columns.Update<ARTranUpdate.isTranCostFinal>((PXDataFieldAssign.AssignBehavior) 0);
      if (cache.GetValue<ARTranUpdate.invtReleased>(row) != null)
        columns.Update<ARTranUpdate.invtReleased>((PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
