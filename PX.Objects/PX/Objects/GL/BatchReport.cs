// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BatchReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[Serializable]
public class BatchReport : Batch
{
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.finPeriodID, Equal<Optional<Batch.finPeriodID>>, And<Batch.ledgerID, Equal<Optional<Batch.ledgerID>>, And<Batch.released, Equal<True>, And<Batch.posted, Equal<True>, And<Where<Batch.module, Equal<BatchModule.moduleGL>, Or<Batch.module, Equal<BatchModule.moduleCM>>>>>>>>>))]
  [PXUIField]
  public override 
  #nullable disable
  string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  public new abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchReport.batchNbr>
  {
  }
}
