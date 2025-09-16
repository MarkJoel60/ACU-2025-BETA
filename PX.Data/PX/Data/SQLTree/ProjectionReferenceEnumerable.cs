// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionReferenceEnumerable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionReferenceEnumerable : ProjectionItem
{
  private readonly ProjectionReference _wrappedProjection;

  public ProjectionReferenceEnumerable(System.Type type)
  {
    this.type_ = type.GetGenericArguments()[0];
    this._wrappedProjection = new ProjectionReference(this.type_);
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    IList instance = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(this.type_));
    instance.Add(this._wrappedProjection.GetValue(data, ref position, context));
    return (object) instance;
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    IList instance = (IList) Activator.CreateInstance(typeof (List<>).MakeGenericType(this.type_));
    foreach (object obj in (IEnumerable) value)
      instance.Add(this._wrappedProjection.CloneValue(obj, context));
    return (object) instance;
  }

  internal override object Transform(
    object value,
    Func<System.Type, object, bool> predicate,
    Func<object, object> map)
  {
    IList list = (IList) base.Transform(value, predicate, map);
    for (int index = 0; index < list.Count; ++index)
      list[index] = this._wrappedProjection.Transform(list[index], predicate, map);
    return (object) list;
  }

  public override string ToString() => $"IEnumerable<{this.type_.Name}>";
}
