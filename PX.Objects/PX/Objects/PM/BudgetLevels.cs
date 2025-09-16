// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BudgetLevels
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class BudgetLevels
{
  public const string Task = "T";
  public const string Item = "I";
  public const string CostCode = "C";
  public const string Detail = "D";
  public static string[] BudgetLevelsWithItem = new string[2]
  {
    "I",
    "D"
  };
  public static string[] BudgetLevelsWithCostCode = new string[2]
  {
    "C",
    "D"
  };
}
