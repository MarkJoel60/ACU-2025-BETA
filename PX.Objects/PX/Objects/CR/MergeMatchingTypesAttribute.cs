// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MergeMatchingTypesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
[Obsolete]
public class MergeMatchingTypesAttribute : PXIntListAttribute
{
  public const int _EQUALS_TO = 0;
  public const int _LIKE = 1;
  public const int _THE_SAME = 2;
  public const int _GREATER_THAN = 3;
  public const int _LESS_THAN = 4;
  private static readonly int[] _commonValues = new int[3]
  {
    0,
    1,
    2
  };
  private static readonly string[] _commonLabels = new string[3]
  {
    "Equals To",
    "Like",
    "The Same"
  };
  private static readonly int[] _comparableValues = new int[5]
  {
    0,
    1,
    2,
    3,
    4
  };
  private static readonly string[] _comparableLabels = new string[5]
  {
    "Equals To",
    "Like",
    "The Same",
    "Greater Than",
    "Less Than"
  };

  [Obsolete]
  static MergeMatchingTypesAttribute()
  {
  }

  [Obsolete]
  public MergeMatchingTypesAttribute()
    : base(MergeMatchingTypesAttribute.CommonValues, MergeMatchingTypesAttribute.CommonLabels)
  {
  }

  public static int[] CommonValues => MergeMatchingTypesAttribute._commonValues;

  public static string[] CommonLabels => MergeMatchingTypesAttribute._commonLabels;

  public static int[] ComparableValues => MergeMatchingTypesAttribute._comparableValues;

  public static string[] ComparableLabels => MergeMatchingTypesAttribute._comparableLabels;
}
