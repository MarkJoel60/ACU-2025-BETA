// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostBatchAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.FS;

public class FSPostBatchAttribute : PXCustomSelectorAttribute
{
  public FSPostBatchAttribute()
    : base(typeof (FSPostBatchAttribute.PostBatch.batchID), new Type[1]
    {
      typeof (FSPostBatchAttribute.PostBatch.batchNbr)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (FSPostBatchAttribute.PostBatch.batchNbr);
    ((PXSelectorAttribute) this).ValidateValue = false;
  }

  protected virtual 
  #nullable disable
  IEnumerable GetRecords()
  {
    FSPostBatchAttribute postBatchAttribute = this;
    PXCache cach = postBatchAttribute._Graph.Caches[typeof (FSBillHistory)];
    FSBillHistory fsBillHistory = PXView.Currents.OfType<FSBillHistory>().FirstOrDefault<FSBillHistory>() ?? (FSBillHistory) cach.Current;
    IEnumerable<FSPostBatchAttribute.PostBatch> postBatches;
    if (string.IsNullOrEmpty(fsBillHistory.ServiceContractRefNbr))
      postBatches = GraphHelper.RowCast<FSPostBatch>((IEnumerable) PXSelectBase<FSPostBatch, PXSelect<FSPostBatch, Where<FSPostBatch.batchID, Equal<Required<FSPostBatch.batchID>>>>.Config>.Select(postBatchAttribute._Graph, new object[1]
      {
        (object) fsBillHistory.BatchID
      })).Select<FSPostBatch, FSPostBatchAttribute.PostBatch>((Func<FSPostBatch, FSPostBatchAttribute.PostBatch>) (batch => new FSPostBatchAttribute.PostBatch()
      {
        BatchID = batch.BatchID,
        BatchNbr = batch.BatchNbr,
        IsContractBatch = new bool?(false)
      }));
    else
      postBatches = GraphHelper.RowCast<FSContractPostBatch>((IEnumerable) PXSelectBase<FSContractPostBatch, PXSelect<FSContractPostBatch, Where<FSContractPostBatch.contractPostBatchID, Equal<Required<FSContractPostBatch.contractPostBatchID>>>>.Config>.Select(postBatchAttribute._Graph, new object[1]
      {
        (object) fsBillHistory.BatchID
      })).Select<FSContractPostBatch, FSPostBatchAttribute.PostBatch>((Func<FSContractPostBatch, FSPostBatchAttribute.PostBatch>) (contractBatch => new FSPostBatchAttribute.PostBatch()
      {
        BatchID = contractBatch.ContractPostBatchID,
        BatchNbr = contractBatch.ContractPostBatchNbr,
        IsContractBatch = new bool?(true)
      }));
    foreach (object record in postBatches)
      yield return record;
  }

  [PXVirtual]
  [Serializable]
  public class PostBatch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt(IsKey = true)]
    [PXUIField(Enabled = false, Visible = false, DisplayName = "Batch ID")]
    public virtual int? BatchID { get; set; }

    [PXString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXUIField]
    public virtual string BatchNbr { get; set; }

    [PXString(1, IsKey = true)]
    public virtual string BatchType { get; set; }

    [PXBool]
    [PXDefault(false)]
    public virtual bool? IsContractBatch { get; set; }

    public abstract class batchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSPostBatchAttribute.PostBatch.batchID>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FSPostBatchAttribute.PostBatch.batchNbr>
    {
    }

    public abstract class batchType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      FSPostBatchAttribute.PostBatch.batchType>
    {
    }

    public abstract class isContractBatch : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      FSPostBatchAttribute.PostBatch.isContractBatch>
    {
    }
  }
}
