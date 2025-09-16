// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.HashcodeCombiner
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public static class HashcodeCombiner
{
  /// <summary>
  /// Combines a sequence of hash codes into a single hash code using
  /// an algorithm that ensures a minimum number of collisions.
  /// </summary>
  public static int Combine(IEnumerable<int> hashCodes)
  {
    int num = 17;
    foreach (int hashCode in hashCodes)
      num = num * 31 /*0x1F*/ + hashCode;
    return num;
  }

  /// <summary>
  /// Combines the hash codes of a sequence of objects
  /// into a single hash code.
  /// </summary>
  /// <remarks>
  /// If any object passed is <c>null</c>, its hash code
  /// is considered to be zero.
  /// </remarks>
  public static int Combine(IEnumerable<object> objects)
  {
    return HashcodeCombiner.Combine(objects.Select<object, int>((Func<object, int>) (entity => entity == null ? 0 : entity.GetHashCode())));
  }
}
