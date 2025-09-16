// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BudgetLevelHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable enable
namespace PX.Objects.PM;

public static class BudgetLevelHelper
{
  public static bool ContainsItem(string budgetLevel) => budgetLevel == "I" || budgetLevel == "D";

  public static bool ContainsCostCode(string budgetLevel)
  {
    return budgetLevel == "C" || budgetLevel == "D";
  }
}
