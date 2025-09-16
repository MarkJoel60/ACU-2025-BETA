// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionKvExtAttributes
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionKvExtAttributes : ProjectionConst
{
  private readonly System.Type _dac;
  private readonly string _propertyName;

  public ProjectionKvExtAttributes(System.Type dac, string propertyName)
    : base(typeof (string))
  {
    this._dac = dac;
    this._propertyName = propertyName;
  }

  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    string firstColumn = base.GetValue(data, ref position, context) as string;
    PXCache cach = context.Graph.Caches[this._dac];
    ISqlDialect sqlDialect = context.Graph.SqlDialect;
    string[] attributes;
    return !string.IsNullOrEmpty(firstColumn) && cach._KeyValueAttributeNames != null && sqlDialect.tryExtractAttributes(firstColumn, (IDictionary<string, int>) cach._KeyValueAttributeNames, out attributes) ? (object) attributes[cach._KeyValueAttributeNames[this._propertyName]] : (object) firstColumn;
  }
}
