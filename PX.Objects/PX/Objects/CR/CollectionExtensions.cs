// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CollectionExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

internal static class CollectionExtensions
{
  public static void Add<T>(this ICollection<T> col, IEnumerable<T> addPart)
  {
    EnumerableExtensions.ForEach<T>(addPart, new Action<T>(col.Add));
  }
}
