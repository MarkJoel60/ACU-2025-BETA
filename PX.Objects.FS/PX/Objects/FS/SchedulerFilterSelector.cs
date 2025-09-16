// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerFilterSelector
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

internal static class SchedulerFilterSelector
{
  public static PXFilterRow[] ForDACs(
    this PXView.PXFilterRowCollection filters,
    string[] dacNames,
    bool includeMainDAC = false)
  {
    return SchedulerFilterSelector.AccountForDACs(PXView.PXFilterRowCollection.op_Implicit(filters), dacNames, includeMainDAC);
  }

  public static PXFilterRow[] ForDACs(
    this PXFilterRow[] filters,
    string[] dacNames,
    bool includeMainDAC = false)
  {
    return SchedulerFilterSelector.AccountForDACs(filters, dacNames, includeMainDAC);
  }

  public static PXFilterRow[] ExceptForDACs(
    this PXFilterRow[] filters,
    string[] dacNames,
    bool exceptMainDAC = false)
  {
    return SchedulerFilterSelector.AccountForDACs(filters, dacNames, exceptMainDAC, false);
  }

  private static PXFilterRow[] AccountForDACs(
    PXFilterRow[] filters,
    string[] dacNames,
    bool accountForMainDAC = false,
    bool shouldInclude = true)
  {
    return ((IEnumerable<PXFilterRow>) filters).Where<PXFilterRow>((Func<PXFilterRow, bool>) (x =>
    {
      foreach (string dacName in dacNames)
      {
        if (x.DataField.StartsWith(dacName + "__"))
          return shouldInclude;
      }
      return !x.DataField.Contains("__") & accountForMainDAC ? shouldInclude : !shouldInclude;
    })).ToArray<PXFilterRow>();
  }
}
