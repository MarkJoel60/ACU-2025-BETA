// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRDropDownAutoValueAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
[Obsolete]
public sealed class CRDropDownAutoValueAttribute : PXEventSubscriberAttribute
{
  private readonly System.Type _refField;
  private int _refFieldOrdinal;
  private BqlCommand _bqlCommand;

  [Obsolete]
  public CRDropDownAutoValueAttribute(System.Type dependsOnField)
  {
    if (dependsOnField == (System.Type) null)
      throw new ArgumentNullException(nameof (dependsOnField));
    this._refField = typeof (IBqlField).IsAssignableFrom(dependsOnField) ? dependsOnField : throw new ArgumentException($"'{MainTools.GetLongName(typeof (IBqlField))}' is expected.");
  }

  public bool CheckOnInsert { get; set; }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._refFieldOrdinal = sender.GetFieldOrdinal(sender.GetField(this._refField));
    if (this.CheckOnInsert)
    {
      // ISSUE: method pointer
      sender.Graph.RowInserting.AddHandler(this._refField.DeclaringType, new PXRowInserting((object) this, __methodptr(RowInsertingHandler)));
    }
    // ISSUE: method pointer
    sender.Graph.RowUpdating.AddHandler(this._refField.DeclaringType, new PXRowUpdating((object) this, __methodptr(RowUpdatingHandler)));
    this._bqlCommand = BqlCommand.CreateInstance(new System.Type[2]
    {
      typeof (Select<>),
      this._BqlTable
    });
  }

  private void RowInsertingHandler(PXCache sender, PXRowInsertingEventArgs e)
  {
    object obj = sender.GetValue(e.Row, this._FieldOrdinal);
    PXGraph graph = sender.Graph;
    if (graph.Views[graph.PrimaryView].GetItemType() != sender.GetItemType())
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) null);
    WorkflowExtensions.ApplyWorkflowState(graph, e.Row);
    PXStringState stateExt = sender.GetStateExt(e.Row, this._FieldName) as PXStringState;
    sender.SetValue(e.Row, this._FieldOrdinal, obj);
    if (stateExt != null)
    {
      string[] allowedValues = stateExt.AllowedValues;
      if (allowedValues == null || !string.IsNullOrEmpty(obj as string) && Array.IndexOf<string>(allowedValues, (string) obj) >= 0)
        return;
      sender.SetValue(e.Row, this._FieldOrdinal, (object) ((IEnumerable<string>) allowedValues).FirstOrDefault<string>());
    }
    else
    {
      if (obj == null)
        return;
      sender.SetValue(e.Row, this._FieldOrdinal, (object) null);
    }
  }

  private void RowUpdatingHandler(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (object.Equals(sender.GetValue(e.Row, this._refFieldOrdinal), sender.GetValue(e.NewRow, this._refFieldOrdinal)))
      return;
    PXGraph graph = sender.Graph;
    object obj = sender.GetValue(e.NewRow, this._FieldOrdinal);
    sender.SetValue(e.NewRow, this._FieldOrdinal, (object) null);
    object newRow = e.NewRow;
    WorkflowExtensions.ApplyWorkflowState(graph, newRow);
    PXStringState stateExt = sender.GetStateExt(e.NewRow, this._FieldName) as PXStringState;
    sender.SetValue(e.NewRow, this._FieldOrdinal, obj);
    if (stateExt != null)
    {
      string[] allowedValues = stateExt.AllowedValues;
      if (allowedValues == null || !string.IsNullOrEmpty(obj as string) && Array.IndexOf<string>(allowedValues, (string) obj) >= 0)
        return;
      sender.SetValue(e.NewRow, this._FieldOrdinal, (object) ((IEnumerable<string>) allowedValues).FirstOrDefault<string>());
    }
    else
    {
      if (sender.GetValue(e.NewRow, this._FieldOrdinal) != sender.GetValue(e.Row, this._FieldOrdinal))
        return;
      sender.SetValue(e.NewRow, this._FieldOrdinal, (object) null);
    }
  }
}
