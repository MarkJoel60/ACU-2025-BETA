// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.DAC.Accumulators.TemplateItemLastModifiedUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.Matrix.DAC.Accumulators;

[PXHidden]
[TemplateItemLastModifiedUpdate.Accumulator(BqlTable = typeof (InventoryItem))]
public class TemplateItemLastModifiedUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual 
  #nullable disable
  string LastModifiedByScreenID { get; set; }

  [PXDBTimestamp(ForbidChangesOfPersistedRecords = true)]
  public virtual byte[] tstamp { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TemplateItemLastModifiedUpdate.inventoryID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TemplateItemLastModifiedUpdate.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TemplateItemLastModifiedUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TemplateItemLastModifiedUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    TemplateItemLastModifiedUpdate.Tstamp>
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
      TemplateItemLastModifiedUpdate lastModifiedUpdate = (TemplateItemLastModifiedUpdate) row;
      columns.UpdateOnly = true;
      columns.Update<TemplateItemLastModifiedUpdate.lastModifiedByID>((object) lastModifiedUpdate.LastModifiedByID, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<TemplateItemLastModifiedUpdate.lastModifiedDateTime>((object) lastModifiedUpdate.LastModifiedDateTime, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<TemplateItemLastModifiedUpdate.lastModifiedByScreenID>((object) lastModifiedUpdate.LastModifiedByScreenID, (PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
