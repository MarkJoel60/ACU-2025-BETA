// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MergeMethodsAttribute
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
public class MergeMethodsAttribute : PXIntListAttribute
{
  public const int _FIRST = 0;
  public const int _SELECT = 1;
  public const int _CONCAT = 2;
  public const int _SUM = 3;
  public const int _MAX = 4;
  public const int _MIN = 5;
  public const int _COUNT = 6;
  private const string _FIRST_LABEL = "First";
  private const string _SELECT_LABEL = "Select";
  private const string _CONCAT_LABEL = "Concat";
  private const string _SUM_LABEL = "Sum";
  private const string _MAX_LABEL = "Max";
  private const string _MIN_LABEL = "Min";
  private const string _COUNT_LABEL = "Count";
  private static readonly int[] _NUMBER_VALUES = new int[6]
  {
    0,
    1,
    3,
    4,
    5,
    6
  };
  private static readonly string[] _NUMBER_LABELS = new string[6]
  {
    "First",
    "Select",
    "Sum",
    "Max",
    "Min",
    "Count"
  };
  private static readonly int[] _STRING_VALUES = new int[3]
  {
    0,
    1,
    2
  };
  private static readonly string[] _STRING_LABELS = new string[3]
  {
    "First",
    "Select",
    "Concat"
  };
  private static readonly int[] _DATE_VALUES = new int[4]
  {
    0,
    1,
    4,
    5
  };
  private static readonly string[] _DATE_LABELS = new string[4]
  {
    "First",
    "Select",
    "Max",
    "Min"
  };
  private static readonly int[] _COMMON_VALUES = new int[2]
  {
    0,
    1
  };
  private static readonly string[] _COMMON_LABELS = new string[2]
  {
    "First",
    "Select"
  };

  [Obsolete]
  public MergeMethodsAttribute()
    : base(new int[7]{ 0, 1, 2, 3, 4, 5, 6 }, new string[7]
    {
      "First",
      "Select",
      "Concat",
      "Sum",
      "Max",
      "Min",
      "Count"
    })
  {
  }

  public static void SetNumberList<TField>(PXCache cache, object row) where TField : IBqlField
  {
    PXIntListAttribute.SetList<TField>(cache, row, MergeMethodsAttribute._NUMBER_VALUES, MergeMethodsAttribute._NUMBER_LABELS);
  }

  public static void SetStringList<TField>(PXCache cache, object row) where TField : IBqlField
  {
    PXIntListAttribute.SetList<TField>(cache, row, MergeMethodsAttribute._STRING_VALUES, MergeMethodsAttribute._STRING_LABELS);
  }

  public static void SetDateList<TField>(PXCache cache, object row) where TField : IBqlField
  {
    PXIntListAttribute.SetList<TField>(cache, row, MergeMethodsAttribute._DATE_VALUES, MergeMethodsAttribute._DATE_LABELS);
  }

  public static void SetCommonList<TField>(PXCache cache, object row) where TField : IBqlField
  {
    PXIntListAttribute.SetList<TField>(cache, row, MergeMethodsAttribute._COMMON_VALUES, MergeMethodsAttribute._COMMON_LABELS);
  }
}
