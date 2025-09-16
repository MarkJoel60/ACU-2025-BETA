// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.StatusCodeDescriptions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.PM;

[PXLocalizable]
public static class StatusCodeDescriptions
{
  public const string InclusiveTaxesInRevenueBudgetIntroduced = "The revenue project balances now include the applicable inclusive taxes. To make sure that the project reports include correct tax information, recalculate the project balance by using the Recalculate Project Balance command in the More menu.";
  public const string DateSensitiveActualsIntroduced = "To make sure that the Cost Projection by Date (PM305500) and Project Financial Vision (PM405000) forms show accurate data, recalculate the project balances on the Recalculate Project Balances (PM504000) form.";
  public const string ProjectBudgetHistoryIntroduced = "To make sure that the Project Profitability (PM658100) and Project Variance (PM651020) inquiries show accurate data, recalculate the project balances on the Recalculate Project Balances (PM504000) form with the Recalculate Project Budget History checkbox selected.";
}
