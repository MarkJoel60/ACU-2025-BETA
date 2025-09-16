// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.OptimizedExportScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Api.Export;

public class OptimizedExportScope : IDisposable
{
  private Dictionary<System.Type, HashSet<string>> calculatedInSubselectExpressions = new Dictionary<System.Type, HashSet<string>>();
  private readonly OptimizedExportScope _parentScope;
  private (string Name, bool IsDetail) _currentView;
  internal bool AddExpressionToParent = true;
  private readonly bool _isScoped;

  private bool AddExpression(System.Type cacheBqlTable, string field)
  {
    HashSet<string> stringSet;
    if (!this.calculatedInSubselectExpressions.TryGetValue(cacheBqlTable, out stringSet) || stringSet == null)
    {
      stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this.calculatedInSubselectExpressions[cacheBqlTable] = stringSet;
    }
    int num = stringSet.Add(field) ? 1 : 0;
    if (!this.AddExpressionToParent)
      return num != 0;
    OptimizedExportScope parentScope = this._parentScope;
    if (parentScope == null)
      return num != 0;
    parentScope.AddExpression(cacheBqlTable, field);
    return num != 0;
  }

  internal static void CleanupExpressions()
  {
    OptimizedExportScope slot = PXContext.GetSlot<OptimizedExportScope>();
    if (slot == null)
      return;
    slot.calculatedInSubselectExpressions = new Dictionary<System.Type, HashSet<string>>();
  }

  internal static SQLExpression GetProjectionExpression(
    PXCache cache,
    PXCommandPreparingEventArgs.FieldDescription description,
    string field,
    System.Type alias)
  {
    OptimizedExportScope slot = PXContext.GetSlot<OptimizedExportScope>();
    if (slot == null)
      return (SQLExpression) null;
    System.Type itemType = cache.GetItemType();
    return !slot.AddExpression(itemType, field) ? (SQLExpression) new Column(field, alias) : description.Expr;
  }

  internal static bool HasExpression(PXCache cache, string field)
  {
    OptimizedExportScope slot = PXContext.GetSlot<OptimizedExportScope>();
    if (slot == null)
      return false;
    System.Type itemType = cache.GetItemType();
    HashSet<string> stringSet;
    return slot.calculatedInSubselectExpressions.TryGetValue(itemType, out stringSet) && stringSet != null && stringSet.Contains(field);
  }

  internal static (string Name, bool IsDetail) GetCurrentView()
  {
    OptimizedExportScope slot = PXContext.GetSlot<OptimizedExportScope>();
    return slot == null ? ((string) null, false) : slot._currentView;
  }

  internal static void SetCurrentView(string view, bool isDetail = false)
  {
    OptimizedExportScope slot = PXContext.GetSlot<OptimizedExportScope>();
    if (slot == null)
      return;
    slot._currentView = (view, isDetail);
  }

  internal static void UsingViewName(string viewName, System.Action action, bool isDetail = false)
  {
    OptimizedExportScope optimizedExportScope = (OptimizedExportScope) null;
    if (!OptimizedExportScope.IsScoped)
      optimizedExportScope = new OptimizedExportScope();
    OptimizedExportScope.SetCurrentView(viewName, isDetail);
    try
    {
      action();
    }
    finally
    {
      OptimizedExportScope.SetCurrentView((string) null);
      optimizedExportScope?.Dispose();
    }
  }

  public static OptimizedExportScope CreateChildScopeIfScoped()
  {
    if (!OptimizedExportScope.IsScoped)
      return (OptimizedExportScope) null;
    (string str, bool flag) = OptimizedExportScope.GetCurrentView();
    OptimizedExportScope childScopeIfScoped = new OptimizedExportScope();
    if (str == null)
      return childScopeIfScoped;
    OptimizedExportScope.SetCurrentView(str, flag);
    return childScopeIfScoped;
  }

  public static bool IsScoped
  {
    get
    {
      OptimizedExportScope slot = PXContext.GetSlot<OptimizedExportScope>();
      return slot != null && slot._isScoped;
    }
  }

  public OptimizedExportScope()
  {
    OptimizedExportScope slot = PXContext.GetSlot<OptimizedExportScope>();
    this._isScoped = true;
    this._parentScope = slot;
    PXContext.SetSlot<OptimizedExportScope>(this);
  }

  public void Dispose() => PXContext.SetSlot<OptimizedExportScope>(this._parentScope);
}
