// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBAction`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDBAction<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  private Delegate _InnerHandler;

  public PXDBAction(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this._Handler = (Delegate) new PXButtonDelegate(((PXAction<TNode>) this).Handler);
    this._InnerHandler = handler;
  }

  protected override IEnumerable Handler(PXAdapter adapter)
  {
    bool flag = false;
    if (this._Graph.IsDirty || adapter.View.Cache.Current == null || adapter.View.Cache.GetStatus(adapter.View.Cache.Current) == PXEntryStatus.Inserted)
    {
      if (this.Graph.IsImport || adapter.View.Ask(PXLocalizer.Localize("The changes need to be saved. Do you want to proceed?", typeof (ActionsMessages).ToString()), MessageButtons.YesNo) == WebDialogResult.Yes)
      {
        this._Graph.Actions.PressSave((PXAction) this);
        for (int index = 0; index < adapter.SortColumns.Length; ++index)
          adapter.Searches[index] = adapter.View.Cache.GetValue(adapter.View.Cache.Current, adapter.SortColumns[index]);
      }
      else
        flag = true;
    }
    if (!flag)
    {
      if (this._InnerHandler is PXButtonDelegate)
        return ((PXButtonDelegate) this._InnerHandler)(adapter);
      try
      {
        this._InnerHandler.DynamicInvoke();
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }
    return adapter.Get();
  }
}
