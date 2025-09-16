// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionFabricMethod
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

/// <summary>Encapsulates method that creates object</summary>
internal class ProjectionFabricMethod : ProjectionItem
{
  private readonly MethodInfo _method;
  private readonly ProjectionItem[] _elements;

  public ProjectionFabricMethod(MethodInfo method, int size)
  {
    this.type_ = (System.Type) null;
    this._method = method;
    this._elements = new ProjectionItem[size];
  }

  public void SetElement(int idx, ProjectionItem item) => this._elements[idx] = item;

  public ProjectionItem GetElement(int idx) => this._elements[idx];

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    object[] parameters = new object[this._elements.Length];
    for (int index = 0; index < this._elements.Length; ++index)
      parameters[index] = this._elements[index].GetValue(data, ref position, context);
    return this._method.Invoke((object) null, parameters);
  }

  protected override object CloneValueInternal(object value, CloneContext context) => value;

  public override string ToString()
  {
    return $"{this._method.Name}({string.Join<ProjectionItem>(", ", ((IEnumerable<ProjectionItem>) this._elements).AsEnumerable<ProjectionItem>())})";
  }
}
