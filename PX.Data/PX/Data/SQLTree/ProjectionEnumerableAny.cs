// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionEnumerableAny
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionEnumerableAny : ProjectionConst
{
  private ProjectionItem _parentProjection;

  public ProjectionEnumerableAny(ProjectionItem parentProjection)
    : base(ProjectionEnumerableAny.GetParentResultType(parentProjection))
  {
    this._parentProjection = parentProjection;
  }

  public override string ToString() => "Any";

  private static System.Type GetParentResultType(ProjectionItem parentProjection)
  {
    switch (parentProjection)
    {
      case ProjectionPXResult projectionPxResult:
        return ((IEnumerable<System.Type>) projectionPxResult.GetResultTypes()).First<System.Type>();
      case ProjectionReference _:
      case ProjectionReferenceEnumerable _:
        if (typeof (IBqlTable).IsAssignableFrom(parentProjection.GetResultType()))
          return parentProjection.GetResultType();
        break;
    }
    throw new PXNotSupportedException("ProjectionItem of type {0} not supported", new object[1]
    {
      (object) parentProjection?.GetType()
    });
  }

  public override IEnumerable<object> GetEmptyResult()
  {
    return (IEnumerable<object>) new object[1]
    {
      (object) false
    };
  }

  internal static bool CouldBeProjectionOf(ProjectionItem parentProjection)
  {
    switch (parentProjection)
    {
      case ProjectionPXResult _:
      case ProjectionReference _:
        return true;
      default:
        return parentProjection is ProjectionReferenceEnumerable;
    }
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    if (this._parentProjection is ProjectionPXResult parentProjection)
    {
      object obj = parentProjection.GetValue(data, ref position, context);
      return obj is PXResult pxResult ? pxResult[this.type_] : obj;
    }
    return (this._parentProjection is ProjectionReference || this._parentProjection is ProjectionReferenceEnumerable) && typeof (IBqlTable).IsAssignableFrom(this._parentProjection.GetResultType()) ? this._parentProjection.GetValue(data, ref position, context) : (object) true;
  }

  protected override object CloneValueInternal(object value, CloneContext context)
  {
    if (this._parentProjection is ProjectionPXResult parentProjection)
      return parentProjection.CloneValue(value, context);
    return (this._parentProjection is ProjectionReference || this._parentProjection is ProjectionReferenceEnumerable) && typeof (IBqlTable).IsAssignableFrom(this._parentProjection.GetResultType()) ? this._parentProjection.CloneValue(value, context) : value;
  }
}
