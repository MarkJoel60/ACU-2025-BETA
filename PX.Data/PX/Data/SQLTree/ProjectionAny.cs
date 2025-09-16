// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionAny
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionAny : ProjectionConst
{
  public ProjectionAny()
    : base(typeof (bool))
  {
  }

  public override string ToString() => "Any";

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    return (object) true;
  }

  public override IEnumerable<object> GetEmptyResult()
  {
    return (IEnumerable<object>) new object[1]
    {
      (object) false
    };
  }

  protected override object CloneValueInternal(object value, CloneContext context) => value;
}
