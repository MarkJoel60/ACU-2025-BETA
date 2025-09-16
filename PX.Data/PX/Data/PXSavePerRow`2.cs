// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSavePerRow`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSavePerRow<TNode, SurrogateKey> : PXSavePerRow<TNode>
  where TNode : class, IBqlTable, new()
  where SurrogateKey : IBqlField
{
  protected PXView _Select;
  protected PXSavePerRow<TNode, SurrogateKey>.KeyInterceptor _Cache;

  protected virtual void RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    object newValue = cache.GetValue(e.Row, cache.Keys[0]);
    if (this._Select == null)
      this._Select = new PXView(cache.Graph, false, BqlCommand.CreateInstance(typeof (Select<,>), cache.GetItemType(), typeof (Where<,>), cache.BqlKeys[0], typeof (Equal<>), typeof (Required<>), cache.BqlKeys[0]));
    if (newValue == null)
      return;
    if (this._Select.SelectSingle(newValue) == null)
      return;
    cache.RaiseExceptionHandling(cache.Keys[0], e.Row, newValue, (Exception) new PXException("The record already exists."));
    e.Cancel = true;
  }

  public PXSavePerRow(PXGraph graph, string name)
    : base(graph)
  {
    PXCache pxCache = (PXCache) null;
    if (graph.Caches.TryGetValue(typeof (TNode), out pxCache) && !typeof (PXSavePerRow<TNode, SurrogateKey>.KeyInterceptor).IsAssignableFrom(pxCache.GetType()))
      pxCache = (PXCache) null;
    if (pxCache == null)
    {
      this._Cache = new PXSavePerRow<TNode, SurrogateKey>.KeyInterceptor(graph);
      PXCache cache = (PXCache) this._Cache;
      for (System.Type type = typeof (TNode); type.IsBqlTable(); type = type.BaseType)
        graph.Caches[type] = cache;
      graph.RowInserting.AddHandler(typeof (TNode), new PXRowInserting(this.RowInserting));
    }
    else
      this._Cache = (PXSavePerRow<TNode, SurrogateKey>.KeyInterceptor) pxCache;
    this.SetHandler((Delegate) new PXButtonDelegate(((PXAction<TNode>) this).Handler), name);
    foreach (PXEventSubscriberAttribute attribute in graph.Caches[typeof (TNode)].GetAttributes((string) null))
    {
      if (attribute is IPXInterfaceField pxInterfaceField && (pxInterfaceField.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
      {
        this.fieldName = attribute.FieldName;
        break;
      }
    }
  }

  /// <exclude />
  protected class KeyInterceptor(PXGraph graph) : PXCache<TNode>(graph)
  {
    public override int Update(IDictionary keys, IDictionary values)
    {
      PXSavePerRow<TNode, SurrogateKey>.KeyInterceptor.bypassHandler bypassHandler = (PXSavePerRow<TNode, SurrogateKey>.KeyInterceptor.bypassHandler) null;
      object obj = (object) null;
      bool flag = false;
      try
      {
        if (this.Keys.Count > 0 && keys.Contains((object) this.Keys[0]))
        {
          obj = keys[(object) this.Keys[0]];
          if (obj != null)
          {
            bypassHandler = new PXSavePerRow<TNode, SurrogateKey>.KeyInterceptor.bypassHandler(obj);
            this.FieldVerifyingEvents[this.Keys[0].ToLower()] += new PXFieldVerifying(bypassHandler.bypassCheck);
          }
        }
        flag = this.Locate(keys) > 0;
      }
      finally
      {
        if (bypassHandler != null)
          this.FieldVerifyingEvents[this.Keys[0].ToLower()] -= new PXFieldVerifying(bypassHandler.bypassCheck);
      }
      if (values.Contains((object) PXCache.IsNewRow) & flag && this.GetStatus(this.Current) != PXEntryStatus.Inserted)
        return this.Insert(values);
      int num = -1;
      object objB = values[(object) this.Keys[0]];
      if (obj != null && objB != null && !object.Equals(obj, objB) && (!flag || this.GetStatus(this.Current) != PXEntryStatus.Inserted))
      {
        WebDialogResult webDialogResult = WebDialogResult.None;
        foreach (PXView pxView in new List<PXView>((IEnumerable<PXView>) this.Graph.Views.Values))
        {
          if (pxView.GetItemType() == typeof (TNode))
          {
            webDialogResult = pxView.Ask("Are you sure you want to change the key of this record?", MessageButtons.YesNo);
            break;
          }
        }
        if (webDialogResult == WebDialogResult.Yes)
        {
          try
          {
            switch (bypassHandler)
            {
              case null:
              case null:
                num = base.Update(keys, values);
                break;
              default:
                this.FieldVerifyingEvents[this.Keys[0].ToLower()] += new PXFieldVerifying(bypassHandler.bypassCheck);
                goto case null;
            }
          }
          finally
          {
            if (bypassHandler != null)
              this.FieldVerifyingEvents[this.Keys[0].ToLower()] -= new PXFieldVerifying(bypassHandler.bypassCheck);
          }
        }
        else
        {
          values[(object) this.Keys[0]] = keys[(object) this.Keys[0]];
          num = 1;
        }
      }
      if (num == -1)
      {
        if (!values.Contains((object) PXImportAttribute.ImportFlag) && this.GetValue((object) values, typeof (SurrogateKey).Name) == null)
        {
          num = this.Insert(values);
          if (num >= 0)
            keys[(object) this.Keys[0]] = values[(object) this.Keys[0]];
        }
        else
          num = base.Update(keys, values);
      }
      return num;
    }

    /// <exclude />
    private class bypassHandler
    {
      private object _oldKey;

      public bypassHandler(object oldKey) => this._oldKey = oldKey;

      public void bypassCheck(PXCache sender, PXFieldVerifyingEventArgs e)
      {
        if (!object.Equals(e.NewValue, this._oldKey) && (!(e.NewValue is string) || !(this._oldKey is string) || !string.Equals(((string) e.NewValue).TrimEnd(), ((string) this._oldKey).TrimEnd(), StringComparison.InvariantCultureIgnoreCase)))
          return;
        e.Cancel = true;
      }
    }
  }
}
