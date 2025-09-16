// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualDetailTaxAdjPostedUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

[POAccrualDetailTaxAdjPostedUpdate.Accumulator(BqlTable = typeof (POAccrualDetail))]
[PXHidden]
[Serializable]
public class POAccrualDetailTaxAdjPostedUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  public virtual Guid? POAccrualRefNoteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? POAccrualLineNbr { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault]
  [PX.Objects.PO.POAccrualType.List]
  public virtual 
  #nullable disable
  string POAccrualType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  public virtual string TaxAdjRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? TaxAdjPosted { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class pOAccrualRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.pOAccrualRefNoteID>
  {
  }

  public abstract class pOAccrualLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.pOAccrualLineNbr>
  {
  }

  public abstract class pOAccrualType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.pOAccrualType>
  {
  }

  public abstract class taxAdjRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.taxAdjRefNbr>
  {
  }

  public abstract class taxAdjPosted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.taxAdjPosted>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    POAccrualDetailTaxAdjPostedUpdate.Tstamp>
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
      POAccrualDetailTaxAdjPostedUpdate taxAdjPostedUpdate = (POAccrualDetailTaxAdjPostedUpdate) row;
      columns.UpdateOnly = true;
      columns.Update<POAccrualDetailTaxAdjPostedUpdate.taxAdjPosted>((object) taxAdjPostedUpdate.TaxAdjPosted, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POAccrualDetailTaxAdjPostedUpdate.lastModifiedByID>((object) taxAdjPostedUpdate.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POAccrualDetailTaxAdjPostedUpdate.lastModifiedDateTime>((object) taxAdjPostedUpdate.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<POAccrualDetailTaxAdjPostedUpdate.lastModifiedByScreenID>((object) taxAdjPostedUpdate.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
