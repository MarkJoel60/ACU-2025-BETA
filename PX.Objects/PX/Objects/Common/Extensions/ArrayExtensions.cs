// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.ArrayExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class ArrayExtensions
{
  public static T?[] SparseArrayAddDistinct<T>(this T?[] sparseArray, T? item) where T : struct
  {
    if (sparseArray == null)
      throw new ArgumentNullException(nameof (sparseArray));
    if (!item.HasValue)
      throw new ArgumentNullException(nameof (item));
    int index1 = -1;
    for (int index2 = 0; index2 < sparseArray.Length; ++index2)
    {
      if (object.Equals((object) sparseArray[index2], (object) item))
        return sparseArray;
      if (!sparseArray[index2].HasValue && index1 == -1)
        index1 = index2;
    }
    if (index1 != -1)
    {
      sparseArray[index1] = item;
      return sparseArray;
    }
    T?[] nullableArray = new T?[sparseArray.Length == 0 ? 4 : sparseArray.Length * 2];
    sparseArray.CopyTo((Array) nullableArray, 0);
    nullableArray[sparseArray.Length] = item;
    return nullableArray;
  }

  public static void SparseArrayRemove<T>(this T?[] sparseArray, T? item) where T : struct
  {
    if (sparseArray == null)
      throw new ArgumentNullException(nameof (sparseArray));
    if (!item.HasValue)
      throw new ArgumentNullException(nameof (item));
    for (int index = 0; index < sparseArray.Length; ++index)
    {
      if (object.Equals((object) sparseArray[index], (object) item))
      {
        sparseArray[index] = new T?();
        break;
      }
    }
  }

  public static void SparseArrayClear<T>(this T?[] sparseArray) where T : struct
  {
    if (sparseArray == null)
      throw new ArgumentNullException(nameof (sparseArray));
    for (int index = 0; index < sparseArray.Length; ++index)
      sparseArray[index] = new T?();
  }

  public static T?[] SparseArrayCopy<T>(this T?[] sparseArray) where T : struct
  {
    if (sparseArray == null)
      throw new ArgumentNullException(nameof (sparseArray));
    int index1 = 0;
    T?[] array = new T?[sparseArray.Length];
    for (int index2 = 0; index2 < sparseArray.Length; ++index2)
    {
      if (sparseArray[index2].HasValue)
      {
        array[index1] = sparseArray[index2];
        ++index1;
      }
    }
    Array.Resize<T?>(ref array, index1 + 1);
    return array;
  }
}
