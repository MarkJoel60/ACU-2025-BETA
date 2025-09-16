// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRoleRightAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Property)]
public class PXRoleRightAttribute : PXIntListAttribute, IPXFieldUpdatingSubscriber
{
  private List<int> values = new List<int>();
  private List<string> labels = new List<string>();

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.values = new List<int>((IEnumerable<int>) ListRoleRight.GetIndexesByType("All"));
    this.labels = new List<string>((IEnumerable<string>) ListRoleRight.GetByType("All"));
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is Role row))
    {
      for (int index = 0; index < this.labels.Count; ++index)
        this.labels[index] = PXMessages.LocalizeNoPrefix(this.labels[index]);
      e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(false), new int?(1), new int?(), new int?(), this.values.ToArray(), this.labels.ToArray(), (System.Type) null, new int?());
    }
    else
    {
      if (e.ReturnState is PXIntState && ((PXIntState) e.ReturnState).AllowedValues != null)
        return;
      this.values.Clear();
      this.labels.Clear();
      this.SetValuesAndLabelsByIdentifier(this.GetIdentifier(row, sender.Graph));
      e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(false), new int?(1), new int?(), new int?(), this.values.ToArray(), this.labels.ToArray(), (System.Type) null, new int?());
    }
  }

  public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    string identifier = this.GetIdentifier((Role) e.Row, sender.Graph);
    if (!(e.NewValue is string newValue) || !newValue.Equals("Multiple Rights", StringComparison.OrdinalIgnoreCase) || !string.Equals(identifier, "Workspaces", StringComparison.OrdinalIgnoreCase))
      return;
    this.SetValuesAndLabelsByIdentifier("WorkspacesWithMultiple");
    e.NewValue = (object) 6;
  }

  protected virtual string GetIdentifier(Role row, PXGraph graph)
  {
    return ListRoleRight.GetRoleRightIdentifier(row, graph);
  }

  private void SetValuesAndLabelsByIdentifier(string identifier)
  {
    this.values = ListRoleRight.GetIndexesByType(identifier);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.labels = ListRoleRight.GetByType(identifier).Select<string, string>(PXRoleRightAttribute.\u003C\u003EO.\u003C0\u003E__LocalizeNoPrefix ?? (PXRoleRightAttribute.\u003C\u003EO.\u003C0\u003E__LocalizeNoPrefix = new Func<string, string>(PXMessages.LocalizeNoPrefix))).ToList<string>();
  }
}
