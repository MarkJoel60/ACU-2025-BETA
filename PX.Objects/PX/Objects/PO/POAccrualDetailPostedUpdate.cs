// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualDetailPostedUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

[POAccrualDetailPostedUpdate.Accumulator(BqlTable = typeof (POAccrualDetail))]
[PXHidden]
[Serializable]
public class POAccrualDetailPostedUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string POReceiptType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string POReceiptNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  public virtual bool? Posted { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string FinPeriodID { get; set; }

  [PXDBDecimal(4)]
  public virtual Decimal? AccruedCost { get; set; }

  [PXDecimal(4)]
  public virtual Decimal? PreviousCost { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class pOReceiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.pOReceiptType>
  {
  }

  public abstract class pOReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.pOReceiptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POAccrualDetailPostedUpdate.lineNbr>
  {
  }

  public abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POAccrualDetailPostedUpdate.posted>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.finPeriodID>
  {
  }

  public abstract class accruedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.accruedCost>
  {
  }

  public abstract class previousCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.previousCost>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    POAccrualDetailPostedUpdate.Tstamp>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    protected virtual bool PrepareInsert(
      PXCache sender,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(sender, row, columns))
        return false;
      POAccrualDetailPostedUpdate detailPostedUpdate = (POAccrualDetailPostedUpdate) row;
      columns.UpdateOnly = true;
      if (detailPostedUpdate.Posted.HasValue)
        columns.Update<POAccrualDetailPostedUpdate.posted>((object) detailPostedUpdate.Posted, (PXDataFieldAssign.AssignBehavior) 0);
      if (detailPostedUpdate.FinPeriodID != null)
        columns.Update<POAccrualDetailPostedUpdate.finPeriodID>((object) detailPostedUpdate.FinPeriodID, (PXDataFieldAssign.AssignBehavior) 0);
      if (detailPostedUpdate.AccruedCost.HasValue)
        columns.Update<POAccrualDetailPostedUpdate.accruedCost>((object) detailPostedUpdate.AccruedCost, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POAccrualDetailPostedUpdate.lastModifiedByID>((object) detailPostedUpdate.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POAccrualDetailPostedUpdate.lastModifiedDateTime>((object) detailPostedUpdate.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POAccrualDetailPostedUpdate.lastModifiedByScreenID>((object) detailPostedUpdate.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
