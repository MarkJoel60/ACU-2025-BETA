// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlUnaryFullText
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
internal interface IBqlUnaryFullText : IBqlUnary, IBqlCreator, IBqlVerifier
{
  System.Type FullTextField { get; }

  System.Type OperandValue { get; }

  IBqlUnary getLikeCounterpart();

  IBqlUnary getMatchAgainstCounterpart();

  BqlFullTextRenderingMethod getFullTextSupportMode(
    PXDatabaseProvider pXDatabaseProvider,
    PXGraph graph,
    List<System.Type> tables,
    BqlCommand.Selection selection);
}
