// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal abstract class ProjectionItem
{
  protected internal System.Type type_;

  public virtual System.Type GetResultType() => this.type_;

  public abstract override string ToString();

  internal abstract object GetValue(PXDataRecord data, ref int position, MergeCacheContext context);

  public object CloneValue(object value, CloneContext context)
  {
    object clone;
    return context != null && context.CustomHandler(value, out clone) ? clone : this.CloneValueInternal(value, context);
  }

  protected abstract object CloneValueInternal(object value, CloneContext context);

  public virtual IEnumerable<object> GetEmptyResult() => Enumerable.Empty<object>();

  internal virtual object Transform(
    object value,
    Func<System.Type, object, bool> predicate,
    Func<object, object> map)
  {
    return predicate(this.type_, value) ? map(value) : value;
  }
}
