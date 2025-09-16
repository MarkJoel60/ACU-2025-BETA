// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.DataRecordExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class DataRecordExtensions
{
  public static bool AreAllKeysFilled<TNode>(this TNode record, PXGraph graph) where TNode : class, IBqlTable, new()
  {
    return record.AreAllKeysFilled<TNode>(graph.Caches[typeof (TNode)]);
  }

  public static bool AreAllKeysFilled<TNode>(this TNode record, PXCache cache) where TNode : class, IBqlTable, new()
  {
    return ((IEnumerable<string>) cache.Keys).All<string>((Func<string, bool>) (fieldName => cache.GetValue((object) (TNode) record, fieldName) != null));
  }
}
