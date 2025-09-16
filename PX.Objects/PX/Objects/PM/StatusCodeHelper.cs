// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.StatusCodeHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public static class StatusCodeHelper
{
  public static bool IsValidStatus(
    int? statusCodeValue,
    out string statusDescription,
    out PXErrorLevel errorLevel)
  {
    statusDescription = string.Empty;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ref errorLevel = 0;
    if (!statusCodeValue.HasValue)
      return true;
    StatusCodes valueOrDefault = (StatusCodes) statusCodeValue.GetValueOrDefault();
    if (valueOrDefault == StatusCodes.Valid)
      return true;
    List<string> stringList = new List<string>();
    if (StatusCodeHelper.CheckStatus(valueOrDefault, StatusCodes.InclusiveTaxesInRevenueBudgetIntroduced))
      stringList.Add("The revenue project balances now include the applicable inclusive taxes. To make sure that the project reports include correct tax information, recalculate the project balance by using the Recalculate Project Balance command in the More menu.");
    if (StatusCodeHelper.CheckStatus(valueOrDefault, StatusCodes.DateSensitiveActualsIntroduced))
      stringList.Add("To make sure that the Cost Projection by Date (PM305500) and Project Financial Vision (PM405000) forms show accurate data, recalculate the project balances on the Recalculate Project Balances (PM504000) form.");
    if (StatusCodeHelper.CheckStatus(valueOrDefault, StatusCodes.ProjectBudgetHistoryIntroduced))
      stringList.Add("To make sure that the Project Profitability (PM658100) and Project Variance (PM651020) inquiries show accurate data, recalculate the project balances on the Recalculate Project Balances (PM504000) form with the Recalculate Project Budget History checkbox selected.");
    statusDescription = Str.Join((IEnumerable<string>) stringList, " ");
    if (string.IsNullOrWhiteSpace(statusDescription))
      return true;
    if (StatusCodeHelper.CheckStatus(valueOrDefault, StatusCodes.Warning))
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref errorLevel = 3;
    }
    else if (StatusCodeHelper.CheckStatus(valueOrDefault, StatusCodes.Error))
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref errorLevel = 5;
    }
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    return ^(int&) ref errorLevel == 0;
  }

  public static bool CheckStatus(int? statusCodeValue, StatusCodes statusCodeToCheck)
  {
    return statusCodeValue.HasValue && StatusCodeHelper.CheckStatus((StatusCodes) statusCodeValue.GetValueOrDefault(), statusCodeToCheck);
  }

  private static bool CheckStatus(StatusCodes statusCode, StatusCodes statusCodeToCheck)
  {
    return (statusCode & statusCodeToCheck) == statusCodeToCheck;
  }

  public static int? ResetStatus(int? statusCodeValue, StatusCodes statusCodeToReset)
  {
    if (!statusCodeValue.HasValue)
      return new int?();
    StatusCodes statusCodes = StatusCodeHelper.ResetStatus((StatusCodes) statusCodeValue.GetValueOrDefault(), statusCodeToReset);
    switch (statusCodes)
    {
      case StatusCodes.Warning:
      case StatusCodes.Error:
        statusCodes = StatusCodes.Valid;
        break;
    }
    return new int?((int) statusCodes);
  }

  public static int? AppendStatus(int? statusCodeValue, StatusCodes statusCodeToAppend)
  {
    return new int?((int) StatusCodeHelper.AppendStatus((StatusCodes) statusCodeValue.GetValueOrDefault(), statusCodeToAppend));
  }

  private static StatusCodes AppendStatus(
    StatusCodes currentStatusCode,
    StatusCodes statusCodeToAppend)
  {
    return currentStatusCode | statusCodeToAppend;
  }

  private static StatusCodes ResetStatus(
    StatusCodes currentStatusCode,
    StatusCodes statusCodeToReset)
  {
    return currentStatusCode & ~statusCodeToReset;
  }
}
