// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCancel`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXCancel<TNode, TField> : PXCancel<TNode>
  where TNode : class, IBqlTable, new()
  where TField : class, IBqlField
{
  private string _fieldName;
  private System.Type _fieldType;

  public PXCancel(PXGraph graph, string name)
    : base(graph, name)
  {
    this.Initialize(graph);
  }

  public PXCancel(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.Initialize(graph);
  }

  private void Initialize(PXGraph graph)
  {
    this._fieldName = graph.Caches[typeof (TNode)].GetField(typeof (TField));
    this._fieldType = graph.Caches[typeof (TNode)].GetFieldType(this._fieldName);
    if (this._fieldType != typeof (string))
      throw new PXException("Invalid type of the generic parameter TField");
  }

  public override IEnumerable Press(PXAdapter adapter)
  {
    if (this._Graph.IsImport && adapter.MaximumRows == 1 && adapter.SortColumns != null && adapter.Searches != null)
    {
      bool flag = false;
      for (int index = 0; index < adapter.SortColumns.Length && !flag; ++index)
      {
        int num1 = adapter.Searches.Length <= index ? 0 : (adapter.Searches[index] != null ? 1 : 0);
        string sortColumn = adapter.SortColumns[index];
        int num2 = adapter.View.Cache.Keys.Contains(sortColumn) ? 1 : 0;
        if ((num1 & num2) != 0)
        {
          System.Type fieldType = adapter.View.Cache.GetFieldType(sortColumn);
          if (adapter.Searches[index].GetType() == typeof (string) && fieldType.IsAssignableFrom(typeof (int)) && !int.TryParse(adapter.Searches[index] as string, NumberStyles.Number, (IFormatProvider) CultureInfo.InvariantCulture, out int _))
          {
            adapter.SortColumns[index] = this._fieldName;
            flag = true;
          }
        }
      }
    }
    return base.Press(adapter);
  }
}
