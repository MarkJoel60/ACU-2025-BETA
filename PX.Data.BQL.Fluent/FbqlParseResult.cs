// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlParseResult
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using System;

#nullable disable
namespace PX.Data.BQL.Fluent;

internal struct FbqlParseResult(
  Type bqlCommand,
  bool hasJoin,
  bool hasWhere,
  bool hasAggregate,
  bool hasOrder)
{
  public Type BqlCommand { get; } = bqlCommand;

  public bool HasJoin { get; } = hasJoin;

  public bool HasWhere { get; } = hasWhere;

  public bool HasAggregate { get; } = hasAggregate;

  public bool HasOrder { get; } = hasOrder;
}
