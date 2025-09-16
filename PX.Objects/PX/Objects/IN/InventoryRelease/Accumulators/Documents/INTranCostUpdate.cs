// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.Documents.INTranCostUpdate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.Documents;

[PXHidden]
[INTranCostUpdate.Accumulator(BqlTable = typeof (INTranCost))]
public class INTranCostUpdate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault]
  public virtual string CostDocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public virtual string CostRefNbr { get; set; }

  [PXDBLong(IsKey = true)]
  [PXDefault]
  public virtual long? CostID { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? ResetOversoldFlag { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsOversold { get; set; }

  [PXString(1, IsFixed = true)]
  public virtual string ValMethod { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(typeof (decimal0))]
  public virtual Decimal? OversoldQty { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(typeof (decimal0))]
  public virtual Decimal? OversoldTranCost { get; set; }

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
  INTranCostUpdate.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCostUpdate.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCostUpdate.lineNbr>
  {
  }

  public abstract class costDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCostUpdate.costDocType>
  {
  }

  public abstract class costRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCostUpdate.costRefNbr>
  {
  }

  public abstract class costID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INTranCostUpdate.costID>
  {
  }

  public abstract class resetOversoldFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INTranCostUpdate.resetOversoldFlag>
  {
  }

  public abstract class isOversold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTranCostUpdate.isOversold>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCostUpdate.valMethod>
  {
  }

  public abstract class oversoldQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTranCostUpdate.oversoldQty>
  {
  }

  public abstract class oversoldTranCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTranCostUpdate.oversoldTranCost>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INTranCostUpdate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranCostUpdate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranCostUpdate.lastModifiedDateTime>
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
      INTranCostUpdate inTranCostUpdate = (INTranCostUpdate) row;
      columns.UpdateOnly = true;
      if (inTranCostUpdate.ResetOversoldFlag.GetValueOrDefault())
        columns.Update<INTranCostUpdate.isOversold>((object) false, (PXDataFieldAssign.AssignBehavior) 0);
      columns.Update<INTranCostUpdate.oversoldQty>((PXDataFieldAssign.AssignBehavior) 1);
      columns.Update<INTranCostUpdate.oversoldTranCost>((PXDataFieldAssign.AssignBehavior) 1);
      if (inTranCostUpdate.ResetOversoldFlag.GetValueOrDefault())
      {
        columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
        {
          (PXAccumulatorRestriction) new PXAccumulatorRestriction<INTranCostUpdate.oversoldQty>((PXComp) 0, (object) 0)
        });
        if (inTranCostUpdate.ValMethod != "T")
          columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
          {
            (PXAccumulatorRestriction) new PXAccumulatorRestriction<INTranCostUpdate.oversoldTranCost>((PXComp) 0, (object) 0)
          });
      }
      else
      {
        columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
        {
          (PXAccumulatorRestriction) new PXAccumulatorRestriction<INTranCostUpdate.oversoldQty>((PXComp) 3, (object) 0)
        });
        if (inTranCostUpdate.ValMethod != "T")
        {
          columns.AppendException(string.Empty, new PXAccumulatorRestriction[1]
          {
            (PXAccumulatorRestriction) new PXAccumulatorRestriction<INTranCostUpdate.oversoldTranCost>((PXComp) 3, (object) 0)
          });
          columns.AppendException(string.Empty, new PXAccumulatorRestriction[2]
          {
            (PXAccumulatorRestriction) new PXAccumulatorRestriction<INTranCostUpdate.oversoldQty>((PXComp) 2, (object) 0),
            (PXAccumulatorRestriction) new PXAccumulatorRestriction<INTranCostUpdate.oversoldTranCost>((PXComp) 0, (object) 0)
          });
        }
      }
      return true;
    }

    public virtual bool PersistInserted(PXCache cache, object row)
    {
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXRestrictionViolationException ex)
      {
        INTranCostUpdate inTranCostUpdate = (INTranCostUpdate) row;
        INTran inTran = INTran.PK.Find(cache.Graph, inTranCostUpdate.DocType, inTranCostUpdate.RefNbr, inTranCostUpdate.LineNbr);
        throw new PXRestartOperationException((Exception) new PXException("The document has not been released because the cost layer of the '{0} {1}' item was not updated. Try to release the document again.", new object[2]
        {
          cache.Graph.Caches[typeof (INTran)].GetValueExt<INTran.inventoryID>((object) inTran),
          cache.Graph.Caches[typeof (INTran)].GetValueExt<INTran.subItemID>((object) inTran)
        }));
      }
    }
  }
}
