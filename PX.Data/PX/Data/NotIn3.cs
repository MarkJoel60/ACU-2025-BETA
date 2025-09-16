// Decompiled with JetBrains decompiler
// Type: PX.Data.NotIn3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class NotIn3 : InBase3
{
  protected NotIn3(params System.Type[] types)
    : base(types)
  {
  }

  [Obsolete]
  protected override string SqlOperator => " NOT IN ";

  protected override bool IsNegative => true;
}
