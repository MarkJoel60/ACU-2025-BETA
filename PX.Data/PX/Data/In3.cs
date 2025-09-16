// Decompiled with JetBrains decompiler
// Type: PX.Data.In3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class In3 : InBase3
{
  protected In3(params System.Type[] types)
    : base(types)
  {
  }

  [Obsolete]
  protected override string SqlOperator => " IN ";

  protected override bool IsNegative => false;
}
