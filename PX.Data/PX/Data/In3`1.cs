// Decompiled with JetBrains decompiler
// Type: PX.Data.In3`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class In3<Constants> : InBase3<Constants> where Constants : IBqlConstants, new()
{
  protected override void AppendExpression(ref SQLExpression exp, SQLExpression sequence)
  {
    exp = exp.In(sequence);
  }

  protected override bool Verify(object value, IEnumerable<object> sequence)
  {
    return EnumerableExtensions.IsIn<object>(value, sequence);
  }
}
