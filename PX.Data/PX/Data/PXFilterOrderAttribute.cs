// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilterOrderAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXFilterOrderAttribute : PXDBLongAttribute, IPXRowPersistingSubscriber
{
  public void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || ((long?) sender.GetValue<FilterHeader.filterOrder>(e.Row)).HasValue)
      return;
    string viewName = (string) sender.GetValue<FilterHeader.viewName>(e.Row);
    string screenId = (string) sender.GetValue<FilterHeader.screenID>(e.Row);
    IQueryable<FilterHeader> source = sender.Graph.Select<FilterHeader>();
    if (!string.IsNullOrEmpty(viewName))
      source = source.Where<FilterHeader>((Expression<Func<FilterHeader, bool>>) (f => f.ViewName == viewName));
    if (!string.IsNullOrEmpty(screenId))
      source = source.Where<FilterHeader>((Expression<Func<FilterHeader, bool>>) (f => f.ScreenID == screenId));
    long num = source.Max<FilterHeader, long?>((Expression<Func<FilterHeader, long?>>) (f => f.FilterOrder)) ?? 1L;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) (num + 1L));
  }
}
