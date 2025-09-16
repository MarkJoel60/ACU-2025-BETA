// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.LevelHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PM.Project.Overview;

public static class LevelHelper
{
  public const string Normal = "normal";
  public const string Warning = "warning";
  public const string Error = "error";

  public static string NormalNonConditional() => "normal";

  public static string NormalIfPositive(Decimal amount) => amount > 0M ? "normal" : "error";

  public static string NormalIfNonNegative(Decimal amount) => amount >= 0M ? "normal" : "error";

  public static string NormalIfZero(Decimal amount) => amount == 0M ? "normal" : "error";

  public static string NormalIfLessThen(Decimal amount, Decimal value)
  {
    return amount < value ? "normal" : "error";
  }

  public static string WarningIfNotZero(Decimal amount) => amount == 0M ? "normal" : "warning";
}
