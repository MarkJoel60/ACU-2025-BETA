// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlOn
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <summary>A BQL <tt>On</tt> clause.</summary>
public interface IBqlOn : IBqlCreator, IBqlVerifier
{
  IBqlUnary GetMatchingWhere();

  bool AppendJoiner(
    Joiner join,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection);
}
