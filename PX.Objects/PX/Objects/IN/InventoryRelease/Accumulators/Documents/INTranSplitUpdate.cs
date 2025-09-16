// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Documents.INTranSplitUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.Documents;

[PXHidden]
[INTranSplitUpdate.Accumulator(BqlTable = typeof (INTranSplit))]
public class INTranSplitUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CostSiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CostSubItemID { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalCost { get; set; }

  [PXDBLong]
  public virtual long? PlanID { get; set; }

  [PXBool]
  public virtual bool? ResetPlanID { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp(ForbidChangesOfPersistedRecords = true)]
  public virtual byte[] tstamp { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplitUpdate.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranSplitUpdate.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplitUpdate.lineNbr>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplitUpdate.costSiteID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranSplitUpdate.costSubItemID>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplitUpdate.totalQty>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranSplitUpdate.totalCost>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INTranSplitUpdate.planID>
  {
  }

  public abstract class resetPlanID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTranSplitUpdate.resetPlanID>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INTranSplitUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranSplitUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranSplitUpdate.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTranSplitUpdate.Tstamp>
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
      columns.Update<INTranSplitUpdate.totalQty>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INTranSplitUpdate.totalCost>((PXDataFieldAssign.AssignBehavior) 1);
      if (((INTranSplitUpdate) row).ResetPlanID.GetValueOrDefault())
        columns.Update<INTranSplitUpdate.planID>((PXDataFieldAssign.AssignBehavior) 5);
      return true;
    }
  }
}
