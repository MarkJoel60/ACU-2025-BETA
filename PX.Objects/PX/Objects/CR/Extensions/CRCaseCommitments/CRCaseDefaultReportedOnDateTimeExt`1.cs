// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCaseCommitments.CRCaseDefaultReportedOnDateTimeExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCaseCommitments;

/// <summary>
/// The graph extension that calculates <see cref="P:PX.Objects.CR.CRCase.ReportedOnDateTime" /> on the <see cref="P:PX.Data.PXGraph.RowPersisting" /> event.
/// </summary>
/// <typeparam name="TGraph">The base graph</typeparam>
public abstract class CRCaseDefaultReportedOnDateTimeExt<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  /// <exclude />
  public virtual void DefaultReportedOnDateTimeIfNeeded(CRCase row)
  {
    CRCase crCase = row;
    DateTime? reportedOnDateTime = crCase.ReportedOnDateTime;
    reportedOnDateTime.GetValueOrDefault();
    if (reportedOnDateTime.HasValue)
      return;
    DateTime now = PXTimeZoneInfo.Now;
    crCase.ReportedOnDateTime = new DateTime?(now);
  }

  protected virtual void _(Events.RowPersisting<CRCase> e)
  {
    CRCase row = e.Row;
    if (row == null || e.Operation == 3)
      return;
    this.DefaultReportedOnDateTimeIfNeeded(row);
    ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRCase>>) e).Cache.Update((object) row);
  }
}
