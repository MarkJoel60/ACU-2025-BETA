// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Documents.INTranUpdate
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
[INTranUpdate.Accumulator(BqlTable = typeof (INTran))]
public class INTranUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranCost { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranUpdate.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranUpdate.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranUpdate.lineNbr>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranUpdate.tranCost>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INTranUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranUpdate.lastModifiedDateTime>
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
      columns.Update<INTranUpdate.tranCost>((PXDataFieldAssign.AssignBehavior) 1);
      return true;
    }
  }
}
