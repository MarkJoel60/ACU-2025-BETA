// Decompiled with JetBrains decompiler
// Type: PX.Data.PXActionCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace PX.Data;

/// <summary>Represents a collection of ordered actions.</summary>
/// <remarks>In this collection, you can access an action by the name of the PXAction member that defines the action.</remarks>
public class PXActionCollection : OrderedDictionary
{
  private readonly PXGraph _Parent;

  public PXActionCollection(PXGraph parent)
    : base((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
  {
    this._Parent = parent;
  }

  public PXAction this[string name]
  {
    get => !this.Contains((object) name) ? (PXAction) null : this[(object) name] as PXAction;
    set
    {
      if (this.Contains((object) name))
        this[(object) name] = (object) value;
      else
        base.Add((object) name, (object) value);
    }
  }

  public void Insert(string prevAction, string name, PXAction action)
  {
    this.Insert(prevAction, name, action, false);
  }

  public void Insert(string prevAction, string name, PXAction action, bool insertAfter)
  {
    int index = this.IndexOf(prevAction);
    if (index <= -1)
      return;
    if (insertAfter)
    {
      if (index >= this.Values.Count)
        this.Add((object) name, (object) action);
      else
        this.Insert(index + 1, (object) name, (object) action);
    }
    else
      this.Insert(index, (object) name, (object) action);
  }

  public void Move(string prevAction, string name) => this.Move(prevAction, name, false);

  public void Move(string prevAction, string name, bool moveAfter)
  {
    PXAction pxAction = this[name];
    if (pxAction == null)
      return;
    int num1 = this.IndexOf(prevAction);
    if (num1 <= -1)
      return;
    this.Remove((object) name);
    int num2 = this.IndexOf(prevAction);
    int index = num2 > -1 ? num2 : num1;
    if (moveAfter)
    {
      if (index >= this.Values.Count)
        this.Add((object) name, (object) pxAction);
      else
        this.Insert(index + 1, (object) name, (object) pxAction);
    }
    else
      this.Insert(index, (object) name, (object) pxAction);
  }

  private int IndexOf(string name)
  {
    int num = 0;
    foreach (object key in (IEnumerable) this.Keys)
    {
      if (string.Compare(key as string, name, true) == 0)
        return num;
      ++num;
    }
    return -1;
  }

  public new virtual void Add(object key, object value) => this[key] = value;

  /// <summary>
  /// Calls the Save action to save changes in the database.
  /// </summary>
  /// <remarks>
  /// We do not recommended that you use this method in a long-running workflow action.
  /// Instead, use the <tt>Save.Press()</tt> method of the current graph.
  /// </remarks>
  public virtual void PressSave() => this.PressSave((PXAction) null);

  internal virtual void PressSave(PXAction caller)
  {
    System.Type itemType = !string.IsNullOrEmpty(this._Parent.PrimaryView) ? this._Parent.Views[this._Parent.PrimaryView].GetItemType() : (System.Type) null;
    foreach (PXAction pxAction in new ArrayList(this.Values))
    {
      if (pxAction.GetState((object) null) is PXButtonState state && state.SpecialType == PXSpecialButtonType.Save && (itemType == (System.Type) null || state.ItemType == (System.Type) null || itemType == state.ItemType || itemType.IsSubclassOf(state.ItemType)))
      {
        if (pxAction != caller)
        {
          System.Type rowType = pxAction.GetRowType();
          BqlCommand bqlCommand;
          if (!string.IsNullOrEmpty(this._Parent.PrimaryView))
          {
            PXView view = this._Parent.Views[this._Parent.PrimaryView];
            if (view.GetItemType() == rowType || view.GetItemType().IsAssignableFrom(rowType))
              bqlCommand = view.BqlSelect;
            else
              bqlCommand = BqlCommand.CreateInstance(typeof (Select<>), rowType);
          }
          else
            bqlCommand = BqlCommand.CreateInstance(typeof (Select<>), rowType);
          PXGraph parent = this._Parent;
          BqlCommand command = bqlCommand;
          List<object> records;
          if (this._Parent.Caches[rowType].Current == null)
          {
            records = new List<object>();
          }
          else
          {
            records = new List<object>();
            records.Add(this._Parent.Caches[rowType].Current);
          }
          foreach (object obj in this._Parent.Actions[state.Name].Press(new PXAdapter((PXView) new PXView.Dummy(parent, command, records))
          {
            InternalCall = caller != null
          }))
            ;
        }
        else
          break;
      }
    }
    this._Parent.Persist();
  }

  public virtual void PressCancel() => this.PressCancel((PXAction) null);

  internal virtual void PressCancel(PXAction caller)
  {
    System.Type itemType = !string.IsNullOrEmpty(this._Parent.PrimaryView) ? this._Parent.Views[this._Parent.PrimaryView].GetItemType() : (System.Type) null;
    foreach (PXAction pxAction in new ArrayList(this.Values))
    {
      if (pxAction.GetState((object) null) is PXButtonState state && state.SpecialType == PXSpecialButtonType.Cancel && (itemType == (System.Type) null || state.ItemType == (System.Type) null || itemType == state.ItemType || itemType.IsSubclassOf(state.ItemType)))
      {
        if (pxAction == caller)
          break;
        pxAction.Press();
      }
    }
  }

  internal virtual IEnumerable PressCancel(PXAction caller, PXAdapter adapter)
  {
    System.Type itemType = !string.IsNullOrEmpty(this._Parent.PrimaryView) ? this._Parent.Views[this._Parent.PrimaryView].GetItemType() : (System.Type) null;
    foreach (PXAction pxAction in new ArrayList(this.Values))
    {
      if (pxAction.GetState((object) null) is PXButtonState state && state.SpecialType == PXSpecialButtonType.Cancel && pxAction.GetType().GetGenericTypeDefinition() != typeof (PXCancelClose<>) && (itemType == (System.Type) null || state.ItemType == (System.Type) null || itemType == state.ItemType || itemType.IsSubclassOf(state.ItemType)))
      {
        if (pxAction != caller)
          return pxAction.Press(adapter);
        break;
      }
    }
    return (IEnumerable) null;
  }
}
