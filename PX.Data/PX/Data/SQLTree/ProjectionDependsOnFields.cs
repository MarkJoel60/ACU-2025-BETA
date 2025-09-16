// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.ProjectionDependsOnFields
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data.SQLTree;

internal class ProjectionDependsOnFields : ProjectionReference
{
  private readonly System.Type _propertyType;
  private readonly string _propertyName;
  private readonly HashSet<string> _dependsOnProperties;

  public ProjectionDependsOnFields(
    System.Type dac,
    System.Type propertyType,
    string propertyName,
    HashSet<string> dependsOnProperties)
    : base(dac)
  {
    this._propertyType = propertyType;
    this._propertyName = propertyName;
    this._dependsOnProperties = dependsOnProperties;
  }

  public override System.Type GetResultType() => this._propertyType;

  protected override IEnumerable<PropertyInfo> GetDacProperties(System.Type dac)
  {
    return ((IEnumerable<PropertyInfo>) dac.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => this._dependsOnProperties.Contains(p.Name)));
  }

  /// <inheritdoc />
  internal override object GetValue(PXDataRecord data, ref int position, MergeCacheContext context)
  {
    return ProjectionDependsOnFields.GetPropertyValue(base.GetValue(data, ref position, (MergeCacheContext) null), this._propertyName);
  }

  private static object GetPropertyValue(object obj, string propertyName)
  {
    PropertyInfo runtimeProperty = obj != null ? obj.GetType().GetRuntimeProperty(propertyName) : (PropertyInfo) null;
    return runtimeProperty == (PropertyInfo) null ? (object) null : runtimeProperty.GetValue(obj);
  }
}
