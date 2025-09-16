// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ReclassBatch
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public class ReclassBatch : Batch
{
  public new abstract class batchNbr : BqlType<IBqlString, string>.Field<
  #nullable disable
  ReclassBatch.batchNbr>
  {
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReclassBatch.module>
  {
  }

  public new abstract class posted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ReclassBatch.posted>
  {
  }
}
