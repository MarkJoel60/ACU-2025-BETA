// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQL
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.SQLTree;

public static class SQL
{
  private const string InvalidOperationExceptionMessage = "This method should be never invoked. It is used for LINQ parsing only.";

  public static bool Like<T>(T expression, string pattern)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static T Abs<T>(T p)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static int Len<T>(T p)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static int BinaryLen<T>(T p)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  /// <summary>SQL-specific startIndex behavior (starts with 1)!</summary>
  /// <param name="startIndex">Starts with 1</param>
  public static string Substring<T>(T exp, int startIndex)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  /// <summary>SQL-specific startIndex behavior (starts with 1)!</summary>
  /// <param name="startIndex">Starts with 1</param>
  public static string Substring<T>(T exp, int startIndex, int length)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static T Round<T>(T exp, int precision)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string TrimEnd<T>(T exp)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string TrimStart<T>(T exp)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Trim<T>(T exp)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string NullIf<T>(T exp, T other)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Coalesce<T>(T exp, T replacement)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static bool Between<T0, T1>(T0 exp, T1 min, T1 max)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static bool NotBetween<T0, T1>(T0 exp, T1 min, T1 max)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1>(T0 p0, T1 p1)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2>(T0 p0, T1 p1, T2 p2)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2, T3>(T0 p0, T1 p1, T2 p2, T3 p3)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2, T3, T4>(T0 p0, T1 p1, T2 p2, T3 p3, T4 p4)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2, T3, T4, T5>(T0 p0, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2, T3, T4, T5, T6>(
    T0 p0,
    T1 p1,
    T2 p2,
    T3 p3,
    T4 p4,
    T5 p5,
    T6 p6)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2, T3, T4, T5, T6, T7>(
    T0 p0,
    T1 p1,
    T2 p2,
    T3 p3,
    T4 p4,
    T5 p5,
    T6 p6,
    T7 p7)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
    T0 p0,
    T1 p1,
    T2 p2,
    T3 p3,
    T4 p4,
    T5 p5,
    T6 p6,
    T7 p7,
    T8 p8)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  public static string Concat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    T0 p0,
    T1 p1,
    T2 p2,
    T3 p3,
    T4 p4,
    T5 p5,
    T6 p6,
    T7 p7,
    T8 p8,
    T9 p9)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }

  /// <summary>
  /// This method allows to refer property that is not declared in DAC. For LINQ use only.
  /// </summary>
  public static TPropertyType GetExtensionProperty<TPropertyType>(
    IBqlTable dac,
    string propertyName)
  {
    throw new InvalidOperationException("This method should be never invoked. It is used for LINQ parsing only.");
  }
}
