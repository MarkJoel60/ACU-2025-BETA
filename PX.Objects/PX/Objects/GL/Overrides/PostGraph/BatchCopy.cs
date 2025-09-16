// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.PostGraph.BatchCopy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.Overrides.PostGraph;

[PXHidden]
[Serializable]
public class BatchCopy : Batch
{
  public new abstract class origBatchNbr : BqlType<IBqlString, string>.Field<
  #nullable disable
  BatchCopy.origBatchNbr>
  {
  }

  public new abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BatchCopy.origModule>
  {
  }

  public new abstract class autoReverseCopy : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BatchCopy.autoReverseCopy>
  {
  }
}
