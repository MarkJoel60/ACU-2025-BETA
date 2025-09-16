// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIDetailUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
[INPIDetailUpdate.Accumulator(BqlTable = typeof (INPIDetail))]
[Serializable]
public class INPIDetailUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string PIID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? FinalExtVarCost { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp(ForbidChangesOfPersistedRecords = true)]
  public virtual byte[] tstamp { get; set; }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIDetailUpdate.pIID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIDetailUpdate.lineNbr>
  {
  }

  public abstract class finalExtVarCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INPIDetailUpdate.finalExtVarCost>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIDetailUpdate.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INPIDetailUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIDetailUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIDetailUpdate.Tstamp>
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
      INPIDetailUpdate inpiDetailUpdate = (INPIDetailUpdate) row;
      columns.UpdateOnly = true;
      columns.Update<INPIDetailUpdate.finalExtVarCost>((object) inpiDetailUpdate.FinalExtVarCost, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INPIDetailUpdate.lastModifiedByID>((object) inpiDetailUpdate.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INPIDetailUpdate.lastModifiedDateTime>((object) inpiDetailUpdate.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INPIDetailUpdate.lastModifiedByScreenID>((object) inpiDetailUpdate.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
